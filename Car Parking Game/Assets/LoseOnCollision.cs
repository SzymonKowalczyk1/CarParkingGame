using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOnCollision : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Car car;
    public DrawWithMouse drawControll;
    public GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log("Game Over");

            if (gameManager != null)
            {
                gameManager.ShowGameOverPanel();
            }
            else
            {
                Debug.LogError("GameManager reference not set in LoseOnCollision script!");
            }
        }
    }
}
