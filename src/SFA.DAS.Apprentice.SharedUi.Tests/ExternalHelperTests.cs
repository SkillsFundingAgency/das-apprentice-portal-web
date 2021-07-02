using AutoFixture.NUnit3;
using NUnit.Framework;
using System;

namespace SFA.DAS.Apprentice.SharedUi.Tests
{
    [Parallelizable]
    public class ExternalHelperTests
    {
        [Test, AutoData]
        public void Build_from_base_with_trailing_slash_and_controller(string controller)
        {
            var sut = new ExternalUrlHelper(new Uri("https://test.com/"));
            var result = sut.Generate(controller: controller);
            Assert.That(result, Is.EqualTo($"https://test.com/{controller}"));
        }

        [Test, AutoData]
        public void Build_from_base_without_trailing_slash_and_controller(string controller)
        {
            var sut = new ExternalUrlHelper(new Uri("https://test.com"));
            var result = sut.Generate(controller: controller);
            Assert.That(result, Is.EqualTo($"https://test.com/{controller}"));
        }

        [Test, AutoData]
        public void Build_from_base_path_without_trailing_slash_and_controller(string controller)
        {
            var sut = new ExternalUrlHelper(new Uri("https://test.com/page"));
            var result = sut.Generate(controller: controller);
            Assert.That(result, Is.EqualTo($"https://test.com/page/{controller}"));
        }

        [Test, AutoData]
        public void Build_from_base_path_with_trailing_slash_and_controller(string controller)
        {
            var sut = new ExternalUrlHelper(new Uri("https://test.com/page/"));
            var result = sut.Generate(controller: controller);
            Assert.That(result, Is.EqualTo($"https://test.com/page/{controller}"));
        }
    }
}