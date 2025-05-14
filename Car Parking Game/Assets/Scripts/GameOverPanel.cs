using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public Button restartButton;
    public Button menuButton;
    
    void Start()
    {
        //Konfiguracja przycisku restartu
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartLevel);
        }
        
        //Konfiguracja przycisku menu
        if (menuButton != null)
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }
    
    //Restartuje aktualny poziom
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    //Powraca do menu glownego
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
} 