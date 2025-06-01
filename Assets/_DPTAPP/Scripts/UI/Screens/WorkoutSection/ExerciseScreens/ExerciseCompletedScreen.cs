using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Scripts.UI;
public class ExerciseCompletedScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text workoutNameText;
    [SerializeField] private TMP_Text xpEarnedText;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text levelUpText;  
    
    [Header("Animation Settings")]
    [SerializeField] private float initialDelay = 0.5f;
    [SerializeField] private float xpAnimationDuration = 1.5f;
    [SerializeField] private float levelUpAnimationDuration = 1.0f;
    [SerializeField] private Ease xpAnimationEase = Ease.OutQuad;
    [SerializeField] private Ease levelUpTextEase = Ease.OutBack;
    
    private UserData userData;
    private float earnedXP;
    
    void OnEnable()
    {
        levelUpText.gameObject.SetActive(false);
        AppConstants.CurrentSession.isSessionCompleted = true;
        AppConstants.completedWorkouts++;
        workoutNameText.text = AppConstants.CurrentSession.name + " (" + AppConstants.completedWorkouts + "/" + AppConstants.totalWorkouts + ")";
        earnedXP = AppConstants.CurrentSession.sessionPoints;
        xpEarnedText.text = "+" + earnedXP + " xp";
        
        userData = AppConstants.GetCurrentUser();
        
        // Setup initial state
        UpdateLevelText(userData.userLevel);
        SetupXPSlider();
        
        // Start animation sequence after a short delay
        Invoke("UpdateSlider", initialDelay);
    }
    
    void SetupXPSlider()
    {
        float currentXP = userData.levelProgress;
        float xpForLevel = AppConstants.LevelProgress[userData.userLevel + 1];
        
        xpSlider.maxValue = xpForLevel;
        xpSlider.value = currentXP;
    }
    
    void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level;
        levelText.transform.localScale = Vector3.zero;
        levelText.transform.DOScale(1f, 0.5f).SetEase(levelUpTextEase);
    }
    
    void UpdateSlider()
    {
        // Store current values
        int currentLevel = userData.userLevel;
        float currentXP = userData.levelProgress;
        float xpForLevel = AppConstants.LevelProgress[currentLevel + 1];
        
        // Calculate if level up will occur
        bool willLevelUp = (currentXP + earnedXP) >= xpForLevel;
        
        Sequence xpSequence = DOTween.Sequence();
        
        if (!willLevelUp)
        {
            // Simple case: just animate XP bar
            xpSequence.Append(DOTween.To(() => xpSlider.value, x => xpSlider.value = x, currentXP + earnedXP, xpAnimationDuration)
                .SetEase(xpAnimationEase));
                
            xpSequence.OnComplete(() => {
                userData.AddXP(earnedXP);
            });
        }
        else
        {
            // Level up case
            float xpToNextLevel = xpForLevel - currentXP;
            float excessXP = earnedXP - xpToNextLevel;
            
            // First phase: fill to max
            xpSequence.Append(DOTween.To(() => xpSlider.value, x => xpSlider.value = x, xpForLevel, xpAnimationDuration * (xpToNextLevel/earnedXP))
                .SetEase(xpAnimationEase));
                
            // Show level up animation
            xpSequence.AppendCallback(() => {
                // Show level up text with animation
                levelUpText.gameObject.SetActive(true);
                levelUpText.transform.localScale = Vector3.zero;
                levelUpText.transform.DOScale(1f, 0.5f).SetEase(levelUpTextEase);
                
                // Update user data partially to increase level
                userData.AddXP(xpToNextLevel);
                
                // Update level text
                UpdateLevelText(userData.userLevel);
            });
            
            // Wait for level up celebration
            xpSequence.AppendInterval(levelUpAnimationDuration);
            
            // Hide level up text
            xpSequence.AppendCallback(() => {
                levelUpText.transform.DOScale(0f, 0.3f).OnComplete(() => {
                    levelUpText.gameObject.SetActive(false);
                });
            });
            
            // Reset slider for new level
            xpSequence.AppendCallback(() => {
                // Update slider max value for new level
                xpSlider.maxValue = AppConstants.LevelProgress[userData.userLevel + 1];
                xpSlider.value = 0;
                
                // Animate the excess XP into the new level
                if (excessXP > 0)
                {
                    DOTween.To(() => xpSlider.value, x => xpSlider.value = x, excessXP, xpAnimationDuration * (excessXP/earnedXP))
                        .SetEase(xpAnimationEase)
                        .OnComplete(() => {
                            userData.AddXP(excessXP);
                        });
                }
            });
        }
    }
    public void ContinueButtonClick()
    {
        WorkoutScreensManager.Instance.ShowWorkoutDetailScreen();
    }
}