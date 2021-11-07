using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTransition : MonoBehaviour
{
    public void CameraTransite1()
    {
        //float x = gameObject.transform.localPosition.x + 2000;
        gameObject.transform.localPosition = new Vector3(2000, 200, 550);
    }

    public void CameraTransite2()
    {
        //float x = gameObject.transform.localPosition.x - 2000;
        gameObject.transform.localPosition = new Vector3(0, 200, 550);
    }
}
