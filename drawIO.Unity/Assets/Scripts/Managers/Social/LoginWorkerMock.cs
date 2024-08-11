using System;
using System.Collections;
using UnityEngine;

namespace Managers.Social
{
    public class LoginWorkerMock
    {
        private readonly string providerName;
        private event Action<LoginResult> LoginSubscribers;

        public LoginWorkerMock(string providerName)
        {
            this.providerName = providerName;
        }

        public void Login(Action<LoginResult> onLogin)
        {
            if (onLogin != null) {
                this.LoginSubscribers += onLogin;    
            }
            // A helper object to be in sync with unity main thread
            GameManager.Instance.StartCoroutine(ResolveLogin());
        }

        private IEnumerator ResolveLogin()
        {
            // calling imaginary API
            if (!Application.isEditor) {
                yield return new WaitForSeconds(0.5f);    
            }
            var loginResult = new LoginResult() {
                IsSuccess = true,
                Provider = this.providerName,
                Token = Guid.NewGuid().ToString()
            };
            
            this.LoginSubscribers?.Invoke(loginResult);
        }
    }
}