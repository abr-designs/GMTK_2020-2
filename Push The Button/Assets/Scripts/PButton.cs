using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PButton : MonoBehaviour
{
    public bool active;
    private Action pressedCallback;

    [SerializeField]
    private Color buttonColor;

    private new SpriteRenderer renderer;
    
    // Start is called before the first frame update
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = buttonColor;
    }

    public void SetActive(bool state, Action Callback)
    {
        active = state;
        pressedCallback = Callback;
    }

    public void Pressed()
    {
        if (!active)
            return;
        
        pressedCallback?.Invoke();
    }

}
