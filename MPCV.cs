using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace LightGun
{
    class MPCV
    {
        public static void Gray(Bitmap bitmap)
        {
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * bitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);

            for (int y = 0; y < bitmap.Height; y++)
            {
                int yPos = y * bmpData.Stride;
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int pos = yPos + x * bytesPerPixel;
                    byte blue = pixels[pos];
                    byte green = pixels[pos + 1];
                    byte red = pixels[pos + 2];
                    byte gray = (byte)(.299 * red + .587 * green + .114 * blue);
                    pixels[pos] = gray;
                    pixels[pos + 1] = gray;
                    pixels[pos + 2] = gray;
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bitmap.UnlockBits(bmpData);
        }

        public static void Threshold(Bitmap bitmap, double threshold, double maxValue)
        {
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * bitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);

            for (int y = 0; y < bitmap.Height; y++)
            {
                int yPos = y * bmpData.Stride;
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int pos = yPos + x * bytesPerPixel;
                    byte blue = pixels[pos];
                    byte green = pixels[pos + 1];
                    byte red = pixels[pos + 2];
                    pixels[pos] = blue >= threshold ? (byte)maxValue : (byte)0;
                    pixels[pos + 1] = green >= threshold ? (byte)maxValue : (byte)0;
                    pixels[pos + 2] = red >= threshold ? (byte)maxValue : (byte)0;
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bitmap.UnlockBits(bmpData);
        }
    }
}

