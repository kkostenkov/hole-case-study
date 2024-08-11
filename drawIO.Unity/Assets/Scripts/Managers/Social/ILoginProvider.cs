using System;

namespace Managers.Social
{
    public interface ILoginProvider
    {
        event Action OnLoginComplete;
        void Initialize();
        void Login();
    }
}