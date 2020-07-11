using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField]
    private Vector2 anchorOffset;

    private bool dragging;

    [SerializeField]
    private float minScale = 0.75f;
    [SerializeField]
    private float maxScale = 1.5f;
    
    private Transform transform;

    [SerializeField]
    private List<Sprite> sprites;

    private new Rigidbody2D rigidbody
    {
        get
        {
            if (_rigidbody)
                _rigidbody = GetComponent<Rigidbody2D>();
            return _rigidbody;
        }
    }
    private Rigidbody2D _rigidbody;
    
    private TargetJoint2D joint
    {
        get
        {
            if (_joint)
                _joint = GetComponent<TargetJoint2D>();
            return _joint;
        }
    }
    private TargetJoint2D _joint;
    
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
    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    private void Start()
    {
        transform = gameObject.transform;
        collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _joint = GetComponent<TargetJoint2D>();
        _renderer = GetComponent<SpriteRenderer>();

        _renderer.sprite = sprites[Random.Range(0, sprites.Count)];
        _renderer.flipX = Random.value >= 0.5f;
        transform.localScale = Vector3.one * Random.Range(minScale, maxScale); 
        
        collider = gameObject.AddComponent<PolygonCollider2D>();

        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0f;
        
        joint.anchor = Vector2.zero;
        joint.target = transform.position;
    }


    public void OnDragStart(Vector2 clickPosition)
    {
        anchorOffset = transform.InverseTransformPoint(clickPosition);
        rigidbody.isKinematic = false;

        joint.anchor = anchorOffset;
    }

    public void OnDragUpdate(Vector2 targetPosition)
    {
        joint.target = targetPosition - anchorOffset;
    }

    public void OnDragEnd()
    {
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0f;
        
        //rigidbody.simulated = false;
        joint.anchor = anchorOffset = Vector2.zero;
        
    }

}
