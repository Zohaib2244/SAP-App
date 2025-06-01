using TMPro;
using UnityEngine;
using DG.Tweening;
using Scripts.Resources;
using System;
using System.Collections.Generic;
public class ExerciseBreakScreen : MonoBehaviour
{

    [Header("Segment Completed")]
    [SerializeField] private Transform segmentCompletedContainer;
    [SerializeField] private Transform warmUpCompletedText;
    [SerializeField] private Transform mainWorkoutCompletedText;
    [Header("Segment Not Completed")]
    [SerializeField] private Transform segmentNotCompletedContainer;
    [SerializeField] private TMP_Text completedExerciseNameText;
    [Header("Other")]
    [SerializeField] private TMP_Text cooldownCountdownText;
    [SerializeField] private TMP_Text nextExerciseNameText;
    [SerializeField] private TMP_Text complimentText;
    [Header("Audio")]
    [SerializeField] private AudioClip beepSound;
    [SerializeField] private AudioClip finalBeepSound;

    List<string> compliments = new List<string>()
    {
        "You're doing great!",
        "Keep it up!",
        "You're doing amazing!",
        "You're killing it!",
        "You're a rockstar!",
        "Good Job!",
        "You're doing awesome!",
        "You're doing fantastic!",
        "You're doing incredible!",
    };
    float duration;
    Sequence timerSequence;
    void OnEnable()
    {
        ConfigureUI();
        DOVirtual.DelayedCall(0.3f, () =>
        {
            StartCooldownTimer();
        });
    }
    void OnDisable()
    {
        timerSequence.Kill();
    }
    void ConfigureUI()
    {
        completedExerciseNameText.text = AppConstants.CurrentSession.GetCurrentExercise().name;
        AppConstants.CurrentSession.currentExerciseIndex++;

        if (AppConstants.CurrentSession.GetCurrentExercise().measurementType == ExerciseMeasurementType.Time)
        {
            nextExerciseNameText.text = AppConstants.CurrentSession.GetCurrentExercise().name + AppConstants.CurrentSession.GetCurrentExercise().durationSeconds + " s";
        }
        else
        {
            nextExerciseNameText.text = AppConstants.CurrentSession.GetCurrentExercise().name + " x " + AppConstants.CurrentSession.GetCurrentExercise().reps;
        }


        SessionSegmentTypes segmentTypes = AppConstants.CurrentSession.HasNewSegmentStarted();
        if (segmentTypes == SessionSegmentTypes.WarmUp)
        {
            segmentNotCompletedContainer.gameObject.SetActive(false);
            warmUpCompletedText.gameObject.SetActive(true);
            mainWorkoutCompletedText.gameObject.SetActive(false);
            segmentCompletedContainer.gameObject.SetActive(true);
            duration = AppConstants.segmentBreakTime;
        }
        else if (segmentTypes == SessionSegmentTypes.MainWorkout)
        {
            segmentNotCompletedContainer.gameObject.SetActive(false);
            warmUpCompletedText.gameObject.SetActive(false);
            mainWorkoutCompletedText.gameObject.SetActive(true);
            segmentCompletedContainer.gameObject.SetActive(true);
            duration = AppConstants.segmentBreakTime;
        }
        else
        {
            segmentNotCompletedContainer.gameObject.SetActive(true);
            segmentCompletedContainer.gameObject.SetActive(false);
            if (AppConstants.CurrentSession.GetCurrentExercise().restAfterExercise == 0)
            {
                duration = AppConstants.exerciseBreakTime;
            }
            else
            {
                duration = AppConstants.CurrentSession.GetCurrentExercise().restAfterExercise;
            }
        }
        GiveCompliment();
    }
    public void GiveCompliment()
    {
        complimentText.text = compliments[UnityEngine.Random.Range(0, compliments.Count)];
        complimentText.transform.localScale = Vector3.zero;
        complimentText.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                complimentText.transform.GetComponent<CanvasGroup>().DOFade(0, 0.3f).SetDelay(5.5f);
            });
    }
    void StartCooldownTimer()
    {
        // Set initial text
        int minutes = Mathf.FloorToInt(duration / 60);
        int seconds = Mathf.FloorToInt(duration % 60);
        cooldownCountdownText.text = $"{minutes:00}:{seconds:00}";

        timerSequence = DOTween.Sequence();
        float remainingTime = duration;

        // Create the countdown sequence
        float stepDuration = 1.0f; // One second per step

        for (int i = 0; i < duration; i++)
        {
            int currentIndex = i;

            // Play beep sound when 5 seconds or less remain
            timerSequence.AppendCallback(() =>
            {
                remainingTime = duration - currentIndex;
                if (remainingTime <= 5 && remainingTime > 0)
                {
                    AudioSource.PlayClipAtPoint(beepSound, Camera.main.transform.position);
                }

                // Update the displayed time
                int mins = Mathf.FloorToInt(remainingTime / 60);
                int secs = Mathf.FloorToInt(remainingTime % 60);
                cooldownCountdownText.text = $"{mins:00}:{secs:00}";

                // Animate the text
                cooldownCountdownText.transform.DOScale(1.1f, 0.3f).SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        cooldownCountdownText.transform.DOScale(1f, 0.2f).SetEase(Ease.OutQuad);
                    });
            });

            timerSequence.AppendInterval(stepDuration);
        }

        // Final step - play the final beep and transition to next exercise
        timerSequence.AppendCallback(() =>
        {
            // Play the final sound
            if (finalBeepSound != null)
            {
                AudioSource.PlayClipAtPoint(finalBeepSound, Camera.main.transform.position);
            }
            else if (beepSound != null)
            {
                AudioSource.PlayClipAtPoint(beepSound, Camera.main.transform.position);
            }

            cooldownCountdownText.text = "00:00";
            cooldownCountdownText.transform.DOScale(1.3f, 0.5f).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    cooldownCountdownText.transform.DOScale(1f, 0.3f);
                });
        });

        timerSequence.AppendInterval(0.8f);

        // Set the complete action
        timerSequence.OnComplete(() =>
        {
            ExerciseScreenManager.Instance.StartNextExercise();
        });

        // Start the sequence
        timerSequence.Play();
    }
}