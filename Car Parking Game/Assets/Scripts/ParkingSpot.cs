using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSpot : MonoBehaviour
{
    [SerializeField] private float parkingAngleTolerance = 15f;
    [SerializeField] private CoinRatingSystem coinRatingSystem;
    [SerializeField] private GameManager gameManager;

    private bool levelCompleted = false;

    private void Start()
    {
        // If gameManager is not assigned, try to find it in the scene
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Skip if level already completed
        if (levelCompleted) return;

        // Check if it's the player's car
        if (collision.CompareTag("Player"))
        {
            Car car = collision.GetComponent<Car>();
            if (car != null)
            {
                CheckParking(car);
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Skip if level already completed
        if (levelCompleted) return;

        // Check if it's the player's car
        if (collision.CompareTag("Player"))
        {
            Car car = collision.GetComponent<Car>();
            if (car != null)
            {
                CheckParking(car);
            }
        }
    }
    
    private void CheckParking(Car car)
    {
        // Check if car is still moving
        if (car.GetCurrentSpeed() > 0.1f)
            return;

        // Check if car is properly aligned with parking spot
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(car.transform.eulerAngles.z, transform.eulerAngles.z));
        if (angleDifference > parkingAngleTolerance)
            return;

        // Car is parked correctly!
        levelCompleted = true;
        Debug.Log("Success! Car parked correctly.");

        // Show coin rating if available
        if (coinRatingSystem != null)
        {
            coinRatingSystem.ShowRating();
        }
        else
        {
            // If no coin rating system, use the game manager to show level complete
            if (gameManager != null)
            {
                gameManager.ShowLevelCompletePanel();
            }
            else
            {
                Debug.LogWarning("Both CoinRatingSystem and GameManager references are missing on ParkingSpot!");
            }
        }

        // Make the parking spot change color to indicate success
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.green;
        }

        // Optional: Disable car controls to prevent further movement
        if (car.drawControll != null)
        {
            car.enabled = false;
        }
    }
}


