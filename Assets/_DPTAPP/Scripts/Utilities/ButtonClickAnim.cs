using UnityEngine;
using UnityEngine.Events;
using DG.Tweening; // Import DoTween namespace
using UnityEngine.UI;
using System.Collections.Generic;
using Lofelt.NiceVibrations;
using Scripts.UI;
public class ButtonClickAnim : MonoBehaviour
{
    public UnityEvent actions;
    public float animationDuration = 0.1f;
    public float scaleFactor = 1.05f;
    private Button button;
    private AudioClip clickSound;
    void Start()
    {
        // Automatically detect the button component
        button = GetComponent<Button>();

        if (button != null)
        {
            // Add the ButtonClick method as a listener to the button's onClick event
            button.onClick.AddListener(ButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
        clickSound = Resources.Load<AudioClip>("Sounds/click");
    }
    public void ButtonClick()
    {
        AnimateButtonClick();
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        Debug.Log("Button Clicked Once");
      
    }

    private void AnimateButtonClick()
    {
        button.interactable = false;
        // Store the original scale
        Vector3 originalScale = transform.localScale;
        ScreenManager.Instance.graphicRaycaster.enabled = false;
        // Scale up and then scale down
        transform.DOScale(originalScale * scaleFactor, animationDuration)
            .SetUpdate(true).OnComplete(() => 
            {
                transform.DOScale(originalScale, animationDuration)
                    .SetUpdate(true).OnComplete(() => 
                    {
                        button.interactable = true;
                        actions?.Invoke();
                        ScreenManager.Instance.graphicRaycaster.enabled = true;
                      
                    });
            });
    }
}