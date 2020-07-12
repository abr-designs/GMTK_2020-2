using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PButton : MonoBehaviour
{
    public bool active;
    private Action pressedCallback;

    [SerializeField]
    private ButtonSprites ButtonSprites;

    private new SpriteRenderer renderer
    {
        get
        {
            if (_renderer == null)
                _renderer = GetComponent<SpriteRenderer>();

            return _renderer;
        }
    }
    private SpriteRenderer _renderer;

    [SerializeField]
    private List<Color> colorOptions;

    private Color inactiveColor;
    private Color activeColor;
    
    //================================================================================================================//

    public void SetColor()
    {
        var selected = colorOptions[Random.Range(0, colorOptions.Count)];
        
        Color.RGBToHSV(selected, out var h, out _, out _);
        inactiveColor = Color.HSVToRGB(h, 0.65f, 0.35f);
        activeColor = Color.HSVToRGB(h, 0.8f, 1f);


        renderer.color = inactiveColor;
    }
    
    public void SetActive(bool state, Action Callback)
    {
        renderer.color = state ? activeColor : inactiveColor;
        active = state;
        pressedCallback = Callback;
    }
    
    //================================================================================================================//

    public void Pressed()
    {
        SetPressedSprite(true);
        
        LevelManager.PredicateCall(() => Input.GetKeyUp(KeyCode.Mouse0), () =>
        {
            SetPressedSprite(false);
        });
        
        if (!active)
            return;

        renderer.color = inactiveColor;

        pressedCallback?.Invoke();
    }

    public void SetPressedSprite(bool pressed)
    {
        renderer.sprite = pressed ? ButtonSprites.DownSprite : renderer.sprite = ButtonSprites.UpSprite;
    }

    //================================================================================================================//
}
