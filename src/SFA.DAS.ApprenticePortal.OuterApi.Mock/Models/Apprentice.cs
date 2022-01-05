using Newtonsoft.Json;
using System;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock.Models
{
    public sealed class Apprentice
    {
        public Guid ApprenticeId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public bool TermsOfUseAccepted { get; }

        [JsonIgnore]
        public Apprenticeship? Apprenticeship { get; }

        public Apprentice(
            Guid apprenticeId,
            string firstName,
            string lastName,
            bool termsOfUseAccepted)
        {
            ApprenticeId = apprenticeId;
            FirstName = firstName;
            LastName = lastName;
            TermsOfUseAccepted = termsOfUseAccepted;
        }

        private Apprentice(
            Guid id,
            string firstName,
            string lastName,
            bool termsAccepted,
            Apprenticeship? apprenticeship)
        {
            ApprenticeId = id;
            FirstName = firstName;
            LastName = lastName;
            TermsOfUseAccepted = termsAccepted;
            Apprenticeship = apprenticeship;
        }

        public Apprentice WithId(Guid id) => With(id: id);

        public Apprentice WithFirstName(string firstName) => With(firstName: firstName);

        public Apprentice WithLastName(string lastName) => With(lastName: lastName);

        public Apprentice WithTermsOfUseAccepted(bool termsOfUseAccepted)
            => With(termsAccepted: termsOfUseAccepted);

        public Apprentice WithApprenticeship(Apprenticeship apprenticeship)
            => With(apprenticeship: Update.With(apprenticeship.ForApprentice(this)));

        public Apprentice WithoutApprenticeship()
            => With(apprenticeship: Update.With<Apprenticeship>(null));

        private Apprentice With(
            Guid? id = null,
            string? firstName = null,
            string? lastName = null,
            bool? termsAccepted = null,
            Update<Apprenticeship>? apprenticeship = null)
            => new Apprentice(
                id ?? ApprenticeId,
                firstName ?? FirstName,
                lastName ?? LastName,
                termsAccepted ?? TermsOfUseAccepted,
                apprenticeship == null ? Apprenticeship : apprenticeship.Value);
    }
}