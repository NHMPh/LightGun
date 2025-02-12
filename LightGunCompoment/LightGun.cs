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

namespace LightGun.LightGunCompoment
{
    public class LightGun
    {
        private int index = 0;
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

        private bool isStart = false;

        private Camera camera = new Camera();
        private ImageProcessor processor = new ImageProcessor();
        private ArduinoMouse arduinoMouse = new ArduinoMouse();
        private Image<Bgr, byte> image;
        private Point point;
        public LightGun(int index)
        {
            this.index = index;
        }
        public LightGun(int xOffset, int yOffset, int thresdhold, int brightness, int contrast, int hue, int saturation, int sharpness, int gamma, int whiteBalance, int exposure)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.thresdhold = thresdhold;
            this.brightness = brightness;
            this.contrast = contrast;
            this.hue = hue;
            this.saturation = saturation;
            this.sharpness = sharpness;
            this.gamma = gamma;
            this.whiteBalance = whiteBalance;
            this.exposure = exposure;

        }

        public void SetAndStartCamera(int index)
        {
            try
            {
                camera.OpenCamera(index);
                //camera.SetBrightness(brightness);
                //camera.SetContrast(contrast);
                //camera.SetHue(hue);
                //camera.SetSharpness(sharpness);
                //camera.SetSaturation(saturation);
                //camera.SetWhiteBalance(whiteBalance);
                //camera.SetExposure(exposure);
                //processor.SetOffset(xOffset, yOffset);
                //processor.SetThresdHold(thresdhold);
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
        }

        public void SetCameraContrast(int contrast)
        {
            camera.SetContrast(contrast);
            this.contrast = contrast;
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
            camera.SetGamma(saturation);
            this.gamma = gamma;
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
        }

        public void SetProcessorOffset(int xOffset, int yOffset)
        {
            processor.SetOffset(xOffset, yOffset);
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        public void SetProcessorThreadHold(int thresdhold)
        {
            processor.SetThresdHold(thresdhold);
            this.thresdhold = thresdhold;
        }

        public void SetArduinoMouse(string comPort)
        {
            arduinoMouse.OpenPort(comPort);
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
                        arduinoMouse.SendCursorPos(point);
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