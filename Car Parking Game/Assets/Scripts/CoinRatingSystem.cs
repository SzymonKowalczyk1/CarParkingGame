using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinRatingSystem : MonoBehaviour
{
    [Header("References")]
    public CoinManager coinManager;
    public GameObject resultPanel;
    public TMP_Text resultText;
    public TMP_Text messageText;
    public GameManager gameManager;

    [Header("Coin Icons")]
    public Image[] coinIcons; //Tablica ikon monet do pokazania ilosci monet

    [Header("Navigation")]
    public Button restartButton; //Przycisk restartu
    public Button nextLevelButton; //Przycisk nastepnego poziomu

    [Header("Settings")]
    public int totalCoins = 3; //Calkowita liczba monet do zebrania
    public string nextLevelName; //Nazwa nastepnego poziomu

    private bool ratingShown = false; //Flaga pokazania oceny

    
    void Start()
    {
        //sprawdza czy panel wynikow jest ukryty na poczatku
        if (resultPanel != null)
            resultPanel.SetActive(false);


        //Konfiguracja przyciskow
        if (nextLevelButton != null)
        {
             if (!string.IsNullOrEmpty(nextLevelName))
             
            {
                nextLevelButton.onClick.AddListener(LoadNextLevel);

            }
            
            
        }

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);
    }

    //Pokazuje ocene na podstawie zebranych monet
    public void ShowRating()
    {
        if (ratingShown) return;
        ratingShown = true;

        int collectedCoins = coinManager.coinCount;
       
        //Pokaz panel wynikow
        resultPanel.SetActive(true);

        //Zaktualizuj tekst wyniku
        if (resultText != null)
            resultText.text = "Coins Collected: " + collectedCoins + "/" + totalCoins;

        //Zaktualizuj wiadomosc na podstawie zebranych monet
        if (messageText != null)
        {
            switch (collectedCoins)
            {
                case 0:
                    messageText.text = "Try collecting coins next time!";
                    break;
                case 1:
                    messageText.text = "Good job!";
                    break;
                case 2:
                    messageText.text = "Great job!";
                    break;
                case 3:
                    messageText.text = "Perfect parking!";
                    break;
                default:
                    messageText.text = "Level completed!";
                    break;
            }
        }

        //Aktualizuje ikony monet
        for (int i = 0; i < coinIcons.Length; i++)
        {
            if (coinIcons[i] != null)
            {
                //Pokazuje/ukrywa na podstawie zebranych monet
                if (i < collectedCoins)
                {
                    coinIcons[i].gameObject.SetActive(true);
                    coinIcons[i].color = Color.white;
                }
                else
                {
                    coinIcons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    //Restartuje aktualny poziom
    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    //Laduje nastepny poziom
    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        }
    }
}