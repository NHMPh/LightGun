using ArduinoUploader.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGun.UIControl
{
    internal class FirmwareUploadTab
    {
        private LightGunCompoment.LightGun lightGunP1;
        private LightGunCompoment.LightGun lightGunP2;
        public FirmwareUploadTab(LightGunCompoment.LightGun lightGunP1, LightGunCompoment.LightGun lightGunP2)
        {
            this.lightGunP1 = lightGunP1;
            this.lightGunP2 = lightGunP2;
        }

        public void UploadFirmwareP1(ArduinoModel model)
        {
            if(lightGunP1.GetIsStart())
            {
                MessageBox.Show("Please stop the arduino first");
                return;
            }

            if(lightGunP1.IsArduinoOpen())
            lightGunP1.UploadFirmware(model);
            else
                MessageBox.Show("Please connect the arduino first");
        }
        public void UploadFirmwareP2(ArduinoModel model)
        {
            if (lightGunP2.GetIsStart())
            {
                MessageBox.Show("Please stop the arduino first");
                return;
            }

            if (lightGunP2.IsArduinoOpen())
                lightGunP2.UploadFirmware(model);
            else
                MessageBox.Show("Please connect the arduino first");
        }
    }
}
