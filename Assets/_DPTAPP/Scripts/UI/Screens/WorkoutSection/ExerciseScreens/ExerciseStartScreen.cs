using TMPro;
using UnityEngine;
using DG.Tweening;
public class ExerciseStartScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text sessionNameText;
    [SerializeField] private TMP_Text startCountdownText;
    [SerializeField] private AudioClip beepSound;
    [SerializeField] private AudioClip goSound;
    void OnEnable()
    {
        sessionNameText.text = AppConstants.CurrentSession.name;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        startCountdownText.text = "5";

        AudioSource.PlayClipAtPoint(beepSound, Camera.main.transform.position);
        startCountdownText.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            startCountdownText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                AudioSource.PlayClipAtPoint(beepSound, Camera.main.transform.position);
                startCountdownText.text = "4";
                startCountdownText.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    startCountdownText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        AudioSource.PlayClipAtPoint(beepSound, Camera.main.transform.position);
                        startCountdownText.text = "3";
                        startCountdownText.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                        {
                            startCountdownText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                            {
                                AudioSource.PlayClipAtPoint(beepSound, Camera.main.transform.position);
                                startCountdownText.text = "2";
                                startCountdownText.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                                {
                                    startCountdownText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                                    {
                                        AudioSource.PlayClipAtPoint(beepSound, Camera.main.transform.position);
                                        startCountdownText.text = "1";
                                        startCountdownText.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                                        {
                                            startCountdownText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                                            {
                                                AudioSource.PlayClipAtPoint(goSound, Camera.main.transform.position);
                                                startCountdownText.text = "GO!";
                                                startCountdownText.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                                                {
                                                    startCountdownText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                                                    {
                                                        ExerciseScreenManager.Instance.StartNextExercise();     
                                                    });
                                                });
                                            });
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });

    }   
}