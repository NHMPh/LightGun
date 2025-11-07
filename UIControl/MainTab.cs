using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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
        public void ComBoxCamP1(object sender, EventArgs e)
        {
            int cameraIndex = (sender as ComboBox).SelectedIndex;
            if (cameraIndex == -1) return;
            
            lightGunP1.SetIsStart(false);
            
            if (lightGunP2.CamIndex == cameraIndex)
            {
                lightGunP2.CloseCamera();
            }
            lightGunP1.SetAndStartCamera(cameraIndex);
        }
        public void ComBoxCamP2(object sender, EventArgs e)
        {
            int cameraIndex = (sender as ComboBox).SelectedIndex;
            if (cameraIndex == -1) return;
            lightGunP2.SetIsStart(false);
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
            if (lightGunP1.ComPortString == arduinoIndex) return;
            lightGunP1.SetIsStart(false);
            if (lightGunP2.ComPortString == arduinoIndex)
            {
                lightGunP2.CloseArduino();
            }
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
            if (lightGunP2.ComPortString == arduinoIndex) return;
            lightGunP2.SetIsStart(false);
            if (lightGunP1.ComPortString == arduinoIndex)
            {
                lightGunP1.CloseArduino();
            }
            lightGunP2.SetArduinoMouse(arduinoIndex); ;
            if (lightGunP2.ComPortString == "")
            {
                (sender as ComboBox).SelectedIndex = -1;
            }
        }
        public List<string> GetSortedWebcamsByLastArrivalDate()
        {
            List<(string Name, DateTime LastArrivalDate)> webcams = new List<(string, DateTime)>();

            foreach (var mo in new ManagementObjectSearcher(null, "SELECT * FROM Win32_PnPEntity WHERE Description LIKE '%camera%' OR Description LIKE '%video%'").Get().OfType<ManagementObject>())
            {
                var args = new object[] { new string[] { "DEVPKEY_Device_FriendlyName", "DEVPKEY_Device_LastArrivalDate" }, null };

                mo.InvokeMethod("GetDeviceProperties", args);

                var mbos = (ManagementBaseObject[])args[1]; // one mbo for each device property key

                var name = mbos[0].Properties.OfType<PropertyData>().FirstOrDefault(p => p.Name == "Data")?.Value;
                var lastArrivalDateStr = mbos[1].Properties.OfType<PropertyData>().FirstOrDefault(p => p.Name == "Data")?.Value;

                if (name != null && lastArrivalDateStr != null)
                {
                    DateTime lastArrivalDate;
                    if (DateTime.TryParseExact(lastArrivalDateStr.ToString().Substring(0, 14), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out lastArrivalDate))
                    {
                        webcams.Add((name.ToString(), lastArrivalDate));
                    }
                }
            }

            var sortedWebcams = webcams.OrderBy(w => w.LastArrivalDate).Select(w => w.Name).ToList();

            return sortedWebcams;
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
