using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    public LineRenderer line; //Komponent do rysowania linii
    private Vector3 previousPosition; //Poprzednia pozycja punktu linii

    [SerializeField]
    private float minDistance = 0.1f; //Minimalna odleglosc miedzy punktami linii

    [SerializeField, Range(0.1f,2f)]
    private float width; //Szerokosc linii

    
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 1; 
        previousPosition = transform.position;
    }
    
    //Rozpoczyna rysowanie linii od podanej pozycji
    public void StartLine(Vector2 position)
    {
        line.positionCount = 1;
        line.SetPosition(0, position);
        line.startWidth = line.endWidth = width;

        previousPosition = position; //Ustawia poprzednia pozycje na punkt poczatkowy
    }

    //Aktualizuje linie podczas przeciagania myszka
    public void UpdateLine()
    {
        if (Input.GetMouseButton(0))
        {
            //Konwertuj pozycje myszy na wspolrzedne
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            //Dodaj nowy punkt tylko jesli jest wystarczajaco oddalony od poprzedniego aby uniknac zbyt duzej ilosci punktow
            if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
            {
                if(previousPosition == transform.position)
                {
                    line.SetPosition(0, currentPosition);
                }
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, currentPosition);
                previousPosition = currentPosition;
            }
        }
    }
}
