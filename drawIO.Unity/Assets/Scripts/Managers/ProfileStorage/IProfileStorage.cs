using System.Threading.Tasks;
using Data;

namespace Managers.ProfileStorage
{
    public interface IProfileStorage
    {
        Task SaveImmediate(ProfileData profile);
        Task<ProfileData> LoadProfileAsync();
    }
}