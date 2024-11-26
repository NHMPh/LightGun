# NHMPh Light gun

Just wanting to show you how to play light gun game with a DIY light gun with line of sight accuracy.


A program that use image processing to translate image from the light gun's webcam into mouse corrdinate.
## Table of Contents
- [Hardware Set Up](#installation)
- [Usage](#usage)
- [Demo](#features)
---
### 1. **Hardware Set Up**  
The light gun comprises a toy gun, a compact USB webcam (OV9726) and a USB mouse.

The camera is mounted on the barrel, while a switch is securely attached behind the trigger.

The switch is soldered to the PCB, replacing the mouse's left-click

---

### 2. **Usage**  

   - #### 0 Download and open NHMPh's Light gun software
     You can compile it yourself or download the ZIP file.
  - #### 1 Set up the webcam
     Select your webcam from the options here.
    [image]

  - #### 2 Enable the overlay border
     Hold ``LeftCtrl + B`` to enable the overlay border.

     Hold ``LeftCtrl + B`` again to turn it off.

     You can also set the border width to make it more visiable.

     Note: The border will not be visible if the game is in ***fullscreen mode***. Ensure that the game is set to either windowed mode or borderless windowed mode.
     
  - #### 3 Configure the webcam property
    
    [image]
    
     Ensure that the camera can consistently detect the white border on the screen. You should Experiment with the threshold, contrast, gamma, and exposure settings to achieve optimal performance.
  - #### 4 Start and calibrate the gun

    Start your gun by holding  ``LeftShift + B``

    Note: This will take over your current mouse.
    
    Use your gun sight to adjust the mouse position by clicking on the button

    [image]

 - #### 5 Have fun
   Be sure to save your setting and turn on the overlay border when you start your game. I recommend THOTD2, THOTD3, hotd overkill, or any light gun games that can be control with a usb mouse.

  ### 3. **Demo**  
  [Video]
