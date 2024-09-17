using SFA.DAS.GovUK.Auth.Models;

namespace SFA.DAS.ApprenticePortal.Web.Models;

public class StubAuthenticationViewModel : StubAuthUserDetails
{
    public string ReturnUrl { get; set; }
}