using System.Collections.Generic;
using Scripts.Resources;
using UnityEngine;

public static class AppConstants
{
    private static UserData _currentUser = null;
    private static bool firstSession = true;
    public static int exerciseBreakTime = 30;
    public static int segmentBreakTime = 60;
    public static int totalWorkouts;
    public static int completedWorkouts;
    public static UnitWorkout CurrentUnitWorkout;
    public static Session CurrentSession;
    public static Dictionary<int, float> LevelProgress = new Dictionary<int, float>
    {
        {1, 0.0f},
        {2, 100.0f},
        {3, 200.0f},
        {4, 300.0f},
        {5, 400.0f},
        {6, 500.0f},
        {7, 600.0f},
        {8, 700.0f},
        {9, 800.0f},
        {10, 900.0f}
    };
    public static void CreateNewUser(UserData userData)
    {
        _currentUser = userData;
        _currentUser.SaveUserData();
    }

    public static UserData GetCurrentUser()
    {
        if (_currentUser == null)
        {
            _currentUser = new UserData();
            _currentUser.LoadUserData();
        }
        return _currentUser;
    }
}