using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders.Testing;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.SharedUi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Tests
{
    public class NavigationAnchorTagHelperTests
    {
        private TagHelperContext tagHelperContext;
        private TagHelperOutput tagHelperOutput;
        private NavigationAnchorTagHelper sut;
        private NavigationSectionUrls urls;
        private Mock<IMenuVisibility> something;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            fixture.Register<TagHelperContent>(() =>
            {
                var content = new DefaultTagHelperContent();
                content.SetHtmlContent("hello text");
                return content;
            });
            fixture.Customize<NavigationAnchorTagHelper>(c => c.OmitAutoProperties());

            something = new Mock<IMenuVisibility>();
            fixture.Inject(something.Object);
            urls = fixture.Freeze<NavigationSectionUrls>();
            sut = fixture.Create<NavigationAnchorTagHelper>();
            tagHelperContext = fixture.Create<TagHelperContext>();
            tagHelperOutput = fixture.Create<TagHelperOutput>();

            something.Setup(x => x.ShowConfirmMyApprenticeship()).ReturnsAsync(true);
        }

        [TestCase(NavigationSection.ConfirmMyApprenticeship)]
        [TestCase(NavigationSection.Home)]
        public async Task Href_is_set_to_external_section(NavigationSection externalSection)
        {
            sut.ExternalSection = externalSection;

            await sut.ProcessAsync(tagHelperContext, tagHelperOutput);

            tagHelperOutput.Attributes.Should().Contain(new TagHelperAttribute("href", urls.ToUri(externalSection).ToString()));
        }

        [TestCase(NavigationSection.ConfirmMyApprenticeship, "banana")]
        [TestCase(NavigationSection.Home, "apple")]
        public async Task Href_is_set_to_external_section_with_page(NavigationSection externalSection, string page)
        {
            sut.ExternalSection = externalSection;
            sut.ExternalPage = page;

            await sut.ProcessAsync(tagHelperContext, tagHelperOutput);

            var expectedUrl = new Uri(urls.ToUri(externalSection), page).ToString();
            tagHelperOutput.Attributes.Should().Contain(new TagHelperAttribute("href", expectedUrl));
        }

        [Test]
        public async Task Output_is_suppressed_when_appropriate()
        {
            sut.ExternalSection = NavigationSection.ConfirmMyApprenticeship;
            something.Setup(x => x.ShowConfirmMyApprenticeship()).ReturnsAsync(false);

            await sut.ProcessAsync(tagHelperContext, tagHelperOutput);

            tagHelperOutput.Content.GetContent().Should().BeEmpty();
        }

        [Test]
        public async Task Output_is_not_suppressed_when_inappropriate()
        {
            sut.ExternalSection = NavigationSection.ConfirmMyApprenticeship;
            something.Setup(x => x.ShowConfirmMyApprenticeship()).ReturnsAsync(true);

            await sut.ProcessAsync(tagHelperContext, tagHelperOutput);

            tagHelperOutput.Content.GetContent().Should().NotBeEmpty();
        }
    }

    public class TestableHtmlGenerator : DefaultHtmlGenerator
    {
        private IDictionary<string, object> _validationAttributes;

        public TestableHtmlGenerator(IModelMetadataProvider metadataProvider)
            : this(metadataProvider, Mock.Of<IUrlHelper>())
        {
        }

        public TestableHtmlGenerator(IModelMetadataProvider metadataProvider, IUrlHelper urlHelper)
            : this(
                  metadataProvider,
                  GetOptions(),
                  urlHelper,
                  validationAttributes: new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase))
        {
        }

        public TestableHtmlGenerator(
            IModelMetadataProvider metadataProvider,
            IOptions<MvcViewOptions> options,
            IUrlHelper urlHelper,
            IDictionary<string, object> validationAttributes)
            : base(
                  Mock.Of<IAntiforgery>(),
                  options,
                  metadataProvider,
                  CreateUrlHelperFactory(urlHelper),
                  new HtmlTestEncoder(),
                  new DefaultValidationHtmlAttributeProvider(options, metadataProvider, new ClientValidatorCache()))
        {
            _validationAttributes = validationAttributes;
        }

        public IDictionary<string, object> ValidationAttributes
        {
            get { return _validationAttributes; }
        }

        public static ViewContext GetViewContext(
            object model,
            IHtmlGenerator htmlGenerator,
            IModelMetadataProvider metadataProvider)
        {
            return GetViewContext(model, htmlGenerator, metadataProvider, modelState: new ModelStateDictionary());
        }

        public static ViewContext GetViewContext(
            object model,
            IHtmlGenerator htmlGenerator,
            IModelMetadataProvider metadataProvider,
            ModelStateDictionary modelState)
        {
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor(),
                modelState);
            var viewData = new ViewDataDictionary(metadataProvider, modelState)
            {
                Model = model,
            };
            var viewContext = new ViewContext(
                actionContext,
                Mock.Of<IView>(),
                viewData,
                Mock.Of<ITempDataDictionary>(),
                TextWriter.Null,
                new HtmlHelperOptions());

            return viewContext;
        }

        public override IHtmlContent GenerateAntiforgery(ViewContext viewContext)
        {
            var tagBuilder = new TagBuilder("input")
            {
                Attributes =
                {
                    { "name", "__RequestVerificationToken" },
                    { "type", "hidden" },
                    { "value", "olJlUDjrouRNWLen4tQJhauj1Z1rrvnb3QD65cmQU1Ykqi6S4" }, // 50 chars of a token.
                },
            };

            tagBuilder.TagRenderMode = TagRenderMode.SelfClosing;
            return tagBuilder;
        }

        protected override void AddValidationAttributes(
            ViewContext viewContext,
            TagBuilder tagBuilder,
            ModelExplorer modelExplorer,
            string expression)
        {
            tagBuilder.MergeAttributes(ValidationAttributes);
        }

        private static IOptions<MvcViewOptions> GetOptions()
        {
            var mockOptions = new Mock<IOptions<MvcViewOptions>>();
            mockOptions
                .SetupGet(options => options.Value)
                .Returns(new MvcViewOptions());

            return mockOptions.Object;
        }

        private static IUrlHelperFactory CreateUrlHelperFactory(IUrlHelper urlHelper)
        {
            var factory = new Mock<IUrlHelperFactory>();
            factory
                .Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper);

            return factory.Object;
        }
    }
}