namespace Scripts.Resources
{
    public enum ScreenTypes
    {
        SignIn,
        MainMenu,
        WikiMain,
        Workouts,
        WorkoutPlan,
        SessionDetail,
        SettingsScreen,
    }
    public enum BodyTypes
    {
        None,
        Thin,
        Normal,
        Fat
    }
    public enum SessionSegmentTypes
    {
        WarmUp,
        MainWorkout,
        CoolDown,
        None
    }

    // Add this enum for exercise measurement types
    public enum ExerciseMeasurementType
    {
        Time,
        Reps,
    }
    public enum ActivityLevel
    {
        Sedentary,
        LightlyActive,
        ModeratelyActive,
        VeryActive,
        ExtraActive
    }
    public enum GoalType
    {
        LoseWeight,
        MaintainWeight,
        GainWeight
    }
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public enum Gender
    {
        Male,
        Female
    }
    public enum ExerciseCompletionType
    {
        SegmentCompleted,
        ExerciseCompleted
    }
}