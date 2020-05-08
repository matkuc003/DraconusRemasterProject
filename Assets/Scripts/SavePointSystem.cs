using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointSystem : MonoBehaviour
{
    private Vector2 savePoint = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setSavePoint(Vector2 position)
    {
        savePoint = position;
    }

    public Vector2 getSavePoint()
    {
        return savePoint;
    }
}
