using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject gameOverPanel; // Panel Game Over
    public GameObject levelCompletePanel; // Panel for level completion
    

    [Header("Buttons")]
    public Button restartButton;     // Przycisk restartu
    public Button mainMenuButton;    // Button to return to main menu
    public Button menuButton;
    public Button nextLevelButton;   // Button to go to next level

    [Header("Level Settings")]
    public string nextLevelName = "Level2"; // Name of the next level scene

    public Car carController;
    public DrawWithMouse drawController;

    private bool isGameOver = false;
    private void Start()
    {
        // Ukryj GameOverPanel na pocztku gry
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
            
        // Hide level complete panel at start
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);

        // Jeli masz przycisk restartu, przypisz go
        if (restartButton != null)
        {
            // Remove any existing listeners first to avoid duplicates
            restartButton.onClick.RemoveAllListeners();
            // Add our listener
            restartButton.onClick.AddListener(RestartLevel);
            Debug.Log("Restart button listener added");
        }
        else
        {
            Debug.LogWarning("Restart button reference is missing!");
        }
        
        // Assign main menu button listener
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }

        if (menuButton != null)
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(ReturnToMainMenu);
        }
        
        // Assign next level button listener
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.AddListener(LoadNextLevel);
        }
    }

    // Ta metoda bdzie wywoywana po kolizji z "Wall"
    public void ShowGameOverPanel()
    {
        if (isGameOver) return;

        isGameOver = true;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Poka panel Game Over
            
            // Re-add button listeners just to be safe
            if (restartButton != null)
            {
                restartButton.onClick.RemoveAllListeners();
                restartButton.onClick.AddListener(RestartLevel);
                Debug.Log("Restart button listener re-added in game over panel");
            }
            
            if (menuButton != null)
            {
                menuButton.onClick.RemoveAllListeners();
                menuButton.onClick.AddListener(ReturnToMainMenu);
            }
        }

        if (carController != null)
        {
            carController.StopCar();
        }

        if (drawController != null)
        {
            drawController.enabled = false;
        }
    }
    
    // Show level complete panel
    public void ShowLevelCompletePanel()
    {
        if (isGameOver) return;
        
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }
        
        if (carController != null)
        {
            carController.enabled = false;
        }
        
        if (drawController != null)
        {
            drawController.enabled = false;
        }
    }

    // Funkcja do restartu poziomu
    public void RestartLevel()
    {
        Debug.Log("RestartLevel method called");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restartuj scen
    }
    
    // Function to return to the main menu
    public void ReturnToMainMenu()
    {
        Debug.Log("ReturnToMainMenu method called");
        SceneManager.LoadScene("MainMenu");
    }
    
    // Function to load the next level
    public void LoadNextLevel()
    {
        Debug.Log("LoadNextLevel method called");
        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        // If we're in Level1, load Level2
        if (currentSceneName == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        // If we're in Level2, load Level3
        else if (currentSceneName == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
        // For any other level or if next level doesn't exist, use the nextLevelName variable
        else if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        // Fallback to main menu if no next level is defined
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
