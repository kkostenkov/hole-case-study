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

        public LocalProfileStorage()
        {
            this.SavePath = $"{Application.persistentDataPath}/{SaveFileName}";
        }
        
        public Task SaveImmediate(ProfileData profile)
        {
            return Save(profile);
        }

        public async Task<ProfileData> LoadProfileAsync()
        {
            await this.semaphore.WaitAsync();
            try {
                if (!File.Exists(this.SavePath)) {
                    return null;
                }

                var json = await File.ReadAllTextAsync(this.SavePath);
                var data = JsonConvert.DeserializeObject<ProfileData>(json);
                return data;
            }
            finally {
                this.semaphore.Release();
            }
        }
        
        public ProfileData LoadProfile()
        {
            if (!File.Exists(this.SavePath)) {
                return null;
            }

            var json = File.ReadAllText(this.SavePath);
            var data = JsonConvert.DeserializeObject<ProfileData>(json);
            return data;
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
            Debug.Log("Profile saved");
        }
    }
}