using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomUwcWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = gameObject.transform.localScale.x;
        float y = gameObject.transform.localScale.y;
        float x1 = x * 10;
        float y1 = y * 10;

        gameObject.transform.localScale = new Vector3(x1, y1, 1);
    }
}
