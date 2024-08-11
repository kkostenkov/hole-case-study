using System;
using UnityEngine;

namespace Managers.Social
{
    public abstract class LoginProviderBase : ILoginProvider
    {
        public abstract string ProviderName { get; }
        public LoginResult LastLoginResult { get; set; }
        protected event Action LoginComplete;
        protected string TokenPrefsKey => ProviderName + "_Token";
        protected LoginWorkerMock worker;

        public virtual void Initialize()
        {
            this.worker = new LoginWorkerMock(ProviderName);
        }

        public virtual void Login(Action onComplete)
        {
            this.LoginComplete += onComplete;
            this.worker.Login(OnLogin);
        }

        public virtual bool TryExpressLogin()
        {
            Debug.LogError("Express login is called on base method. Use overrides");
            return false;
        }

        public virtual void Logout()
        {
            DeleteToken();
        }

        private void OnLogin(LoginResult result)
        {
            Debug.Log(result.IsSuccess ? $"Login is done. Token: {result.Token}" : "Unsuccessful login");

            this.LastLoginResult = result;
            if (LastLoginResult.IsSuccess) {
                SaveToken(LastLoginResult.Token);
            }
            this.LoginComplete?.Invoke();
            this.LoginComplete = null;
        }

        private void SaveToken(string token)
        {
            PlayerPrefs.SetString(TokenPrefsKey, token);
        }

        public void DeleteToken()
        {
            PlayerPrefs.DeleteKey(TokenPrefsKey);
        }
    }
}