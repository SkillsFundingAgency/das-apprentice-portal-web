using FluentAssertions;
using NUnit.Framework;

namespace SFA.DAS.ApprenticePortal.Authentication.UnitTests;

public class DomainExtensionsTests
{
    [TestCase("LocAL","")]
    [TestCase("TEST","test-aas.apprenticeships.education.gov.uk")]
    [TestCase("PRD","my.apprenticeships.education.gov.uk")]
    [TestCase("prePROD","preprod-aas.apprenticeships.education.gov.uk")]
    public void Then_The_Domain_Is_Correct_For_Each_Environment(string environment, string expectedDomain)
    {
        var actual = DomainExtensions.GetDomain(environment);

        actual.Should().Be(expectedDomain);
    }
}