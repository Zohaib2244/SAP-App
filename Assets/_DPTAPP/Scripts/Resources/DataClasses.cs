using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Scripts.Resources
{
    [Serializable]
    public class UnitWorkout
    {
        public string title;
        public string goal;
        public List<string> equipment;
        public BodyTypes excludedCategory;
        public List<Session> sessions;
        public Session GetCurrentSession()
        {
            foreach (var session in sessions)
            {
                if (!session.isSessionCompleted)
                {
                    return session;
                }
            }
            return null;
        }
    }
    [Serializable]
    public class Session
    {
        public string name;
        public int sessionPoints;
        private bool _isSessionCompleted = false;
        public int currentExerciseIndex = 0;
        public bool isReadyToBeShown;

        public int totalExercises
        {
            get
            {
                return warmupSegment.exercises.Count + mainSegment.exercises.Count + cooldownSegment.exercises.Count;
            }
        }
        public SessionSegment warmupSegment;
        public SessionSegment mainSegment;
        public SessionSegment cooldownSegment;
        public SessionSegmentTypes HasNewSegmentStarted()
        {
            int warmupCount = warmupSegment.exercises.Count;
            int mainCount = mainSegment.exercises.Count;
            
            // Check transition from warmup to main
            if (currentExerciseIndex == warmupCount)
            {
            return SessionSegmentTypes.WarmUp;
            }
            
            // Check transition from main to cooldown
            if (currentExerciseIndex == warmupCount + mainCount)
            {
                return SessionSegmentTypes.MainWorkout;
            }

            return SessionSegmentTypes.None;
        }
        public Exercise GetCurrentExercise()
        {
            if (currentExerciseIndex < warmupSegment.exercises.Count)
            {
                return warmupSegment.exercises[currentExerciseIndex];
            }
            else if (currentExerciseIndex < warmupSegment.exercises.Count + mainSegment.exercises.Count)
            {
                return mainSegment.exercises[currentExerciseIndex - warmupSegment.exercises.Count];
            }
            else
            {
                return cooldownSegment.exercises[currentExerciseIndex - warmupSegment.exercises.Count - mainSegment.exercises.Count];
            }
        }
        public bool isSessionCompleted
        {
            get
            {
                return _isSessionCompleted = PlayerPrefs.GetInt(name, 0) == 1;
            }
            set
            {
                PlayerPrefs.SetInt(name, value ? 1 : 0);
                _isSessionCompleted = value;
            }
        }
    }

    [Serializable]
    public class SessionSegment
    {
        public int totalDuration; // Total duration in minutes
        public List<Exercise> exercises;
    }
    [Serializable]
    public class Exercise
    {
        public string name;
        // Added properties
        public ExerciseMeasurementType measurementType;
        public VideoClip tutorialVideo; 
        // For rep-based exercises
        public int reps;
        
        // For time-based exercises
        public int durationSeconds;
        
        // Rest information
        public int restAfterExercise;
        
        // Step-by-step instructions
        public List<string> instructions;
    }
    [Serializable]
    public class UIScreen
    {
        public ScreenTypes screenType;
        public Transform screenTransform;
    }
}