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
        this.Updated?.Invoke();
        this.loginService.LoginWithGoogle();
    }

    public void LoginFacebook()
    {
        IsAcceptingInput = false;
        this.Updated?.Invoke();
        this.loginService.LoginWithFacebook();
    }

    public void Logout()
    {
        IsAcceptingInput = false;
        this.Updated?.Invoke();
        this.loginService.Logout();
    }

    public void OnLoginStatusChanged()
    {
        IsAcceptingInput = true;
        this.Updated?.Invoke();
    }
}