using System;
using Managers.Social;

internal class SocialPanelController
{
    public event Action Updated; 
    public bool IsLoggedIn;
    private LoginService loginService;

    public SocialPanelController()
    {
        this.loginService = new LoginService();
    }

    public void LoginGoogle()
    {
        this.IsLoggedIn = true;
        this.Updated?.Invoke();
    }

    public void LoginFacebook()
    {
        this.IsLoggedIn = true;
        this.Updated?.Invoke();
    }

    public void Logout()
    {
        this.IsLoggedIn = false;
        this.Updated?.Invoke();
    }
}