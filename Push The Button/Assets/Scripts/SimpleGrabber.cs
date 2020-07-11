using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleGrabber : MonoBehaviour
{
    [SerializeField]
    private Grabbers.UIArm _arm;

    public Vector2 mousePos;

    private float maxReach;

    private RectTransform Transform;
    
    // Start is called before the first frame update
    private void Start()
    {
        _arm.SetColor(Color.HSVToRGB(Random.Range(0f,1f), 0.5f, 1f));
        
        Transform = gameObject.transform as RectTransform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _arm.IsHandOpen(false);
        else if(Input.GetKeyUp(KeyCode.Mouse0))
            _arm.IsHandOpen(true);

        Position();
    }

    [SerializeField]
    private Canvas _canvas;
    private void Position()
    {
        //var centreX = Transform.anchoredPosition.x * _canvas.scaleFactor;
        //var centreY = Transform.anchoredPosition.y * _canvas.scaleFactor;
        Transform.anchoredPosition = Input.mousePosition;// - new Vector2(Screen.width / 2f, Screen.height/2f);
    }
}
