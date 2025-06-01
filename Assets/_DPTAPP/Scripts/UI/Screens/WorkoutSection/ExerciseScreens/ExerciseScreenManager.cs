using DG.Tweening;
using Scripts.Resources;
using UnityEngine;
using Scripts.UI;
public class ExerciseScreenManager : MonoBehaviour
{
    #region Singleton
    public static ExerciseScreenManager Instance;
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
    [SerializeField] private Transform ExerciseStartScreen;
    [SerializeField] private Transform ExerciseBreakScreen;
    [SerializeField] private Transform ExerciseScreen;
    [SerializeField] private Transform ExerciseCompleteScreen;
    [SerializeField] private Transform ExerciseQuitPopup;
    [SerializeField] private CanvasGroup overlayCanvasGroup;
   
    private CanvasGroup exerciseStartScreenCanvasGroup;
    private CanvasGroup exerciseBreakScreenCanvasGroup;
    private CanvasGroup exerciseScreenCanvasGroup;
    private CanvasGroup exerciseCompleteScreenCanvasGroup;
      [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float fadeOutDuration = 0.3f;
    [SerializeField] private Ease fadeInEase = Ease.OutQuad;
    [SerializeField] private Ease fadeOutEase = Ease.InQuad;
    void Start()
    {
        exerciseStartScreenCanvasGroup = ExerciseStartScreen.GetComponent<CanvasGroup>();
        exerciseBreakScreenCanvasGroup = ExerciseBreakScreen.GetComponent<CanvasGroup>();
        exerciseScreenCanvasGroup = ExerciseScreen.GetComponent<CanvasGroup>();
        exerciseCompleteScreenCanvasGroup = ExerciseCompleteScreen.GetComponent<CanvasGroup>();
    }
      public void StartSession()
    {
        if(!AppConstants.CurrentSession.isReadyToBeShown)
        {
            return;
        }
        WorkoutScreensManager.Instance.HideAllScreens();
        overlayCanvasGroup.gameObject.SetActive(false);
        AppConstants.CurrentSession.currentExerciseIndex = 0;
        
        // Enable screen first
        ExerciseStartScreen.gameObject.SetActive(true);
        exerciseStartScreenCanvasGroup.alpha = 0;
        // Fade in
        exerciseStartScreenCanvasGroup.DOFade(1, fadeInDuration).SetEase(fadeInEase);
    }

    public void StartNextExercise()
    {
        // Fade out start screen if it's active
        if (ExerciseStartScreen.gameObject.activeSelf)
        {
            FadeOutScreen(exerciseStartScreenCanvasGroup, ExerciseStartScreen.gameObject, () => {
                // Enable exercise screen
                EnableScreenWithFade(ExerciseScreen, exerciseScreenCanvasGroup);
            });
        }
        // Fade out break screen if it's active
        else if (ExerciseBreakScreen.gameObject.activeSelf)
        {
            FadeOutScreen(exerciseBreakScreenCanvasGroup, ExerciseBreakScreen.gameObject, () => {
                // Enable exercise screen
                EnableScreenWithFade(ExerciseScreen, exerciseScreenCanvasGroup);
            });
        }
        else
        {
            // Direct enable if no fade needed
            EnableScreenWithFade(ExerciseScreen, exerciseScreenCanvasGroup);
        }
    }

    public void GoToPreviousExercise()
    {
        AppConstants.CurrentSession.currentExerciseIndex--;
        
        // Fade out current screens
        FadeOutScreen(exerciseScreenCanvasGroup, ExerciseScreen.gameObject, () => {
            // Enable exercise screen with fade
            EnableScreenWithFade(ExerciseScreen, exerciseScreenCanvasGroup);
        });
    }

    public void FinishExercise()
    {
        if (AppConstants.CurrentSession.currentExerciseIndex < AppConstants.CurrentSession.totalExercises - 1)
        {
            ShowExerciseBreakScreen();
        }
        else
        {
            ShowExerciseCompleteScreen();
        }
    }

    void ShowExerciseBreakScreen()
    {
        FadeOutScreen(exerciseScreenCanvasGroup, ExerciseScreen.gameObject, () => {
            // Enable break screen with fade
            EnableScreenWithFade(ExerciseBreakScreen, exerciseBreakScreenCanvasGroup);
        });
    }

    void ShowExerciseCompleteScreen()
    {
        // Fade out current screens
        if (ExerciseScreen.gameObject.activeSelf)
        {
            FadeOutScreen(exerciseScreenCanvasGroup, ExerciseScreen.gameObject, () => {
                // Enable complete screen with fade
                EnableScreenWithFade(ExerciseCompleteScreen, exerciseCompleteScreenCanvasGroup);
            });
        }
        else if (ExerciseBreakScreen.gameObject.activeSelf)
        {
            FadeOutScreen(exerciseBreakScreenCanvasGroup, ExerciseBreakScreen.gameObject, () => {
                // Enable complete screen with fade
                EnableScreenWithFade(ExerciseCompleteScreen, exerciseCompleteScreenCanvasGroup);
            });
        }
    }
    public void CLosePopup()
    {
        overlayCanvasGroup.DOFade(0f, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            overlayCanvasGroup.gameObject.SetActive(false);
        });
        ExerciseQuitPopup.DOScale(Vector3.one * 0.5f, 0.5f).SetUpdate(true).SetEase(Ease.InBack).OnComplete(() =>
        {
            ExerciseQuitPopup.gameObject.SetActive(false);
            Time.timeScale = 1;
        });
    }
    public void QuitExercise()
    {
        overlayCanvasGroup.gameObject.SetActive(true);
        ExerciseQuitPopup.gameObject.SetActive(true);
        overlayCanvasGroup.alpha = 0;
        overlayCanvasGroup.DOFade(1f, 0.5f).SetUpdate(true);
        ExerciseQuitPopup.localScale = Vector3.one * 0.5f;
        ExerciseQuitPopup.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
        Time.timeScale = 0;
    }
    public void AcceptQuit()
    {
        Time.timeScale = 1;
        overlayCanvasGroup.DOFade(0f, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            overlayCanvasGroup.gameObject.SetActive(false);
        });

