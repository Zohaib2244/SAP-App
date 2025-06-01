using Scripts.Resources;
using Scripts.UI;
using Scripts.UI.Screens;
using UnityEngine;

public class SigninScreen_3 : MonoBehaviour {
    [SerializeField] private AdvancedDropdown goalDropdown;
    private GoalType goal;
    [SerializeField] private AdvancedDropdown activityDropdown;
    private ActivityLevel activityLevel;
    [SerializeField] private AdvancedDropdown difficultyDropdown;
    private Difficulty difficulty;
    
    void Start()
    {
        string[] goalNames = System.Enum.GetNames(typeof(GoalType));
        goalDropdown.AddOptions(goalNames);
        goalDropdown.onChangedValue += OnGoalValueChanged;
        
        string[] activityNames = System.Enum.GetNames(typeof(ActivityLevel));
        activityDropdown.AddOptions(activityNames);
        activityDropdown.onChangedValue += OnActivityValueChanged;
        
        string[] difficultyNames = System.Enum.GetNames(typeof(Difficulty));
        difficultyDropdown.AddOptions(difficultyNames);
        difficultyDropdown.onChangedValue += OnDifficultyValueChanged;
    }
    public void OnNextButtonClicked()
    {
        UserSignInScreen.Instance.newUserData.AddSignIn3Data(goal, activityLevel, difficulty);
        UserSignInScreen.Instance.SignInComplete();
    }
    private void OnGoalValueChanged(int index)
    {
        goal = (GoalType)index;
    }
    private void OnActivityValueChanged(int index)
    {
        activityLevel = (ActivityLevel)index;
    }
    private void OnDifficultyValueChanged(int index)
    {
        difficulty = (Difficulty)index;
    }
    void ResetEverything()
    {
        goalDropdown.SetDefaultText();
        activityDropdown.SetDefaultText();
        difficultyDropdown.SetDefaultText();
    }
     void OnEnable()
    {
        ResetEverything();
    }
}