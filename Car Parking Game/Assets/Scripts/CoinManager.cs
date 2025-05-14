using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount; //Licznik zebranych monet
    public Text coinText; //Tekst wyswietlajacy liczbe monet
    
    //Aktualizuje tekst z liczba monet
    void Update()
    {
        coinText.text = "Coins: " + coinCount.ToString();
    }
}
