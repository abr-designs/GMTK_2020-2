using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private Grabbers _grabbers;
    
    //[SerializeField]
    //private List<PButton> buttons;
    [SerializeField]
    private GameObject garbagePrefab;
    //[SerializeField]
    //private int garbageCount;
    //[SerializeField]
    //private Rect garbageArea;

    [SerializeField]
    private List<Draggable> activeGarbage;

    //[SerializeField]
    //private bool useRandomSeed;
    //[SerializeField]
    private int seed;

    //================================================================================================================//

    
    // Start is called before the first frame update
    private void Start()
    {
        activeGarbage = new List<Draggable>();

        if (useRandomSeed)
        {
            seed = Random.Range(int.MinValue, int.MaxValue);
            Random.InitState(seed);
            Debug.Log($"Seed: {seed}");
        }
        else
        {
            Random.InitState(seed);
        }
        
        
        SetupButtons();
        SetupGarbage();
        
        _grabbers.SetupArms(Values.age, Values.color);
    }

    //================================================================================================================//

    public void SetSeed(int seed)
    {
        this.seed = seed;
    }
    
    private void SetupButtons()
    {
        var selectedIndex = Random.Range(0, buttons.Count);

        for (var i = 0; i < buttons.Count; i++)
        {
            var active = i == selectedIndex;
            
            if(active)
                buttons[i].SetActive(true, OnButtonPressed);
            else
            {
                buttons[i].SetActive(false, null);
            }
        }

    }


    private void OnButtonPressed()
    {
        CleanupGarbage();
    }
    
    //================================================================================================================//

    private void SetupGarbage()
    {
        for (int i = 0; i < garbageCount; i++)
        {
            var locX = Random.Range(garbageArea.xMin, garbageArea.xMax);
            var locY = Random.Range(garbageArea.yMin, garbageArea.yMax);

            var temp = Instantiate(garbagePrefab).GetComponent<Draggable>();
            temp.renderer.sortingOrder = i + 1;
            temp.transform.position = new Vector3(locX, locY, -i * 0.01f);
            temp.transform.rotation = Quaternion.Euler(0f, 0, Random.Range(0f,359f));
            
            activeGarbage.Add(temp);
        }
        
        

    }

    private void CleanupGarbage()
    {
        Debug.Log("Button Pressed");
        
        for (var i = activeGarbage.Count - 1; i >= 0; i--)
        {
            Destroy(activeGarbage[i].gameObject);
        }
        
        activeGarbage.Clear();
    }
    
    //================================================================================================================//
    


}
