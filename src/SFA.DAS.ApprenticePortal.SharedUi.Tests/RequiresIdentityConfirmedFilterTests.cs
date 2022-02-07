using FluentAssertions;
using NUnit.Framework;
using System;
using SFA.DAS.ApprenticePortal.SharedUi.Menu;
using SFA.DAS.ApprenticePortal.Authentication.TestHelpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using SFA.DAS.ApprenticePortal.Authentication.Filters;

namespace SFA.DAS.ApprenticePortal.SharedUi.Tests
{
    public class RequiresIdentityConfirmedFilterTests
    {

        private NavigationSectionUrls _navigationSectionUrls;
        private NavigationUrlHelper _navigationUrlHelper;
        private Guid _apprenticeId;
        private AuthorizationFilterContext _authorizationFilterContext;

        [SetUp]
        public void Initiaise()
        {
            _apprenticeId = Guid.NewGuid();

            _navigationSectionUrls = new NavigationSectionUrls
            {
                ApprenticeHomeUrl = new Uri("http://home"),
                ApprenticeAccountsUrl = new Uri("http://accounts"),
                ApprenticeCommitmentsUrl = new Uri("http://cmad")
            };

            _navigationUrlHelper = new NavigationUrlHelper(_navigationSectionUrls);
            var actionResult = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            _authorizationFilterContext = new AuthorizationFilterContext(actionResult, new IFilterMetadata[] { }); 
        }

        [Test]
        public void When_user_has_created_account_and_agreed_to_terms_of_use_Then_no_redirect_action_set()
        {
            var sut = new RequiresIdentityConfirmedFilter(_navigationUrlHelper, AuthenticatedUsersForTesting.FakeLocalUserFullyVerified);
            sut.OnAuthorization(_authorizationFilterContext);

            _authorizationFilterContext.Result.Should().BeNull();
        }

        [Test]
        public void When_user_has_not_created_account_should_redirect_to_register()
        {
            var sut = new RequiresIdentityConfirmedFilter(_navigationUrlHelper, AuthenticatedUsersForTesting.FakeLocalUserWithNoAccount);
            sut.OnAuthorization(_authorizationFilterContext);

            _authorizationFilterContext.Result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be("http://cmad/register");
        }

        [Test]
        public void When_user_has_created_account_but_not_accepted_terms_of_use_Then_should_redirect_to_terms_of_use()
        {
            var sut = new RequiresIdentityConfirmedFilter(_navigationUrlHelper, AuthenticatedUsersForTesting.FakeLocalUserWithAccountButTermsOfUseNotAccepted);
            sut.OnAuthorization(_authorizationFilterContext);

            _authorizationFilterContext.Result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be("http://accounts/AcceptTermsOfUse");
        }
    }
}