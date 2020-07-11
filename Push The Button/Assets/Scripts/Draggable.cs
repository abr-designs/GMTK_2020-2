using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField]
    private Vector2 anchorOffset;

    private bool dragging;
    
    private Transform transform;
    private Collider2D collider;

    public new SpriteRenderer renderer
    {
        get
        {
            if (_renderer == null)
                _renderer = GetComponent<SpriteRenderer>();

            return _renderer;
        }
    }
    public SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    private void Start()
    {
        transform = gameObject.transform;
        collider = GetComponent<Collider2D>();
    }


    public void OnDragStart(Vector2 clickPosition)
    {
        anchorOffset = clickPosition - (Vector2)transform.position;
    }

    public void OnDragUpdate(Vector2 targetPosition)
    {
        transform.position = targetPosition - anchorOffset;
    }

    public void OnDragEnd()
    {
        anchorOffset = Vector2.zero;
    }

}
