namespace Managers.Social
{
    public class FacebookLoginProvider : LoginProviderBase
    {
        public override string ProviderName { get; } = "Facebook";

        public override bool TryExpressLogin()
        {
            LastLoginResult = new LoginResult() {
                IsSuccess = false,
                Provider = ProviderName,
                Token = LoadToken()
            };
            if (!this.worker.CheckLogin()) {
                return false;
            }

            LastLoginResult.IsSuccess = true;
            return true;
        }
    }
}