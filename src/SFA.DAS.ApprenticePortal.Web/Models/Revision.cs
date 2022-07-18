﻿using System;

namespace SFA.DAS.ApprenticePortal.Web.Models
{
    public class Revision
    {
        public Revision() { }

        public Revision(string? heading, string? description, DateTime? revisionDate) =>
            (Heading, Description, RevisionDate) = (heading, description, revisionDate);

        public string? Heading { get; set; } = "";
        public string? Description { get; set; } = "";
        public DateTime? RevisionDate { get; set; }
    }
}
