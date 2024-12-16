using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGun
{
    class MPCV
    {
        public static void Gray(Bitmap bitmap)
        {
            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Convert to grayscale.
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                byte red = rgbValues[i + 2];
                byte green = rgbValues[i + 1];
                byte blue = rgbValues[i];

                // Calculate the gray value.
                byte gray = (byte)(.299 * red + .587 * green + .114 * blue);

                // Set the RGB components to the gray value.
                rgbValues[i] = gray;      // Blue
                rgbValues[i + 1] = gray;  // Green
                rgbValues[i + 2] = gray;  // Red
            }

            // Copy the RGB values back to the bitmap.
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bitmap.UnlockBits(bmpData);
        }


        public static void Threshold(Bitmap bitmap, double threshold, double maxValue)
        {
            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Apply threshold.
            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                // Pixel format is BGRA, so rgbValues[i] is Blue, rgbValues[i + 1] is Green, rgbValues[i + 2] is Red, rgbValues[i + 3] is Alpha
                byte blue = rgbValues[i];
                byte green = rgbValues[i + 1];
                byte red = rgbValues[i + 2];

                // Calculate the intensity to apply the threshold
                double intensity = (blue + green + red) / 3.0;

                // Apply the threshold
                byte value = (intensity >= threshold) ? (byte)maxValue : (byte)0;

                // Set the pixel values to black or white
                rgbValues[i] = value;      // Blue
                rgbValues[i + 1] = value;  // Green
                rgbValues[i + 2] = value;  // Red
                rgbValues[i + 3] = (byte)255; // Set Alpha to fully opaque
            }

            // Copy the RGB values back to the bitmap.
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bitmap.UnlockBits(bmpData);
        }
    }
}

