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
        //Konfiguracja przyciskow
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
        
        //Ukrywa panel na poczatku
        gameObject.SetActive(false);
    }
} 