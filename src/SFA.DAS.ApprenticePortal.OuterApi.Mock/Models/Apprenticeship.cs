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

        public Apprenticeship WithEmployerName(string employerName)
            => With(employerName: employerName);

        public Apprenticeship WithCourseName(string courseName)
            => With(courseName: courseName);

        public Apprenticeship WithApprovedOn()
            => With(approvedOn: DateTime.UtcNow);

        public Apprenticeship WithApprovedOn(DateTime? approvedOn)
            => With(approvedOn: approvedOn, previousActionOn: approvedOn);

        public Apprenticeship WithConfirmedOn()
            => With(confirmedOn: DateTime.UtcNow, previousActionOn: DateTime.UtcNow);

        public Apprenticeship WithConfirmedOn(DateTime? confirmedOn)
            => With(confirmedOn: confirmedOn, previousActionOn: confirmedOn);

        public Apprenticeship WithStoppedReceivedOn()
            => With(stoppedReceivedOn: DateTime.UtcNow, previousActionOn: DateTime.UtcNow);

        public Apprenticeship WithStoppedReceivedOn(DateTime? stoppedOn)
            => With(stoppedReceivedOn: stoppedOn, previousActionOn: stoppedOn);

        public Apprenticeship WithLastViewedOn()
            => With(lastViewedOn: DateTime.UtcNow, previousActionOn: DateTime.UtcNow);

        public Apprenticeship WithLastViewedOn(DateTime? lastViewedOn)
            => With(lastViewedOn: lastViewedOn, previousActionOn: lastViewedOn);

        private Apprenticeship With(
            Guid apprenticeId = default,
            string? employerName = null,
            string? courseName = null,
            DateTime? approvedOn = null,
            DateTime? confirmedOn = null,
            DateTime? stoppedReceivedOn = null,
            DateTime? lastViewedOn = null,
            DateTime? previousActionOn = null)
            => new Apprenticeship(
                apprenticeId == default ? ApprenticeId : apprenticeId,
                Id,
                employerName ?? EmployerName,
                courseName ?? CourseName,
                approvedOn ?? ApprovedOn,
                confirmedOn ?? ConfirmedOn,
                lastViewedOn ?? LastViewed,
                stoppedReceivedOn ?? StoppedReceivedOn,
                previousActionOn ?? this.previousActionOn);

        public Apprenticeship FollowedByConfirmedOn()
        {
            var confirmedOn = previousActionOn.AddDays(1);
            return With(confirmedOn: confirmedOn, previousActionOn: confirmedOn);
        }

        public Apprenticeship FollowedByStoppedReceivedOn()
        {
            var stoppedReceivedOn = previousActionOn.AddDays(1);
            return With(stoppedReceivedOn: stoppedReceivedOn, previousActionOn: stoppedReceivedOn);
        }

        public Apprenticeship FollowedByViewedOn()
        {
            var lastViewedOn = previousActionOn.AddDays(1);
            return With(lastViewedOn: lastViewedOn, previousActionOn: lastViewedOn);
        }
    }
}