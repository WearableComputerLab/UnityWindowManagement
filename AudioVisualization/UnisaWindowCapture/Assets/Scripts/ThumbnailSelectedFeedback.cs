using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThumbnailSelectedFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    public void ColorFeedback()
    {
        gameObject.GetComponent<Text>().color = Color.green;
    }  

}
