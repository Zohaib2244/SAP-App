using UnityEngine;
using TMPro;
using DG.Tweening;
using Scripts.UI;
public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private Transform settingsScreen;
    [SerializeField] private CanvasGroup settingsScreenCanvasGroup;
    [SerializeField] private float fadeTime = 0.3f; // Add transition time parameter

    [SerializeField] private TMP_InputField nameInputField;

    void ConfigureUI()
    {
        nameInputField.text = AppConstants.CurrentSession.name;
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        HideSettingsScreen();
        ScreenManager.Instance.ActivateScreen(Scripts.Resources.ScreenTypes.SignIn);
    }
    public void ShowSettingsScreen()
    {
        // Activate and fade in settings screen
        settingsScreen.gameObject.SetActive(true);
        FadeInScreen(settingsScreenCanvasGroup);
    }

    public void HideSettingsScreen()
    {
        FadeOutScreen(settingsScreenCanvasGroup);
    }

    private void FadeOutScreen(CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(0, fadeTime).onComplete += () =>
        {
            canvasGroup.gameObject.SetActive(false);
        };
    }

    private void FadeInScreen(CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(1, fadeTime);
    }
}