using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using SFA.DAS.GovUK.Auth.Services;

namespace SFA.DAS.ApprenticePortal.Authentication;

public class ApprenticeAccountPostAuthenticationClaimsHandler : ICustomClaims
{
    private readonly IApprenticeAccountProvider _provider;

    public ApprenticeAccountPostAuthenticationClaimsHandler(IApprenticeAccountProvider provider)
    {
        _provider = provider;
    }
    public async Task<IEnumerable<Claim>> GetClaims(TokenValidatedContext tokenValidatedContext)
    {
        var userId = tokenValidatedContext.Principal.Claims
            .First(c => c.Type.Equals(ClaimTypes.NameIdentifier))
            .Value;
        
        var email = tokenValidatedContext.Principal.Claims
            .First(c => c.Type.Equals(ClaimTypes.Email))
            .Value;

        var apprentice = await _provider.PutApprenticeAccount(email,userId);
        
        // add claims

        var claims = new List<Claim>
        {
            new(IdentityClaims.ApprenticeId, apprentice!.Id.ToString()),
            new(IdentityClaims.Name, email)
        };

        if (!string.IsNullOrEmpty(apprentice.FirstName))
        {
            claims.Add(new Claim(IdentityClaims.GivenName, apprentice.FirstName));
        }
        if (!string.IsNullOrEmpty(apprentice.LastName))
        {
            claims.Add(new Claim(IdentityClaims.FamilyName, apprentice.LastName));
        }
        if (apprentice.TermsOfUseAccepted)
        {
            claims.Add(new Claim(IdentityClaims.TermsOfUseAccepted, "True"));
        }
           
        return claims;
    }
}