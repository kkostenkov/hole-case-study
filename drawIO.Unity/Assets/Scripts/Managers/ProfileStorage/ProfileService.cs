using System.Threading;
using System.Threading.Tasks;
using Data;
using UnityEngine;

namespace Managers.ProfileStorage
{
    public class ProfileService : IProfileService
    {
        private ProfileData profile;
        private readonly LocalProfileStorage localStorage = new();
        private CancellationTokenSource deferredSaveCancellation;
        private const int DeferredSaveDelayMilliseconds = 2000;
        private bool IsDeferredSaveScheduled => this.deferredSaveCancellation != null;

        public async Task LoadOrCreateProfileAsync()
        {
            var loadedProfile = await this.localStorage.LoadProfileAsync();
            if (loadedProfile == null) {
                Debug.Log("No local profile found. Creating a new one");
                this.profile = CreateNewProfile();
                await SaveProfile();
            }
            this.profile = loadedProfile;
        }

        public void LoadOrCreateProfile()
        {
            var loadedProfile = this.localStorage.LoadProfile();
            if (loadedProfile == null) {
                Debug.Log("No local profile found. Creating a new one");
                this.profile = CreateNewProfile();
                TryScheduleDeferredSave();
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
            TryScheduleDeferredSave();
        }

        public int GetSkin()
        {
            return this.profile.View.FavoriteSkin;
        }

        public void SetSkin(int skin)
        {
            this.profile.View.FavoriteSkin = skin;
            TryScheduleDeferredSave();
        }

        private Task SaveProfile()
        {
            this.deferredSaveCancellation.Cancel();
            this.deferredSaveCancellation = null;
            return this.localStorage.SaveImmediate(this.profile);
        }

        private void TryScheduleDeferredSave()
        {
            if (this.IsDeferredSaveScheduled) {
                return;
            }
            this.deferredSaveCancellation = new CancellationTokenSource();
            var cancellationToken = this.deferredSaveCancellation.Token;
            var delayedTask = Task.Run(async () => {
                await Task.Delay(DeferredSaveDelayMilliseconds, cancellationToken);
                if (cancellationToken.IsCancellationRequested) {
                    return;
                }
                await SaveProfile();
                this.deferredSaveCancellation = null;
            }, cancellationToken);
        }
    }
}