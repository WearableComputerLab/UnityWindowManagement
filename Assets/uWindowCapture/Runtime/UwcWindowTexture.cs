using UnityEngine;
using System.Collections.Generic;

namespace uWindowCapture
{

    public class UwcWindowTexture : MonoBehaviour
    {
        bool shouldUpdateWindow_ = true;
        bool shouldUpdateWindow
        {
            get
            {
                return shouldUpdateWindow_;
            }
            set
            {
                if (value && searchTiming == WindowSearchTiming.Manual) return;
                shouldUpdateWindow_ = value;
            }
        }

        [SerializeField]
        WindowSearchTiming searchTiming_ = WindowSearchTiming.OnlyWhenParameterChanged;
        public WindowSearchTiming searchTiming
        {
            get
            {
                return searchTiming_;
            }
            set
            {
                searchTiming_ = value;
                if (searchTiming_ == WindowSearchTiming.Manual)
                {
                    shouldUpdateWindow = false;
                }
                else
                {
                    shouldUpdateWindow = true;
                }
            }
        }

        [SerializeField]
        WindowTextureType type_ = WindowTextureType.Window;
        public WindowTextureType type
        {
            get
            {
                return type_;
            }
            set
            {
                shouldUpdateWindow = true;
                type_ = value;
            }
        }

        [SerializeField]
        bool altTabWindow_ = false;
        public bool altTabWindow
        {
            get
            {
                return altTabWindow_;
            }
            set
            {
                shouldUpdateWindow = true;
                altTabWindow_ = value;
            }
        }

        [SerializeField]
        bool createChildWindows_ = true;
        public bool createChildWindows
        {
            get
            {
                return createChildWindows_;
            }
            set
            {
                createChildWindows_ = value;

                var manager = GetComponent<UwcWindowTextureChildrenManager>();
                if (createChildWindows_)
                {
                    if (!manager)
                    {
                        gameObject.AddComponent<UwcWindowTextureChildrenManager>();
                    }
                }
                else
                {
                    if (manager)
                    {
                        Destroy(manager);
                    }
                }
            }
        }

        public GameObject childWindowPrefab;
        public float childWindowZDistance = 0.02f;

        [SerializeField]
        string partialWindowTitle_;
        public string partialWindowTitle{
            get
            {
                return partialWindowTitle_;
            }
            set
            {
                shouldUpdateWindow = true;
                partialWindowTitle_ = value;
            }
        }

        [SerializeField]
        int desktopIndex_ = 0;
        public int desktopIndex
        {
            get
            {
                return desktopIndex_;
            }
            set
            {
                shouldUpdateWindow = true;
                desktopIndex_ = (UwcManager.desktopCount > 0) ?
                    Mathf.Clamp(value, 0, UwcManager.desktopCount - 1) : 0;
            }
        }

        public CaptureMode captureMode = CaptureMode.Auto;
        public CapturePriority capturePriority = CapturePriority.Auto;
        public WindowTextureCaptureTiming captureRequestTiming = WindowTextureCaptureTiming.OnlyWhenVisible;
        public int captureFrameRate = 30;
        public bool drawCursor = true;
        public bool updateTitle = true;
        public bool searchAnotherWindowWhenInvalid = false;

        public WindowTextureScaleControlType scaleControlType = WindowTextureScaleControlType.BaseScale;
        public float scalePer1000Pixel = 1f;
        public bool updateScaleForcely = false;

        static HashSet<UwcWindowTexture> list_ = new HashSet<UwcWindowTexture>();
        public static HashSet<UwcWindowTexture> list
        {
            get { return list_; }
        }

        UwcWindow window_;
        public UwcWindow window
        {
            get
            {
                return window_;
            }
            set
            {
                if (window_ == value)
                {
                    return;
                }

                if (window_ != null)
                {
                    window_.onCaptured.RemoveListener(OnCaptured);
                }

                var old = window_;
                window_ = value;
                onWindowChanged_.Invoke(window_, old);

                if (window_ != null)
                {
                    shouldUpdateWindow = false;
                    window_.onCaptured.AddListener(OnCaptured);
                    window_.RequestCapture(CapturePriority.High);
                }
            }
        }

