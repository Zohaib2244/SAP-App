using Scripts.Resources;
using UnityEngine;
using UnityEngine.UI;

public class WorkoutPlanScreen : MonoBehaviour
{
    [SerializeField] private UnitWorkout workoutData;
    [SerializeField] private GameObject sessionItemPrefab;
    [SerializeField] private Transform sessionItemsParent;
    [SerializeField] private TMPro.TextMeshProUGUI titleText;
    [SerializeField] private TMPro.TextMeshProUGUI goalText;
    [SerializeField] private TMPro.TextMeshProUGUI equipmentText;
    private ScrollRect scrollRect;
    void OnEnable()
    {
        workoutData = AppConstants.CurrentUnitWorkout;
        ConfigureScreen();
        LoadSessionItems();
    }
    void ConfigureScreen()
    {
        titleText.text = workoutData.title;
        goalText.text = workoutData.goal;
        equipmentText.text = string.Join(", ", workoutData.equipment.ToArray());
    }
    void LoadSessionItems()
    {
        foreach (Transform child in sessionItemsParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var session in workoutData.sessions)
        {
            GameObject sessionItem = Instantiate(sessionItemPrefab, sessionItemsParent);
            sessionItem.GetComponent<SessionItem>().SetData(session);
        }
    }
}