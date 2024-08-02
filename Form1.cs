using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using Emgu.CV.Aruco;
using System.Runtime.InteropServices;
using Emgu.CV.Cuda;
using System.Drawing;
using System.Diagnostics;
using Emgu.CV.Dai;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
namespace LightGun
{
    public partial class Form1 : Form
    {

        // Import the necessary Windows API functions

        int xres = 1920;
        int yres = 1080;
/*        int xres = 600;
        int yres = 480;*/


        static int ixres = 1280;
        static int iyres = 720;

        int t1 = 50;
        int t2 = 150;
        int t3 = 200;
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
       static bool movable = false;
        // Mouse event constants
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        bool streamVideo = false;
        Mat frame = new Mat();
        Size frameSize = new Size(ixres, iyres);
        Image<Bgr, byte> image;
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        static int cameraIndex = 1;
        static VideoCapture capture = new VideoCapture(cameraIndex);

        public Form1()
        {
            InitializeComponent();
            // Set the capture resolution
            capture.Set(CapProp.FrameWidth, ixres);
            capture.Set(CapProp.FrameHeight, iyres);

            capture.Set(CapProp.Fps, 30);
            this.KeyDown += MyForm_KeyDown;
            KeyPreview = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

        }
        private void MyForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if a specific key is pressed
            if (e.KeyCode == Keys.C)
            {
                movable =!movable;
               /* capture.Read(frame);
                CvInvoke.Resize(frame, frame, frameSize);
                image = frame.ToImage<Bgr, byte>();
                MoveMouseToPointAsync(DetectEdge(image));
                Image<Bgr, byte> img = frame.ToImage<Bgr, byte>();
                img.Draw(new CircleF(new PointF(capture.Width / 2, capture.Height / 2), 2), new Bgr(0, 0, 255), 1);
                pictureBox1.Image = img.AsBitmap();*/
            }
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);
        public static void MoveMouseToPointAsync(PointF targetPoint, int steps = 16, int duration = 35)
        {
            if (targetPoint == PointF.Empty) return;
            int x = (int)targetPoint.X;
            int y = (int)targetPoint.Y;
            /* if (targetPoint == PointF.Empty) return;
             Point startPoint;
             GetCursorPos(out startPoint);

             PointF increment = new PointF(
                 (targetPoint.X - startPoint.X) / steps,
                 (targetPoint.Y - startPoint.Y) / steps);

             for (int i = 0; i < steps; i++)
             {
                 PointF nextPoint = new PointF(
                     startPoint.X + increment.X * (i + 1),
                     startPoint.Y + increment.Y * (i + 1));

                 await Task.Delay(1);
             }*/
            if(movable)
               SetCursorPos(x, y);
        }
        public static Image<Bgr, byte> CropImageByRedDots(Image<Bgr, byte> image)
        {
            // Load the image


            // Convert to HSV
            Image<Hsv, byte> hsvImage = image.Convert<Hsv, byte>();

            // Define the lower and upper bounds for the red color in the first range
            Hsv lowerRed1 = new Hsv(0, 70, 50); // Adjust these values as needed
            Hsv upperRed1 = new Hsv(10, 255, 255); // Adjust these values as needed

            // Define the lower and upper bounds for the red color in the second range
            Hsv lowerRed2 = new Hsv(170, 70, 50); // Adjust these values as needed
            Hsv upperRed2 = new Hsv(180, 255, 255); // Adjust these values as needed

            // Create masks for both red ranges and combine them
            Image<Gray, byte> redMask1 = hsvImage.InRange(lowerRed1, upperRed1);
            Image<Gray, byte> redMask2 = hsvImage.InRange(lowerRed2, upperRed2);
            Image<Gray, byte> redMask = redMask1.Or(redMask2);

            // Optional: Apply some morphological operations to clean up the mask
            // e.g., CvInvoke.MorphologyEx(redMask, redMask, MorphOp.Close, null, new Point(-1, -1), 1, BorderType.Reflect, new MCvScalar());

            // Invert the mask if you want to highlight red areas instead of masking them
            // var mask = redMask.Not();

            // Set non-red areas to black (if that's the intention)

            // Find contours in the red mask
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(redMask, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            // Create a new mask to draw the filtered contours
            Image<Gray, byte> filteredRedMask = new Image<Gray, byte>(redMask.Width, redMask.Height, new Gray(0));

            // Minimum area threshold for red areas
            double minArea = 4000; // Adjust this value as needed
            double maxArea = 30000;
            for (int i = 0; i < contours.Size; i++)
            {
                // Calculate the area of each contour
                double area = CvInvoke.ContourArea(contours[i]);

                // If the area is above the threshold, draw the contour on the new mask
                if (area > minArea && area < maxArea)
                {
                    CvInvoke.DrawContours(filteredRedMask, contours, i, new MCvScalar(255), -1); // -1 fills the contour
                }
            }

            // Now, use the filteredRedMask instead of redMask for further processing
            // If you want to set non-red areas to black (and have only the significant red areas), use the filtered mask
            image.SetValue(new Bgr(0, 0, 0), filteredRedMask.Not());
            // Find contours again, this time possibly for the filtered red dots
            VectorOfVectorOfPoint dotContours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(filteredRedMask, dotContours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

            List<Point> centroids = new List<Point>();
            for (int i = 0; i < dotContours.Size; i++)
            {
                Moments moments = CvInvoke.Moments(dotContours[i]);
                int x = (int)(moments.M10 / moments.M00);
                int y = (int)(moments.M01 / moments.M00);
                centroids.Add(new Point(x, y));
            }

            // Sort centroids if necessary, depending on the shape you expect to form
            // This step is context-dependent and might require custom logic

            // Draw and fill the polygon formed by the centroids
            if (centroids.Count >= 3) // Need at least 3 points to form a polygon
            {
                // Sort the centroids if necessary. The sorting logic depends on the expected shape.
                // For a general quadrilateral, you might sort by X, then Y, but this is an oversimplification.
                // Custom sorting logic might be required for specific shapes or orientations.

                // Example of a simple sort that might not work for all quadrilaterals:
                centroids = centroids.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();

                // Create a vector of points from the sorted centroids
                VectorOfPoint quadrilateralPoints = new VectorOfPoint(centroids.ToArray());

                // Draw and fill the quadrilateral
                // Note: FillConvexPoly works if the quadrilateral is convex. For non-convex shapes, this approach might not be suitable.
                CvInvoke.FillConvexPoly(image, quadrilateralPoints, new MCvScalar(255, 0, 0)); // Fill with red for visibility
            }

            return image;
            return image;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            streamVideo = true;

            Task.Run(async () => await StreamVideo());
        }

        private async Task StreamVideo()
        {

            while (streamVideo)
            {

                capture.Read(frame);
                CvInvoke.Resize(frame, frame, frameSize);
                image = frame.ToImage<Bgr, byte>();
                MoveMouseToPointAsync(DetectEdge(image));
                await Task.Delay(1);

            }
        }
        VectorOfPoint BiggestContour(List<VectorOfPoint> contours)
        {
            VectorOfPoint biggest = new VectorOfPoint();
            double maxArea = 0;
            foreach (var contour in contours)
            {
                double area = CvInvoke.ContourArea(contour);
                if (area > 1000)
                {
                    VectorOfPoint approx = new VectorOfPoint();
                    CvInvoke.ApproxPolyDP(contour, approx, 0.015 * CvInvoke.ArcLength(contour, true), true);
                    if (area > maxArea && approx.Size == 4)
                    {
                        biggest = approx;
                        maxArea = area;
                    }
                }
            }
            return biggest;
        }
        private PointF DetectEdge(Image<Bgr, byte> image)
        {
            // Convert to grayscale
            Image<Gray, Byte> gray = image.Convert<Gray, Byte>();

            // Increase contrast
            gray._EqualizeHist();

            // Apply Gaussian Blur to reduce noise
         //   Image<Gray, Byte> blurred = gray.SmoothGaussian(5);
            CvInvoke.GaussianBlur(gray,gray, new System.Drawing.Size(5, 5), 1.5);
            // Apply threshold to isolate white areas
            Mat thresholded = new Mat(); CvInvoke.Threshold(gray, thresholded, t3, 255, ThresholdType.Binary);

            // Apply morphological operations to enhance edges
            Mat morph = new Mat(); Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1)); 
            CvInvoke.MorphologyEx(thresholded, morph, MorphOp.Dilate, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar()); 
            CvInvoke.MorphologyEx(morph, morph, MorphOp.Erode, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

            // Detect edges using Canny
            Image<Gray, Byte> edged = new Image<Gray, byte>(gray.Size); 
            pictureBox1.Image = morph.ToBitmap();
           
           // CvInvoke.Canny(morph, edged, 100, 200); 
            // Adjust these values
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(morph, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);

            List<VectorOfPoint> contourList = new List<VectorOfPoint>();
            for (int i = 0; i < contours.Size; i++)
            {
                if (contours[i].Size >= 4)
                {
                    contourList.Add(contours[i]);
                }

            }

            // Sort contours by area and take the top 10
            contourList.Sort((c1, c2) => CvInvoke.ContourArea(c2).CompareTo(CvInvoke.ContourArea(c1)));
            // contourList = contourList.GetRange(0, Math.Min(1, contourList.Count));
            VectorOfPoint biggest = BiggestContour(contourList);





            PointF topLeft = PointF.Empty;
            PointF topRight = PointF.Empty;
            PointF bottomRight = PointF.Empty;
            PointF bottomLeft = PointF.Empty;

            // topLeft = corners[0]; PointF topRight = corners[1]; PointF bottomRight = corners[2]; PointF bottomLeft = corners[3];
            // Assuming 'biggest' is a VectorOfPoint representing the biggest contour
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
                image.Draw(new CircleF(topLeft, 20), new Bgr(0, 0, 255), 10);
                image.Draw(new CircleF(topRight, 20), new Bgr(0, 255, 0), 10);
                image.Draw(new CircleF(bottomRight, 20), new Bgr(255, 0, 0), 10);
                image.Draw(new CircleF(bottomLeft, 20), new Bgr(0, 255, 255), 10);
                image.Draw(new CircleF(new PointF(image.Width/2, image.Height/2), 10), new Bgr(255, 255, 0), 5);
                pictureBox2.Image = image.ToBitmap();
                PointF pointToTrack = new PointF(ixres / 2, iyres / 2);
                Mat pointMat = new Mat(1, 1, DepthType.Cv32F, 2);
                pointMat.SetTo(new float[] { pointToTrack.X, pointToTrack.Y });
                // Assuming topLeft, topRight, bottomRight, and bottomLeft are defined and represent your source points
                PointF[] pts1 = new PointF[] {
                            topLeft,    // corners[0]
                            topRight,   // corners[1]
                            bottomRight,// corners[2]
                            bottomLeft  // corners[3]
                    };
                // Assuming you have another set of points representing the destination points
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
                    Mat matrix = CvInvoke.GetPerspectiveTransform(srcPoints, dstPoints);
                    // Apply the perspective transformation
                    Mat transformedPointMat = new Mat();
                    CvInvoke.PerspectiveTransform(pointMat, transformedPointMat, matrix);
                    // Convert the transformed Mat back to PointF
                    float[] transformedPointValues = new float[2];
                    Marshal.Copy(transformedPointMat.DataPointer, transformedPointValues, 0, 2);
                    PointF transformedPoint = new PointF((transformedPointValues[0] / ixres) * xres, (transformedPointValues[1] / iyres) * yres);
                    return transformedPoint;

                    // 'transformedFrame' now contains the transformed image
                }

            }
            else
            {
                pictureBox2.Image = image.ToBitmap();
            }
            return PointF.Empty;




        }

















        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = (sender as TrackBar).Value.ToString();
            t2 = (sender as TrackBar).Value;
        }

        private void TrackBar2_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = (sender as TrackBar).Value.ToString();
            t1 = (sender as TrackBar).Value;
        }
        private void TrackBar3_ValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = (sender as TrackBar).Value.ToString();
            t3 = (sender as TrackBar).Value;
        }
        private Image<Bgr, byte> DetectAruco(Image<Bgr, byte> _image)
        {


            // Load the image
            Mat image = _image.Mat;

            // Create an empty Mat to store the transformed frame
            Mat transformedFrame = new Mat();

            // Initialize the detector parameters (optional)
            DetectorParameters parameters = DetectorParameters.GetDefault();

            // Create the dictionary for 6x6 Aruco markers
            Dictionary dictionary = new Dictionary(Dictionary.PredefinedDictionaryName.Dict6X6_250);

            // Prepare storage for detected markers
            using (VectorOfInt markerIds = new VectorOfInt())
            using (VectorOfVectorOfPointF markerCorners = new VectorOfVectorOfPointF())
            {
                // Detect markers
                ArucoInvoke.DetectMarkers(image, dictionary, markerCorners, markerIds, parameters);
                PointF pointToTrack = new PointF(capture.Width / 2, capture.Height / 2);
                // Convert PointF to Mat
                Mat pointMat = new Mat(1, 1, DepthType.Cv32F, 2);
                pointMat.SetTo(new float[] { pointToTrack.X, pointToTrack.Y });
                // Check if at least one marker was detected
                if (markerIds.Size == 4)
                {
                    // Draw detected markers
                    ArucoInvoke.DrawDetectedMarkers(image, markerCorners, markerIds, new MCvScalar(255, 0, 0));

                    // Optionally, you can also estimate the pose of each marker if you know the camera parameters and marker size
                    // This step requires camera calibration
                    // Iterate through all detected markers
                    PointF topLeft = PointF.Empty;
                    PointF topRight = PointF.Empty;
                    PointF bottomRight = PointF.Empty;
                    PointF bottomLeft = PointF.Empty;

                    // topLeft = corners[0]; PointF topRight = corners[1]; PointF bottomRight = corners[2]; PointF bottomLeft = corners[3];
                    for (int i = 0; i < markerIds.Size; i++)
                    {
                        switch (markerIds[i])
                        {
                            case 0:
                                topLeft = markerCorners[i][0];
                                break;
                            case 1:
                                topRight = markerCorners[i][1];
                                break;
                            case 2:
                                bottomRight = markerCorners[i][2];
                                break;
                            case 3:
                                bottomLeft = markerCorners[i][3];
                                break;
                        }
                    }
                    // Assuming topLeft, topRight, bottomRight, and bottomLeft are defined and represent your source points
                    PointF[] pts1 = new PointF[] {
                            topLeft,    // corners[0]
                            topRight,   // corners[1]
                            bottomRight,// corners[2]
                            bottomLeft  // corners[3]
                    };

                    // Assuming you have another set of points representing the destination points
                    PointF destTopLeft = new PointF(0, 0);
                    PointF destTopRight = new PointF(capture.Width, 0);
                    PointF destBottomRight = new PointF(capture.Width, capture.Height);
                    PointF destBottomLeft = new PointF(0, capture.Height);

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
                        Mat matrix = CvInvoke.GetPerspectiveTransform(srcPoints, dstPoints);


                        // Apply the perspective transformation
                        Mat transformedPointMat = new Mat();
                        CvInvoke.PerspectiveTransform(pointMat, transformedPointMat, matrix);
                        // Convert the transformed Mat back to PointF
                        float[] transformedPointValues = new float[2];
                        Marshal.Copy(transformedPointMat.DataPointer, transformedPointValues, 0, 2);
                        PointF transformedPoint = new PointF((transformedPointValues[0] / capture.Width) * xres, (transformedPointValues[1] / capture.Height) * yres);

                        // Apply the perspective warp transformation
                        CvInvoke.WarpPerspective(image, transformedFrame, matrix, new Size(capture.Width, capture.Height));
                        var retrunimg = transformedFrame.ToImage<Bgr, byte>();
                        retrunimg.Draw(new CircleF(transformedPoint, 2), new Bgr(0, 0, 255), 1);

                        return retrunimg;
                        // 'transformedFrame' now contains the transformed image
                    }
                }
                else
                {
                    Console.WriteLine("No Aruco markers detected.");
                }


            }


            // Save or display the image
            // For example, to save the image with detected markers drawn on it:
            return image.ToImage<Bgr, byte>();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
