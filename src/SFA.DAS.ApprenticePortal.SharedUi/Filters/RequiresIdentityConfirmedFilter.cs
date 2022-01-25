using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticePortal.Authentication.Filters
{
    public class RequiresIdentityConfirmedFilter : IAuthorizationFilter
    {
        private readonly NavigationUrlHelper _urlHelper;
        private readonly AuthenticatedUser _user;

        public RequiresIdentityConfirmedFilter(NavigationUrlHelper urlHelper, AuthenticatedUser user)
        {
            _urlHelper = urlHelper;
            _user = user;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_user.HasCreatedAccount)
                context.Result = new RedirectResult(_urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship, $"register{context.HttpContext.Request.QueryString}"));
            else if (!_user.HasAcceptedTermsOfUse)
                context.Result = new RedirectResult(_urlHelper.Generate(NavigationSection.ApprenticeAccounts, "AcceptTermsOfUse"));
        }
    }
}