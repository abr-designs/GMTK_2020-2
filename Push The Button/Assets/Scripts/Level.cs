using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField]
    public string levelName;
    [SerializeField]
    public List<PButton> _buttons;
    [SerializeField]
    public int GarbageCount;
    [SerializeField]
    public Rect garbageArea;

    [SerializeField]
    public float time;

    public float startTimeMin;
    public float startTimeMax;

    public bool countingDown;

    [SerializeField]
    public int seed;
    
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        var TL = new Vector2(garbageArea.xMin, garbageArea.yMax);
        var TR = new Vector2(garbageArea.xMax, garbageArea.yMax);
        var BR = new Vector2(garbageArea.xMax, garbageArea.yMin);
        var BL = new Vector2(garbageArea.xMin, garbageArea.yMin);
        
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawLine(TL,TR);
        Gizmos.DrawLine(TR,BR);
        Gizmos.DrawLine(BR,BL);
        Gizmos.DrawLine(BL,TL);
    }


#endif
}
