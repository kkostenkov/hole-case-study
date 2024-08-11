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
        UpdateView();
    }

    private void OnDestroy()
    {
        this.LogoutButton.onClick.RemoveAllListeners();
        this.FacebookLoginButton.onClick.RemoveAllListeners();
        this.GoogleLoginButton.onClick.RemoveAllListeners();
        this.controller.Updated -= OnControllerUpdated;
    }

    private void UpdateView()
    {
        var isLoggedIn = this.controller.IsLoggedIn;
        LogoutButton.gameObject.SetActive(isLoggedIn);
        LogoutButton.interactable = this.controller.IsAcceptingInput;
        FacebookLoginButton.gameObject.SetActive(!isLoggedIn);
        FacebookLoginButton.interactable = this.controller.IsAcceptingInput;
        GoogleLoginButton.gameObject.SetActive(!isLoggedIn);
        GoogleLoginButton.interactable = this.controller.IsAcceptingInput;
    }

    private void OnControllerUpdated()
    {
        if (!gameObject.activeSelf) {
            return;
        }
        UpdateView();
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