﻿using SFA.DAS.ApprenticePortal.SharedUi.Home;

namespace SFA.DAS.ApprenticePortal.Web.Models
{
    public class HomepageModel
    {
        public string? CurrentHashedApprenticeshipId{ get; set; } = null!;
        public string? CourseName { get; set; } = null!;
        public string? EmployerName { get; set; } = null!;
        public bool? Complete { get; set; } = false;
        public bool? HasStopped { get; set; } = false;
        public HomeNotification? Notification { get; set; }
        public bool ShowConfirmMyApprenticeshipCard { get; set; }
        public bool ShowMyApprenticeshipCard { get; set; }
    }

    public enum ApprenticeshipStatus
    {
        Incomplete,
        Complete,
        Stopped
    }

    public static class ApprenticeModelExtensions
    {
        public static ApprenticeshipStatus Status(this HomepageModel? model)
        {
            if (model == null)
            {
                return ApprenticeshipStatus.Incomplete;
            }

            if (model.HasStopped == true)
            {
                return ApprenticeshipStatus.Stopped;
            }

            if (model.Complete == true)
            {
                return ApprenticeshipStatus.Complete;
            }

            return ApprenticeshipStatus.Incomplete;
        }
    }
}