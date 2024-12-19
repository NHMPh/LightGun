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

            //Random rand = new Random();

            //foreach (var contour in contours)
            //{
            //    // Generate a random color
            //    byte blue = (byte)rand.Next(256);
            //    byte green = (byte)rand.Next(256);
            //    byte red = (byte)rand.Next(256);

            //    foreach (var point in contour)
            //    {
            //        int pos = point.Y * bmpData.Stride + point.X * bytesPerPixel;
            //        pixels[pos] = blue; // Blue
            //        pixels[pos + 1] = green; // Green
            //        pixels[pos + 2] = red; // Red
            //    }
            //}

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
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(new Point(startX, startY));

            while (queue.Count > 0)
            {
                Point p = queue.Dequeue();
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
                    queue.Enqueue(new Point(newX, newY));
                }
            }
        }
        private static void TraceContour(byte[] pixels, bool[,] visited, int startX, int startY, int stride, int bytesPerPixel, List<Point> contour)
        {
            int[] dx = { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] dy = { 0, -1, -1, -1, 0, 1, 1, 1 };
            int dir = 0;
            int x = startX, y = startY;

            do
            {
                contour.Add(new Point(x, y));
                visited[x, y] = true;

                bool foundNext = false;
                for (int i = 0; i < 8; i++)
                {
                    int newDir = (dir + i) % 8;
                    int newX = x + dx[newDir];
                    int newY = y + dy[newDir];

                    if (newX >= 0 && newX < visited.GetLength(0) && newY >= 0 && newY < visited.GetLength(1))
                    {
                        int pos = newY * stride + newX * bytesPerPixel;
                        if (IsWhitePixel(pixels, pos) && !visited[newX, newY])
                        {
                            x = newX;
                            y = newY;
                            dir = (newDir + 6) % 8;
                            foundNext = true;
                            break;
                        }
                    }
                }

                if (!foundNext)
                {
                    break;
                }
            } while (x != startX || y != startY);
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

        public static double ContourArea(List<Point> contour)
        {
            if (contour.Count < 3)
            {
                return 0.0;
            }
            double area = 0.0;
            int n = contour.Count;

            for (int i = 0; i < n; i++)
            {
                Point p1 = contour[i];
                Point p2 = contour[(i + 1) % n];
                area += p1.X * p2.Y - p2.X * p1.Y;
            }

            return Math.Abs(area) / 2.0;
        }

        public static List<Point> GetCorners(List<Point> contour)
        {
            // Find the top-left, top-right, bottom-left, and bottom-right points
            Point topLeft = contour.OrderBy(p => p.X + p.Y).First();
            Point topRight = contour.OrderBy(p => p.X - p.Y).First();
            Point bottomLeft = contour.OrderBy(p => p.X + p.Y).Last();
            Point bottomRight = contour.OrderBy(p => p.X - p.Y).Last();

            return new List<Point> { topLeft, topRight, bottomLeft, bottomRight };
        }
        public static Point PerspectiveTransform(Point point, double[,] matrix)
        {
            if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
            {
                throw new ArgumentException("Transformation matrix must be 3x3.");
            }
            double x = point.X * matrix[0, 0] + point.Y * matrix[0, 1] + matrix[0, 2];
            double y = point.X * matrix[1, 0] + point.Y * matrix[1, 1] + matrix[1, 2];
            double w = point.X * matrix[2, 0] + point.Y * matrix[2, 1] + matrix[2, 2];
            return new Point((int)(x / w), (int)(y / w));
        }
        public static double[,] GetPerspectiveTransform(List<Point> src, List<Point> dst)
        {
            if (src.Count != 4 || dst.Count != 4)
            {
                throw new ArgumentException("Both source and destination points must contain exactly 4 points.");
            }

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
                if (factor == 0)
                {
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
                }
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
