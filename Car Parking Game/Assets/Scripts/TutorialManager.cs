using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("References")]
    public Car carController;
    public DrawWithMouse drawController;
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public Button nextButton;
    public Image tutorialHighlight;
    public GameObject tutorialArrow;
    public GameObject[] tutorialStepObjects;
    
    [Header("Completion Panel")]
    public GameObject tutorialCompletePanel;
    public Button nextLevelButton;
    public Button menuButton;
    public Button skipButton;
    
    [Header("Tutorial Settings")]
    [TextArea(3, 5)]
    public string[] tutorialSteps;
    public Vector2[] highlightPositions;
    public Vector2[] arrowPositions;
    
    private int currentStep = 0;
    private bool tutorialActive = true;
    
    void Start()
    {
        //Inicjalizacja samouczka
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
            
        if (nextButton != null)
            nextButton.onClick.AddListener(NextTutorialStep);
        
        //Konfiguracja przyciskow panelu ukonczenia
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(GoToNextLevel);
            
        if (menuButton != null)
            menuButton.onClick.AddListener(GoToMainMenu);
            
        //Konfiguracja przycisku pominiecia
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(SkipTutorial);
            skipButton.gameObject.SetActive(true);
        }
            
        //Ukrywa panel ukonczenia na poczatku
        if (tutorialCompletePanel != null)
            tutorialCompletePanel.SetActive(false);
            
        //Wylacza kontrolery samochodu dopoki samouczek na to nie pozwoli
        if (carController != null)
            carController.enabled = false;
            
        if (drawController != null)
            drawController.enabled = false;
            
        //Pokazuje pierwszy krok samouczka
        ShowTutorialStep(0);
    }
    
    //Pokazuje okreslony krok samouczka
    void ShowTutorialStep(int stepIndex)
    {
        if (stepIndex >= tutorialSteps.Length)
        {
            //Samouczek ukonczony
            EndTutorial();
            return;
        }
        
        //Aktualizuje tekst
        if (tutorialText != null)
            tutorialText.text = tutorialSteps[stepIndex];
               
        //Wlacza konkretne obiekty dla tego kroku
        for (int i = 0; i < tutorialStepObjects.Length; i++)
        {
            if (tutorialStepObjects[i] != null)
                tutorialStepObjects[i].SetActive(i == stepIndex);
        }
        
        //Wlacz kontrolery samochodu na konkretnych krokache
        if (stepIndex == 1) //krok 2 - w tym momencie mozna rysowac
        {
            if (drawController != null)
                drawController.enabled = true;
        }
        
        if (stepIndex == 2) //krok 3 - mozna ruszyc samochod
        {
            if (carController != null)
                carController.enabled = true;
        }
        
        currentStep = stepIndex;
    }
    
    //Przechodzi do nastepnego kroku samouczka
    public void NextTutorialStep()
    {
        ShowTutorialStep(currentStep + 1);
    }
    
    //konczy samouczek
    void EndTutorial()
    {
        tutorialActive = false;
        
        //Wlacza wszystkie kontrolery
        if (carController != null)
            carController.enabled = true;
            
        if (drawController != null)
            drawController.enabled = true;
            
        //Ukrywa UI samouczka
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
            
        if (tutorialHighlight != null)
            tutorialHighlight.gameObject.SetActive(false);
            
        if (tutorialArrow != null)
            tutorialArrow.gameObject.SetActive(false);
            
        //Ukrywa przycisk pominiecia
        if (skipButton != null)
            skipButton.gameObject.SetActive(false);
            
        //Pokazuje panel ukonczenia samouczka
        if (tutorialCompletePanel != null)
            tutorialCompletePanel.SetActive(true);
    }
    
    //metida do pomijania samouczka
    public void SkipTutorial()
    {
        EndTutorial();
    }
    
    //Sprawdza czy samouczek jest aktualnie aktywny
    public bool IsTutorialActive()
    {
        return tutorialActive;
    }
    
    //pokazuje ukonczenie samouczka
    public void ShowTutorialComplete()
    {
        //Ukrywa przycisk pominiecia
        if (skipButton != null)
            skipButton.gameObject.SetActive(false);
            
        //Pokazuje panel ukonczenia samouczka
        if (tutorialCompletePanel != null)
            tutorialCompletePanel.SetActive(true);
    }
    
    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
} 