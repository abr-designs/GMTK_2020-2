using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    //================================================================================================================//
    [SerializeField, Header("Summary Window")]
    private GameObject summaryWindow;
    [SerializeField]
    private TMP_Text summaryText;
    [SerializeField]
    private Button summaryButton;
    
    //================================================================================================================//
    
    [SerializeField, Header("Retire Window"), Space(10f)]
    private GameObject retireWindow;
    [SerializeField]
    private Image armImage;
    [SerializeField]
    private TMP_Text retirementText;
    [SerializeField]
    private Button retireButton;
    
    [SerializeField, Header("Retire Window"), Space(10f)]
    private GameObject failedWindow;

    [SerializeField]
    private TMP_Text failedText;
    [SerializeField]
    private Button failedButton;
    
    //================================================================================================================//
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //================================================================================================================//

    public void ShowSummaryWindow(string title, Action ButtonCallback)
    {
        summaryWindow.SetActive(true);
        
        var text = $"You're doing great work {Values.name} time for your next promotion to: {title}!";

        summaryText.text = text;
        
        summaryButton.onClick.RemoveAllListeners();
        summaryButton.onClick.AddListener(() =>
        {
            ButtonCallback?.Invoke();
            HideWindows();
        });
    }
    
    //================================================================================================================//

    public void ShowRetireWindow(int years, Action ButtonCallback)
    {
        retireWindow.SetActive(true);
        
        var text = $"{Values.name}\n<color=red>{years} Yearz of SErvice</color>\nWOW! you really are amazing.\nSigned: Your Boss";
        
        armImage.color = Values.color;
        retirementText.text = text;
        
        retireButton.onClick.RemoveAllListeners();
        retireButton.onClick.AddListener(() =>
        {
            ButtonCallback?.Invoke();
            HideWindows();
        });
    }
    
    //================================================================================================================//

    public void ShowFailedWindow(Action ButtonCallback)
    {
        failedWindow.SetActive(true);
        var text = string.Empty;
        
        //if(age < )

    }
    
    //================================================================================================================//


    
    public void HideWindows()
    {
        failedWindow.SetActive(false);
        summaryWindow.SetActive(false);
        retireWindow.SetActive(false);
    }
}
