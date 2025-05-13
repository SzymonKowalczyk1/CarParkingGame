using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public DrawWithMouse drawControll;
    public float speed = 10f; 

    Vector3[] positions;
    bool startMovment = false;
    int moveIndex;
    private bool isGameOver = false;
    private void OnMouseDown()
    {
        if (!startMovment && !isGameOver)
        {
            drawControll.StartLine(transform.position);
        }
       
    }

    private void OnMouseDrag()
    {
        if (!startMovment && !isGameOver)
        {
            drawControll.UpdateLine();
        }
        
        
    }
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
            Vector2 currentPos = positions[moveIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentPos,speed * Time.deltaTime);

            Vector2 dir = currentPos - (Vector2)transform.position;
            if (dir.magnitude > 0.01f) // Only rotate if we have a meaningful direction
            {
                float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90f), speed * Time.deltaTime);
            }


            float distance = Vector2.Distance(currentPos, transform.position);
            if (distance <= 0.1f)
            {
                moveIndex++;
            }

            if(moveIndex > positions.Length - 1)
            {
                startMovment = false;
               
            }

        }
    }
    public void StopCar()
    {
        startMovment = false;
        isGameOver = true;
    }
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
