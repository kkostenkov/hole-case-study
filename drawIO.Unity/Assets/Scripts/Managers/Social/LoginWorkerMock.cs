using System;
using System.Collections;
using UnityEngine;
using Utils;

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
            var runner = new CoroutineRunner();
            runner.StartCoroutine(ResolveLogin());
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

        public bool CheckLogin(string token = null)
        {
            return true;
        }
    }
}