using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace SFA.DAS.ApprenticePortal.SharedUi.Home
{
    [HtmlTargetElement(Attributes = "asp-notification")]
    public class NotificationTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeNotification? AspNotification { get; set; }

        public NotificationTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!PageHasThisNotification())
                output.SuppressOutput();
        }

        public bool PageHasThisNotification()
        {
            if (!_contextAccessor.HttpContext.Request.Query.TryGetValue("notification", out var notificationQuery))
                return false;

            if (!Enum.TryParse<HomeNotification>(notificationQuery, ignoreCase: true, out var notification))
                return false;

            return notification == AspNotification;
        }
    }
}