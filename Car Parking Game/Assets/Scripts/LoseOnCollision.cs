using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnCollision : MonoBehaviour
{
    public GameManager gameManager;
    
    //Wywolywane gdy obiekt zderzy sie z przeszkoda
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            //Wywoluje metode pokazujaca panel porazki
            if (gameManager != null)
            {
                gameManager.ShowGameOverPanel();
            }
        }
    }
}
