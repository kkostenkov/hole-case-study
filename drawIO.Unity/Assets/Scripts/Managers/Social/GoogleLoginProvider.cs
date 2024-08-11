using System;
using UnityEngine;

namespace Managers.Social
{
    public class GoogleLoginProvider : LoginProviderBase
    {
        public override string ProviderName { get; } = "Google";

        public override bool TryExpressLogin()
        {
            var token = LoadToken();
            LastLoginResult = new LoginResult() {
                IsSuccess = false,
                Provider = ProviderName,
                Token = token,
            };
            if (!this.worker.CheckLogin(token)) {
                return false;
            }

            LastLoginResult.IsSuccess = true;
            return true;
        }
    }
}