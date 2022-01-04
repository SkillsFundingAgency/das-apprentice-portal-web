using Newtonsoft.Json;
using System;

namespace SFA.DAS.ApprenticePortal.OuterApi.Mock.Models
{
    public static class Natural
    {
        public static Apprentice Apprentice => new Apprentice(Guid.NewGuid(), "Bob", "Bobbertson", true);
        public static Apprenticeship Apprenticeship => new Apprenticeship(Guid.NewGuid(), 12, "WorkCo", "LearnCo", new DateTime(2020, 08, 20), null, null, null);
    }

    public sealed class Apprentice
    {
        public Guid ApprenticeId { get;}
        public string FirstName { get; }
        public string LastName { get; }
        public bool TermsOfUseAccepted { get;}

        [JsonIgnore]
        public Apprenticeship? Apprenticeship { get; }

        public Apprentice(
            Guid id,
            string firstName,
            string lastName,
            bool termsAccepted)
        {
            ApprenticeId = id;
            FirstName = firstName;
            LastName = lastName;
            TermsOfUseAccepted = termsAccepted;
        }

        private Apprentice(
            Guid id,
            string firstName,
            string lastName,
            bool termsAccepted,
            Apprenticeship apprenticeship)
        {
            ApprenticeId = id;
            FirstName = firstName;
            LastName = lastName;
            TermsOfUseAccepted = termsAccepted;
            Apprenticeship = apprenticeship;
        }

        public Apprentice WithId(Guid id) => With(id: id);

        public Apprentice WithApprenticeship(Apprenticeship apprenticeship)
            => new Apprentice(ApprenticeId, FirstName, LastName, TermsOfUseAccepted, apprenticeship.ForApprentice(this));

        public Apprentice WithoutApprenticeship()
            => new Apprentice(ApprenticeId, FirstName, LastName, TermsOfUseAccepted);

        private Apprentice With(Guid? id = null, string? firstName = null, string? lastName = null, bool? termsAccepted = null)
            => new Apprentice(
                id ?? ApprenticeId,
                firstName ?? FirstName,
                lastName ?? LastName,
                termsAccepted ?? TermsOfUseAccepted);
        
        public Guid WithApprenticeship(object p) => throw new NotImplementedException();
    }
}