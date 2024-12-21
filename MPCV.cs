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
            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };
            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(startX, startY));

            while (stack.Count > 0)
            {
                Point p = stack.Pop();
                int x = p.X;
                int y = p.Y;

                if (x < 0 || x >= visited.GetLength(0) || y < 0 || y >= visited.GetLength(1))
                    continue;

                if (visited[x, y])
                    continue;

                int pos = y * stride + x * bytesPerPixel;
                if (!IsWhitePixel(pixels, pos))
                    continue;

                visited[x, y] = true;
                contour.Add(new Point(x, y));

                for (int i = 0; i < 4; i++)
                {
                    int newX = x + dx[i];
                    int newY = y + dy[i];
                    stack.Push(new Point(newX, newY));
                }
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
            double[,] matrix = new double[8, 9];

            for (int i = 0; i < 4; i++)
            {
                int j = i * 2;
                matrix[j, 0] = src[i].X;
                matrix[j, 1] = src[i].Y;
                matrix[j, 2] = 1;
                matrix[j, 3] = 0;
                matrix[j, 4] = 0;
                matrix[j, 5] = 0;
                matrix[j, 6] = -src[i].X * dst[i].X;
                matrix[j, 7] = -src[i].Y * dst[i].X;
                matrix[j, 8] = dst[i].X;

                matrix[j + 1, 0] = 0;
                matrix[j + 1, 1] = 0;
                matrix[j + 1, 2] = 0;
                matrix[j + 1, 3] = src[i].X;
                matrix[j + 1, 4] = src[i].Y;
                matrix[j + 1, 5] = 1;
                matrix[j + 1, 6] = -src[i].X * dst[i].Y;
                matrix[j + 1, 7] = -src[i].Y * dst[i].Y;
                matrix[j + 1, 8] = dst[i].Y;
            }

            // Solve the system of linear equations using Gaussian elimination
            for (int i = 0; i < 8; i++)
            {
                // Find the pivot row
                int maxRow = i;
                for (int k = i + 1; k < 8; k++)
                {
                    if (Math.Abs(matrix[k, i]) > Math.Abs(matrix[maxRow, i]))
                    {
                        maxRow = k;
                    }
                }

                // Swap the pivot row with the current row
                for (int j = 0; j < 9; j++)
                {
                    double temp = matrix[i, j];
                    matrix[i, j] = matrix[maxRow, j];
                    matrix[maxRow, j] = temp;
                }

                // Normalize the pivot row
                double factor = matrix[i, i];
                for (int j = 0; j < 9; j++)
                {
                    matrix[i, j] /= factor;
                }

                // Eliminate the column
                for (int k = 0; k < 8; k++)
                {
                    if (k != i)
                    {
                        factor = matrix[k, i];
                        for (int j = 0; j < 9; j++)
                        {
                            matrix[k, j] -= factor * matrix[i, j];
                        }
                    }
                }
            }

            // Extract the perspective transformation matrix
            double[,] transform = new double[3, 3];
            transform[0, 0] = matrix[0, 8];
            transform[0, 1] = matrix[1, 8];
            transform[0, 2] = matrix[2, 8];
            transform[1, 0] = matrix[3, 8];
            transform[1, 1] = matrix[4, 8];
            transform[1, 2] = matrix[5, 8];
            transform[2, 0] = matrix[6, 8];
            transform[2, 1] = matrix[7, 8];
            transform[2, 2] = 1;

            return transform;
        }
    }

}
