using System;
using System.Threading;
using System.Threading.Tasks;
using Data;

namespace Managers.Profile.Storage
{
    public class ProfileSaver
    {
        private const int DeferredSaveDelayMilliseconds = 2000;
        private readonly IProfileStorage localStorage;
        private CancellationTokenSource deferredSaveCancellation;
        private ProfileData profileToSave;
        private bool IsDeferredSaveScheduled => this.deferredSaveCancellation != null;

        public ProfileSaver(IProfileStorage localStorage)
        {
            this.localStorage = localStorage;
        }

        public Task Save(ProfileData profileToSave)
        {
            this.profileToSave = profileToSave;
            return Save();
        }

        public void TryScheduleDeferredSave(ProfileData profileToSave)
        {
            this.profileToSave = profileToSave;
            if (this.IsDeferredSaveScheduled) {
                return;
            }
            ScheduleDeferredSave();
        }

        private void ScheduleDeferredSave()
        {
            this.deferredSaveCancellation = new CancellationTokenSource();
            var cancellationToken = this.deferredSaveCancellation.Token;
            var delayedTask = Task.Run((Func<Task>)(async () => {
                await Task.Delay(DeferredSaveDelayMilliseconds, cancellationToken);
                if (cancellationToken.IsCancellationRequested) {
                    return;
                }
                await Save();
                this.deferredSaveCancellation = null;
            }), cancellationToken);
        }

        private Task Save()
        {
            // No need to save later if we're saving right now  
            this.deferredSaveCancellation.Cancel();
            this.deferredSaveCancellation = null;
            return this.localStorage.SaveImmediate(this.profileToSave);
        }
    }
}