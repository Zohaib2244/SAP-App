using Scripts.Resources;
using UnityEngine;
using Scripts.UI;

public class WorkoutItem : MonoBehaviour
{
    private UnitWorkout workoutData;
    [SerializeField] private TMPro.TextMeshProUGUI titleText;
    public void SetData(UnitWorkout workout)
    {
        workoutData = workout;
        titleText.text = workout.title;
    }
    public void OnClick()
    {
        AppConstants.CurrentUnitWorkout = workoutData;
        WorkoutScreensManager.Instance.ShowWorkoutDetailScreen();
    }
}