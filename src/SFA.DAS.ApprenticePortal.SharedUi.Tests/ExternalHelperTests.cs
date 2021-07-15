using NUnit.Framework;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using System;

namespace SFA.DAS.ApprenticePortal.SharedUi.Tests
{
    [Parallelizable]
    public class ExternalHelperTests
    {
        [TestCase("https://test.com", "abcd", "https://test.com/abcd")]
        [TestCase("https://test.com/", "abcd", "https://test.com/abcd")]
        [TestCase("https://test.com/path", "abcd", "https://test.com/path/abcd")]
        [TestCase("https://test.com/path/", "abcd", "https://test.com/path/abcd")]
        public void Build_from_base_and_controller(string uri, string controller, string expected)
        {
            var sut = new ExternalUrlHelper(new Uri(uri));
            var result = sut.Generate(controller: controller);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("https://test.com", "abcd", "subsite", "https://subsite.test.com/abcd")]
        public void Build_from_base_and_controller_with_subdomain(string uri, string controller, string subdomain, string expected)
        {
            var sut = new ExternalUrlHelper(new Uri(uri));
            var result = sut.Generate(controller: controller, subdomain: subdomain);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Constructs_url_for_only_section()
        {
            var sections = new NavigationSectionUrls
            {
                ApprenticeHomeUrl = "https://testhome.com",
            };

            var sut = new NavigationUrlHelper(sections);

            var result = sut.Generate(NavigationSection.Home, "xyz");

            Assert.That(result, Is.EqualTo("https://testhome.com/xyz"));
        }

        [TestCase(NavigationSection.Home, "https://home.com/")]
        [TestCase(NavigationSection.ConfirmMyApprenticeship, "https://confirm.com/")]
        [TestCase(NavigationSection.Login, "https://login.com/")]
        public void Constructs_url_for_correct_section(NavigationSection section, string url)
        {
            var sections = new NavigationSectionUrls
            {
                ApprenticeHomeUrl = "https://home.com",
                ApprenticeCommitmentsUrl = "https://confirm.com",
                ApprenticeLoginUrl = "https://login.com",
            };

            var sut = new NavigationUrlHelper(sections);

            var result = sut.Generate(section);

            Assert.That(result, Is.EqualTo(url));
        }

        [Test]
        public void Cannot_construct_url_for_missing_section()
        {
            var sections = new NavigationSectionUrls
            {
                ApprenticeCommitmentsUrl = "https://confirm.com",
            };

            var sut = new NavigationUrlHelper(sections);

            var ex = Assert.Throws<Exception>(() => sut.Generate(NavigationSection.HelpAndSupport));
            Assert.That(ex.Message, Is.EqualTo("URL for navigation section `HelpAndSupport` is not configured"));
        }

        [Test]
        public void Cannot_construct_url_for_invalid_section()
        {
            var sections = new NavigationSectionUrls
            {
                ApprenticeCommitmentsUrl = "https://confirm.com",
            };

            var sut = new NavigationUrlHelper(sections);

            var ex = Assert.Throws<Exception>(() => sut.Generate((NavigationSection)100));
            Assert.That(ex.Message, Is.EqualTo("Unknown nagivation section 100"));
        }
    }
}