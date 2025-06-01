using UnityEngine;
using TMPro;

public class SessionInfoScreen : MonoBehaviour
{
    [SerializeField] private GameObject exerciseItemPrefab;
    [SerializeField] private TextMeshProUGUI sessionNameText;
    [SerializeField] private TMP_Text warmUpDurationText;
    [SerializeField] private TMP_Text warmUpExerciseCountText;
    [SerializeField] private TMP_Text mainDurationText;
    [SerializeField] private TMP_Text mainExerciseCountText;
    [SerializeField] private TMP_Text coolDownDurationText;
    [SerializeField] private TMP_Text coolDownExerciseCountText;

    void OnEnable()
    {
        ConfigureUI();
    }
    void ConfigureUI()
    {
        sessionNameText.text = AppConstants.CurrentSession.name;
        warmUpDurationText.text = AppConstants.CurrentSession.warmupSegment.totalDuration + "s";
        warmUpExerciseCountText.text = "x " + AppConstants.CurrentSession.warmupSegment.exercises.Count.ToString();
       
        mainDurationText.text = AppConstants.CurrentSession.mainSegment.totalDuration + "s";
        mainExerciseCountText.text = "x " + AppConstants.CurrentSession.mainSegment.exercises.Count.ToString();

        coolDownDurationText.text = AppConstants.CurrentSession.cooldownSegment.totalDuration + "s";
        coolDownExerciseCountText.text = "x " + AppConstants.CurrentSession.cooldownSegment.exercises.Count.ToString();
    }
}