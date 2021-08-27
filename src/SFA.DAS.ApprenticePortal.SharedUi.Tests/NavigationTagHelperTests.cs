using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NUnit.Framework;
using SFA.DAS.ApprenticePortal.SharedUi.Home;

namespace SFA.DAS.ApprenticePortal.SharedUi.Tests
{
    public class NotificationTagHelperTests
    {
        private TagHelperContext TagHelperContext;
        private TagHelperOutput TagHelperOutput;
        private NotificationTagHelper Sut;
        private HttpContext HttpContext;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            fixture.Inject<HttpContext>(new DefaultHttpContext());
            fixture.Register<TagHelperContent>(() =>
            {
                var content = new DefaultTagHelperContent();
                content.SetContent("Hello from tag helper output");
                return content;
            });
            fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });

            Sut = fixture.Create<NotificationTagHelper>();
            HttpContext = fixture.Create<HttpContext>();
            TagHelperContext = fixture.Create<TagHelperContext>();
            TagHelperOutput = fixture.Create<TagHelperOutput>();
        }

        [TestCase("?notification=ApprenticeshipDidNotMatch", HomeNotification.ApprenticeshipDidNotMatch)]
        [TestCase("?notification=apprenticeshipmatched", HomeNotification.ApprenticeshipMatched)]
        public void Shows_notification_when_in_querystring(string queryString, HomeNotification notification)
        {
            HttpContext.Request.QueryString = new QueryString(queryString);
            Sut.AspNotification = notification;

            Sut.Process(TagHelperContext, TagHelperOutput);

            TagHelperOutput.Content.GetContent().Should().NotBeEmpty();
        }

        [TestCase("", HomeNotification.ApprenticeshipDidNotMatch)]
        [TestCase("?something-else=true", HomeNotification.ApprenticeshipDidNotMatch)]
        [TestCase("?notification=", HomeNotification.ApprenticeshipDidNotMatch)]
        [TestCase("?notification=NotValidValue", HomeNotification.ApprenticeshipDidNotMatch)]
        [TestCase("?notification=ApprenticeshipMatched", HomeNotification.ApprenticeshipDidNotMatch)]
        public void Hides_notification_when_not_in_querystring(string queryString, HomeNotification notification)
        {
            HttpContext.Request.QueryString = new QueryString(queryString);
            Sut.AspNotification = notification;

            Sut.Process(TagHelperContext, TagHelperOutput);

            TagHelperOutput.Content.GetContent().Should().BeEmpty();
        }
    }
}