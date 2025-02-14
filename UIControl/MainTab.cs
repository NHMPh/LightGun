using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LightGun.UIControl
{
    internal class MainTab
    {


        private LightGunCompoment.LightGun lightGunP1;
        private LightGunCompoment.LightGun lightGunP2;

        public MainTab(LightGunCompoment.LightGun lightGunP1, LightGunCompoment.LightGun lightGunP2)
        {
            this.lightGunP1 = lightGunP1;
            this.lightGunP2 = lightGunP2;
        }

        public void Start(object? sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            bool isStartP1 = lightGunP1.GetIsStart();
            bool isStartP2 = lightGunP2.GetIsStart();

            lightGunP1.SetIsStart(!isStartP1);
            lightGunP2.SetIsStart(!isStartP2);

            if (!isStartP1 || !isStartP2)
            {
                button.BackColor = Color.Red;
                button.Text = "Stop";
            }
            else
            {
                button.BackColor = Color.Green;
                button.Text = "Start";
            }
        }
        public void ComBoxCamP1(object sender, EventArgs e)
        {
            int cameraIndex = (sender as ComboBox).SelectedIndex;
            if (lightGunP2.CamIndex == cameraIndex)
            {
                lightGunP2.CloseCamera();
            }
            lightGunP1.SetAndStartCamera(cameraIndex);
        }
        public void ComBoxCamP2(object sender, EventArgs e)
        {
            int cameraIndex = (sender as ComboBox).SelectedIndex;
            if (lightGunP1.CamIndex == cameraIndex)
            {
                lightGunP1.CloseCamera();
            }
            lightGunP2.SetAndStartCamera(cameraIndex);
        }
        public void ComBoxArP1(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1) return;
            string arduinoIndex = (sender as ComboBox).SelectedItem.ToString();
            lightGunP1.SetArduinoMouse(arduinoIndex);
            if (lightGunP1.ComPortString == "")
            {              
                (sender as ComboBox).SelectedIndex = -1;
            }



        }
        public void ComBoxArP2(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1) return;
            string arduinoIndex = (sender as ComboBox).SelectedItem.ToString();
            lightGunP2.SetArduinoMouse(arduinoIndex); ;
            if (lightGunP2.ComPortString == "")
            {
                (sender as ComboBox).SelectedIndexChanged -= ComBoxArP2;
                (sender as ComboBox).SelectedIndex = -1;
                (sender as ComboBox).SelectedIndexChanged += ComBoxArP2;
            }
        }

        public Bitmap picBoxRawP1()
        {
            return lightGunP1.GetRawImage();
        }
        public Bitmap picBoxRawP2()
        {
            return lightGunP2.GetRawImage();
        }
        public Bitmap picBoxProP1()
        {
            return lightGunP1.GetProcessImage();
        }
        public Bitmap picBoxProP2()
        {
            return lightGunP2.GetProcessImage();
        }
        //
        public void tTrackBarP1(object sender, EventArgs e)
        {
            lightGunP1.SetProcessorThreadHold((sender as TrackBar).Value);
        }
        public void bTrackBarP1(object sender, EventArgs e)
        {
            lightGunP1.SetCameraBrightness((sender as TrackBar).Value);
        }
        public void cTrackBarP1(object sender, EventArgs e)
        {
            lightGunP1.SetCameraContrast((sender as TrackBar).Value);
        }
        public void gTrackBarP1(object sender, EventArgs e)
        {
            lightGunP1.SetCameraGamma((sender as TrackBar).Value);
        }
        public void eTrackBarP1(object sender, EventArgs e)
        {
            lightGunP1.SetCameraExposure((sender as TrackBar).Value);
        }
        public void tTrackBarP2(object sender, EventArgs e)
        {
            lightGunP2.SetProcessorThreadHold((sender as TrackBar).Value);
        }
        public void bTrackBarP2(object sender, EventArgs e)
        {
            lightGunP2.SetCameraBrightness((sender as TrackBar).Value);
        }
        public void cTrackBarP2(object sender, EventArgs e)
        {
            lightGunP2.SetCameraContrast((sender as TrackBar).Value);
        }
        public void gTrackBarP2(object sender, EventArgs e)
        {
            lightGunP2.SetCameraGamma((sender as TrackBar).Value);
        }
        public void eTrackBarP2(object sender, EventArgs e)
        {
            lightGunP2.SetCameraExposure((sender as TrackBar).Value);
        }
    }
}
