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
    
    [SerializeField]
    private Canvas _canvas;

    private RectTransform Transform;
    private RectTransform canvasTransform;
    private Camera camera;
    
    // Start is called before the first frame update
    private void Start()
    {
        Transform = gameObject.transform as RectTransform;
        canvasTransform = _canvas.transform as RectTransform;
        camera = Camera.main;
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


    private void Position()
    {
        mousePos = camera.ScreenToViewportPoint(Input.mousePosition);
        
        Transform.anchoredPosition = mousePos * canvasTransform.sizeDelta;// - new Vector2(Screen.width / 2f, Screen.height/2f);
    }

    public void SetColor(Color color)
    {
        _arm.SetColor(color);
    }
}
