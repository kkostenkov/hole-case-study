using System.Threading.Tasks;
using Data;

namespace Managers.ProfileStorage
{
    public interface IProfileStorage
    {
        void SaveDeferred(ProfileData profile);
        Task SaveImmediate(ProfileData profile);
        Task<ProfileData> LoadProfile();
    }
}