using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideChange : MonoBehaviour
{
    // Start is called before the first frame update
    public void ShowIt()
    {
        if (gameObject.active == false)
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    public void HideIt()
    {
        if (gameObject.active == true)
        {
            gameObject.SetActive(false);
        }
    }
}
