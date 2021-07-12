using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



namespace SFA.DAS.ApprenticePortal.SharedUi.Identity
{
    public class RequiresIdentityConfirmedAttribute : TypeFilterAttribute
    {
        public RequiresIdentityConfirmedAttribute() : base(typeof(ClaimRequirementFilter))
        {
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (VerifiedUser.UserHasUnconfirmedIdentity(context.HttpContext))
                context.Result = new RedirectResult("/ConfirmYourIdentity");
        }
    }
}
