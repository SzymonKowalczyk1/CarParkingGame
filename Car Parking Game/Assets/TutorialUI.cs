using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject highlightCircle;
    public GameObject tutorialArrow;
    public Button skipButton;
    public TutorialManager tutorialManager;
    
    void Start()
    {
        if (skipButton != null && tutorialManager != null)
        {
            skipButton.onClick.AddListener(tutorialManager.SkipTutorial);
        }
        
        // Start animations automatically
        if (highlightCircle != null)
        {
            AnimateHighlight();
        }
        
        if (tutorialArrow != null)
        {
            AnimateArrow();
        }
    }
    
    // Helper method to animate the highlight circle
    public void AnimateHighlight()
    {
        if (highlightCircle != null)
        {
            // Simple scale animation
            StartCoroutine(PulseAnimation(highlightCircle.GetComponent<RectTransform>()));
        }
    }
    
    // Helper method to animate the arrow
    public void AnimateArrow()
    {
        if (tutorialArrow != null)
        {
            // Simple bounce animation
            StartCoroutine(BounceAnimation(tutorialArrow.GetComponent<RectTransform>()));
        }
    }
    
    private IEnumerator PulseAnimation(RectTransform target)
    {
        if (target == null) yield break;
        
        Vector3 originalScale = target.localScale;
        Vector3 targetScale = originalScale * 1.2f;
        
        while (true)
        {
            // Scale up
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                target.localScale = Vector3.Lerp(originalScale, targetScale, Mathf.SmoothStep(0, 1, t));
                yield return null;
            }
            
            // Scale down
            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                target.localScale = Vector3.Lerp(targetScale, originalScale, Mathf.SmoothStep(0, 1, t));
                yield return null;
            }
        }
    }
    
    private IEnumerator BounceAnimation(RectTransform target)
    {
        if (target == null) yield break;
        
        Vector2 originalPosition = target.anchoredPosition;
        Vector2 targetPosition = originalPosition + new Vector2(0, 10);
        
        while (true)
        {
            // Move up
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * 2;
                target.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, Mathf.SmoothStep(0, 1, t));
                yield return null;
            }
            
            // Move down
            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * 2;
                target.anchoredPosition = Vector2.Lerp(targetPosition, originalPosition, Mathf.SmoothStep(0, 1, t));
                yield return null;
            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }
} 