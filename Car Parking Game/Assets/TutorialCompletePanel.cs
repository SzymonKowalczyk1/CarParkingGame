using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialCompletePanel : MonoBehaviour
{
    public Button nextLevelButton;
    public Button menuButton;
    
    void Start()
    {
        // Make sure buttons have correct listeners
        if (nextLevelButton != null)
        {
            TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
            if (tutorialManager != null)
            {
                nextLevelButton.onClick.AddListener(tutorialManager.GoToNextLevel);
            }
        }
        
        if (menuButton != null)
        {
            TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
            if (tutorialManager != null)
            {
                menuButton.onClick.AddListener(tutorialManager.GoToMainMenu);
            }
        }
        
        // Hide panel at start
        gameObject.SetActive(false);
    }
} 