using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomSubKey : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float x = gameObject.transform.localScale.x;
        float y = gameObject.transform.localScale.y;
        float x1 = x * 10;
        float y1 = y * 10;

        gameObject.transform.localScale = new Vector3(x1, y1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
