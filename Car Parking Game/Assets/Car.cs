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
    private void OnMouseDown()
    {
        drawControll.StartLine(transform.position);
    }

    private void OnMouseDrag()
    {
        drawControll.UpdateLine();
        
    }
    private void OnMouseUp()
    {
        positions = new Vector3[drawControll.line.positionCount];
        drawControll.line.GetPositions(positions);
        startMovment = true;
        moveIndex = 0;
    }
    private void Update()
    {
        if (startMovment == true)
        {
            Vector2 currentPos = positions[moveIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentPos,speed * Time.deltaTime);

            Vector2 dir = currentPos - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,0f,angle * Mathf.Rad2Deg - 90f),speed);

            float distance = Vector2.Distance(currentPos, transform.position);
            if (distance <= 0.5f)
            {
                moveIndex++;
            }
            if(moveIndex > positions.Length - 1)
            {
                startMovment = false;
               
            }

        }
    }
}
