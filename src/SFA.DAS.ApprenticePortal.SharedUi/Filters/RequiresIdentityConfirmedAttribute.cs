using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticePortal.Authentication.Filters;

namespace SFA.DAS.ApprenticePortal.SharedUi.Filters
{
    public class RequiresIdentityConfirmedAttribute : TypeFilterAttribute
    {
        public RequiresIdentityConfirmedAttribute() : base(typeof(RequiresIdentityConfirmedFilter))
        {
        }
    }
}
