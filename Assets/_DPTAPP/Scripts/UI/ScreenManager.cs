using System.Collections.Generic;
using UnityEngine;
using Scripts.Resources;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class ScreenManager : MonoBehaviour
    {
        #region Singleton
        public static ScreenManager Instance;
        public GraphicRaycaster graphicRaycaster;
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
        [SerializeField] private List<UIScreen> screens = new List<UIScreen>();

        public void ActivateScreen(ScreenTypes screenType)
        {
            foreach (var screen in screens)
            {
                if (screen.screenType == screenType)
                {
                    screen.screenTransform.gameObject.SetActive(true);
                }
                else
                {
                    screen.screenTransform.gameObject.SetActive(false);
                }
            }
        }
        void Start()
        {
            if(AppConstants.GetCurrentUser().Username == "")
            {
                ActivateScreen(ScreenTypes.SignIn);
            }
            else
            {
                ActivateScreen(ScreenTypes.MainMenu);
            }
        }
    }
}