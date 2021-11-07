using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveFar : Button
{
    public float speed = 1700;

    private void Update()
    {
        GameObject mc = GameObject.Find("MainCamera");
        float z = mc.transform.localPosition.z;

        if (IsPressed() && z >= 350)
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            mc.transform.Translate(x, 0, 0);
            mc.transform.Translate(0, 0, -50 * Time.deltaTime);
        }
    }

}