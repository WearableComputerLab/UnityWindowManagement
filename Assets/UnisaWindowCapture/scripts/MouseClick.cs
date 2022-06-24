//This script was an attempt at mouse interaction functionality and was ultimately not used in this project
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using uWindowCapture;

public class MouseClick : MonoBehaviour
{
    [DllImport("user32.dll", EntryPoint = "SendMessageA")]
    public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

    //[DllImport("user32.dll")]
    //static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
    //public UwcWindow window { get; set; }


    void Update()//This is just our first attempt at the mouse click function.
    {
        string win = gameObject.GetComponent<UwcWindowTexture>().partialWindowTitle;


        RaycastHit hit;
        Vector2 clickedUvCoords;
        Collider someCollider = gameObject.GetComponent<MeshCollider>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //IntPtr hwnd = (IntPtr)FindWindow(window.title, null);
        const int WM_LBUTTONDOWN = 0x201;
        const int WM_LBUTTONUP = 0x202;

        if (Input.GetMouseButtonDown(0))
        {
            IntPtr maindHwnd = new IntPtr(0);
            maindHwnd = FindWindow(null, "Spyder (Python 3.7)");
            //int inthwnd = int.Parse(win);
            //IntPtr maindHwnd = new IntPtr(inthwnd);

            if (someCollider.Raycast(ray, out hit, Mathf.Infinity))
            {
                clickedUvCoords = hit.textureCoord;

                float x = someCollider.bounds.size.x;
                float y = someCollider.bounds.size.y;
                clickedUvCoords.x *= x * 10;
                clickedUvCoords.y *= y * 10;


                int x1 = Convert.ToInt32(clickedUvCoords.x);
                int x2 = (int)x1;
                int y1 = Convert.ToInt32(clickedUvCoords.y);
                int y2 = (int)y1;

                SendMessage(maindHwnd, WM_LBUTTONDOWN, IntPtr.Zero, new IntPtr(x2 + (y2 << 16)));
                SendMessage(maindHwnd, WM_LBUTTONUP, IntPtr.Zero, new IntPtr(x2 + (y2 << 16)));

                Debug.Log(clickedUvCoords);
                Debug.Log(maindHwnd);
                Debug.Log(x2);
                return;
            }
        }
    }
}
