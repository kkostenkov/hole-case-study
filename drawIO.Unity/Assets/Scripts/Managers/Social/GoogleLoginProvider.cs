using System;
using UnityEngine;

namespace Managers.Social
{
    public class GoogleLoginProvider : LoginProviderBase
    {
        public override string ProviderName { get; } = "Google";

        public override bool TryExpressLogin()
        {
            var token = PlayerPrefs.GetString(TokenPrefsKey);
            if (string.IsNullOrEmpty(token)) {
                return false;
            }
            return this.worker.CheckLogin(token);
        }
    }
}