using UnityEngine;
using DG.Tweening;
using Scripts.Resources;
using TMPro;
using UnityEngine.UI;
namespace Scripts.UI.Screens
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text userNameText;
        [SerializeField] private TMP_Text userLevelText;
        [SerializeField] private Slider userLevelSlider;
        [SerializeField] private CanvasGroup aboutUsCanvasGroup;
        Tween sliderTween;
        void OnEnable()
        {
            UpdateUI();
        }
        void OnDisable()
        {
            sliderTween?.Kill();
        }
        void UpdateUI()
        {
            userNameText.text = AppConstants.GetCurrentUser().Username;
            DOVirtual.DelayedCall(0.1f, () =>
            {
                UpdateSlider();
            });
        }
                void UpdateSlider()
        {
            userLevelText.text = "Lvl " + AppConstants.GetCurrentUser().userLevel;
            float currentXP = AppConstants.GetCurrentUser().levelProgress;
            float xpForLevel = AppConstants.LevelProgress[AppConstants.GetCurrentUser().userLevel + 1];
            
            userLevelSlider.maxValue = xpForLevel;
            userLevelSlider.value = 0; // Start from zero
            
            // Animate the slider from 0 to current XP
            sliderTween = DOTween.To(
                () => userLevelSlider.value,
                x => userLevelSlider.value = x,
                currentXP,
                1.0f // Animation duration in seconds
            ).SetEase(Ease.OutQuad);
        }
        public void OnWikiBtnClick()
        {
        ScreenManager.Instance.ActivateScreen(ScreenTypes.WikiMain);
        }
        public void OnWorkoutsBtnClick()
        {
        ScreenManager.Instance.ActivateScreen(ScreenTypes.Workouts);
        }
        public void openAboutUs()
        {
            aboutUsCanvasGroup.gameObject.SetActive(true);
            aboutUsCanvasGroup.alpha = 0;
            aboutUsCanvasGroup.DOFade(1, 0.5f);
        }
        public void closeAboutUs()
        {
            aboutUsCanvasGroup.DOFade(0, 0.5f).OnComplete(() =>
            {
                aboutUsCanvasGroup.gameObject.SetActive(false);
            });
        }
    }
}