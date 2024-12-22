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
                    byte gray = (byte)((red * 77 + green * 150 + blue * 29) >> 8); // Optimized grayscale conversion
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

            byte thresholdByte = (byte)threshold;
            byte maxValueByte = (byte)maxValue;

            for (int y = 0; y < bitmap.Height; y++)
            {
                int yPos = y * bmpData.Stride;
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int pos = yPos + x * bytesPerPixel;
                    byte blue = pixels[pos];
                    byte green = pixels[pos + 1];
                    byte red = pixels[pos + 2];
                    byte value = (byte)((blue >= thresholdByte || green >= thresholdByte || red >= thresholdByte) ? maxValueByte : 0);
                    pixels[pos] = value;
                    pixels[pos + 1] = value;
                    pixels[pos + 2] = value;
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bitmap.UnlockBits(bmpData);
        }

        public static List<List<Point>> FindContour(Bitmap bitmap)
        {
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bmpData.Stride * bitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);

            List<List<Point>> contours = new List<List<Point>>();
            bool[,] visited = new bool[bitmap.Width, bitmap.Height];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int pos = y * bmpData.Stride + x * bytesPerPixel;
                    if (IsWhitePixel(pixels, pos) && !visited[x, y])
                    {
                        List<Point> contour = new List<Point>();
                        FloodFill(pixels, visited, x, y, bmpData.Stride, bytesPerPixel, contour);
                        contours.Add(contour);
                    }
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bitmap.UnlockBits(bmpData);
            return contours;
        }

        private static bool IsWhitePixel(byte[] pixels, int pos)
        {
            return pixels[pos] == 255 && pixels[pos + 1] == 255 && pixels[pos + 2] == 255;
        }

        private static void FloodFill(byte[] pixels, bool[,] visited, int startX, int startY, int stride, int bytesPerPixel, List<Point> contour)
        {
           
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(new Point(startX, startY));

            while (queue.Count > 0)
            {
                Point p = queue.Dequeue();
                int x = p.X;
                int y = p.Y;

                //Check out of bound
                if (x < 0 || x >= visited.GetLength(0) || y < 0 || y >= visited.GetLength(1))
                    continue;
                //Check if point is visited
                if (visited[x, y])
                    continue;
                //Check if is not white pixel
                int pos = y * stride + x * bytesPerPixel;
                if (!IsWhitePixel(pixels, pos))
                    continue;

                //Pass all condition
                visited[x, y] = true;
                contour.Add(new Point(x, y));

                queue.Enqueue(new Point(x + 1, y));
                queue.Enqueue(new Point(x - 1, y));
                queue.Enqueue(new Point(x, y + 1));
                queue.Enqueue(new Point(x, y - 1));

            }
        }



        public static void DrawContour(Bitmap bitmap, List<Point> contour)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                if (contour.Count > 1)
                {
                    using (Pen pen = new Pen(Color.Red, 10))
                    {
                        g.DrawPolygon(pen, contour.ToArray());
                    }
                }
            }
        }

        public static void DrawPoint(Bitmap bitmap, List<Point> contour)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                if (contour.Count > 1)
                {
                    using (Pen pen = new Pen(Color.Red, 10))
                    {
                        foreach (Point p in contour)
                        {
                            g.DrawEllipse(pen, p.X, p.Y, 10, 10);
                        }
                    }
                }
            }
            
        }

        public static List<Point> GetCorners(List<Point> contour)
        {
            int minTopLeft = int.MaxValue;
            int minTopRight = int.MaxValue;
            int maxBottomLeft = int.MinValue;
            int maxBottomRight = int.MinValue;
            Point topLeft = new Point(0, 0);
            Point topRight = new Point(0, 0);
            Point bottomLeft = new Point(0, 0);
            Point bottomRight = new Point(0, 0);

            for (int i = 0; i < contour.Count; i++)
            {
                int sum = contour[i].X + contour[i].Y;
                int sub = contour[i].X - contour[i].Y;

                if (sum < minTopLeft)
                {
                    topLeft = contour[i];
                    minTopLeft = sum;
                }
                if (sub < minTopRight)
                {
                    topRight = contour[i];
                    minTopRight = sub;
                }

                if (sum > maxBottomLeft)
                {
                    bottomLeft = contour[i];
                    maxBottomLeft = sum;
                }
                if (sub > maxBottomRight)
                {
                    bottomRight = contour[i];
                    maxBottomRight = sub;
                }
            }

            return new List<Point> { topLeft, topRight, bottomLeft, bottomRight };
        }
        public static Point PerspectiveTransform(Point point, double[,] matrix)
        {
            double x = (double)point.X * matrix[0, 0] + (double)point.Y * matrix[0, 1] + matrix[0, 2];
            double y = (double)point.X * matrix[1, 0] + (double)point.Y * matrix[1, 1] + matrix[1, 2];
            double w = (double)point.X * matrix[2, 0] + (double)point.Y * matrix[2, 1] + matrix[2, 2];
            return new Point((int)(x / w), (int)(y / w));
        }
        public static double[,] GetPerspectiveTransform(List<Point> src, List<Point> dst)
        {

            double[,] A = {{src[0].X,src[0].Y,1},
                           {src[1].X,src[1].Y,1},
                           {src[3].X,src[3].Y,1}};

            double[] B1 = { dst[0].X, dst[1].X, dst[3].X };

            double[] B2 = { dst[0].Y, dst[1].Y, dst[3].Y };

            double[] X1, X2;
            GaussJordanEliminationWithPartialPivoting(A, B1, B2, out X1, out X2);

            double[,] transform = new double[3, 3]
            {
                { X1[0], X1[1], X1[2] },
                { X2[0], X2[1], X2[2] },
                { 0, 0, 1 }
            };



            return transform;
        }

        private static void GaussJordanEliminationWithPartialPivoting(double[,] A, double[] B1, double[] B2, out double[] X1, out double[] X2)
        {
            int n = B1.Length;
            double[,] AB1 = new double[n, n + 1];
            double[,] AB2 = new double[n, n + 1];
            X1 = new double[n];
            X2 = new double[n];

            //Form the Augmented Matrices
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    AB1[i, j] = A[i, j];
                    AB2[i, j] = A[i, j];
                }
                AB1[i, n] = B1[i];
                AB2[i, n] = B2[i];
            }

            for (int i = 0; i < n; i++)
            {
                // Find the entry in the left column with the largest absolute value. This entry is called the pivot.
                int maxRow1 = i;
                int maxRow2 = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(AB1[j, i]) > Math.Abs(AB1[maxRow1, i]))
                        maxRow1 = j;
                    if (Math.Abs(AB2[j, i]) > Math.Abs(AB2[maxRow2, i]))
                        maxRow2 = j;
                }
                // Perform row interchange, so that the pivot is in the first row.
                for (int j = 0; j <= n; j++)
                {
                    double temp1 = AB1[i, j];
                    AB1[i, j] = AB1[maxRow1, j];
                    AB1[maxRow1, j] = temp1;

                    double temp2 = AB2[i, j];
                    AB2[i, j] = AB2[maxRow2, j];
                    AB2[maxRow2, j] = temp2;
                }
                // Gaussian Elimination
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        double factor1 = AB1[j, i] / AB1[i, i];
                        double factor2 = AB2[j, i] / AB2[i, i];
                        for (int k = i; k <= n; k++)
                        {
                            AB1[j, k] -= factor1 * AB1[i, k];
                            AB2[j, k] -= factor2 * AB2[i, k];
                        }
                    }
                }
            }
            // Back Substitution
            for (int i = n - 1; i >= 0; i--)
            {
                X1[i] = AB1[i, n];
                X2[i] = AB2[i, n];

                for (int j = i + 1; j < n; j++)
                {
                    X1[i] -= AB1[i, j] * X1[j];
                    X2[i] -= AB2[i, j] * X2[j];
                }

                X1[i] /= AB1[i, i];
                X2[i] /= AB2[i, i];
            }
        }
    }

}
