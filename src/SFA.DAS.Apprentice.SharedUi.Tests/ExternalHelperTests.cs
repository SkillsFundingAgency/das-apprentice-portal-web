using AutoFixture.NUnit3;
using NUnit.Framework;
using System;

namespace SFA.DAS.Apprentice.SharedUi.Tests
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
        //[TestCase("https://test.com/", "abcd", "subsite", "https://subsite.test.com/abcd")]
        //[TestCase("https://test.com/path", "abcd", "subsite", "https://subsite.test.com/path/abcd")]
        //[TestCase("https://test.com/path/", "abcd", "subsite", "https://subsite.test.com/path/abcd")]
        public void Build_from_base_and_controller_with_subdomain(string uri, string controller, string subdomain, string expected)
        {
            var sut = new ExternalUrlHelper(new Uri(uri));
            var result = sut.Generate(controller: controller, subdomain: subdomain);
            Assert.That(result, Is.EqualTo(expected));
        }


    }
}