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
                  //  byte gray = (byte)((red * 77 + green * 150 + blue * 29) >> 8);
                  //  byte value = (byte)(gray >= thresholdByte  ? maxValueByte : 0);
                    byte value = (byte)((red>=thresholdByte||blue>=thresholdByte|green>=thresholdByte)? maxValueByte : 0);
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
                        MooreNeighborTracing(pixels, visited, x, y, bmpData.Stride, bytesPerPixel, contour);
                        contours.Add(contour);

                        // Mark the entire contour as visited to avoid finding contours inside it
                        foreach (Point p in contour)
                        {
                            visited[p.X, p.Y] = true;
                        }
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
        
        private static void MooreNeighborTracing(byte[] pixels, bool[,] visited, int startX, int startY, int stride, int bytesPerPixel, List<Point> contour)
        {
            int[,] directions = { { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };
            int dir = 0;
            int x = startX;
            int y = startY;
            int startDir = 0;

            do
            {
                contour.Add(new Point(x, y));
                visited[x, y] = true;

                bool foundNext = false;
                for (int i = 0; i < 8; i++)
                {
                    int newDir = (startDir + i) % 8;
                    int newX = x + directions[newDir, 0];
                    int newY = y + directions[newDir, 1];

                    if (newX >= 0 && newX < visited.GetLength(0) && newY >= 0 && newY < visited.GetLength(1))
                    {
                        int pos = newY * stride + newX * bytesPerPixel;
                        if (IsWhitePixel(pixels, pos) && !visited[newX, newY])
                        {
                            x = newX;
                            y = newY;
                            startDir = (newDir + 6) % 8;
                            foundNext = true;
                            break;
                        }
                    }
                }

                if (!foundNext)
                {
                    break;
                }

            } while (x != startX || y != startY || dir != startDir);
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

        public static void DrawPoint(Bitmap bitmap, List<Point> contour,Color color)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                if (contour.Count > 1)
                {
                    using (Pen pen = new Pen(color, 10))
                    {
                        foreach (Point p in contour)
                        {
                            g.DrawEllipse(pen, p.X, p.Y, 10, 10);
                        }
                    }
                }
            }
            
        }
 public static List<Point> GetCornersHarris(List<Point> contour)
{
    int width = contour.Max(p => p.X) + 1;
    int height = contour.Max(p => p.Y) + 1;
    double[,] harrisResponse = new double[width, height];

    // Compute Harris response for each point in the contour
    foreach (var point in contour)
    {
        double ix = 0, iy = 0, ixx = 0, iyy = 0, ixy = 0;
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                int x = point.X + dx;
                int y = point.Y + dy;
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    ix += dx;
                    iy += dy;
                    ixx += dx * dx;
                    iyy += dy * dy;
                    ixy += dx * dy;
                }
            }
        }

        double det = ixx * iyy - ixy * ixy;
        double trace = ixx + iyy;
        harrisResponse[point.X, point.Y] = det - 0.04 * trace * trace;
    }

    // Find local maxima in the Harris response
    List<Point> corners = new List<Point>();
    for (int y = 1; y < height - 1; y++)
    {
        for (int x = 1; x < width - 1; x++)
        {
            double response = harrisResponse[x, y];
            if (response > harrisResponse[x - 1, y - 1] && response > harrisResponse[x, y - 1] &&
                response > harrisResponse[x + 1, y - 1] && response > harrisResponse[x - 1, y] &&
                response > harrisResponse[x + 1, y] && response > harrisResponse[x - 1, y + 1] &&
                response > harrisResponse[x, y + 1] && response > harrisResponse[x + 1, y + 1])
            {
                corners.Add(new Point(x, y));
            }
        }
    }

    return corners;
}
        public static List<Point> GetCorners(List<Point> contour)
        {
            int minTopLeft = int.MaxValue;
            int maxTopRight = int.MinValue;
            int minBottomLeft = int.MaxValue;
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
                if (sub > maxTopRight)
                {
                    topRight = contour[i];
                    maxTopRight = sub;
                }

                if (sub < minBottomLeft)
                {
                    bottomLeft = contour[i];
                    minBottomLeft = sub;
                }
                if (sum > maxBottomRight)
                {
                    bottomRight = contour[i];
                    maxBottomRight = sum;
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
            //Setting up matries
            double[,] A = {{src[0].X,src[0].Y,1},
                           {src[1].X,src[1].Y,1},
                           {src[3].X,src[3].Y,1}};

            double[] B1 = { dst[0].X, dst[1].X, dst[3].X };

            double[] B2 = { dst[0].Y, dst[1].Y, dst[3].Y };
            
            //Output X1[a1,a2,a3] X2[a4, a5, a6]
            double[] X1, X2;
            GaussJordanEliminationWithPartialPivoting(A, B1, B2, out X1, out X2);

            //Transformation matrix obtain
            double[,] transform = new double[3, 3]
            {
                { X1[0], X1[1], X1[2] },
                { X2[0], X2[1], X2[2] },
                { 0, 0, 1 }
            };

            return transform;
        }
        public static List<Point> approxPolyDP(List<Point> contour, double epsilon)
        {
            return DouglasPeucker(contour, epsilon);
        }

        private static List<Point> DouglasPeucker(List<Point> pointList, double epsilon)
        {
            // Find the point with the maximum distance
            double dmax = 0;
            int index = 0;
            int end = pointList.Count;
            for (int i = 1; i < end - 1; i++)
            {
                double d = PerpendicularDistance(pointList[i], pointList[0], pointList[end - 1]);
                if (d > dmax)
                {
                    index = i;
                    dmax = d;
                }
            }

            List<Point> resultList = new List<Point>();

            // If max distance is greater than epsilon, recursively simplify
            if (dmax > epsilon)
            {
                // Recursive call
                List<Point> recResults1 = DouglasPeucker(pointList.GetRange(0, index + 1), epsilon);
                List<Point> recResults2 = DouglasPeucker(pointList.GetRange(index, end - index), epsilon);

                // Build the result list
                resultList.AddRange(recResults1.Take(recResults1.Count - 1));
                resultList.AddRange(recResults2);
            }
            else
            {
                resultList.Add(pointList[0]);
                resultList.Add(pointList[end - 1]);
            }

            // Return the result
            return resultList;
        }

        private static double PerpendicularDistance(Point point, Point lineStart, Point lineEnd)
        {
            double dx = lineEnd.X - lineStart.X;
            double dy = lineEnd.Y - lineStart.Y;

            double mag = Math.Sqrt(dx * dx + dy * dy);
            if (mag > 0.0)
            {
                dx /= mag;
                dy /= mag;
            }

            double pvx = point.X - lineStart.X;
            double pvy = point.Y - lineStart.Y;

            double pvdot = dx * pvx + dy * pvy;

            double ax = pvx - pvdot * dx;
            double ay = pvy - pvdot * dy;

            return Math.Sqrt(ax * ax + ay * ay);
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
