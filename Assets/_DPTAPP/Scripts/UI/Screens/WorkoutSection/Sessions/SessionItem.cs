using UnityEngine;
using UnityEngine.UI;
using Scripts.Resources;
using Scripts.UI;
public class SessionItem : MonoBehaviour
{
    private Session sessionData;
   [SerializeField] private Sprite sessionCompletedSprite;
    [SerializeField] private Sprite sessionNotCompletedSprite;
    [SerializeField]private Image sessionStatusImage;
    [SerializeField] private TMPro.TextMeshProUGUI sessionNameText;
    public void SetData(Session session)
    {
        sessionData = session;
        ConfigureUI();
    }
    void ConfigureUI()
    {
        sessionNameText.text = sessionData.name;
        sessionStatusImage.sprite = sessionData.isSessionCompleted ? sessionCompletedSprite : sessionNotCompletedSprite;
    }
    public void OnClick()
    {
        AppConstants.CurrentSession = sessionData;
        WorkoutScreensManager.Instance.ShowWorkoutPlanScreen();
    }
}