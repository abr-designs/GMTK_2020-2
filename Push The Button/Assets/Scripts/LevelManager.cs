using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private GameManager _gameManager;
    private GameUIController _gameUiController;
    
    //[SerializeField]
    private Level[] levels;

    [SerializeField]
    private int levelIndex;

    [SerializeField]
    private GameObject garbagePrefab;

    //private Level currentLevel => levels[levelIndex] is null;

    private float timeLeft;
    private int remainingCycles;
    private int lastSelectedButton = -1;
    
    private List<Draggable> activeGarbage;

    private static LevelManager _instance;

    private new Transform transform;

    [SerializeField] 
    private TextMeshPro timeText;
    [SerializeField] 
    private TextMeshPro instructionText;

    //================================================================================================================//
    
    private void Start()
    {
        levels = GetComponentsInChildren<Level>(true);
        
        _instance = this;
        
        transform = gameObject.transform;

        _gameUiController = FindObjectOfType<GameUIController>();
        _gameManager = FindObjectOfType<GameManager>();
        activeGarbage = new List<Draggable>();
        
        foreach (var level in levels)
        {
            level.gameObject.SetActive(false);
        }
        
        waiting = true;

        StartLevel();
    }

    private void LateUpdate()
    {
        if (waiting)
            return;

        UpdateTimeText();

        if (timeLeft < 0f)
            LostLevel();
    }

    //================================================================================================================//
    
    private void UpdateTimeText()
    {
        timeText.text = $"{timeLeft:#.00}s";
        timeLeft -= Time.deltaTime;
    }
    
    //================================================================================================================//

    private void StartLevel()
    {
        lastSelectedButton = -1;
        timeText.gameObject.SetActive(false);
        
        var level = levels[levelIndex];
        level.gameObject.SetActive(true);
        
        _gameManager.SetSeed(level.seed);
        SetupButtons(level._buttons);
        SetupGarbage(level.GarbageCount, level.garbageArea);

        instructionText.text = level.instruction;
        timeLeft = level.time;
        UpdateTimeText();

        remainingCycles = level.Cycles;
        

        var waitTime = Random.Range(level.startTimeMin, level.startTimeMax);
        
        DelayedCall(waitTime, () =>
        {
            waiting = false;
            timeText.gameObject.SetActive(true);
            EnableButton(level._buttons);
        });
    }

    private void LoadNextLevel()
    {
        CleanupGarbage();
        levels[levelIndex].gameObject.SetActive(false);
        levelIndex++;

        if (levelIndex >= levels.Length)
        {
            Debug.Log("No more Levels");
            return;
        }
        
        Debug.Log($"Loading level {levels[levelIndex].gameObject.name}");


        
        StartLevel();
    }

    private void RestartLevel()
    {
        CleanupGarbage();
        
        StartLevel();
    }

    private void LostLevel()
    {
        waiting = true;
    }
    
    //================================================================================================================//
    
    private void SetupGarbage(int count, Rect area)
    {
        for (int i = 0; i < count; i++)
        {
            var locX = Random.Range(area.xMin, area.xMax);
            var locY = Random.Range(area.yMin, area.yMax);

            var temp = Instantiate(garbagePrefab).GetComponent<Draggable>();
            temp.renderer.sortingOrder = i + 1;
            temp.transform.position = new Vector3(locX, locY, -i * 0.01f);
            temp.transform.rotation = Quaternion.Euler(0f, 0, Random.Range(0f,359f));
            temp.transform.SetParent(_gameManager.transform, true);
            
            activeGarbage.Add(temp);
        }
    }

    private void CleanupGarbage()
    {
        for (var i = activeGarbage.Count - 1; i >= 0; i--)
        {
            Destroy(activeGarbage[i].gameObject);
        }
        
        activeGarbage.Clear();
    }
    
    private void SetupButtons(List<PButton> buttons)
    {
        for (var i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetColor();
        }
    }

    
    private void EnableButton(List<PButton> buttons)
    {
        var selectedIndex = Random.Range(0, buttons.Count);
        if (lastSelectedButton >= 0)
        {
            while (selectedIndex == lastSelectedButton)
            {
                selectedIndex = Random.Range(0, buttons.Count);
            }
        }
        
        for (var i = 0; i < buttons.Count; i++)
        {
            var active = i == selectedIndex;
            
            if(active)
                buttons[i].SetActive(true, 
                    OnButtonPressed);
            else
            {
                buttons[i].SetActive(false, null);
            }
        }

        lastSelectedButton = selectedIndex;
    }
    
    //================================================================================================================//

    private bool waiting = false;
    private void OnButtonPressed()
    {
        if (waiting)
            return;
        
        remainingCycles--;

        if (remainingCycles > 0)
        {
            EnableButton(levels[levelIndex]._buttons);
            return;
        }
        
        waiting = true;

        if (levelIndex + 1 >= levels.Length)
        {
            timeText.gameObject.SetActive(false);
            DelayedCall(1f, () =>
            {
                _gameUiController.ShowRetireWindow(Random.Range(1,100), () =>
                {
                    SceneManager.LoadScene(0);
                });
            });
            return;
        }
        

        DelayedCall(1f, () =>
        {
            _gameUiController.ShowSummaryWindow("Controller", LoadNextLevel);
        });
    }
    
    //================================================================================================================//


    public static void PredicateCall(Func<bool> predicate, Action callback)
    {
        _instance.StartCoroutine(PredicateCoroutine(predicate, callback));
    }
    
    private static IEnumerator PredicateCoroutine(Func<bool> predicate, Action callback)
    {
        yield return new WaitUntil(predicate);
        
        callback?.Invoke();
    }
    
    public static void DelayedCall(float time, Action callback)
    {
        _instance.StartCoroutine(DelayCoroutine(time, callback));
    }
    
    private static IEnumerator DelayCoroutine(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        
        callback?.Invoke();
    }
    
    //================================================================================================================//
}
