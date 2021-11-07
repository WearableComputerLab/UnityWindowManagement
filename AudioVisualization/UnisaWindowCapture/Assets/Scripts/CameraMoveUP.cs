using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveUP : Button
{
    public float speed = 2300;

    private void Update()
    {
        GameObject mc = GameObject.Find("MainCamera");
        
        if (IsPressed())
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            mc.transform.Translate(x, 0, 0);
            mc.transform.Translate(0, 50 * Time.deltaTime, 0);
        }
    }

}