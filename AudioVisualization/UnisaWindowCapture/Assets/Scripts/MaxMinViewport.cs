using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxMinViewport : MonoBehaviour
{
    // Start is called before the first frame update
    public void MaxView()
    {
        float x = gameObject.transform.localPosition.x;
        float y = gameObject.transform.localPosition.y;
        gameObject.transform.localPosition = new Vector3(x, y, 770);

    }

    // Update is called once per frame
    public void MinView()
    {
        float x = gameObject.transform.localPosition.x;
        float y = gameObject.transform.localPosition.y;
        gameObject.transform.localPosition = new Vector3(x, y, 350);
    }
}
