using UnityEngine;
using TMPro;
using UnityEngine.Video;
using Scripts.Resources;
using DG.Tweening;
using UnityEngine.UI;
public class ExerciseActivityScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text sessionNameText;
    [SerializeField] private TMP_Text sessionCountText;
    [SerializeField] private TMP_Text exerciseNameText;
    [SerializeField] private TMP_Text exerciseMetricsText;
    [SerializeField] private VideoPlayer tutorialVideoPlayer;
    [SerializeField] private RawImage tutorialVideoRawImage;
    [SerializeField] private Transform timerBasedExercisePanel;
    [SerializeField] private Transform timerBasedExerciseCountdownText;
    [SerializeField] private Slider timerBasedExerciseSlider;
    [SerializeField] private Button doneButton;
    [SerializeField] private Button goBackButton;
    [SerializeField] private AudioClip beepSound;
    [SerializeField] private AudioClip finalBeepSound;
    Exercise currentExercise;
    Tween timerTween;
    Tween sliderTween;
    Tween textAnimTween;
    int previousTimeLeft;
    void OnEnable()
    {
        ConfigureUI();
        DOVirtual.DelayedCall(0.3f, () =>
        {
            StartExercise();
            StartExerciseTimer();
        });
    }
    void OnDisable()
    {
        timerTween?.Kill();
        sliderTween?.Kill();
        textAnimTween?.Kill();
    }
    void ConfigureUI()
    {
        sessionNameText.text = AppConstants.CurrentSession.name;
        sessionCountText.text = $"{AppConstants.CurrentSession.currentExerciseIndex + 1}/{AppConstants.CurrentSession.totalExercises}";
        currentExercise = AppConstants.CurrentSession.GetCurrentExercise();
        exerciseNameText.text = currentExercise.name;
        if (currentExercise.measurementType == ExerciseMeasurementType.Reps)
        {
            timerBasedExercisePanel.gameObject.SetActive(false);
            doneButton.interactable = true;
            exerciseMetricsText.gameObject.SetActive(true);
            exerciseMetricsText.text = $"{currentExercise.reps} Reps";
        }
        else
        {
            timerBasedExercisePanel.gameObject.SetActive(true);
            doneButton.interactable = true; //! make it false in final build
            exerciseMetricsText.gameObject.SetActive(false);
        }
        goBackButton.interactable = AppConstants.CurrentSession.currentExerciseIndex > 0;
    }
    void StartExercise()
    {
        if (!currentExercise.tutorialVideo)
        {
            tutorialVideoRawImage.gameObject.SetActive(false);
        }
        else
        {
            tutorialVideoPlayer.clip = AppConstants.CurrentSession.GetCurrentExercise().tutorialVideo;
            tutorialVideoPlayer.Play();
        }
    }
    void StartExerciseTimer()
    {
        if (currentExercise.measurementType == ExerciseMeasurementType.Time)
        {
            timerBasedExerciseCountdownText.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

            // Use float for smooth animation
            float timeRemaining = currentExercise.durationSeconds;
            int displayTime = Mathf.CeilToInt(timeRemaining);
            previousTimeLeft = displayTime;

            TMP_Text countdownText = timerBasedExerciseCountdownText.GetComponent<TMP_Text>();
            countdownText.text = displayTime.ToString();

            // Initial slider setup
            timerBasedExerciseSlider.value = 1f;

            // Create a smoother animation sequence
            timerTween = DOTween.To(
                () => timeRemaining,
                x =>
                {
                    // Update the time remaining
                    timeRemaining = x;

                    // Update slider with smooth interpolation
                    float normalizedTime = timeRemaining / currentExercise.durationSeconds;
                    timerBasedExerciseSlider.value = normalizedTime;

                    // Update display time only when crossing a second boundary
                    int newDisplayTime = Mathf.CeilToInt(timeRemaining);

                    if (newDisplayTime != previousTimeLeft)
                    {
                        previousTimeLeft = newDisplayTime;
                        countdownText.text = newDisplayTime.ToString();

                        // Play beep sound when timer is 10 seconds or less
                        if (newDisplayTime <= 10 && newDisplayTime > 0)
                        {
                            AudioSource.PlayClipAtPoint(beepSound, Camera.main.transform.position);
                        }

                        // Create a smooth and subtle text animation
                        textAnimTween?.Kill();

                        // Scale the text down slightly first, then bounce it back
                        textAnimTween = DOTween.Sequence()
                            .Append(timerBasedExerciseCountdownText.DOScale(0.85f, 0.1f).SetEase(Ease.OutQuad))
                            .Append(timerBasedExerciseCountdownText.DOScale(1.15f, 0.2f).SetEase(Ease.OutBack))
                            .Append(timerBasedExerciseCountdownText.DOScale(1f, 0.1f).SetEase(Ease.OutQuad));
                    }
                },
                0,
                // Add a bit extra time for visual smoothness
                currentExercise.durationSeconds + 0.5f
            )
            .SetEase(Ease.Linear) // Linear ease for consistent timer movement
            .OnComplete(() =>
            {
                // Play the final beep sound
                AudioSource.PlayClipAtPoint(finalBeepSound, Camera.main.transform.position);
                timerBasedExerciseCountdownText.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    ExerciseScreenManager.Instance.FinishExercise();
                });
            });
        }
    }
}