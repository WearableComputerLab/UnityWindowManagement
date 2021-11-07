using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSubKey : MonoBehaviour
{
    public GameObject a;
    public void Update()
    {
        float x = gameObject.transform.localScale.x;
        float y = gameObject.transform.localScale.y;
        float x1 = x /2+1;
        float y1 = y /2;

        gameObject.transform.localPosition = new Vector3(-x1, -y1, 0);
    }
}
