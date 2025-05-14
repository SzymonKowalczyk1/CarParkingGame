using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    [Header("Panels")]
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    

    [Header("Buttons")]
    public Button restartButton;
    public Button menuButton;
    public Button nextLevelButton;

    [Header("References")]
    public CoinRatingSystem coinRatingSystem; // Referencja do CoinRatingSystem
    public Car carController;
    public DrawWithMouse drawController;

    private bool isGameOver = false; //Flaga konca gry
    
    private void Start()
    {
        //Ukrywa panel porazki na poczatku gry
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
            
        //Ukrywa panel ukonczenia poziomu na poczatku
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);

        //Przypisanie listenera do przycisku restartu
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartLevel);
        }
        
        //Przypisanie listenera do przycisku menu glownego
        if (menuButton != null)
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(ReturnToMainMenu);
        }
        
        //Przypisanie listenera do przycisku nastepnego poziomu
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.AddListener(LoadNextLevel);
        }
        
        // Znajdz CoinRatingSystem jesli nie jest przypisany
        if (coinRatingSystem == null)
        {
            coinRatingSystem = FindObjectOfType<CoinRatingSystem>();
        }
    }

    //Wyswietla panel porazki po kolizji
    public void ShowGameOverPanel()
    {
        if (isGameOver) return;

        isGameOver = true;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); //Pokaz panel porazki
            
  
            if (restartButton != null)
            {
                restartButton.onClick.RemoveAllListeners();
                restartButton.onClick.AddListener(RestartLevel);
            }
            
            if (menuButton != null)
            {
                menuButton.onClick.RemoveAllListeners();
                menuButton.onClick.AddListener(ReturnToMainMenu);
            }
        }

        //Zatrzymuje samochod
        if (carController != null)
        {
            carController.StopCar();
        }

        //Wylacza rysowanie trasy
        if (drawController != null)
        {
            drawController.enabled = false;
        }
    }
    
    //Wyswietla panel ukonczenia poziomu
    public void ShowLevelCompletePanel()
    {
        if (isGameOver) return;
        
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }
        
        //Wylacza kontroler samochodu
        if (carController != null)
        {
            carController.enabled = false;
        }
        
        //Wylacza rysowanie trasy
        if (drawController != null)
        {
            drawController.enabled = false;
        }
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Restartuje scene
    }
    
    //Funkcja powrotu do menu glownego
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    //Funkcja ladowania nastepnego poziomu
    public void LoadNextLevel()
    {
        //korzystamy z funkcji ktora jest juz w CoinRatingSystem
        if (coinRatingSystem != null && !string.IsNullOrEmpty(coinRatingSystem.nextLevelName))
        {
            SceneManager.LoadScene(coinRatingSystem.nextLevelName);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
