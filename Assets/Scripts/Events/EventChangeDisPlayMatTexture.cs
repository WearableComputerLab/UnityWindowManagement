using UnityEngine;
using uWindowCapture;

namespace Events
{
    /// <summary>
    /// Select the thumbnail window and assign a texture to newmesh
    /// </summary>
    public class EventChangeDisPlayMatTexture
    {
        public UwcWindow Window;
        public string windowName;
        public Texture2D texture2D;
    }
}