using System.Threading.Tasks;

namespace Managers.ProfileStorage
{
    public interface IProfileService : IProfileViewService
    {
        Task LoadOrCreateProfileAsync();
        void LoadOrCreateProfile();
    }
}