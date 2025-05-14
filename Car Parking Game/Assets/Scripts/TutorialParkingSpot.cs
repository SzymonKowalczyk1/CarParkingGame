using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialParkingSpot : MonoBehaviour
{
    public TutorialManager tutorialManager; 
    public GameObject directionArrow;
    
    [SerializeField] private float parkingAngleTolerance = 15f; // Tolerancja kata parkowania
    
    //Metoda do pominiecia samouczka
    public void SkipTutorial()
    {
        if (tutorialManager != null)
        {
            tutorialManager.SkipTutorial();
        }
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // sprawdza czy to samochod gracza
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


            
        //Ukrywa strzalke kierunku
        if (directionArrow != null)
            directionArrow.SetActive(false);
        
        //Zmien kolor miejsca parkingowego na zielony
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
        
        if (tutorialManager != null)
        {
            //Sprawdza czy samouczek jest aktywny czy zostal pominiety
            if (tutorialManager.IsTutorialActive())
            {
                tutorialManager.NextTutorialStep();
            }
            else
            {
                //aktywuje panel ukonczenia samouczka
                tutorialManager.ShowTutorialComplete();
            }
        }
    }
} 