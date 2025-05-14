using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Dodajemy namespace dla TextMeshPro

public class CoinManager : MonoBehaviour
{
    public int coinCount; //Licznik zebranych monet
    public TextMeshProUGUI coinText; //Tekst wyswietlajacy liczbe monet
    
    //Aktualizuje tekst z liczba monet
    void Update()
    {
        
        coinText.text = coinCount.ToString();
        
    }
}
