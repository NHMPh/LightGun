using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using System.Runtime.InteropServices;


namespace LightGun.LightGunCompoment
{
    public class ImageProcessor
    {

        private int thresdhold;
        private const int ixres = 640;
        private const int iyres = 480;
        private int xOffset;
        private int yOffset;


        PointF topLeft = PointF.Empty;
        PointF topRight = PointF.Empty;
        PointF bottomRight = PointF.Empty;
        PointF bottomLeft = PointF.Empty;

        private Image<Gray, byte> processImage;
        private Image<Bgr, byte> rawImage;
        private Mat hierarchy = new Mat();
        private Mat matrix = new Mat();
        private Mat transformedPointMat = new Mat();
        public ImageProcessor() { }

        public void SetOffset(int xOffset, int yOffset)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }
        public void SetThresdHold(int thresdHold)
        {
            thresdhold = thresdHold;
        }

        public Bitmap GetRawImage()
        {
            if (rawImage != null)
                return rawImage.ToBitmap();
            else return null;
        }
        public Bitmap GetProcessImage()
        {
            if (processImage == null) return null;
            var process = processImage.ToBitmap().ToImage<Bgr, byte>();
            var pointToTrack = new PointF(ixres / 2 + xOffset, iyres / 2 + yOffset);
            process.Draw(new CircleF(topLeft, 20), new Bgr(0, 0, 255), 10);
            process.Draw(new CircleF(topRight, 20), new Bgr(0, 255, 0), 10);
            process.Draw(new CircleF(bottomRight, 20), new Bgr(255, 0, 0), 10);
            process.Draw(new CircleF(bottomLeft, 20), new Bgr(0, 255, 255), 10);
            process.Draw(new CircleF(new PointF(pointToTrack.X, pointToTrack.Y), 10), new Bgr(255, 255, 0), 5);
            return process.ToBitmap();
        }
        private VectorOfPoint BiggestContour(List<VectorOfPoint> contours)
        {
            VectorOfPoint biggest = new VectorOfPoint();
            double maxArea = 28800;
            foreach (var contour in contours)
            {
                double area = 0;
                area = CvInvoke.ContourArea(contour);
                VectorOfPoint approx = new VectorOfPoint();
                CvInvoke.ApproxPolyDP(contour, approx, 0.015 * CvInvoke.ArcLength(contour, true), true);
                if (area > maxArea && approx.Size == 4)
                {
                    biggest = approx;
                    maxArea = area;
                }


            }
            return biggest;
        }
        public Point GetPointingCoordinate(Image<Bgr, byte> image)
        {

            rawImage = image;
            processImage = rawImage.Convert<Gray, byte>();

            CvInvoke.Threshold(processImage, processImage, thresdhold, 255, ThresholdType.Binary);
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(processImage, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
            List<VectorOfPoint> contourList = new List<VectorOfPoint>();

            for (int i = 0; i < contours.Size; i++)
            {
                if (contours[i].Size >= 4)
                {
                    contourList.Add(contours[i]);
                }

            }

            VectorOfPoint biggest = BiggestContour(contourList);


            if (biggest.Size == 4)
            {
                // Convert VectorOfPoint to PointF array
                Point[] points = biggest.ToArray();
                PointF[] corners = Array.ConvertAll(points, p => new PointF(p.X, p.Y));

                // Calculate the center of the contour
                float centerX = 0, centerY = 0;
                foreach (var corner in corners)
                {
                    centerX += corner.X;
                    centerY += corner.Y;
                }
                centerX /= corners.Length;
                centerY /= corners.Length;

                // Assign corners based on their position relative to the center
                foreach (var corner in corners)
                {
                    if (corner.X < centerX && corner.Y < centerY) topLeft = corner;
                    else if (corner.X > centerX && corner.Y < centerY) topRight = corner;
                    else if (corner.X > centerX && corner.Y > centerY) bottomRight = corner;
                    else if (corner.X < centerX && corner.Y > centerY) bottomLeft = corner;
                }


                PointF pointToTrack = new PointF(ixres / 2 + xOffset, iyres / 2 + yOffset);
                Mat pointMat = new Mat(1, 1, DepthType.Cv32F, 2);
                pointMat.SetTo(new float[] { pointToTrack.X, pointToTrack.Y });
                PointF[] pts1 = new PointF[] {
                            topLeft,    // corners[0]
                            topRight,   // corners[1]
                            bottomRight,// corners[2]
                            bottomLeft  // corners[3]
                    };
                PointF destTopLeft = new PointF(0, 0);
                PointF destTopRight = new PointF(ixres, 0);
                PointF destBottomRight = new PointF(ixres, iyres);
                PointF destBottomLeft = new PointF(0, iyres);
                PointF[] pts2 = new PointF[] {
                            destTopLeft,    // Destination for topLeft
                            destTopRight,   // Destination for topRight
                            destBottomRight,// Destination for bottomRight
                    destBottomLeft  // Destination for bottomLeft
                    };

                // Convert PointF arrays to Mat
                using (Mat srcPoints = new Mat(4, 1, DepthType.Cv32F, 2))
                using (Mat dstPoints = new Mat(4, 1, DepthType.Cv32F, 2))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        srcPoints.SetTo(pts1);
                        dstPoints.SetTo(pts2);
                    }
                    // Get the perspective transformation matrix
                    matrix = CvInvoke.GetPerspectiveTransform(srcPoints, dstPoints);
                    // Apply the perspective transformation
                    CvInvoke.PerspectiveTransform(pointMat, transformedPointMat, matrix);
                    // Convert the transformed Mat back to PointF
                    float[] transformedPointValues = new float[2];
                    Marshal.Copy(transformedPointMat.DataPointer, transformedPointValues, 0, 2);
                    Point returnPoint = new Point((int)transformedPointValues[0], (int)transformedPointValues[1]);
                    Console.WriteLine(returnPoint);
                    return returnPoint;
                }

            }
            return Point.Empty;




        }
    }
}
