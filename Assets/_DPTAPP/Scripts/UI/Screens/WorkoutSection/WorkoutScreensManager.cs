using UnityEngine;
using DG.Tweening;
using Scripts.UI;

public class WorkoutScreensManager : MonoBehaviour
{
    #region Singleton
    public static WorkoutScreensManager Instance;
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

    [SerializeField] private Transform workoutMainScreen;
    [SerializeField] private Transform workoutDetailScreen;
    [SerializeField] private Transform workoutPlanScreen;
    [SerializeField] private float fadeTime = 0.3f; // Add transition time parameter

    [SerializeField] private CanvasGroup workoutMainScreenCanvasGroup;
    [SerializeField] private CanvasGroup workoutDetailScreenCanvasGroup;
    [SerializeField] private CanvasGroup workoutPlanScreenCanvasGroup;


    void OnEnable()
    {
        ShowWorkoutMainScreen();
    }

    public void ShowWorkoutPlanScreen()
    {
        // Fade out current screens
        FadeOutScreen(workoutMainScreenCanvasGroup);
        FadeOutScreen(workoutDetailScreenCanvasGroup);

        // Activate and fade in plan screen
        workoutPlanScreen.gameObject.SetActive(true);
        FadeInScreen(workoutPlanScreenCanvasGroup);
    }

    public void ShowWorkoutDetailScreen()
    {
        ExerciseScreenManager.Instance.HideALLScreens();

        // Fade out current screens
        FadeOutScreen(workoutMainScreenCanvasGroup);
        FadeOutScreen(workoutPlanScreenCanvasGroup);

        // Activate and fade in detail screen
        workoutDetailScreen.gameObject.SetActive(true);
        FadeInScreen(workoutDetailScreenCanvasGroup);
    }

    public void ShowWorkoutMainScreen()
    {
        // Fade out current screens
        FadeOutScreen(workoutDetailScreenCanvasGroup);
        FadeOutScreen(workoutPlanScreenCanvasGroup);

        // Activate and fade in main screen
        workoutMainScreen.gameObject.SetActive(true);
        FadeInScreen(workoutMainScreenCanvasGroup);
    }

    public void HideAllScreens()
    {
        FadeOutScreen(workoutMainScreenCanvasGroup);
        FadeOutScreen(workoutDetailScreenCanvasGroup);
        FadeOutScreen(workoutPlanScreenCanvasGroup);
    }

    // Helper method to fade in screens
    private void FadeInScreen(CanvasGroup canvasGroup)
    {
        ScreenManager.Instance.graphicRaycaster.enabled = false;
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, fadeTime).OnComplete(() =>
            ScreenManager.Instance.graphicRaycaster.enabled = true);
    }

    // Helper method to fade out screens
    private void FadeOutScreen(CanvasGroup canvasGroup)
    {
        ;
        canvasGroup.DOFade(0, fadeTime).OnComplete(() =>
        {
            canvasGroup.transform.gameObject.SetActive(false);
            ScreenManager.Instance.graphicRaycaster.enabled = true;
        }
            );
    }
}