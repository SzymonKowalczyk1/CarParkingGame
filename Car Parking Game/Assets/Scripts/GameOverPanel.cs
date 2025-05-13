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
        // Set up restart button
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartLevel);
        }
        else
        {
            Debug.LogError("Restart button reference is missing in GameOverPanel!");
        }
        
        // Set up menu button
        if (menuButton != null)
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }
    
    public void RestartLevel()
    {
        Debug.Log("GameOverPanel: RestartLevel called");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ReturnToMainMenu()
    {
        Debug.Log("GameOverPanel: ReturnToMainMenu called");
        SceneManager.LoadScene("MainMenu");
    }
} 