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
    public Button skipTutorialButton;

    private TutorialManager tutorialManager;

    void Start()
    {
        //panel startowy widoczny
        if (startGamePanel != null)
            startGamePanel.SetActive(true);

        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        if (tutorialButton != null)
            tutorialButton.onClick.AddListener(StartTutorial);
            
    }

    //Rozpoczyna pierwszy poziom
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    
    //Rozpoczyna samouczek
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    //Wychodzi z gry
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 