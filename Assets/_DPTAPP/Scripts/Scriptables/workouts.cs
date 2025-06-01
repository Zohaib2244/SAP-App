using UnityEngine;
using Scripts.Resources;
using System.Collections.Generic;
namespace Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "workouts", menuName = "Scriptable Objects/workouts")]
    public class workouts : ScriptableObject
    {
        [SerializeField] private List<UnitWorkout> workoutData = new List<UnitWorkout>();
        
        public List<UnitWorkout> WorkoutData
        {
            get { return workoutData; }
        }
        
        public void AddWorkout(UnitWorkout workout)
        {
            // Check if workout with same title already exists
            for (int i = 0; i < workoutData.Count; i++)
            {
                if (workoutData[i].title == workout.title)
                {
                    // Replace existing workout
                    workoutData[i] = workout;
                    return;
                }
            }
            
            // Add new workout
            workoutData.Add(workout);
        }
    }
}