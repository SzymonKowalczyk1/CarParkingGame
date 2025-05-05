using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSpot : MonoBehaviour
{

    [SerializeField] private float parkingAngleTolerance = 15f;

    private bool levelCompleted = false;

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

        // Make the parking spot change color to indicate success (optional)
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.green;
        }
    }

}
