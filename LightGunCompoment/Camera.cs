using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LightGun.LightGunCompoment
{
    public class Camera
    {

        private const int ixres = 640;
        private const int iyres = 480;

        private Mat frame = new Mat();
        Image<Bgr, byte> image;
        Size frameSize = new Size(ixres, iyres);
        private VideoCapture camera;
        public Camera() { }


        public void OpenCamera(int index)
        {
            camera = new VideoCapture(index,VideoCapture.API.DShow);
            camera.Set(CapProp.FrameWidth, ixres);
            camera.Set(CapProp.FrameHeight, iyres);
            camera.Set(CapProp.Fps, 60);
        }

        public Image<Bgr, byte> GetVideoFrame()
        {
            camera.Read(frame);
            CvInvoke.Resize(frame, frame, frameSize);
            image = frame.ToImage<Bgr, byte>();
            return image;
        }
        public void CloseCamera()
        {
            camera.Release();
        }

        public void SetBrightness(double brightness)
        {
            camera.Set(CapProp.Brightness, brightness);
        }

        public double GetBrightness()
        {
            return camera.Get(CapProp.Brightness);
        }

        public void SetContrast(double contrast)
        {
            camera.Set(CapProp.Contrast, contrast);
        }

        public double GetContrast()
        {
            return camera.Get(CapProp.Contrast);
        }

        public void SetHue(double hue)
        {
            camera.Set(CapProp.Hue, hue);
        }

        public double GetHue()
        {
            return camera.Get(CapProp.Hue);
        }

        public void SetSaturation(double saturation)
        {
            camera.Set(CapProp.Saturation, saturation);
        }

        public double GetSaturation()
        {
            return camera.Get(CapProp.Saturation);
        }

        public void SetSharpness(double sharpness)
        {
            camera.Set(CapProp.Sharpness, sharpness);
        }

        public double GetSharpness()
        {
            return camera.Get(CapProp.Sharpness);
        }

        public void SetGamma(double gamma)
        {
            camera.Set(CapProp.Gamma, gamma);
        }

        public double GetGamma()
        {
            return camera.Get(CapProp.Gamma);
        }

        public void SetWhiteBalance(double whiteBalance)
        {
            camera.Set(CapProp.WhiteBalanceRedV, whiteBalance);
        }

        public double GetWhiteBalance()
        {
            return camera.Get(CapProp.WhiteBalanceRedV);
        }

        public void SetExposure(double exposure)
        {
            camera.Set(CapProp.Exposure, exposure);
        }

        public double GetExposure()
        {
            return camera.Get(CapProp.Exposure);
        }
    }
}