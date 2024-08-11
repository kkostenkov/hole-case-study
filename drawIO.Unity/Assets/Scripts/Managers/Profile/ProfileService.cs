using System.Threading.Tasks;
using Data;
using Managers.Profile.Storage;
using UnityEngine;

namespace Managers.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileStorage localStorage = new LocalProfileStorage();
        private ProfileData profile;
        private readonly ProfileSaver profileSaver;

        public ProfileService()
        {
            this.profileSaver = new ProfileSaver(this.localStorage);
        }

        public async Task LoadOrCreateProfileAsync()
        {
            this.profile = await this.localStorage.LoadProfileAsync();
            if (this.profile == null) {
                CreateNewProfile();    
            }
            Debug.Log("Profile loaded");
        }

        public void LoadOrCreateProfile()
        {
            this.profile = this.localStorage.LoadProfile();
            if (this.profile == null) {
                CreateNewProfile();    
            }
            Debug.Log("Profile loaded");
        }

        string IProfileViewService.GetNickname()
        {
            return this.profile.View.Nickname;
        }

        void IProfileViewService.SetNickname(string name)
        {
            this.profile.View.Nickname = name;
            this.profileSaver.TryScheduleDeferredSave(this.profile);
        }

        int IProfileViewService.GetSkin()
        {
            return this.profile.View.FavoriteSkin;
        }

        void IProfileViewService.SetSkin(int skin)
        {
            this.profile.View.FavoriteSkin = skin;
            this.profileSaver.TryScheduleDeferredSave(this.profile);
        }

        private void CreateNewProfile()
        {
            Debug.Log("Creating new profile");
            this.profile = new ProfileData {
                View = {
                    Nickname = Constants.c_DefaultPlayerName
                }
            };
            this.profileSaver.TryScheduleDeferredSave(this.profile);
        }
    }
}