using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    private Draggable currentlyDragging;

    [SerializeField]
    private Camera camera;

    private Vector2 grabPoint;

    [SerializeField]
    private Grabbers _grabbers;

    [SerializeField]
    private LayerMask LayerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        grabPoint = _grabbers.Position;
        //screenPoint = camera.ScreenToWorldPoint(Input.mousePosition);
        
        DraggableSolver();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            ClickParser();

    }

    private void ClickParser()
    {
        var ray = new Ray((Vector3)grabPoint - Vector3.forward * 10f, Vector3.forward);
        var hit = Physics2D.GetRayIntersection(ray, 15f, LayerMask.value);

        if (hit.collider == null)
            return;

        Debug.Log($"Hit: {hit.collider.gameObject.name}", hit.collider);
        
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
        
        currentlyDragging.OnDragUpdate(grabPoint);

        if (!Input.GetKeyUp(KeyCode.Mouse0)) 
            return;
        if (!currentlyDragging)
            return;
            
        currentlyDragging.OnDragEnd();
        currentlyDragging = null;
    }
}
