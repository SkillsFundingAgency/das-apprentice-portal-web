using SFA.DAS.ApprenticePortal.OuterApi.Mock.Models;
using System;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using var mock = new PortalOuterApiMock(5121, ssl: true)
                .WithApprentice(An.Apprentice
                    .WithId(Guid.Parse("8c69f92d-952d-4d45-a1a2-b8cacfe0ced8"))
                    .WithApprenticeship(An.Apprenticeship));

            Console.WriteLine("Press any key to stop the servers");
            Console.ReadKey();
        }
    }
}