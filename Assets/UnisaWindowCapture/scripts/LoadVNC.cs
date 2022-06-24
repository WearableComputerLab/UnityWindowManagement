// This script was not used in the final version, in the early the group set the VNC screen to a prefab,
// and this script would instantiate the prefab, but this way may conflict with some features and consume more resources,
// (we didn't do more exploration on this issue after that£¬keep this script because the above way might be one of the possible ideas)

//Finally,the strategy we use is to directly use the VNC screen as a child of the thumbnail prefab(UIPopWindowsListPanel),
//and control the display and hiding of the screen during operation.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVNC : MonoBehaviour 
{
    // Start is called before the first frame update.
    public void openVNC()
    {
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        Vector3 pos = new Vector3(0, 0, 0);
        GameObject instance = (GameObject)Instantiate(Resources.Load("Prefabs/VNCcomponent"), pos, rot);
        instance.GetComponent<CreatMesh>().creatMesh();
    }


}
