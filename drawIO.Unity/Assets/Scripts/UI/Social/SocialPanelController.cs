using System;
using Managers.Social;

internal class SocialPanelController
{
    public event Action Updated;
    public bool IsLoggedIn => this.loginService.IsLoggedIn;
    public bool IsAcceptingInput;
    private LoginService loginService;

    public SocialPanelController()
    {
        this.loginService = new LoginService();
        this.loginService.LoginLogoutFlowComplete += OnLoginStatusChanged;
        loginService.Initialize();
        IsAcceptingInput = true;
    }

    public void LoginGoogle()
    {
        IsAcceptingInput = false;
        this.loginService.LoginWithGoogle();
        this.Updated?.Invoke();
    }

    public void LoginFacebook()
    {
        IsAcceptingInput = false;
        this.loginService.LoginWithFacebook();
    }

    public void Logout()
    {
        IsAcceptingInput = false;
        this.loginService.Logout();
    }

    public void OnLoginStatusChanged()
    {
        IsAcceptingInput = true;
        this.Updated?.Invoke();
    }
}