using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.ApprenticePortal.SharedUi.Home
{
    public class NotificationAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Lazy<List<HomeNotification>> _notifications;

        public NotificationAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _notifications = new Lazy<List<HomeNotification>>(ReadNotificationsFromQuery);
        }

        private List<HomeNotification> ReadNotificationsFromQuery()
        {
            _contextAccessor.HttpContext.Request.Query.TryGetValue("notification", out var notification);
            return ParseQueryString(notification).ToList();
        }

        private static IEnumerable<HomeNotification> ParseQueryString(StringValues notifications)
        {
            foreach (var value in notifications)
                if (Enum.TryParse<HomeNotification>(value, ignoreCase: true, out var notification))
                    yield return notification;
        }

        public HomeNotification? SignificantNotification
            => _notifications.Value
                .OrderBy(x => x)
                .Cast<HomeNotification?>()
                .FirstOrDefault();

        public void Notify(HomeNotification apprenticeshipStopped)
        {
            _notifications.Value.Add(apprenticeshipStopped);
        }
    }
}