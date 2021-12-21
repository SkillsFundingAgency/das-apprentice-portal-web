namespace SFA.DAS.ApprenticePortal.Web.Models
{
    public class HomepageModel
    {
        public string CourseName { get; set; } = null!;
        public string EmployerName { get; set; } = null!;
        public bool Complete { get; set; } = false;
        public bool HasStopped { get; set; } = false;
        public bool DisplayJustStoppedInfoMessage { get; set; } = false;
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

            if (model.HasStopped)
            {
                return ApprenticeshipStatus.Stopped;
            }

            if (model.Complete)
            {
                return ApprenticeshipStatus.Complete;
            }

            return ApprenticeshipStatus.Incomplete;
        }
    }
}