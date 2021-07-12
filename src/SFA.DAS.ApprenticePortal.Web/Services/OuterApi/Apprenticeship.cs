
namespace SFA.DAS.ApprenticePortal.Web.Services.OuterApi
{
    public class Apprenticeship
    {
        public long Id { get; set; }
        public long CommitmentStatementId { get; set; }
        public string CourseName { get; set; } = null!;
    }
}
