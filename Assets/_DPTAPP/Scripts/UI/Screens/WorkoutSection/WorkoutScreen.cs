using UnityEngine;
using Scripts.Scriptables;
using Scripts.UI;
using Scripts.Resources;
public class WorkoutScreen : MonoBehaviour
{
    [SerializeField] private workouts workoutData;
    [SerializeField] private GameObject workoutItemPrefab;
    [SerializeField] private Transform workoutItemsParent;
    void Start()
    {
        AppConstants.totalWorkouts = workoutData.WorkoutData.Count;
    }
    private void OnEnable()
    {
        LoadWorkoutItems();
    }
    void LoadWorkoutItems()
    {
        // Clear existing items
        foreach (Transform child in workoutItemsParent)
        {
            Destroy(child.gameObject);
        }

        // Get the current user's body type
        BodyTypes userBodyType = AppConstants.GetCurrentUser().bodyType;

        // Load workouts that are not excluded for the current user's body type
        foreach (var workout in workoutData.WorkoutData)
        {
            if (workout.excludedCategory != BodyTypes.None)
            {
                // Skip this workout if it's specifically excluded for the user's body type
                if (workout.excludedCategory == userBodyType)
                {
                    // Skip this workout as it's excluded for the user's body type
                    continue;
                }
            }

            // Instantiate and set up workout item if not excluded
            GameObject workoutItem = Instantiate(workoutItemPrefab, workoutItemsParent);
            workoutItem.GetComponent<WorkoutItem>().SetData(workout);
        }
    }
    public void GoBack()
    {
        ScreenManager.Instance.ActivateScreen(Scripts.Resources.ScreenTypes.MainMenu);
    }
}