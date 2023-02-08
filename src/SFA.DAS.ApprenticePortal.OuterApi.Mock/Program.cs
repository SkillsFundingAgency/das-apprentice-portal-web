using SFA.DAS.ApprenticePortal.OuterApi.Mock.Models;
using System;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using var mock = new PortalOuterApiMock(5123, ssl: true)
                .WithApprentice(An.Apprentice
                    .WithAnyId()
                    .WithApprenticeship(An.Apprenticeship));

            Console.WriteLine("Press any key to stop the servers");
            Console.ReadKey();
        }
    }
}