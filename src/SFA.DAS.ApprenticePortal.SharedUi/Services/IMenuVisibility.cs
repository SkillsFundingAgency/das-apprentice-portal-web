using System.Threading.Tasks;

namespace SFA.DAS.ApprenticePortal.SharedUi.Services
{
    public interface IMenuVisibility
    {
        Task<bool> ShowConfirmMyApprenticeship();
    }
}