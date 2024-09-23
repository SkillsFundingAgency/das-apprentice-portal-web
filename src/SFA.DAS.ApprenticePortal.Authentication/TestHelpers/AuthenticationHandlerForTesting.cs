using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SFA.DAS.ApprenticePortal.Authentication.TestHelpers
{
    public class AuthenticationHandlerForTesting : SignInAuthenticationHandler<AuthenticationSchemeOptions>
    {
        private static readonly ConcurrentDictionary<Guid, ClaimsPrincipal> _users
            = new ConcurrentDictionary<Guid, ClaimsPrincipal>();

        public static List<ClaimsPrincipal> Authentications { get; } = new List<ClaimsPrincipal>();

        public AuthenticationHandlerForTesting(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        public static void AddUserWithFullAccount(Guid apprenticeId)
        {
            Console.WriteLine($"Adding logged in user {apprenticeId}");
            _users.TryAdd(apprenticeId, AuthenticatedUsersForTesting.FakeLocalUserFullyVerifiedClaim(apprenticeId).HttpContext.User);
        }

        public static void AddUserWithoutTerms(Guid apprenticeId)
        {
            Console.WriteLine($"Adding logged in user {apprenticeId} who hasn't accepts ToC");
            _users.TryAdd(apprenticeId, AuthenticatedUsersForTesting.FakeLocalUserWithAccountButTermsOfUseNotAcceptedClaim(apprenticeId).HttpContext.User);
        }

        public static void AddUserWithoutAccount(Guid apprenticeId)
        {
            Console.WriteLine($"Adding unverified logged in user {apprenticeId}");
            _users.TryAdd(apprenticeId, AuthenticatedUsersForTesting.FakeLocalUserWithNoAccountClaim(apprenticeId).HttpContext.User);
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(HandleAuthenticate());
        }

        protected AuthenticateResult HandleAuthenticate()
        {
            var guid = FindUserFromHeader();
            if (guid == null) return AuthenticateResult.Fail("No user header found");

            var exists = _users.TryGetValue(guid.Value, out var principal);
            if (!exists) return AuthenticateResult.Fail($"User `{guid}` is not logged in");

            return AuthenticateResult.Success(new AuthenticationTicket(principal, "Test"));
        }

        protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            Authentications.Add(user);
            return Task.CompletedTask;
        }

        protected override Task HandleSignOutAsync(AuthenticationProperties properties) => Task.CompletedTask;

        private Guid? FindUserFromHeader()
        {
            if (Request.Headers.TryGetValue("Authorization", out var value) && Guid.TryParse(value, out var guid))
                return guid;
            return default;
        }
    }
}