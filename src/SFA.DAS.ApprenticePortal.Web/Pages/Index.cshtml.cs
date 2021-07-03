﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.ApprenticePortal.Web.Startup;

namespace SFA.DAS.ApprenticeCommitments.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string ApprenticeCommitmentsBaseUrl { get; }

        public IndexModel(ApplicationConfiguration configuration)
        {
            ApprenticeCommitmentsBaseUrl = configuration.ApprenticeCommitmentsBaseUrl;
        }

        public void OnGet()
        {
        }
    }
}