# UnityWindowManagement
## Overview  
This project is implemented based on Unity. Users can project local/remote desktop windows on the surface of 3D objects,and support users to perform a series of basic management and operations on windows.This project aims to provide a window capture tool to support users' related applications or work in VR/AR and other scenarios,and users can expand based on the work of this project.

## Project/Framework reference  
The work of this project is based on the work of the following mature projects:  
About cropping:  
https://forum.unity.com/threads/released-image-cropper-multiplatform-image-cropping-solution-with-oval-mask-support.526901/​

About window capture:  
https://github.com/hecomi/uWindowCapture

About the remote window link:  
https://github.com/cfloutier/Unity-VNC-Client

About some event-driven and UI:   
https://github.com/liangxiegame/QFramework

## How to use  
After entering the project, please find and open the scene **"UniSAWindowCapture"** to run the project. We recommend that you set the window display to "2 by 3"(Window/Layouts/2 by 3) and adjust the size of the Game window appropriately.If the buttons in the main interface are large, it is because the default interface will be maintained when the project is first opened with unity after downloading, it may be deformed after adjusting to "2 by 3". We can adjust the size appropriately (the above effects are limited to the three buttons in the main interface).  
<img src="https://github.com/WearableComputerLab/UnityWindowManagement/blob/UwcVersion_2022/1.png" width="375">  
Please click the following link to open the running demonstration of this project: https://github.com/WearableComputerLab/UnityWindowManagement/issues/1 

## Running precautions  
When customizing a window on the surface of a 3D object, be sure to define the four vertices of the window in the clockwise order of **"upper left - upper right - lower right - lower left"**, which is related to the logic of mesh generation in the project, other orders or chaotic clicks It will cause the window content to be reversed, mirrored, or unable to generate the window normally.  
<img src="https://github.com/WearableComputerLab/UnityWindowManagement/blob/UwcVersion_2022/2.png" width="375"><img src="https://github.com/WearableComputerLab/UnityWindowManagement/blob/UwcVersion_2022/3.png" width="400" high="375"> 

When the user is ready to project a window, please ensure that the window in the desktop is open. **Minimization** will cause the window content to be lost and will be displayed as blank on the thumbnail.  

Before performing the operation of VNC remote connection, please make sure that **TightVNC Server** is running on the target device.  
Learn more about tightVNC at https://www.tightvnc.com/  


User need to rename every time user add a new window and ensure that the names of the added windows are not repeated. The recognition and operation of windows (such as switching, deleting, etc.) are based on the new name of the window.  

## Remark  
Unity version: **Unity2020.3.16f1c1 Personal**  

Regarding mouse clicks, the project finally chose to keep the work of mouse interaction through the **hook principle** in the branch **"uwcVersion_2021"**,Hecomi(the author of uwindowcapture) gave another solution, please refer to: https://github.com/hecomi/uWindowCapture/issues/17  
It is worth noting that the method mentioned above is to operate the standard desktop windows. For this project, the windows are mostly irregular quadrangles, which may cause the window contents to be squeezed or deformed to varying degrees,in addition there is also the impact of clipping.So more factors need to be considered to fully realize the function.

The following project may be helpful for dynamic cropping: https://github.com/hecomi/uDesktopDuplication  
If the window cannot be displayed normally and an error about the GPU appears when running the above project,can try to turn off the discrete graphics card and use the integrated display, and then restart the project to solve the problem.

## Contact us
If have any questions in the process of using the project, please leave a message at at **issues** or contact us  
huazy041@mymail.unisa.edu.au




