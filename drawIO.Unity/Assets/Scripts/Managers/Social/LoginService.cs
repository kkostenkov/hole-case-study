using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Social
{
    public class LoginService
    {
        public event Action LoginLogoutFlowComplete;
        private Dictionary<string, ILoginProvider> supportedProviders = new();
        private ILoginProvider currentProvider;
        private const string SocialProviderPrefsKey = "SocialProvider";

        public bool IsLoggedIn {
            get {
                var result = this.currentProvider?.LastLoginResult;
                return result?.IsSuccess ?? false;
            }
        }

        public void Initialize()
        {
            CreateProviders();
            TryInitializeAndLoginWithKnownProvider();
        }

        private void CreateProviders()
        {
            ILoginProvider provider = new GoogleLoginProvider();
            supportedProviders.Add(provider.ProviderName, provider);
            provider = new FacebookLoginProvider();
            supportedProviders.Add(provider.ProviderName, provider);
        }

        private void TryInitializeAndLoginWithKnownProvider()
        {
            currentProvider = GetLastProvider();
            if (currentProvider == null) {
                return;
            }
            this.currentProvider.Initialize();
            this.currentProvider.TryExpressLogin();
        }

        public void LoginWithGoogle()
        {
            var provider = this.supportedProviders["Google"];
            InitializeAndLogin(provider);
        }

        public void LoginWithFacebook()
        {
            var provider = this.supportedProviders["Facebook"];
            InitializeAndLogin(provider);
        }

        public void Logout()
        {
            SaveLastActiveProviderName(string.Empty);
            this.currentProvider?.Logout();
            this.LoginLogoutFlowComplete?.Invoke();
            Debug.Log("Logged out");
        }

        private void InitializeAndLogin(ILoginProvider provider)
        {
            provider.Initialize();
            this.currentProvider = provider;
            this.currentProvider.Login(OnLoginComplete);
        }

        private void OnLoginComplete()
        {
            var result = this.currentProvider.LastLoginResult;
            Debug.Log($"Logged {result.IsSuccess} in with {result.Provider}");
            if (result.IsSuccess) {
                SaveLastActiveProviderName(result.Provider);    
            }
            this.LoginLogoutFlowComplete?.Invoke();
        }

        private void SaveLastActiveProviderName(string name)
        {
            PlayerPrefs.SetString(SocialProviderPrefsKey, name);
        }

        private ILoginProvider GetLastProvider()
        {
            var name = PlayerPrefs.GetString(SocialProviderPrefsKey);
            this.supportedProviders.TryGetValue(name, out var provider);
            return provider;
        }
    }
}