using System;
using UnityEngine;
public class MirrorChange : MonoBehaviour
{
    void Update()
    {
        // On every frame, check if the left mouse button was pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera through the point where the mouse cursor is
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits a collider (which should be your window texture), then...
            if (Physics.Raycast(ray, out hit))
            {
                // Get the point on the texture where the mouse cursor is, in local (texture) coordinates
                Vector2 localPoint = hit.textureCoord;

                // Get the dimensions of the foreground window
                Rect rect = GetWindowRect(WinAPI.GetForegroundWindow());

                // Create a point in client coordinates that corresponds to the point on the texture that was clicked
                WinAPI.POINT point;
                point.X = (int)(localPoint.x * rect.width);
                point.Y = (int)(localPoint.y * rect.height);

                // Convert the point from Unity's coordinate system to the Windows screen coordinate system
                WinAPI.ScreenToClient(WinAPI.GetForegroundWindow(), ref point);

                // Send the click event to the window
                // The 'lParam' for these messages is the coordinates of the cursor, packed into a single IntPtr
                WinAPI.SendMessage(WinAPI.GetForegroundWindow(), WinAPI.WM_LBUTTONDOWN, IntPtr.Zero, new IntPtr(point.Y * 0x10000 + point.X));
                WinAPI.SendMessage(WinAPI.GetForegroundWindow(), WinAPI.WM_LBUTTONUP, IntPtr.Zero, new IntPtr(point.Y * 0x10000 + point.X));
            }
        }
    }

    // Helper method to get the dimensions of a window as a Rect
    private Rect GetWindowRect(IntPtr window)
    {
        WinAPI.RECT rect;
        WinAPI.GetWindowRect(window, out rect);

        return new Rect(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
    }
}
