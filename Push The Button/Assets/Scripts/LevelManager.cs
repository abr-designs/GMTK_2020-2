using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private GameManager _gameManager;
    
    [SerializeField]
    private Level[] levels;

    [SerializeField]
    private int levelIndex;

    [SerializeField]
    private GameObject garbagePrefab;

    //private Level currentLevel => levels[levelIndex] is null;
    
    private List<Draggable> activeGarbage;

    private static LevelManager _instance;

    private new Transform transform;

    //================================================================================================================//
    
    private void Start()
    {
        _instance = this;
        
        transform = gameObject.transform;
        
        _gameManager = FindObjectOfType<GameManager>();
        activeGarbage = new List<Draggable>();
        
        foreach (var level in levels)
        {
            level.gameObject.SetActive(false);
        }

        StartLevel();
    }
    
    //================================================================================================================//

    private void StartLevel()
    {
        var level = levels[levelIndex];
        level.gameObject.SetActive(true);
        
        _gameManager.SetSeed(level.seed);
        SetupButtons(level._buttons);
        SetupGarbage(level.GarbageCount, level.garbageArea);

        waiting = false;
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
        
        Debug.Log($"Loading level {levels[levelIndex].levelName}");


        
        StartLevel();
    }

    private void RestartLevel()
    {
        CleanupGarbage();
        
        StartLevel();
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
        var selectedIndex = Random.Range(0, buttons.Count);

        for (var i = 0; i < buttons.Count; i++)
        {
            var active = i == selectedIndex;
            var color = Color.HSVToRGB(Random.value, 1f, 1f);
            
            if(active)
                buttons[i].SetActive(true, 
                    color, 
                    OnButtonPressed);
            else
            {
                buttons[i].SetActive(false, color, null);
            }
        }

    }
    
    //================================================================================================================//

    private bool waiting = false;
    private void OnButtonPressed()
    {
        if (waiting)
            return;
            
        DelayedCall(2f, LoadNextLevel);
        waiting = true;
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

    #if UNITY_EDITOR
    
    [ContextMenu("Get Levels")]
    private void GetLevels()
    {
        levels = new Level[0];
        levels = GetComponentsInChildren<Level>(true);
    }
    
    #endif
}
