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

namespace LightGun
{
    public class LightGun
    {
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

        private Camera camera = new Camera();
        private ImageProcessor processor = new ImageProcessor();
        private ArduinoMouse arduinoMouse = new ArduinoMouse();
        private Image<Bgr, byte> image;
        private Point point;

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

        public void SetCamera(int index)
        {
            camera.OpenCamera(index);
            camera.SetBrightness(brightness);
            camera.SetContrast(contrast);
            camera.SetHue(hue);
            camera.SetSharpness(sharpness);
            camera.SetSaturation(saturation);
            camera.SetWhiteBalance(whiteBalance);
            camera.SetExposure(exposure);
            processor.SetOffset(xOffset, yOffset);
            processor.SetThresdHold(thresdhold);
            Task.Run(async () => await StreamVideo());
        }

        public void SetCameraBrightness(double brightness)
        {
            camera.SetBrightness(brightness);
        }

        public void SetCameraContrast(double contrast)
        {
            camera.SetContrast(contrast);
        }

        public void SetCameraHue(double hue)
        {
            camera.SetHue(hue);
        }

        public void SetCameraSharpness(double sharpness)
        {
            camera.SetSharpness(sharpness);
        }

        public void SetCameraSaturation(double saturation)
        {
            camera.SetSaturation(saturation);
        }

        public void SetCameraWhiteBalance(double whiteBalance)
        {
            camera.SetWhiteBalance(whiteBalance);
        }

        public void SetCameraExposure(double exposure)
        {
            camera.SetExposure(exposure);
        }

        public void SetProcessorOffset(int xOffset, int yOffset)
        {
            processor.SetOffset(xOffset, yOffset);
        }

        public void SetProcessorThreadHold(int thresdhold)
        {
            processor.SetThresdHold(thresdhold);
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

        private async Task StreamVideo()
        {
            while (true)
            {
                try
                {
                    image = camera.GetVideoFrame();
                    point = processor.GetPointingCoordinate(image);
                    if (arduinoMouse.isOpen())
                        arduinoMouse.SendCursorPos(point);
                    await Task.Delay(1);
                }
                catch
                {
                    // Handle exceptions
                }
            }
        }
    }
}