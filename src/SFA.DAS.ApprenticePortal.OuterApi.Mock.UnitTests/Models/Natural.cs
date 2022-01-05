using System;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock.Models
{
    public static class Natural
    {
        public static Apprentice Apprentice => new Apprentice(
            Guid.NewGuid(),
            Faker.Name.First(),
            Faker.Name.Last(),
            true)
            .WithApprenticeship(Apprenticeship);
        
        public static Apprenticeship Apprenticeship => new Apprenticeship(
            apprenticeId: Guid.NewGuid(),
            id: Faker.RandomNumber.Next(),
            employerName: $"Employer {Faker.Company.Name()}",
            courseName: $"Provider {Faker.Lorem.Sentence()}",
            approvedOn: new DateTime(2020, 08, 20),
            confirmedOn: null,
            stoppedReceivedOn: null,
            lastViewed: null);
    }
}