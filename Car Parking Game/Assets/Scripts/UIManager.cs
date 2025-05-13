using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject startGamePanel;

    [Header("Buttons")]
    public Button startButton;
    public Button quitButton;
    public Button tutorialButton;
    public Button skipTutorialButton; // Optional button to skip tutorial

    private TutorialManager tutorialManager;

    void Start()
    {
        // Ensure the start game panel is visible
        if (startGamePanel != null)
            startGamePanel.SetActive(true);

        // Setup button listeners
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        if (tutorialButton != null)
            tutorialButton.onClick.AddListener(StartTutorial);
            
        // Find tutorial manager if we're in the tutorial scene
        tutorialManager = FindObjectOfType<TutorialManager>();
        
        // Setup skip tutorial button if available
        if (skipTutorialButton != null && tutorialManager != null)
        {
            skipTutorialButton.onClick.AddListener(SkipTutorial);
        }
    }

    // Start the first level
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    
    // Start the tutorial level
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    // Skip the tutorial
    public void SkipTutorial()
    {
        if (tutorialManager != null)
        {
            tutorialManager.SkipTutorial();
        }
    }

    // Quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 