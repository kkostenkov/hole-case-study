using UnityEngine;
using UnityEngine.UI;

public class SocialPanelView : MonoBehaviour
{
    [SerializeField]
    private Button LogoutButton;
    [SerializeField]
    private Button FacebookLoginButton;
    [SerializeField]
    private Button GoogleLoginButton;
    
    private SocialPanelController controller;

    private void Awake()
    {
        this.controller = new SocialPanelController();
        this.controller.Updated += OnControllerUpdated;
        this.LogoutButton.onClick.AddListener(OnLogoutClicked);
        this.FacebookLoginButton.onClick.AddListener(OnFacebookLoginClicked);
        this.GoogleLoginButton.onClick.AddListener(OnGoogleLoginClicked);
    }

    private void OnEnable()
    {
        SetupView();
    }

    private void OnDestroy()
    {
        this.LogoutButton.onClick.RemoveAllListeners();
        this.FacebookLoginButton.onClick.RemoveAllListeners();
        this.GoogleLoginButton.onClick.RemoveAllListeners();
        this.controller.Updated -= OnControllerUpdated;
    }

    private void SetupView()
    {
        var isLoggedIn = this.controller.IsLoggedIn;
        LogoutButton.gameObject.SetActive(isLoggedIn);
        FacebookLoginButton.gameObject.SetActive(!isLoggedIn);
        GoogleLoginButton.gameObject.SetActive(!isLoggedIn);
    }

    private void OnControllerUpdated()
    {
        if (!gameObject.activeSelf) {
            return;
        }
        SetupView();
    }

    private void OnGoogleLoginClicked()
    {
        controller.LoginGoogle();
    }

    private void OnFacebookLoginClicked()
    {
        controller.LoginFacebook();
    }

    private void OnLogoutClicked()
    {
        this.controller.Logout();
    }
}