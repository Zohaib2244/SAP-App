using UnityEngine;
using TMPro;
using Scripts.UI;
using Scripts.Resources;
using DG.Tweening;
namespace Scripts.UI.Screens
{
    public class UserSignInScreen : MonoBehaviour
    {
        #region Singleton
        public static UserSignInScreen Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion
        [SerializeField] private SigninScreen_1 signinScreen_1;
        [SerializeField] private SigninScreen_2 signinScreen_2;
        [SerializeField] private SigninScreen_3 signinScreen_3;
        public UserData newUserData = new UserData();
        [SerializeField] private CanvasGroup screen1CanvasGroup;
        [SerializeField] private CanvasGroup screen2CanvasGroup;
        [SerializeField] private CanvasGroup screen3CanvasGroup;
        void OnEnable()
        {
            signinScreen_2.gameObject.SetActive(false);
            signinScreen_3.gameObject.SetActive(false);
            signinScreen_1.gameObject.SetActive(true);
            screen1CanvasGroup.alpha = 0f;
            screen1CanvasGroup.DOFade(1f, 0.5f);

        }
        public void ShowScreen2()
        {
            screen1CanvasGroup.DOFade(0f, 0.5f).onComplete += () => signinScreen_1.gameObject.SetActive(false);
            signinScreen_2.gameObject.SetActive(true);
            screen2CanvasGroup.alpha = 0f;
            screen2CanvasGroup.DOFade(1f, 0.5f);
                    }
        public void ShowScreen3()
        {
            signinScreen_1.gameObject.SetActive(false);
            screen2CanvasGroup.DOFade(0f, 0.5f).onComplete += () => signinScreen_2.gameObject.SetActive(false);
            signinScreen_3.gameObject.SetActive(true);
            screen3CanvasGroup.alpha = 0f;
            screen3CanvasGroup.DOFade(1f, 0.5f);
        }
        public void SignInComplete()
        {
            // Save the new user data
            AppConstants.CreateNewUser(newUserData);
            // Activate the main menu screen
            Alert.Instance.ShowAlert("Sign In Complete", true);
            ScreenManager.Instance.ActivateScreen(ScreenTypes.MainMenu);
        }
    }
}