using System.Threading.Tasks;
using Data;
using UnityEngine;

namespace Managers.ProfileStorage
{
    public class ProfileService : IProfileService
    {
        private ProfileData profile;
        private readonly LocalProfileStorage localStorage = new();

        public async Task LoadOrCreateProfileAsync()
        {
            var loadedProfile = await this.localStorage.LoadProfileAsync();
            if (loadedProfile == null) {
                Debug.Log("No local profile found. Creating a new one");
                this.profile = CreateNewProfile();
                await this.localStorage.SaveImmediate(this.profile);
            }
            this.profile = loadedProfile;
        }

        public void LoadOrCreateProfile()
        {
            var loadedProfile = this.localStorage.LoadProfile();
            if (loadedProfile == null) {
                Debug.Log("No local profile found. Creating a new one");
                this.profile = CreateNewProfile();
            }

            this.profile = loadedProfile;
        }

        private static ProfileData CreateNewProfile()
        {
            var newProfile = new ProfileData {
                View = {
                    Nickname = Constants.c_DefaultPlayerName
                }
            };
            return newProfile;
        }

        public string GetNickname()
        {
            return this.profile.View.Nickname;
        }

        public void SetNickname(string name)
        {
            this.profile.View.Nickname = name;
        }

        public int GetSkin()
        {
            return this.profile.View.FavoriteSkin;
        }

        public void SetSkin(int skin)
        {
            this.profile.View.FavoriteSkin = skin;
        }
    }
}