        ExerciseQuitPopup.DOScale(Vector3.one * 0.5f, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            ExerciseQuitPopup.gameObject.SetActive(false);
            ExerciseScreen.gameObject.SetActive(false);
            ExerciseBreakScreen.gameObject.SetActive(false);
            ExerciseCompleteScreen.gameObject.SetActive(false);
            WorkoutScreensManager.Instance.ShowWorkoutDetailScreen();
        });
    }
    public void HideALLScreens()
    {
        ExerciseStartScreen.gameObject.SetActive(false);
        ExerciseBreakScreen.gameObject.SetActive(false);
        ExerciseScreen.gameObject.SetActive(false);
        ExerciseCompleteScreen.gameObject.SetActive(false);
        ExerciseQuitPopup.gameObject.SetActive(false);
    }
private void EnableScreenWithFade(Transform screen, CanvasGroup canvasGroup)
    {
        screen.gameObject.SetActive(true);
        ScreenManager.Instance.graphicRaycaster.enabled = false;
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, fadeInDuration).SetEase(fadeInEase).OnComplete(() =>
            ScreenManager.Instance.graphicRaycaster.enabled = true);
    }
    
    private void FadeOutScreen(CanvasGroup canvasGroup, GameObject screen, TweenCallback onComplete = null)
    {
        ScreenManager.Instance.graphicRaycaster.enabled = false;
        canvasGroup.DOFade(0, fadeOutDuration)
            .SetEase(fadeOutEase)
            .OnComplete(() => {
                screen.SetActive(false);
                ScreenManager.Instance.graphicRaycaster.enabled = true;
                onComplete?.Invoke();
            });
    }
}