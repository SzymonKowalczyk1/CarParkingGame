using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSpot : MonoBehaviour
{
    [SerializeField] private float parkingAngleTolerance = 15f; //Tolerancja kata parkowania w stopniach
    [SerializeField] private CoinRatingSystem coinRatingSystem;
    [SerializeField] private GameManager gameManager;

    private bool levelCompleted = false; //Flaga ukonczenia poziomu


    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (levelCompleted) return;
        
        //Sprawdza czy to samochod gracza
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
        //Pomija jesli poziom jest juz ukonczony
        if (levelCompleted) return;

        //Sprawdza czy to samochod gracza
        if (collision.CompareTag("Player"))
        {
            Car car = collision.GetComponent<Car>();
            if (car != null)
            {
                CheckParking(car);
            }
        }
    }
    
    //Sprawdza czy samochod jest prawidlowo zaparkowany
    private void CheckParking(Car car)
    {
        //Sprawdza czy samochod nadal sie porusza
        if (car.GetCurrentSpeed() > 0.1f)
            return;

        //Sprawdza czy samochod jest prawidlowo ustawiony wzgledem miejsca parkingowego
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(car.transform.eulerAngles.z, transform.eulerAngles.z));
        if (angleDifference > parkingAngleTolerance)
            return;

        //Samochod zostal prawidlowo zaparkowany
        levelCompleted = true;

        //Pokazuje ocene za pomoca monet
        if (coinRatingSystem != null)
        {
            coinRatingSystem.ShowRating();
        }

        //Zmienia kolor miejsca parkingowego na zielony
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.green;
        }

        //Wylacza kontroler samochodu aby zapobiec dalszemu ruchowi
        if (car.drawControll != null)
        {
            car.enabled = false;
        }
    }
}


