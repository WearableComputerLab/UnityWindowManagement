using System;
using Cinemachine;
using DG.Tweening;
using Events;
using QFramework;
using UnityEngine;

//This script is one of the scripts that control the camera (also the script being used in the current project), which aims to make the angle of view rotate and zoom around the cube through the mouse.
//Part of this script is quoted from the CSDN community (originally used for Android development),and part of it Edited and rewritten.

public class VCameraScrollWhereClamp : MonoSingleton<VCameraScrollWhereClamp>
{
    private CinemachineFramingTransposer _VCFramingTran;
    public bool isMove = true;
    public float cameraPCXMoveSpeed = 450;
    public float cameraPCYMoveSpeed = 200;
    public float cameraAndroidXMoveSpeed = 80;
    public float cameraAndroidYMoveSpeed = 40;
   [Header("Pulley Control Camera Distance Object Max Min")]
    public Vector2 camDistance = new Vector2(3, 15);
    public float scrollSpedd = 1.5f;
    [Header("Pulley Control FOV Variation Max Min")]
    public Vector2 camFOVClamp= new Vector2(40, 60);
   
    private float inputScrollWhere = 0;
     [Header("Whether to enable the pulley")]
    public bool enableScrol = false;
    private CinemachinePOV _VCPov;

    //Record the last touch position of the mobile phone to determine whether the user is zooming in or zooming out on the left
    
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;
 
 
    private Vector2 lastSingleTouchPosition;
    //zoom factor
    public float scaleFactor = 10f;
    
    private bool m_IsSingleFinger;

    private string lastScaleDelta;
    private bool _isenterUI;
    [Header("Whether to control the camera")]
    public bool openController = true;
    [Header("Two variation modes of pulley control Distance FOV")]
    public EScrolType scrolType;
    private CinemachineVirtualCamera _vcamera;
    public CinemachineVirtualCamera VCamera=>_vcamera;

    private bool autoPlayRotate=false;
 

    public bool AutoPlayRotate => autoPlayRotate;
    [Header("Auto-rotate speed")]
    public float autorotateSpeed = 2f;

    public bool isController=true;
    void Awake()
    {
         _vcamera = GetComponent<CinemachineVirtualCamera>();
        _VCFramingTran = _vcamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _VCPov = _vcamera.GetCinemachineComponent<CinemachinePOV>();

        InitCameraSpeed();

       // TypeEventSystem.Register<EventInOutUI>(OnInOutUI);

       TypeEventSystem.Global.Register<EventVCameraScrollWhereClampCtrol>(va =>
       {
           isController = va.isCtrol;
           _VCPov.m_HorizontalAxis.m_InputAxisName = "";
           _VCPov.m_VerticalAxis.m_InputAxisName = "";
       });
    }

    /// <summary>
    /// True to open the control False to stop the control. 
    /// </summary>
    /// <param name="iscontroller"></param>
    public void SetController(bool iscontroller)
    {
        openController = iscontroller;
        if (!iscontroller)
        {
            _VCPov.m_HorizontalAxis.m_InputAxisName = "";
            _VCPov.m_VerticalAxis.m_InputAxisName = "";
            _VCPov.m_HorizontalAxis.m_InputAxisValue = 0;
            _VCPov.m_VerticalAxis.m_InputAxisValue = 0;
        }
    }
    void InitCameraSpeed()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                _VCPov.m_HorizontalAxis.m_MaxSpeed = cameraPCXMoveSpeed;
                _VCPov.m_VerticalAxis.m_MaxSpeed = cameraPCYMoveSpeed;
                Debug.Log("Editor");
                break;
            case RuntimePlatform.WindowsPlayer:
                _VCPov.m_HorizontalAxis.m_MaxSpeed = cameraPCXMoveSpeed*2;
                _VCPov.m_VerticalAxis.m_MaxSpeed = cameraPCYMoveSpeed*2;
                Debug.Log("PC");
                break;
 
