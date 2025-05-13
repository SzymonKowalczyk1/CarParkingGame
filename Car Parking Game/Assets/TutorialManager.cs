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
    public GameObject[] tutorialStepObjects; // Optional objects to enable/disable during tutorial
    
    [Header("Completion Panel")]
    public GameObject tutorialCompletePanel;
    public Button nextLevelButton;
    public Button menuButton;
    
    [Header("Tutorial Settings")]
    [TextArea(3, 5)]
    public string[] tutorialSteps;
    public Vector2[] highlightPositions;
    public Vector2[] arrowPositions;
    
    private int currentStep = 0;
    private bool tutorialActive = true;
    
    void Start()
    {
        // Initialize tutorial
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
            
        if (nextButton != null)
            nextButton.onClick.AddListener(NextTutorialStep);
        
        // Setup completion panel buttons
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(GoToNextLevel);
            
        if (menuButton != null)
            menuButton.onClick.AddListener(GoToMainMenu);
            
        // Hide completion panel at start
        if (tutorialCompletePanel != null)
            tutorialCompletePanel.SetActive(false);
            
        // Disable car controls until tutorial allows it
        if (carController != null)
            carController.enabled = false;
            
        if (drawController != null)
            drawController.enabled = false;
            
        // Show first tutorial step
        ShowTutorialStep(0);
    }
    
    void ShowTutorialStep(int stepIndex)
    {
        if (stepIndex >= tutorialSteps.Length)
        {
            // Tutorial complete
            EndTutorial();
            return;
        }
        
        // Update text
        if (tutorialText != null)
            tutorialText.text = tutorialSteps[stepIndex];
            
        // Position highlight if needed
        if (tutorialHighlight != null && stepIndex < highlightPositions.Length)
        {
            tutorialHighlight.gameObject.SetActive(true);
            RectTransform rt = tutorialHighlight.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = highlightPositions[stepIndex];
            }
        }
        else if (tutorialHighlight != null)
        {
            tutorialHighlight.gameObject.SetActive(false);
        }
        
        // Position arrow if needed
        if (tutorialArrow != null && stepIndex < arrowPositions.Length)
        {
            tutorialArrow.gameObject.SetActive(true);
            RectTransform rt = tutorialArrow.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = arrowPositions[stepIndex];
            }
        }
        else if (tutorialArrow != null)
        {
            tutorialArrow.gameObject.SetActive(false);
        }
        
        // Enable specific objects for this step
        for (int i = 0; i < tutorialStepObjects.Length; i++)
        {
            if (tutorialStepObjects[i] != null)
                tutorialStepObjects[i].SetActive(i == stepIndex);
        }
        
        // Enable car controls at specific steps if needed
        if (stepIndex == 1) // Assuming step 2 is when we want to allow drawing
        {
            if (drawController != null)
                drawController.enabled = true;
        }
        
        if (stepIndex == 2) // Assuming step 3 is when we want to allow car movement
        {
            if (carController != null)
                carController.enabled = true;
        }
        
        currentStep = stepIndex;
    }
    
    public void NextTutorialStep()
    {
        ShowTutorialStep(currentStep + 1);
    }
    
    void EndTutorial()
    {
        tutorialActive = false;
        
        // Enable all controls
        if (carController != null)
            carController.enabled = true;
            
        if (drawController != null)
            drawController.enabled = true;
            
        // Hide tutorial UI
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
            
        if (tutorialHighlight != null)
            tutorialHighlight.gameObject.SetActive(false);
            
        if (tutorialArrow != null)
            tutorialArrow.gameObject.SetActive(false);
            
        // Show tutorial complete panel
        if (tutorialCompletePanel != null)
            tutorialCompletePanel.SetActive(true);
    }
    
    // Call this method to skip the tutorial
    public void SkipTutorial()
    {
        EndTutorial();
    }
    
    // Check if tutorial is currently active
    public bool IsTutorialActive()
    {
        return tutorialActive;
    }
    
    // Show tutorial completion directly (used when tutorial was skipped)
    public void ShowTutorialComplete()
    {
        // Show tutorial complete panel
        if (tutorialCompletePanel != null)
            tutorialCompletePanel.SetActive(true);
    }
    
    // Navigation methods
    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
} 