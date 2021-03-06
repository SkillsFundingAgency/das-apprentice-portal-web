﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace SFA.DAS.ApprenticePortal.SharedUi.Zendesk
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SetZenDeskValuesAttribute : ResultFilterAttribute
    {
        public ZenDeskConfiguration ZenDeskConfiguration { get; }

        public SetZenDeskValuesAttribute(ZenDeskConfiguration zenDeskConfiguration)
        {
            ZenDeskConfiguration = zenDeskConfiguration;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Controller is PageModel page)
                page.ViewData[ViewDataKeys.ZenDeskConfiguration] = ZenDeskConfiguration;

            if (context.Controller is Controller controller)
                controller.ViewData[ViewDataKeys.ZenDeskConfiguration] = ZenDeskConfiguration;
        }
    }
}