            case RuntimePlatform.Android:
                Debug.Log("Android");
                _VCPov.m_HorizontalAxis.m_MaxSpeed = cameraAndroidXMoveSpeed;
                _VCPov.m_VerticalAxis.m_MaxSpeed = cameraAndroidYMoveSpeed;
                break;
            // case RuntimePlatform.WebGLPlayer:
            //     _VCPov.m_HorizontalAxis.m_MaxSpeed= JSlibUtility.GetMouseXSpeed();
            //     _VCPov.m_VerticalAxis.m_MaxSpeed= JSlibUtility.GetMouseYSpeed();
            //     break;
        }
    }
    /// <summary>
    /// Control the camera's X-axis rotation
    /// </summary>
    /// <param name="autoPlay"></param>

    public void ControllerHorizontalValueAutoRotate(bool autoPlay)
    {
        autoPlayRotate = autoPlay;
        openController = !autoPlay;
        //_VCPov.m_HorizontalAxis.m_InputAxisValue = 0;
      //  _VCPov.m_VerticalAxis.m_InputAxisValue = 0;
      if (autoPlay)
      {
          _VCPov.m_HorizontalAxis.m_InputAxisName = "";
          _VCPov.m_VerticalAxis.m_InputAxisName = "";
          DOTween.To(x => _VCPov.m_VerticalAxis.Value  = x, _VCPov.m_VerticalAxis.Value, 0, 3f)
              .SetEase(Ease.Linear)
              .SetAutoKill();
      }
      else
      {
         // InitCameraSpeed();
      }
    }

    private void XAutoRotate()
    {
        
        _VCPov.m_HorizontalAxis.Value += Time.deltaTime*autorotateSpeed;
        
    }
    // private void OnInOutUI(EventInOutUI obj)
    // {
    //     _isenterUI = obj.isEnterUI;
    // }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log( "H "+_VCPov.m_HorizontalAxis.m_MaxSpeed +" V "+_VCPov.m_VerticalAxis.m_MaxSpeed);
        if (!isController)
        {
            return;
        }
        if (!openController)
        {
            if (autoPlayRotate)
            {
               XAutoRotate();
            }
            return;
        }
       
        if (_VCPov)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WebGLPlayer:
                case RuntimePlatform.WindowsPlayer:
                    if (isMove)
                    {
                        if (_isenterUI)
                        {
                            _VCPov.m_HorizontalAxis.m_InputAxisName = "";
                            _VCPov.m_VerticalAxis.m_InputAxisName = "";
                            _VCPov.m_HorizontalAxis.m_InputAxisValue = 0;
                            _VCPov.m_VerticalAxis.m_InputAxisValue = 0;
                            return;
                        }
                        // Debug.Log("PC");
                        _VCPov.m_HorizontalAxis.m_InputAxisName = Input.GetMouseButton(0) ? "Mouse X" : "";
                        _VCPov.m_VerticalAxis.m_InputAxisName = Input.GetMouseButton(0) ? "Mouse Y" : "";
                        if (Input.GetMouseButtonUp(0))
                        {
                            _VCPov.m_HorizontalAxis.m_InputAxisValue = 0;
                            _VCPov.m_VerticalAxis.m_InputAxisValue = 0;
                        }
                    }
                  

                    if (enableScrol)
                    {
                        inputScrollWhere = Input.GetAxis("Mouse ScrollWheel");
                       
                        if ( inputScrollWhere != 0)
                        {
                            //Debug.Log( inputScrollWhere);
                            switch (scrolType)
                            {
                                case EScrolType.distance:
                                    if (_VCFramingTran)
                                    {
                                        _VCFramingTran.m_CameraDistance = ChangeCameDistance(_VCFramingTran.m_CameraDistance,
                                            inputScrollWhere * scrollSpedd);
                                    }
                                    else
                                    {
                                        Debug.LogError("_VCFramingTran is null");
                                    }
                                   
                                    break;
                                case EScrolType.FOV:
                                     _vcamera.m_Lens.FieldOfView= ChangeCameFOV(_vcamera.m_Lens.FieldOfView, inputScrollWhere * scrollSpedd*10);
                                  //  Debug.Log( _vcamera.m_Lens.FieldOfView);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                           
                        }
                    }

                    break;
                case RuntimePlatform.Android:
                    if (Input.touchCount == 1)
                    {
                        isMove = true;
                        if (Input.GetTouch(0).phase == TouchPhase.Began || !m_IsSingleFinger)
                        {
                            //Record the location of the touch when you start the touch or when you release the two-word finger
                           
                            lastSingleTouchPosition = Input.GetTouch(0).position;
                        }

                        if (isMove)
                        {

                            _VCPov.m_HorizontalAxis.m_InputAxisName =
                                Input.touches[0].phase == TouchPhase.Moved ? "Mouse X" : "";
                            _VCPov.m_VerticalAxis.m_InputAxisName =
                                Input.touches[0].phase == TouchPhase.Moved ? "Mouse Y" : "";
                        }
                        if (Input.touches[0].phase == TouchPhase.Ended)
                        {
                            _VCPov.m_HorizontalAxis.m_InputAxisValue = 0;
                            _VCPov.m_VerticalAxis.m_InputAxisValue = 0;
                        }
                        m_IsSingleFinger = true;
                    }else if (Input.touchCount > 1)
                    {
                        isMove = false;
                        //When going from a single-finger touch to a multi-finger touch, record the position of the touch
                        //Make sure that the calculated zoom starts from a two-finger finger touch
                        
                        if (m_IsSingleFinger)
                        {
                            oldPosition1 = Input.GetTouch(0).position;
                            oldPosition2 = Input.GetTouch(1).position;
                        }
                        if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                        {
                            if (enableScrol)
                            {
                                ScaleCamera();
                            }
                        }
                        oldPosition1 = Input.GetTouch(0).position;
                        oldPosition2 = Input.GetTouch(1).position;
                        m_IsSingleFinger = false;
                    }
                    //Debug.Log("Android");
                    break;
            }
        }
    }

    private void ScaleCamera()
    {

        //Calculate the position of the current two-point touch point Calculate the position of the current two-point touch point
       
        var tempPosition1 = Input.GetTouch(0).position;
            var tempPosition2 = Input.GetTouch(1).position;
 
            float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
            float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);

            var temp = currentTouchDistance - lastTouchDistance <= 0
                ? -Mathf.Clamp01(Mathf.Abs(currentTouchDistance - lastTouchDistance))
                : Mathf.Clamp01(currentTouchDistance - lastTouchDistance);
        // temp = currentTouchDistance - lastTouchDistance;
        //var delta = temp.ToString("F");
        //Calculate the distance gap between the last and this two-finger touch
        //Then go to change the distance of the camera
        
        var distance = temp * scaleFactor * Time.deltaTime;
           
             Debug.Log(""+currentTouchDistance+"-"+lastTouchDistance+"="+(currentTouchDistance - lastTouchDistance+"   "+distance));
            
                var delta = distance;
                switch (scrolType)
                {
                    case EScrolType.distance:
                        if (_VCFramingTran)
                        {
                            _VCFramingTran.m_CameraDistance =
                                ChangeCameDistance(_VCFramingTran.m_CameraDistance, 8 * distance);
                        }
                        break;
                    case EScrolType.FOV:
                        _vcamera.m_Lens.FieldOfView= ChangeCameFOV(_vcamera.m_Lens.FieldOfView, distance *8);
                        Debug.Log( _vcamera.m_Lens.FieldOfView);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
        
    }

    private float ChangeCameDistance(float outdis, float dis)
    {
        outdis -= dis;
        outdis = Mathf.Clamp(outdis, camDistance.x, camDistance.y);
        return outdis;
    }
    private float ChangeCameFOV(float outdis, float dis)
    {
        outdis -= dis;
        outdis = Mathf.Clamp(outdis, camFOVClamp.x, camFOVClamp.y);
        return outdis;
    }
    private void OnDestroy()
    {
       // TypeEventSystem.UnRegister<EventInOutUI>(OnInOutUI);
    }

    public enum EScrolType
    {
        distance,
        FOV
    }
}