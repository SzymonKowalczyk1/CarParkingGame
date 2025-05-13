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