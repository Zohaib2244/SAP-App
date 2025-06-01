using System;
using Scripts.Resources;
using UnityEngine;

public class UserData
{
    public string Username { get; set; }
    public int Age { get; set; }
    public float height { get; set; }
    public Gender gender { get; set; }
    public float weight { get; set; }
    public GoalType goalType { get; set; }
    public ActivityLevel activityLevel { get; set; }
    public Difficulty difficulty { get; set; }
    public int userLevel { get; set; }
    public float levelProgress { get; set; }
    public BodyTypes bodyType = new BodyTypes();

    public void AddSignIn1Data(string username, int age, Gender gender)
    {
        Username = username;
        Age = age;
        this.gender = gender;
    }
    public void AddSignIn2Data(float height, float weight, BodyTypes bodyType)
    {
        this.height = height;
        this.weight = weight;
        this.bodyType = bodyType;
    }
    public void AddSignIn3Data(GoalType goalType, ActivityLevel activityLevel, Difficulty difficulty)
    {
        this.goalType = goalType;
        this.activityLevel = activityLevel;
        this.difficulty = difficulty;
    }

    public bool AddXP(float xp)
    {
        levelProgress += xp;
        bool leveledUp = false;
        
        if (levelProgress >= AppConstants.LevelProgress[userLevel + 1])
        {
            float excess = levelProgress - AppConstants.LevelProgress[userLevel + 1];
            userLevel++; //* Level up Play Animation or Smth
            levelProgress = excess; // Keep excess XP for next level
            leveledUp = true;
        }
        
        PlayerPrefs.SetInt("UserLevel", userLevel);
        PlayerPrefs.SetFloat("LevelProgress", levelProgress);
        return leveledUp;
    }
    public void LoadUserData()
    {
        Username = PlayerPrefs.GetString("Username", "");
        Age = PlayerPrefs.GetInt("Age", 0);
        height = PlayerPrefs.GetFloat("Height", 0f);
        gender = (Gender)PlayerPrefs.GetInt("Gender", 0);
        weight = PlayerPrefs.GetFloat("Weight", 0f);
        goalType = (GoalType)PlayerPrefs.GetInt("GoalType", 0);
        activityLevel = (ActivityLevel)PlayerPrefs.GetInt("ActivityLevel", 0);
        difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty", 0);
        userLevel = PlayerPrefs.GetInt("UserLevel", 0);
        levelProgress = PlayerPrefs.GetFloat("LevelProgress", 0f);
        bodyType = (BodyTypes)PlayerPrefs.GetInt("BodyType", 0);
    }

    public void SaveUserData()
    {
        PlayerPrefs.SetString("Username", Username);
        PlayerPrefs.SetInt("Age", Age);
        PlayerPrefs.SetFloat("Height", height);
        PlayerPrefs.SetInt("Gender", (int)gender);
        PlayerPrefs.SetFloat("Weight", weight);
        PlayerPrefs.SetInt("GoalType", (int)goalType);
        PlayerPrefs.SetInt("ActivityLevel", (int)activityLevel);
        PlayerPrefs.SetInt("Difficulty", (int)difficulty);
        PlayerPrefs.SetInt("UserLevel", userLevel);
        PlayerPrefs.SetFloat("LevelProgress", levelProgress);
        PlayerPrefs.SetInt("BodyType", (int)bodyType);
    }

    internal void AddSignIn1Data(string text1, int v, string text2)
    {
        throw new NotImplementedException();
    }
}