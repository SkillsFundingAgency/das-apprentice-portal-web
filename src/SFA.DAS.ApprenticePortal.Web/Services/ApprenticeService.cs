﻿using RestEase;
using SFA.DAS.ApprenticePortal.SharedUi.Home;
using SFA.DAS.ApprenticePortal.Web.Models;
using SFA.DAS.ApprenticePortal.Web.Services.OuterApi;
using System;
using System.Threading.Tasks;
using SFA.DAS.HashingService;

namespace SFA.DAS.ApprenticePortal.Web.Services
{
    public class ApprenticeService
    {
        private readonly IOuterApiClient _client;
        private readonly NotificationAccessor _notifications;
        private readonly IHashingService _hashingService;

        public ApprenticeService(IOuterApiClient client, NotificationAccessor notifications, IHashingService hashingService)
        {
            _client = client;
            _notifications = notifications;
            _hashingService = hashingService;
        }

        public async Task<HomepageModel?> GetHomepageModel(Guid apprenticeId)
        {
            try
            {
                var apprenticeHomepage = await _client.GetApprenticeHomepage(apprenticeId);

                if (apprenticeHomepage.Apprentice is null)
                    return null;

                var apprenticeship = apprenticeHomepage.Apprenticeship;

                if (apprenticeship?.IsStopped == true &&
                    (apprenticeship.LastViewed == null ||
                     apprenticeship.LastViewed <= apprenticeship.StoppedReceivedOn))
                {
                    _notifications.Notify(HomeNotification.ApprenticeshipStopped);
                }

                var model = new HomepageModel
                {
                    CurrentHashedApprenticeshipId = apprenticeship == null ? null : _hashingService.HashValue(apprenticeship.Id),
                    CourseName = apprenticeship?.CourseName,
                    EmployerName = apprenticeship?.EmployerName,
                    Complete = apprenticeship?.ConfirmedOn.HasValue,
                    HasStopped = apprenticeship?.IsStopped,
                    Notification = _notifications.SignificantNotification,
                    ShowConfirmMyApprenticeshipCard = (apprenticeship is { ConfirmedOn: null }),
                    ShowMyApprenticeshipCard = apprenticeship is { HasBeenConfirmedAtLeastOnce: true }
                };

                return model;
            }
            catch (ApiException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}