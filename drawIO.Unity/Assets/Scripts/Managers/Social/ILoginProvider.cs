
using System;

namespace Managers.Social
{
    public interface ILoginProvider
    {
        string ProviderName { get; }
        LoginResult LastLoginResult { get; set; }
        void Initialize();
        void Login(Action onComplete);
        bool TryExpressLogin();
        void Logout();
    }
}