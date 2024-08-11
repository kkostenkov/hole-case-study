using System.Threading.Tasks;
using Data;

namespace Managers.Profile.Storage
{
    public interface IProfileStorage
    {
        Task SaveImmediate(ProfileData profile);
        ProfileData LoadProfile();
        Task<ProfileData> LoadProfileAsync();
    }
}