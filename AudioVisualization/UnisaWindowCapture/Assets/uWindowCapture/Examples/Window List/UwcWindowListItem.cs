using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

namespace uWindowCapture
{

    [RequireComponent(typeof(Image))]
    public class UwcWindowListItem : MonoBehaviour
    {
        Image image_;
        [SerializeField] Color selected;
        [SerializeField] Color notSelected;

        public UwcWindow window { get; set; }
        public UwcWindowList list { get; set; }
        public UwcWindowTexture windowTexture { get; set; }

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(System.IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        public static extern bool IsWindow(System.IntPtr hWnd);

        [SerializeField] RawImage icon;
        [SerializeField] Text title;
        [SerializeField] Text x;
        [SerializeField] Text y;
        [SerializeField] Text z;
        [SerializeField] Text width;
        [SerializeField] Text height;
        [SerializeField] Text status;

        void Awake()
        {
            image_ = GetComponent<Image>();
            image_.color = notSelected;
        }

        void Update()
        {
            if (window == null) return;

            if (!window.hasIconTexture && !window.isIconic)
            {
                icon.texture = window.texture;
            }
            else
            {
                icon.texture = window.iconTexture;
            }

            //DeleteWindow();
            var windowTitle = window.title;
            title.text = string.IsNullOrEmpty(windowTitle) ? "-No Name-" : windowTitle;

            x.text = window.isMinimized ? "-" : window.x.ToString();
            y.text = window.isMinimized ? "-" : window.y.ToString();
            z.text = window.zOrder.ToString();

            width.text = window.width.ToString();
            height.text = window.height.ToString();

            status.text =
                window.isIconic ? "Iconic" :
                window.isZoomed ? "Zoomed" :
                "-";
        }
        public void OnClick()
        {
            /*var manager = list.windowTextureManager;
            if (windowTexture == null)
            {
                windowTexture = manager.AddWindowTexture(window);
                image_.color = selected;
            }
            else
            {
                manager.RemoveWindowTexture(window);
                windowTexture = null;
                image_.color = notSelected;
            }*/
            var ptrWnd = window.handle;
            if (image_.color == selected)
            {
                Debug.Log(IsWindow(ptrWnd));
                const int WM_CLOSE = 0x0010;
                if (ptrWnd != System.IntPtr.Zero && IsWindow(ptrWnd))
                {
                    SendMessage(ptrWnd, WM_CLOSE, 0, 0);
                    Debug.Log("in");
                }
                else
                {
                    ptrWnd = System.IntPtr.Zero;
                }
            }
        }
        public void DeleteWindow()
        {
            var ptrWnd = window.handle;
            if (image_.color == selected)
            {
                Debug.Log(IsWindow(ptrWnd));
                const int WM_CLOSE = 0x0010;
                if (ptrWnd != System.IntPtr.Zero && IsWindow(ptrWnd))
                {
                    SendMessage(ptrWnd, WM_CLOSE, 0, 0);
                    Debug.Log("in");
                }
                else
                {
                    ptrWnd = System.IntPtr.Zero;
                }
            }
        }
    }
}