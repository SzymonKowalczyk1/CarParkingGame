using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinManager coinManager; 
    
    //Wywolywane gdy inny obiekt wejdzie w obszar monety
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            //Zwieksza licznik monet i niszczy obiekt monety
            coinManager.coinCount++;
            Destroy(gameObject);
        }
    }
}
