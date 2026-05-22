using System.ComponentModel;

namespace SFA.DAS.ApprenticePortal.Web.Models
{
    public enum ApprenticeshipType
    {
        [Description("Apprenticeship")]
        Apprenticeship = 0,

        [Description("Foundation apprenticeship")]
        FoundationApprenticeship = 1
    }
}
