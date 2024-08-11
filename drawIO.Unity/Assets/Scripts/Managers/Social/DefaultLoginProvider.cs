using UnityEngine;

namespace Managers.Social
{
    public class DefaultLoginProvider : LoginProviderBase
    {
        public override string ProviderName { get; } = "DefaultProvider";

        public void CheckLoginFlow()
        {
            OnLoginComplete += OnOnLoginComplete;
            base.Login();
        }

        private void OnOnLoginComplete()
        {
            Debug.Log($"Login flow complete. \n {LastLoginResult}");
        }
    }
}