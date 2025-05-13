using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialParkingSpot : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public GameObject successIndicator;
    public GameObject directionArrow;
    
    [SerializeField] private float parkingAngleTolerance = 15f;
    
    private bool isOccupied = false;
    
    void Start()
    {
        if (successIndicator != null)
            successIndicator.SetActive(false);
    }
    
    // Public method to skip the tutorial
    public void SkipTutorial()
    {
        if (tutorialManager != null)
        {
            tutorialManager.SkipTutorial();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Skip if already occupied
        if (isOccupied) return;

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
        // Skip if already occupied
        if (isOccupied) return;

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
        isOccupied = true;
        
        // Show success indicator
        if (successIndicator != null)
            successIndicator.SetActive(true);
            
        // Hide direction arrow
        if (directionArrow != null)
            directionArrow.SetActive(false);
        
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
        
        // Notify tutorial manager of success
        if (tutorialManager != null)
        {
            // Check if tutorial is active or was skipped
            if (tutorialManager.IsTutorialActive())
            {
                tutorialManager.NextTutorialStep();
            }
            else
            {
                // Tutorial was skipped, directly show completion
                tutorialManager.ShowTutorialComplete();
            }
        }
    }
} 