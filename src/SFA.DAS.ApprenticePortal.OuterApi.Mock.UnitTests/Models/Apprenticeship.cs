using AutoFixture;
using Newtonsoft.Json;
using System;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock.Models
{
    public class Apprenticeship
    {
        private readonly DateTime previousActionOn = DateTime.MinValue;

        public long Id { get; }
        public Guid ApprenticeId { get; }
        public string EmployerName { get; }
        public string CourseName { get; }
        public DateTime ApprovedOn { get; }
        public DateTime? ConfirmedOn { get; }
        public DateTime? LastViewed { get; }
        public DateTime? StoppedReceivedOn { get; }

        public Apprenticeship(
            Guid apprenticeId,
            long id,
            string employerName,
            string courseName,
            DateTime approvedOn,
            DateTime? confirmedOn,
            DateTime? lastViewed,
            DateTime? stoppedReceivedOn)
        {
            ApprenticeId = apprenticeId;
            Id = id;
            EmployerName = employerName;
            CourseName = courseName;
            ConfirmedOn = confirmedOn;
            ApprovedOn = approvedOn;
            LastViewed = lastViewed;
            StoppedReceivedOn = stoppedReceivedOn;
        }

        private Apprenticeship(
            Guid apprenticeId,
            long id,
            string employerName,
            string courseName,
            DateTime approvedOn,
            DateTime? confirmedOn,
            DateTime? lastViewed,
            DateTime? stoppedReceivedOn,
            DateTime previousActionOn)
            : this(
                  apprenticeId,
                  id,
                  employerName,
                  courseName,
                  approvedOn,
                  confirmedOn,
                  lastViewed,
                  stoppedReceivedOn)
        {
            this.previousActionOn = previousActionOn;
        }

        public Apprenticeship ForApprentice(Apprentice apprentice)
            => With(apprenticeId: apprentice.ApprenticeId);

        public Apprenticeship WithConfirmed()
            => With(confirmedOn: DateTime.UtcNow);

        public Apprenticeship WithConfirmed(DateTime confirmedOn)
            => With(confirmedOn: confirmedOn, previousActionOn: confirmedOn);

        private Apprenticeship With(
            Guid apprenticeId = default,
            DateTime? confirmedOn = null,
            DateTime? stoppedReceivedOn = null,
            DateTime? lastViewedOn = null,
            DateTime? previousActionOn = null)
            => new Apprenticeship(
                apprenticeId == default ? ApprenticeId : apprenticeId,
                Id,
                EmployerName,
                CourseName,
                ApprovedOn,
                confirmedOn ?? ConfirmedOn,
                lastViewedOn ?? LastViewed,
                stoppedReceivedOn ?? StoppedReceivedOn,
                previousActionOn ?? this.previousActionOn);
        
        public Apprenticeship FollowedByStopped()
        {
            var stoppedReceivedOn = previousActionOn.AddDays(1);
            return With(stoppedReceivedOn: stoppedReceivedOn, previousActionOn: stoppedReceivedOn);
        }

        public Apprenticeship FollowedByViewed()
        {
            var lastViewedOn = previousActionOn.AddDays(1);
            return With(lastViewedOn: lastViewedOn, previousActionOn: lastViewedOn);
        }

        //public bool IsStopped { get; }
    }
}