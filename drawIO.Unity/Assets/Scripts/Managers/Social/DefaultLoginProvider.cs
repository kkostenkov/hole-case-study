using UnityEngine;

namespace Managers.Social
{
    public class DefaultLoginProvider : LoginProviderBase
    {
        public override string ProviderName { get; } = "DefaultProvider";
        public override bool TryExpressLogin()
        {
            return false;
        }

        public void CheckLoginFlow()
        {
            base.Login(OnOnLoginComplete);
        }

        private void OnOnLoginComplete()
        {
            Debug.Log($"Login flow complete. \n {LastLoginResult}");
        }
    }
}