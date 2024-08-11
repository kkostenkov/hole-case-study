using System.Threading.Tasks;

namespace Managers.Profile
{
    public interface IProfileService : IProfileViewService
    {
        Task LoadOrCreateProfileAsync();
        void LoadOrCreateProfile();
    }
}