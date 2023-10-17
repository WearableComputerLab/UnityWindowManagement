using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace CubeWindow
{
    public class DetectClick : MonoBehaviour
    {
        // Importing functions from user32.dll for simulating mouse clicks and converting points
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public uint Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        public struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        private IntPtr hWnd;

        private void Start()
        {
            hWnd = FindWindow("WindowClassName", "WindowName");
        }
        private void Update()
        {
            // Check for left mouse button click
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Perform raycast
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit object has the correct tag
                    if (hit.collider.gameObject.tag == "ScreenPlane")
                    {
                        // Obtain UV coordinates from the hit point
                        MeshCollider meshCollider = hit.collider as MeshCollider;
                        if (meshCollider == null || meshCollider.sharedMesh == null)
                        {
                            Debug.Log("MeshCollider not found or sharedMesh is null");
                            return;
                        }

                        // Fetch the UV coordinates where the click happened.
                        Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
                        Debug.Log("Texture coordinates of the mouse click: " + pixelUV);

                        //  Mapping the click coordinates to the screenshot.
                        Point point = new Point
                        {
                            X = (int)(pixelUV.x * Screen.width),
                            Y = (int)(pixelUV.y * Screen.height)
                        };

                        IntPtr hWnd = new IntPtr(/*window handle here */);
                        ClientToScreen(hWnd, ref point);

                        if (hWnd != IntPtr.Zero)
                        {
                            ClientToScreen(hWnd, ref point);

                            // Simulating the click event.
                            SimulateMouseClick(point.X, point.Y);
                        }
                        else
                        {
                            Debug.Log("Window handle not found");
                        }
                    }
                    else
                    { 
                        Debug.Log("Object not the targeted mesh / Vertex threshold exceeded");
                    }
                }
            }
        }
        // Method to simulate mouse clicks.
        private void SimulateMouseClick(int x, int y)
        {
            INPUT mouseDownInput = new INPUT
            {
                Type = 0, // Mouse input type
                Data = new MOUSEKEYBDHARDWAREINPUT
                {
                    Mouse = new MOUSEINPUT
                    {
                        X = x,
                        Y = y,
                        Flags = 0x0002, // Mouse move event
                    }
                }
            };

            INPUT mouseUpInput = new INPUT
            {
                Type = 0, // Mouse input type
                Data = new MOUSEKEYBDHARDWAREINPUT
                {
                    Mouse = new MOUSEINPUT
                    {
                        X = x,
                        Y = y,
                        Flags = 0x0004, // Mouse up event
                    }
                }
            };

            // Sending mouse down and mouse up events for a complete click
            SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
            SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        }
    }
}