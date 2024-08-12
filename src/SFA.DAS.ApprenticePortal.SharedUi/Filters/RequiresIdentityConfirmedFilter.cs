using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;

namespace SFA.DAS.ApprenticePortal.Authentication.Filters
{
    public class RequiresIdentityConfirmedFilter : IAsyncAuthorizationFilter
    {
        private readonly NavigationUrlHelper _urlHelper;
        private readonly AuthenticatedUser _user;
        private readonly IApprenticeAccountProvider _provider;

        public RequiresIdentityConfirmedFilter(NavigationUrlHelper urlHelper, AuthenticatedUser user, IApprenticeAccountProvider provider)
        {
            _urlHelper = urlHelper;
            _user = user;
            _provider = provider;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!_user.HasCreatedAccount)
                context.Result = new RedirectResult(_urlHelper.Generate(NavigationSection.ConfirmMyApprenticeship, $"register{context.HttpContext.Request.QueryString}"));
            else if (!_user.HasAcceptedTermsOfUse)
            {
                var apprentice = await _provider.GetApprenticeAccount(_user.ApprenticeId);
                if (!apprentice!.TermsOfUseAccepted)
                {
                    context.Result = new RedirectResult(_urlHelper.Generate(NavigationSection.ApprenticeAccounts, "AcceptTermsOfUse"));    
                }
            }
                
        }
    }
}