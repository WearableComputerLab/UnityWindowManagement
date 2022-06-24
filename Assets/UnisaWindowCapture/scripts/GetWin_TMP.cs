//This script was not used in the final version, its purpose is to get the window's title from the renamed panel
//so that it can be used in the "uwcWindowTexture" script to achieve the target window's projection.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using uWindowCapture;

namespace TMPwin 
{
    public class GetWin_TMP : MonoBehaviour
    {
        public string winTitle;
        public void OnEvent()
        {
            //Get the window title from TextMesh Pro. 
            string winTitle = GameObject.Find("InputEnterName/Text Area/Text").GetComponent<TMP_Text>().text;
            Debug.Log(winTitle);

            //Generate cube window and load title. 
            Quaternion rot = new Quaternion(0, 0, 0, 0);
            Vector3 pos = new Vector3(0, 0, 0);
            GameObject instance = (GameObject)Instantiate(Resources.Load("Prefabs/MeshWindow"), pos, rot);
            instance.name = winTitle;
            instance.GetComponent<CreatMesh>().creatMesh();
            instance.GetComponent<UwcWindowTexture>().partialWindowTitle = winTitle;
            
        }
    }
}

