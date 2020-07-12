using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    
    [SerializeField, Header("Failed Window"), Space(10f)]
    private GameObject failedWindow;

    [SerializeField]
    private TMP_Text failedText;
    [SerializeField]
    private Button failedButton;
    [SerializeField]
    private Button quitButton;
    //================================================================================================================//
    
    // Start is called before the first frame update
    private void Start()
    {
        quitButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    //================================================================================================================//

    public void ShowSummaryWindow(string title,string victoryText, Action ButtonCallback)
    {
        summaryWindow.SetActive(true);
        
        var text = $"{victoryText} {Values.name}\nTime for your promotion to: {title}!";

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
        var age = Values.age;
        var name = Values.name;
        
        if (age == 0)
            age = 25;
        
        failedWindow.SetActive(true);
        string[] options;

        if (age < 16)
        {
            options = new[]
            {
                "Wow they really are hiring younger every year!",
                "Aren't you a little young to be working here?",
                $"{name}, you're way too young for this!",
            };
        }
        else if (age > 75)
        {
            //text = $"Hey {name}, you're getting pretty old, maybe its time to retire?";
            options = new[]
            {
                "Retirement is starting to look good now!",
                "Who keeps hiring seniors?",
                $"Wow {name} this Decade looks great on you",
            };
        }
        else
        {
            options = new[]
            {
                "What a DISASTER!! What are we going to do?! Just try again?",
                $"{name} I'm not mad, i'm just Disappointed",
                $"Wow {name} you were so close!",
            };
        }


        failedText.text = options[Random.Range(0, options.Length)];
        
        failedButton.onClick.RemoveAllListeners();
        failedButton.onClick.AddListener(() =>
        {
            ButtonCallback?.Invoke(); 
            HideWindows();
        });
    }
    
    //================================================================================================================//


    
    public void HideWindows()
    {
        failedWindow.SetActive(false);
        summaryWindow.SetActive(false);
        retireWindow.SetActive(false);
    }
}
