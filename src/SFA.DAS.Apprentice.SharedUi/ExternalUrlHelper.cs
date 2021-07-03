using System;

namespace SFA.DAS.Apprentice.SharedUi
{
    public class ExternalUrlHelper
    {
        public ExternalUrlHelper(Uri baseUrl) => BaseUrl = baseUrl;

        public Uri BaseUrl { get; }

        public string Generate(string? controller, string? subdomain = null)
        {
            var a = BaseUrl.ToString();

            if (!string.IsNullOrWhiteSpace(subdomain))
            {
                var h = $"{subdomain}.{BaseUrl.Host}";
                a = a.Replace(BaseUrl.Host, h);
            }

            if (!string.IsNullOrWhiteSpace(controller))
            {
                if (a[^1] != '/') a += '/';
                a += controller;
            }

            return a;
        }
    }
}