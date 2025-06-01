using Scripts.Resources;
using UnityEngine;

public class ExerciseItem : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI exerciseNameText;
    [SerializeField] private TMPro.TextMeshProUGUI exerciseMtericsText;
    private Exercise exerciseData;
    public void SetData(Exercise exercise)
    {
        exerciseData = exercise;
        ConfigureUI();
    }
    void ConfigureUI()
    {
        exerciseNameText.text = exerciseData.name;
        if(exerciseData.measurementType == ExerciseMeasurementType.Reps)
        {
            exerciseMtericsText.text = "x" + exerciseData.reps;
        }
        else if(exerciseData.measurementType == ExerciseMeasurementType.Time)
        {
            exerciseMtericsText.text = exerciseData.durationSeconds + "s";
        }
    }
}