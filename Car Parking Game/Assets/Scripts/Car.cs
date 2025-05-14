using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public DrawWithMouse drawControll;
    public float speed = 10f; // Predkosc samochodu

    Vector3[] positions;
    bool startMovment = false;
    int moveIndex; //Indeks aktualnej pozycji
    private bool isGameOver = false; // Flaga konca gry
    
    //Wywolanie przy nacisnieciu na samochod
    private void OnMouseDown()
    {
        if (!startMovment && !isGameOver)
        {
            drawControll.StartLine(transform.position);
        }
       
    }

    //Wywolanie podczas przeciagania myszka
    private void OnMouseDrag()
    {
        if (!startMovment && !isGameOver)
        {
            drawControll.UpdateLine();
        }
    }
    
    //Wywolanie po puszczeniu przycisku myszy
    private void OnMouseUp()
    { 
        if (!startMovment && !isGameOver)
        {
            positions = new Vector3[drawControll.line.positionCount];
            drawControll.line.GetPositions(positions);
            if(positions.Length >= 2)
            {
                startMovment = true;
                moveIndex = 0;
            }
        }
    }
    
   
    private void Update()
    {
        if (isGameOver) return;
        if (startMovment == true && positions != null && positions.Length > 0)
        {
            //Pobiera aktualna pozycje docelowa
            Vector2 currentPos = positions[moveIndex];
            //Przesuwa samochod w kierunku pozycji docelowej
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);

            //Obraca samochod w kierunku ruchu
            Vector2 dir = currentPos - (Vector2)transform.position;
            if (dir.magnitude > 0.01f) //Obraca tylko jesli mamy znaczacy kierunek
            {
                float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f), speed * Time.deltaTime);
            }

            //Sprawdza czy osiagnieto pozycje docelowa
            float distance = Vector2.Distance(currentPos, transform.position);
            if (distance <= 0.1f)
            {
                moveIndex++;
            }

            //Sprawdza czy osiagnieto koniec trasy
            if(moveIndex > positions.Length - 1)
            {
                startMovment = false;
            }
        }
    }
    
    //Zatrzymuje samochod
    public void StopCar()
    {
        startMovment = false;
        isGameOver = true;
    }
    
    //Zwraca aktualna predkosc samochodu
    public float GetCurrentSpeed()
    {
        if (startMovment && positions != null && moveIndex < positions.Length)
        {
            Vector2 currentPos = transform.position;
            Vector2 targetPos = positions[moveIndex];
            return Vector2.Distance(currentPos, targetPos) * speed;
        }
        
        return 0f;
    }
}
