using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using NUnit.Framework;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.SharedUi.Services;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Tests
{
    public class ConfirmMyApprenticeshipMenuTitleTagHelperTests
    {
        private TagHelperContext _tagHelperContext;
        private TagHelperOutput _tagHelperOutput;
        private ConfirmMyApprenticeshipMenuTitleTagHelper _sut;
        private Mock<IMenuVisibility> menuVisibility;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Register<TagHelperContent>(() =>
            {
                var content = new DefaultTagHelperContent();
                return content;
            });

            menuVisibility = new Mock<IMenuVisibility>();
            _fixture.Inject(menuVisibility.Object);
            _sut = _fixture.Create<ConfirmMyApprenticeshipMenuTitleTagHelper>();

            _tagHelperContext = _fixture.Create<TagHelperContext>();
            _tagHelperOutput = _fixture.Create<TagHelperOutput>();

        }

        [TestCase(true, "Confirm my apprenticeship details")]
        [TestCase(false, "My apprenticeship details")]
        public async Task MenuHtml(bool showConfirmationMessage, string title)
        {
            menuVisibility.Setup(x => x.ShowConfirmOnMyApprenticeshipTitle()).ReturnsAsync(showConfirmationMessage);

            await _sut.ProcessAsync(_tagHelperContext, _tagHelperOutput);

            _tagHelperOutput.Content.GetContent().Should().Be(title);
        }
    }
}