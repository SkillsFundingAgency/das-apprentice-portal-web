using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SFA.DAS.ApprenticePortal.SharedUi.Home
{
    [HtmlTargetElement(Attributes = "asp-notification")]
    public class NotificationTagHelper : TagHelper
    {
        private readonly NotificationAccessor _notifications;

        public HomeNotification? AspNotification { get; set; }

        public NotificationTagHelper(NotificationAccessor accessor)
            => _notifications = accessor;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!PageHasThisNotification())
                output.SuppressOutput();
        }

        public bool PageHasThisNotification()
            => AspNotification != null
            && _notifications.SignificantNotification == AspNotification.Value;
    }
}