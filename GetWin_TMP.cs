using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using uWindowCapture;


public class GetWin_TMP : MonoBehaviour
{
        public string winTitle;

        //ȷ���Ѵ�TextMesh Pro �л�ȡ�����ڱ���
        public void OnEvent()
        {
            string winTitle = GameObject.Find("InputEnterName/Text Area/Text").GetComponent<TMP_Text>().text;
            //GameObject.Find("MeshWindow").GetComponent<UwcWindowTexture>().partialWindowTitle = winTitle;
            Debug.Log(winTitle);
        }
}