        public UwcWindowTextureManager manager { get; set; }
        public UwcWindowTexture parent { get; set; }

        UwcWindowChangeEvent onWindowChanged_ = new UwcWindowChangeEvent();
        public UwcWindowChangeEvent onWindowChanged
        {
            get { return onWindowChanged_; }
        }

        float basePixel
        {
            get { return 1000f / scalePer1000Pixel; }
        }

        public bool isValid
        {
            get
            {
                return window != null && window.isValid;
            }
        }

        Material material_;
        Renderer renderer_;
        MeshFilter meshFilter_;
        Collider collider_;
        float captureTimer_ = 0f;
        bool isCaptureRequested_ = false;
        bool hasBeenCaptured_ = false;

        void Awake()
        {
            renderer_ = GetComponent<Renderer>();
            material_ = renderer_.material; // clone
            meshFilter_ = GetComponent<MeshFilter>();
            collider_ = GetComponent<Collider>();

            list_.Add(this);
        }

        void OnDestroy()
        {
            list_.Remove(this);
        }

        void Update()
        {
            UpdateSearchTiming();
            UpdateTargetWindow();

            if (!isValid)
            {
                material_.mainTexture = null;
                return;
            }

            UpdateTexture();
            UpdateRenderer();
            UpdateScale();
            UpdateTitle();
            UpdateCaptureTimer();
            UpdateRequestCapture();

            UpdateBasicComponents();
        }

        void OnWillRenderObject()
        {
            if (!isCaptureRequested_) return;

            if (captureRequestTiming == WindowTextureCaptureTiming.OnlyWhenVisible)
            {
                RequestCapture();
            }
        }

        void UpdateTexture()
        {
            if (!isValid) return;

            window.cursorDraw = drawCursor;

            if (material_.mainTexture != window.texture)
            {
                material_.mainTexture = window.texture;
            }
        }

        void UpdateRenderer()
        {
            if (hasBeenCaptured_)
            {
                renderer_.enabled = !window.isIconic && window.isVisible;
            }
        }

