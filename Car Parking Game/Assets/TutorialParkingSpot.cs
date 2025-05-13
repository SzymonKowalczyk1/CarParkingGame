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
            
        if (directionArrow != null)
        {
            // Check if it's a UI element
            if (directionArrow.GetComponent<RectTransform>() != null)
            {
                // UI arrow - animation is handled by TutorialUI
            }
            else
            {
                // World space arrow
                StartCoroutine(PulseArrow());
            }
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
    
    private IEnumerator PulseArrow()
    {
        if (directionArrow == null) yield break;
        
        // This is for world-space objects, not UI
        Transform arrowTransform = directionArrow.transform;
        Vector3 originalScale = arrowTransform.localScale;
        Vector3 targetScale = originalScale * 1.2f;
        
        while (true)
        {
            // Scale up
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                arrowTransform.localScale = Vector3.Lerp(originalScale, targetScale, Mathf.SmoothStep(0, 1, t));
                yield return null;
            }
            
            // Scale down
            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime;
                arrowTransform.localScale = Vector3.Lerp(targetScale, originalScale, Mathf.SmoothStep(0, 1, t));
                yield return null;
            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }
} 