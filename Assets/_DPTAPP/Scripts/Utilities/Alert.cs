using UnityEngine;
using TMPro;
using DG.Tweening;

public class Alert : MonoBehaviour {
    #region Singleton
    public static Alert Instance;
    #endregion
    [Header("UI References")]
    [SerializeField] private UnityEngine.UI.Image alertImage;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite errorSprite;
    [SerializeField] private TextMeshProUGUI alertText;
    
    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    
    private Vector2 originalPosition;
    private Vector2 offScreenPosition;
    private Sequence animationSequence;

    private void Awake()
    {
        // Store the original position and calculate off-screen position
        originalPosition = transform.localPosition;
        offScreenPosition = new Vector2(Screen.width, originalPosition.y);
            if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameObject.SetActive(false);
        transform.localPosition = offScreenPosition;
    }

    public void ShowAlert(string message, bool isSuccess)
    {
        // Kill any ongoing animations
        if(animationSequence != null && animationSequence.IsActive())
        {
            animationSequence.Kill();
        }
        
        // Set message and sprite
        alertText.text = message;
        alertImage.sprite = isSuccess ? successSprite : errorSprite;
        
        // Reset position and make it visible
        transform.localPosition = offScreenPosition;
        gameObject.SetActive(true);
        
        // Reset any previous fade
        var canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        
        // Create animation sequence
        animationSequence = DOTween.Sequence();
        
        // Slide in from right
        animationSequence.Append(transform.DOLocalMoveX(originalPosition.x, animationDuration).SetEase(Ease.OutBack));
        
        // Wait for display duration
        animationSequence.AppendInterval(displayDuration);
        
        // Fade out
        animationSequence.Append(canvasGroup.DOFade(0f, fadeOutDuration));
        
        // Hide the game object when complete
        animationSequence.OnComplete(() => gameObject.SetActive(false));
    }
}