        void UpdateScale()
        {
            if (!isValid || (!updateScaleForcely && window.isChild)) return;

            var scale = transform.localScale;

            switch (scaleControlType)
            {
                case WindowTextureScaleControlType.BaseScale:
                    {
                        var extents = meshFilter_.sharedMesh.bounds.extents;
                        var meshWidth = extents.x * 2f;
                        var meshHeight = extents.y * 2f;
                        var baseHeight = meshHeight * basePixel;
                        var baseWidth = meshWidth * basePixel;
                        scale.x = window.width / baseWidth;
                        scale.y = window.height / baseHeight;
                        break;
                    }
                case WindowTextureScaleControlType.FixedWidth:
                    {
                        scale.y = transform.localScale.x * window.height / window.width;
                        break;
                    }
                case WindowTextureScaleControlType.FixedHeight:
                    {
                        scale.x = transform.localScale.y * window.width / window.height;
                        break;
                    }
                case WindowTextureScaleControlType.Manual:
                    {
                        break;
                    }
            }

            if (float.IsNaN(scale.x)) scale.x = 0f;
            if (float.IsNaN(scale.y)) scale.y = 0f;

            //确保投影窗口与划定范围一致
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        void UpdateTitle()
        {
            if (updateTitle && isValid)
            {
                window.RequestUpdateTitle();
            }
        }

        void UpdateCaptureTimer()
        {
            if (captureFrameRate < 0)
            {
                captureTimer_ = 0f;
                isCaptureRequested_ = true;
            }
            else
            {
                captureTimer_ += Time.deltaTime;

                float T = 1f / captureFrameRate;
                if (captureTimer_ < T) return;

                while (captureTimer_ > T)
                {
                    captureTimer_ -= T;
                }
            }

            isCaptureRequested_ = true;
        }

        void UpdateRequestCapture()
        {
            if (!isCaptureRequested_) return;

            if (captureRequestTiming == WindowTextureCaptureTiming.EveryFrame)
            {
                RequestCapture();
            }
        }

        void UpdateSearchTiming()
        {
            if (searchTiming == WindowSearchTiming.Always)
            {
                shouldUpdateWindow = true;
            }
        }

        void UpdateTargetWindow()
        {
            if (!shouldUpdateWindow) return;

            switch (type)
            {
                case WindowTextureType.Window:
                    window = UwcManager.Find(partialWindowTitle, altTabWindow);
                    break;
                case WindowTextureType.Desktop:
                    window = UwcManager.FindDesktop(desktopIndex);
                    break;
                case WindowTextureType.Child:
                    break;
            }
        }

        void UpdateBasicComponents()
        {
            if (renderer_) renderer_.enabled = isValid;
            if (collider_) collider_.enabled = isValid;
        }

        void OnCaptured()
        {
            hasBeenCaptured_ = true;
        }

        public void RequestCapture()
        {
            if (!isValid) return;

            isCaptureRequested_ = false;
            window.captureMode = captureMode;

            var priority = capturePriority;
            if (priority == CapturePriority.Auto)
            {
                priority = CapturePriority.Low;
                if (window == UwcManager.cursorWindow)
                {
                    priority = CapturePriority.High;
                }
                else if (window.zOrder < UwcSetting.MiddlePriorityMaxZ)
                {
                    priority = CapturePriority.Middle;
                }
            }

            window.RequestCapture(priority);
        }

        public void RequestWindowUpdate()
        {
            shouldUpdateWindow = true;
        }

        static public RayCastResult RayCast(Vector3 from, Vector3 dir, float distance, LayerMask layerMask)
        {
            var ray = new Ray();
            ray.origin = from;
            ray.direction = dir;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distance, layerMask))
            {
                var collider = hit.collider;
                var texture =
                    collider.GetComponent<UwcWindowTexture>() ??
                    collider.GetComponentInChildren<UwcWindowTexture>();
                if (texture)
                {
                    var window = texture.window;
                    var meshFilter = texture.GetComponent<MeshFilter>();
                    if (window != null && meshFilter && meshFilter.sharedMesh)
                    {
                        var localPos = texture.transform.InverseTransformPoint(hit.point);
                        var meshScale = 2f * meshFilter.sharedMesh.bounds.extents;
                        var windowLocalX = (int)((localPos.x / meshScale.x + 0.5f) * window.width);
                        var windowLocalY = (int)((0.5f - localPos.y / meshScale.y) * window.height);
                        var desktopX = window.x + windowLocalX;
                        var desktopY = window.y + windowLocalY;
                        return new RayCastResult
                        {
                            hit = true,
                            texture = texture,
                            position = hit.point,
                            normal = hit.normal,
                            windowCoord = new Vector2(windowLocalX, windowLocalY),
                            desktopCoord = new Vector2(desktopX, desktopY),
                        };
                    }
                }
            }

            return new RayCastResult()
            {
                hit = false,
            };
        }

        //Function to capture a specific area of the window texture. This area is defined by the position and size of the cropping rectangle
        public Texture2D CaptureCroppedArea(RectTransform cropRect)
        {
            // Get the current texture of the window. This represents the entire window's content.
            Texture2D originalTexture = this.GetComponent<Texture2D>();

            // Calculate the starting position of the cropped area.
            // This is the bottom-left corner of the cropping rectangle.
            Vector2Int startPos = new Vector2Int(Mathf.FloorToInt(cropRect.anchoredPosition.x), Mathf.FloorToInt(cropRect.anchoredPosition.y));

            // Calculate the ending position of the cropped area.
            // This is the top-right corner of the cropping rectangle.
            Vector2Int endPos = startPos + new Vector2Int(Mathf.FloorToInt(cropRect.sizeDelta.x), Mathf.FloorToInt(cropRect.sizeDelta.y));

            // Create a new texture that will store the cropped area.
            // Its size is defined by the width and height of the cropping rectangle.
            Texture2D croppedTexture = new Texture2D(endPos.x - startPos.x, endPos.y - startPos.y);

            // Get the pixels from the original texture that lie within the cropping rectangle.
            // Then, set these pixels to the cropped texture.
            croppedTexture.SetPixels(originalTexture.GetPixels(startPos.x, startPos.y, endPos.x - startPos.x, endPos.y - startPos.y));

            // Apply the changes to the cropped texture to finalize it.
            croppedTexture.Apply();

            // Return the cropped texture.
            return croppedTexture;
        }


    }

}