using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    private Draggable currentlyDragging;

    [SerializeField]
    private Camera camera;

    private Vector2 screenPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        screenPoint = camera.ScreenToWorldPoint(Input.mousePosition);
        
        DraggableSolver();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            ClickParser();

    }

    private void ClickParser()
    {
        var hit = Physics2D.Raycast(screenPoint, Vector2.zero);

        if (hit.collider == null)
            return;

        switch (hit.collider.gameObject.tag)
        {
            //--------------------------------------------------------------------------------------------------------//
            case "Button":
                var button = hit.collider.gameObject.GetComponent<PButton>();

                button.Pressed();
                
                break;
            //--------------------------------------------------------------------------------------------------------//
            case "Garbage":
                var drag = hit.collider.gameObject.GetComponent<Draggable>();

                if (!drag) return;
                currentlyDragging = drag;
            
                currentlyDragging.OnDragStart(hit.point);
                break;
            //--------------------------------------------------------------------------------------------------------//
            default:
                return;
            //--------------------------------------------------------------------------------------------------------//
        }
    }

    private void DraggableSolver()
    {
        if (currentlyDragging == null) return;
        
        currentlyDragging.OnDragUpdate(screenPoint);

        if (!Input.GetKeyUp(KeyCode.Mouse0)) 
            return;
        if (!currentlyDragging)
            return;
            
        currentlyDragging.OnDragEnd();
        currentlyDragging = null;
    }
}
