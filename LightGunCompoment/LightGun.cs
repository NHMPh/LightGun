using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Emgu.CV.CvEnum;
using Emgu.CV.XPhoto;
using System.Diagnostics.Contracts;
using System.Text.Json;

namespace LightGun.LightGunCompoment
{
    public class LightGun
    {
        private int index = 0;

        public int Index { get { return index; } }


        private int camIndex = -1;

        public int CamIndex { get { return camIndex; } }

        private string comPortString = "";

        public string ComPortString { get { return comPortString; } }

        private int xOffset;
        private int yOffset;
        // Camera Property
        private int thresdhold;
        private int brightness;
        private int contrast;
        private int hue;
        private int saturation;
        private int sharpness;
        private int gamma;
        private int whiteBalance;
        private int exposure;

        public int Threshold { get { return thresdhold; } }
        public int Brightness { get { return brightness; } }
        public int Contrast { get { return contrast; } }
        public int Gamma { get { return gamma; } }
        public int Exposure { get { return Exposure; } }

        private bool isStart = false;

        private Camera camera = new Camera();
        private ImageProcessor processor = new ImageProcessor();
        private ArduinoMouse arduinoMouse = new ArduinoMouse();
        private Image<Bgr, byte> image;
        private Point point;


        private Settings settings;
        public LightGun(int index, Settings settings)
        {
            this.index = index;
            this.settings = settings;
            LoadSetting();
        }
        private void LoadSetting()
        {
            thresdhold = settings.Players[index].Threshold;
            brightness = settings.Players[index].Brightness;
            contrast = settings.Players[index].Contrast;
            exposure = settings.Players[index].Exposure;
            xOffset = settings.Players[index].Xoffset;
            yOffset = settings.Players[index].Yoffset;
        }
        public void CloseCamera()
        {
            camera.CloseCamera();
        }
        public void SetAndStartCamera(int index)
        {
            try
            {
                camIndex = index;
                camera.OpenCamera(index);
                camera.SetBrightness(brightness);
                camera.SetContrast(contrast);
                camera.SetExposure(exposure);
                processor.SetOffset(xOffset, yOffset);
                processor.SetThresdHold(thresdhold);
                Task.Run(async () => await StreamVideo());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void SetCameraBrightness(int brightness)
        {
            camera.SetBrightness(brightness);
            this.brightness = brightness;
            settings.Players[index].Brightness = brightness;
        }

        public void SetCameraContrast(int contrast)
        {
            camera.SetContrast(contrast);
            this.contrast = contrast;
            settings.Players[index].Contrast = contrast;
        }

        public void SetCameraHue(int hue)
        {
            camera.SetHue(hue);
            this.hue = hue;
        }

        public void SetCameraSharpness(int sharpness)
        {
            camera.SetSharpness(sharpness);
            this.sharpness = sharpness;
        }
        public void SetCameraSaturation(int saturation)
        {
            camera.SetSaturation(saturation);
            this.saturation = saturation;
        }
        public void SetCameraGamma(int gamma)
        {
            camera.SetGamma(gamma);
            this.gamma = gamma;
            settings.Players[index].Gamma = gamma;
        }

        public void SetCameraWhiteBalance(int whiteBalance)
        {
            camera.SetWhiteBalance(whiteBalance);
            this.whiteBalance = whiteBalance;
        }

        public void SetCameraExposure(int exposure)
        {
            camera.SetExposure(exposure);
            this.exposure = exposure;
            settings.Players[index].Exposure = exposure;
        }

        public void SetProcessorOffset(int xOffset, int yOffset)
        {
            this.xOffset += xOffset;
            this.yOffset += yOffset;
            processor.SetOffset(this.xOffset, this.yOffset);
            settings.Players[index].Xoffset = this.xOffset;
            settings.Players[index].Yoffset = this.yOffset;
        }


        public void SetProcessorThreadHold(int thresdhold)
        {
            processor.SetThresdHold(thresdhold);
            this.thresdhold = thresdhold;
            settings.Players[index].Threshold = thresdhold;
        }

        public void SetArduinoMouse(string comPort)
        {
            arduinoMouse.OpenPort(comPort);
            if(arduinoMouse.isOpen())
            comPortString = comPort;
        }
        public bool SetButton(int type, int index, byte buttonCode)
        {
            if (arduinoMouse.isOpen())
            {
                arduinoMouse.SendNewButtonAssignment(type, index, buttonCode);
                return true;
            }

            else
            {
                MessageBox.Show($"The arduino for player {this.index+1} is not open.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
                
        public void SaveButtonSetting(int index, int type, int selectedValue)
        {
            if (type == 0)
                settings.Players[this.index].NormalButton[index].SelectedIndex = selectedValue;
            else
                settings.Players[this.index].OffscreenButton[index].SelectedIndex = selectedValue;
        }
        public Bitmap GetRawImage()
        {
            return processor.GetRawImage();
        }
        public Bitmap GetProcessImage()
        {
            return processor.GetProcessImage();
        }
        public bool GetIsStart()
        {
            return isStart;
        }
        public void SetIsStart(bool state)
        {
            isStart = state;
        }

        private async Task StreamVideo()
        {
            while (true)
            {
                try
                {
                    //get image from the camera and feed to processor
                    image = camera.GetVideoFrame();
                    //processor contains raw and process images
                    point = processor.GetPointingCoordinate(image);
                    if (arduinoMouse.isOpen() && isStart)
                    {
                        arduinoMouse.SendCursorPos(point);
                    }

                    await Task.Delay(16);
                }
                catch
                {
                    // Handle exceptions
                }
            }
        }
    }
}