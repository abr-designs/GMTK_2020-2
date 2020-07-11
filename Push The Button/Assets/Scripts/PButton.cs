using System;
using UnityEngine;

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

    private Color inactiveColor;
    private Color activeColor;
    
    //================================================================================================================//

    public void SetColor(Color color)
    {
        Color.RGBToHSV(color, out var h, out _, out _);
        inactiveColor = Color.HSVToRGB(h, 0.65f, 0.5f);
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
        renderer.sprite = ButtonSprites.DownSprite;
        
        if (!active)
        {
            LevelManager.PredicateCall(() => Input.GetKeyUp(KeyCode.Mouse0), () =>
            {
                renderer.sprite = ButtonSprites.UpSprite;
            });
            return;
        }

        renderer.color = inactiveColor;

        pressedCallback?.Invoke();
    }

    //================================================================================================================//
}
