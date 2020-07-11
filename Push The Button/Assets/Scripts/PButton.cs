using System;
using System.Collections;
using UnityEngine;

public class PButton : MonoBehaviour
{
    public bool active;
    private Action pressedCallback;

    [SerializeField]
    private ButtonSprites ButtonSprites;

    private new SpriteRenderer renderer;

    private Color inactiveColor;
    private Color activeColor;
    
    // Start is called before the first frame update
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void SetActive(bool state, Color color, Action Callback)
    {
        Color.RGBToHSV(color, out var h, out _, out _);
        inactiveColor = Color.HSVToRGB(h, 0.8f, 0.7f);
        activeColor = Color.HSVToRGB(h, 0.8f, 1f);


        renderer.color = state ? activeColor : inactiveColor;
        active = state;
        pressedCallback = Callback;
    }

    public void Pressed()
    {
        if (!active)
            return;

        renderer.sprite = ButtonSprites.DownSprite;
        
        pressedCallback?.Invoke();

        StartCoroutine(DelayCoroutine(0.3f, () =>
        {
            renderer.sprite = ButtonSprites.DownSprite;
        }));
    }

    private IEnumerator DelayCoroutine(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        
        callback?.Invoke();
    }

}
