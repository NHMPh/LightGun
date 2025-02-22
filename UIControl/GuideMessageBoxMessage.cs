using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGun.UIControl
{
    internal class GuideMessageBoxMessage
    {
        public void OverlayTabMessage1(object? sender, EventArgs e)
        {
            MessageBox.Show("Monitor the camera and Arduino connection here. The light gun can only start when both the camera and Arduino checkboxes are selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void OverlayTabMessage2(object? sender, EventArgs e)
        {
            MessageBox.Show("You can use shortcut key LShift + 1 to start Gun 1 and LShift + 2 to start Gun 2.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void BorderMessage(object? sender, EventArgs e)
        {
            MessageBox.Show("You can set the border width here and press LCtrl + B to toggle the border. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void CamMessage(object? sender, EventArgs e)
        {
            MessageBox.Show("Use the slider to adjust the camera properties. Threshold determines the minimum pixel value required for it to be considered white.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void SelectMessage(object? sender, EventArgs e)
        {
            MessageBox.Show("Choose your camera and Arduino here. If you just connected your device, click the Refresh button.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void ButtonAssignMessage(object? sender, EventArgs e)
        {
            MessageBox.Show("The normal button function is triggered when you press the digital pin Dx on your Arduino while pointing the gun at the screen. If pointed offscreen, it behaves differently.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void CalibrationMessage(object? sender, EventArgs e)
        {
            MessageBox.Show("Use the iron sights of your gun to adjust the mouse cursor position, aligning it with the gun’s sight by clicking/shooting the (+10, -10, +1, -1) buttons. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void FirmwareMessage(object? sender, EventArgs e)
        {
            MessageBox.Show("Use this to upload the firmware to a new Leonardo Arduino.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
