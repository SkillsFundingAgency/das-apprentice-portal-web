using System;

namespace SFA.DAS.Apprentice.SharedUi
{
    public class ExternalUrlHelper
    {
        public ExternalUrlHelper(Uri baseUrl) => BaseUrl = baseUrl;

        public Uri BaseUrl { get; }

        public string Generate(string? controller)
        {
            var a = BaseUrl.ToString();
            if (!string.IsNullOrWhiteSpace(controller))
            {
                if (a[^1] != '/') a += '/';
                a += controller;
            }

            return a;
        }
    }
}