using System;
using UnityEngine;

namespace Managers.Social
{
    public abstract class LoginProviderBase : ILoginProvider
    {
        public event Action OnLoginComplete;
        public abstract string ProviderName { get; }
        protected LoginWorkerMock worker;
        protected LoginResult LastLoginResult;

        public virtual void Initialize()
        {
            this.worker = new LoginWorkerMock(ProviderName);
        }

        public virtual void Login()
        {
            this.worker.Login(OnLogin);
        }

        private void OnLogin(LoginResult result)
        {
            Debug.Log(result.IsSuccess ? $"Login is done. Token: {result.Token}" : "Unsuccessful login");

            this.LastLoginResult = result;
            this.OnLoginComplete?.Invoke();
        }
    }
}