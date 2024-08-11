using System;
using System.Collections;
using Data;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Managers.Profile
{
    public class RemoteProfileFetcher
    {
        private event Action<ProfileDataBundle> ProfileFetchComplete;
        
        public void TryFetch(Action<ProfileDataBundle> onComplete)
        {
            if (onComplete != null) {
                this.ProfileFetchComplete += onComplete;
            }

            var runner = new CoroutineRunner();
            runner.StartCoroutine(FetchRemoteProfile());
        }

        private IEnumerator FetchRemoteProfile()
        {
            // calling imaginary API
            if (!Application.isEditor) {
                yield return new WaitForSeconds(0.5f);    
            }
            var result = new ProfileDataBundle() {
                Details = new ProfileSaveDetails() { 
                    SaveTimestamp = new Random().Next(),
                    Version = -1,
                },
                ProfileJson = "{\"some\": \"JsonData\"}",
            };
            this.ProfileFetchComplete?.Invoke(result);
            this.ProfileFetchComplete = null;
        }
    }
}