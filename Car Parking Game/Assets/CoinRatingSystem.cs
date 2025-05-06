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

    [Header("Coin Icons")]
    public Image[] coinIcons; // Array of 3 coin images to show as rating

    [Header("Navigation")]
    public Button restartButton;
    public Button nextLevelButton;

    [Header("Settings")]
    public int totalCoins = 3;
    public string nextLevelName;

    private bool ratingShown = false;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure result panel is hidden at start
        if (resultPanel != null)
            resultPanel.SetActive(false);

        // Setup buttons if they exist
        if (nextLevelButton != null && !string.IsNullOrEmpty(nextLevelName))
            nextLevelButton.onClick.AddListener(LoadNextLevel);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);
    }

    public void ShowRating()
    {
        if (ratingShown) return;
        ratingShown = true;

        int collectedCoins = coinManager.coinCount;
       
        // Show the result panel
        resultPanel.SetActive(true);

        // Update the result text
        if (resultText != null)
            resultText.text = "Coins Collected: " + collectedCoins + "/" + totalCoins;

        // Update the message based on coins collected
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


        // APPROACH 1: Use SetActive directly on each coin icon's GameObject
        for (int i = 0; i < coinIcons.Length; i++)
        {
            if (coinIcons[i] != null)
            {
                // Show/hide based on collected coins
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
            else
            {
                Debug.Log("Warning: Coin icon " + i + " is null!");
            }
        }

    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        }
    }
}