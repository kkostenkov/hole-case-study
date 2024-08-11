using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Managers.ProfileStorage
{
    public class LocalProfileStorage : IProfileStorage
    {
        private const string SaveFileName = "profile.json";
        public readonly string SavePath;
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private ProfileData profileToSave;

        public LocalProfileStorage()
        {
            this.SavePath = $"{Application.persistentDataPath}/{SaveFileName}";
        }
        
        /// <summary>
        /// Call this to save immediately important progress update
        /// </summary>
        public Task SaveImmediate(ProfileData profile)
        {
            this.profileToSave = profile;
            return Save(this.profileToSave);
        }

        private async Task Save(ProfileData profile)
        {
            await this.semaphore.WaitAsync();
            try {
                var json = JsonConvert.SerializeObject(profile, Formatting.Indented);
                await using (var writer = new StreamWriter(this.SavePath)) {
                    await writer.WriteAsync(json);
                }
            }
            finally {
                this.semaphore.Release();
            }
        }
    }
}