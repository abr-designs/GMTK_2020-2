﻿using System.Collections.Generic;
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

    private AudioController _audioController;    

    //[SerializeField]
    //private bool useRandomSeed;
    //[SerializeField]
    private int seed;

    //================================================================================================================//

    
    // Start is called before the first frame update
    private void Start()
    {
        _audioController = FindObjectOfType<AudioController>();

       //if (useRandomSeed)
       //{
       //    seed = Random.Range(int.MinValue, int.MaxValue);
       //    Random.InitState(seed);
       //    Debug.Log($"Seed: {seed}");
       //}
       //else
       //{
       //    Random.InitState(seed);
       //}
        
       _audioController.SetMusic(MUSIC.GAME);
        
        
        _grabbers.SetupArms(Values.age, Values.color);
    }

    //================================================================================================================//

    public void SetSeed(int seed)
    {
        this.seed = seed;
    }

    
    //================================================================================================================//
    


}
