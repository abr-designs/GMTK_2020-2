using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grabbers : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField, Range(0.01f, 1f)]
    private float speedMult;
    [SerializeField]
    private float maxReach;

    private float reach;

    [SerializeField, Range(0f, 1f)] 
    private float shakeLevel;

    [SerializeField]
    private float shakeOffset;

    private Vector2 screenPoint;
    private Vector2 targetPosition;

    private Camera _camera;

    [SerializeField]
    private List<Arm> arms;

    public Vector2 Position => transform.position;

    [SerializeField]
    private AnimationCurve sizeCurve = new AnimationCurve();


    private new Transform transform
    {
        get
        {
            if (_transform == null)
                _transform = gameObject.transform;

            return _transform;
        }
    }

    private Transform _transform;
    
    //================================================================================================================//
    
    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
        
        if(Values.age == 0)
            SetupArms(25, Color.magenta);
    }

    // Update is called once per frame
    private void Update()
    {
        screenPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

        MoveTowardsTarget();
        UpdateHandState();
    }
    
    //================================================================================================================//

    public void SetupArms(int age, Color color)
    {
        transform.localScale = Vector3.one * sizeCurve.Evaluate(age/100f);

        reach = Mathf.Lerp(-maxReach, maxReach, Mathf.InverseLerp(1, 18, age));
        
        speedMult = Mathf.Clamp(1f - Mathf.InverseLerp(50, 100, age), 0.03f, 1f);
        shakeLevel = Mathf.InverseLerp(75, 100, age);
        
        var hasHair = Random.value <= Mathf.InverseLerp(40,100,age);
        var hasWrinkles = Random.value <= (Mathf.InverseLerp(30,100,age) * 0.5f);

        Color.RGBToHSV(color, out var h, out var s, out var v);
        
        color = Color.HSVToRGB(h,
            Mathf.Lerp(s, s/3f, Mathf.InverseLerp(60,100, age)),
            v);
        
        foreach (var arm in arms)
        {
            arm.SetColor(color);
            arm.ShowHair(hasHair, Color.white);
            arm.ShowWrinkles(hasWrinkles, Color.white);
        }
    }
    
    
    //================================================================================================================//

    private void MoveTowardsTarget()
    {
        screenPoint.y = Mathf.Clamp(screenPoint.y, -maxReach, reach);

        var offsest = Vector2.zero;
        if (shakeLevel > 0f)
        {
            offsest = Random.insideUnitCircle * (shakeOffset * shakeLevel);
        }
        
        transform.position = Vector2.MoveTowards(transform.position, screenPoint, speed * (Time.deltaTime * speedMult)) + offsest * Time.deltaTime;
    }

    private void UpdateHandState()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            foreach (var arm in arms)
            {
                arm.IsHandOpen(false);
            }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            foreach (var arm in arms)
            {
                arm.IsHandOpen(true);
            }
    }
    
    [System.Serializable]
    public struct Arm
    {
        public string name;
        
        public SpriteRenderer Hand;
        public SpriteRenderer ForeArm;
        public SpriteRenderer Hair;
        public SpriteRenderer Wrinkles;

        public Sprite OpenHand;
        public Sprite ClosedHand;

        public void SetColor(Color color)
        {
            Hand.color = color;
            ForeArm.color = color;
        }
        
        public void ShowHair(bool state, Color color)
        {
            Hair.color = color;
            Hair.gameObject.SetActive(state);
        }
        
        public void ShowWrinkles(bool state, Color color)
        {
            Wrinkles.color = color;
            Wrinkles.gameObject.SetActive(state);
        }

        public void IsHandOpen(bool open)
        {
            Hand.sprite = open ? OpenHand : ClosedHand;
        }
    }
    
    [System.Serializable]
    public struct UIArm
    {
        public string name;
        
        public Image Hand;
        public Image ForeArm;

        public Sprite OpenHand;
        public Sprite ClosedHand;

        public void SetColor(Color color)
        {
            Hand.color = color;
            ForeArm.color = color;
        }

        public void IsHandOpen(bool open)
        {
            Hand.sprite = open ? OpenHand : ClosedHand;
        }
    }
}
