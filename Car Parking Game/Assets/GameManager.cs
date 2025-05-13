using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject gameOverPanel; // Panel Game Over
    

    [Header("Buttons")]
    public Button restartButton;     // Przycisk restartu
    public Button mainMenuButton;    // Button to return to main menu
    public Button menuButton;

    public Car carController;
    public DrawWithMouse drawController;

    private bool isGameOver = false;
    private void Start()
    {
        // Ukryj GameOverPanel na pocztku gry
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Jeli masz przycisk restartu, przypisz go
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartLevel); // Przypisanie funkcji do przycisku restartu
        }
        
        // Assign main menu button listener
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }

        if (menuButton != null)
        {
            menuButton.onClick.AddListener(ReturnToMainMenu);
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

    // Funkcja do restartu poziomu
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restartuj scen
    }
    
    // Function to return to the main menu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
