namespace Managers.Social
{
    public class FacebookLoginProvider : LoginProviderBase
    {
        public override string ProviderName { get; } = "Facebook";

        public override bool TryExpressLogin()
        {
            return this.worker.CheckLogin();
        }
    }
}