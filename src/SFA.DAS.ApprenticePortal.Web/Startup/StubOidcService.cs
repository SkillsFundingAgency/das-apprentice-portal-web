using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SFA.DAS.GovUK.Auth.Models;
using SFA.DAS.GovUK.Auth.Services;

namespace SFA.DAS.ApprenticePortal.Web.Startup;
//to be deleted afet gov login live
public class StubOidcService :IOidcService
{
    public Task<Token> GetToken(OpenIdConnectMessage openIdConnectMessage)
    {
        throw new System.NotImplementedException();
    }

    public Task PopulateAccountClaims(TokenValidatedContext tokenValidatedContext)
    {
        throw new System.NotImplementedException();
    }

    public Task<GovUkUser> GetAccountDetails(string accessToken)
    {
        throw new System.NotImplementedException();
    }
}