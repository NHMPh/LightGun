using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV;
using System.Runtime.InteropServices;
using System.Text.Json;
using AutoHotkey.Interop;
using AForge.Video.DirectShow;
using System.Text;
using System.Net.NetworkInformation;
using System.Drawing;
using System.Diagnostics;

namespace LightGun
{
    public partial class Form1 : Form
    {





        static int ixres = 640;
        static int iyres = 480;

        static int xres = 1920;
        static int yres = 1080;

        public static float sWidth = 2;

        static TransparentForm border;
        public static bool movable = false;
        public static bool outSideOfScreen = true;
        bool streamVideo = false;
        public bool init = false;
        public static string clickOutside;
        public static string holdOutside;
        //Camera variable
        static int cameraIndex = -1;
        static VideoCapture capture = new VideoCapture(cameraIndex);
        Mat frame = new Mat();
        Image<Bgr, byte> image;
        Size frameSize = new Size(ixres, iyres);

        //Mouse variable
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        float xOffset = 0;
        float yOffset = 0;
        //Camera Property
        int threadhold = 200;
        int brightness = 0;
        int contrast = 0;
        int hue = 0;
        int saturation = 0;
        int sharpness = 0;
        int gamma = 0;
        int whiteBalance = 0;
        int exposure = 0;


        Settings settings;

        Image<Gray, Byte> gray;
        Mat hierarchy = new Mat();
        Mat matrix = new Mat();
        Mat transformedPointMat = new Mat();

        static AutoHotkeyEngine ahk = AutoHotkeyEngine.Instance;
        public Form1()
        {




            //ahk.Suspend();
            InitializeComponent();

            //create new hotkeys

            init = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            KeyPreview = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            string jsonFilePath = ".\\setting.json";
            string jsonString = File.ReadAllText(jsonFilePath);
            settings = JsonSerializer.Deserialize<Settings>(jsonString);
            sWidth = settings.Border;
            borderTextBox.Text = sWidth.ToString();
            LoadWebcams();
            clickOutside = settings.ClickOutSide;
            holdOutside = settings.HoldOutSide;
            clickOutComboBox.SelectedItem = clickOutside.ToString();
            holdOutComboBox.SelectedItem = holdOutside.ToString();
            string script = $@"
clickAction := ""{clickOutside}""
holdAction := ""{holdOutside}""
LButton::

    if (clickAction = ""Click Right"")
        Click right
    else if (clickAction = ""Click Middle"")
        Click middle
    else
        Send %clickAction%

    SetTimer, SendRightClick, 10
    ; Set a timer to run the SendMiddleClick label every 2000 milliseconds (2 seconds)
    SetTimer, SendMiddleClick, 1000
return

LButton Up::
    SetTimer, SendRightClick, Off 
    SetTimer, SendMiddleClick, Off
return

SendMiddleClick:
    SetTimer, SendRightClick, Off 
    if (holdAction = ""Click Right"")
        Click right
    else if (holdAction = ""Click Middle"")
        Click middle
    else
        Send %holdAction%
return
SendRightClick:

    if (clickAction = ""Click Right"")
        Click right
    else if (clickAction = ""Click Middle"")
        Click middle
    else
        Send %clickAction%

   
return

StopTimer(){{
 SetTimer, SendRightClick, Off 
 SetTimer, SendMiddleClick, Off
}}
";
          //  ahk.ExecRaw(script);
            ahk.Suspend();

        }
        private void LoadWebcams()
        {
            // Create a collection to hold the video devices
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // Check if any video devices are found
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No webcams found.");
                return;
            }
            comboBox1.Sorted = false;
            // Add each video device to the ComboBox
            foreach (FilterInfo device in videoDevices)
            {
                comboBox1.Items.Add(device.Name);
            }
        }
        private void SetCamera()
        {

            // Set the capture resolution
            capture.Set(CapProp.FrameWidth, ixres);
            capture.Set(CapProp.FrameHeight, iyres);
            capture.Set(CapProp.Fps, 30);

            // Path to the JSON file

            // Read the JSON file

            // Deserialize the JSON content into the Settings class

            // Assign the values to the variables
            threadhold = settings.Threadhold;
            brightness = settings.Brightness;
            contrast = settings.Contrast;
            hue = settings.Hue;
            saturation = settings.Saturation;
            sharpness = settings.Sharpness;
            gamma = settings.Gamma;
            whiteBalance = settings.WhiteBalance;
            exposure = settings.Exposure;

            xOffset = settings.Xoffset;
            yOffset = settings.Yoffset;



            tTrackBar.Value = threadhold;
            bTrackBar.Value = brightness;
            cTrackBar.Value = contrast;
            hTrackBar.Value = hue;
            saTrackBar.Value = saturation;
            shTrackBar.Value = sharpness;
            gTrackBar.Value = gamma;
            wTrackBar.Value = whiteBalance;
            eTrackBar.Value = exposure;




            tTextBox.Text = threadhold.ToString();
            bTextBox.Text = brightness.ToString();
            cTextBox.Text = contrast.ToString();
            hTextBox.Text = hue.ToString();
            saTextBox.Text = saturation.ToString();
            shTextBox.Text = sharpness.ToString();
            gTextBox.Text = gamma.ToString();
            wTextBox.Text = whiteBalance.ToString();
            eTextBox.Text = exposure.ToString();



            capture.Set(CapProp.Brightness, brightness);
            capture.Set(CapProp.Contrast, contrast);
            capture.Set(CapProp.Hue, hue);
            capture.Set(CapProp.Saturation, saturation);
            capture.Set(CapProp.Sharpness, sharpness);
            capture.Set(CapProp.Gamma, gamma);
            capture.Set(CapProp.WhiteBalanceRedV, whiteBalance);
            capture.Set(CapProp.Exposure, exposure);

        }

       

        public static void MoveMouseToPointAsync(PointF targetPoint, int steps = 16, int duration = 35)
        {
            if (targetPoint == PointF.Empty) return;
            int x = (int)targetPoint.X;
            int y = (int)targetPoint.Y;
            
            if (movable)
                SetCursorPos(x, y);
                
        }

        public static void StartStop()
        {
            movable = !movable;
            button1.Text = movable ? "Stop" : "Start";
            button1.BackColor = movable ? Color.Red : Color.Green;
           if(movable)
            ahk.UnSuspend();
           else ahk.Suspend();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            StartStop();
        }

        private async Task StreamVideo()
        {
            capture.Read(frame);
            SetCamera();
            while (streamVideo)
            {
                try
                {
                    capture.Read(frame);
                    CvInvoke.Resize(frame, frame, frameSize);
                    image = frame.ToImage<Bgr, byte>();
                    MoveMouseToPointAsync(DetectEdge2(image));                    
                    await Task.Delay(1);
                }
                catch
                {
                   
                }


            }
        }
        VectorOfPoint BiggestContour(List<VectorOfPoint> contours)
        {
            VectorOfPoint biggest = new VectorOfPoint();
            double maxArea = 0;
            foreach (var contour in contours)
            {
                double area = CvInvoke.ContourArea(contour);
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
        public static double GetParallelogramArea(List<Point> corners)
        {
            if (corners.Count != 4)
            {
                throw new ArgumentException("There must be exactly 4 corners.");
            }

            // Use the Shoelace formula to calculate the area
            double area = 0;
            for (int i = 0; i < corners.Count; i++)
            {
                Point current = corners[i];
                Point next = corners[(i + 1) % corners.Count];
                area += current.X * next.Y - next.X * current.Y;
            }

            return Math.Abs(area) / 2.0;
        }
        List<Point> _BiggestContour(List<List<Point>> contours)
        {
            List<Point> biggest = new List<Point>();
            double maxArea = 0;
            foreach (var contour in contours)
            {
                List<Point> _conners = MPCV.GetCorners(contour);
                //  return new List<Point> { topLeft, topRight, bottomLeft, bottomRight };
                double area = GetParallelogramArea(_conners);
               
                if (area > maxArea )
                {
                    biggest = _conners;
                    maxArea = area;
                }
              
               
            }
            return biggest;
        }

        private PointF DetectEdge2(Image<Bgr, byte> image)
        {
            if (rawCheckBox.Checked)
                pictureBox1.Image = image.ToBitmap();
            Bitmap mpimg = image.ToBitmap();
            MPCV.Gray(mpimg);
            MPCV.Threshold(mpimg, threadhold, 255);
            List<List<Point>> _contourList = MPCV.FindContour(mpimg);
            List<Point> _biggest = _BiggestContour(_contourList);
            Point _topLeft = Point.Empty;
            Point _topRight = Point.Empty;
            Point _bottomRight = Point.Empty;
            Point _bottomLeft = Point.Empty;
            // Calculate the center of the contour
            float _centerX = 0, _centerY = 0;
            foreach (var corner in _biggest)
            {
                _centerX += corner.X;
                _centerY += corner.Y;
            }
            _centerX /= _biggest.Count;
            _centerY /= _biggest.Count;

            // Assign corners based on their position relative to the center
            foreach (var corner in _biggest)
            {
                if (corner.X < _centerX && corner.Y < _centerY) _topLeft = corner;
                else if (corner.X > _centerX && corner.Y < _centerY) _topRight = corner;
                else if (corner.X > _centerX && corner.Y > _centerY) _bottomRight = corner;
                else if (corner.X < _centerX && corner.Y > _centerY) _bottomLeft = corner;
            }
            PointF _transformedPoint = new PointF();
            Point _pointToTrack = new Point();
            //  List<Point> _des = new List<Point>() { new Point(ixres, 0), new Point(ixres, iyres), new Point(0, iyres) , new Point(0, 0)    };
            List<Point> _des = new List<Point>() { new Point(0, 0), new Point(ixres, 0), new Point(0, iyres), new Point(ixres, iyres) };
            List<Point> _scr = new List<Point>() { _topLeft, _topRight, _bottomLeft, _bottomRight };
            try
            {
               // MPCV.DrawContour(mpimg, _biggest);
                _pointToTrack = new Point((int)(ixres / 2 +xOffset), (int)(iyres / 2+yOffset) );
              

                double[,] _matrix=MPCV.GetPerspectiveTransform(_scr, _des);
                _pointToTrack = MPCV.PerspectiveTransform(_pointToTrack, _matrix);
                _transformedPoint = new PointF((_pointToTrack.X / (float)ixres) * xres, (_pointToTrack.Y / (float)iyres) * yres);
            }
            catch (Exception ex) { }



            pictureBox2.Image = mpimg;
            return _transformedPoint;
        }
        private PointF DetectEdge(Image<Bgr, byte> image)
        {

            if (rawCheckBox.Checked)
                pictureBox1.Image = image.ToBitmap();
            gray = image.Convert<Gray, Byte>();
            //  CvInvoke.CLAHE(gray, 2, new Size(8, 8), gray);
            //  CvInvoke.GaussianBlur(gray, gray, new Size(5, 5), 0);
            CvInvoke.Threshold(gray, gray, threadhold, 255, ThresholdType.Binary);
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(gray, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
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
                double area = CvInvoke.ContourArea(biggest);
                if (area < 28800)
                {
                    if (processCheckBox.Checked)
                    {
                       var process2 = gray.ToBitmap();
                        pictureBox2.Image = process2;
                    }
                    outSideOfScreen = true;
                    if(movable)
                        ahk.UnSuspend();
                    else
                        ahk.Suspend();
                    return System.Drawing.PointF.Empty;
                }
                // Convert VectorOfPoint to PointF array
                Point[] points = biggest.ToArray();
                PointF[] corners = Array.ConvertAll(points, p => new PointF(p.X, p.Y));

                PointF topLeft = PointF.Empty;
                PointF topRight = PointF.Empty;
                PointF bottomRight = PointF.Empty;
                PointF bottomLeft = PointF.Empty;
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


                PointF pointToTrack = new PointF(ixres / 2 +xOffset , iyres / 2 +yOffset);
                var process = gray.ToBitmap().ToImage<Bgr, byte>();
                process.Draw(new CircleF(new PointF(pointToTrack.X, pointToTrack.Y), 10), new Bgr(255, 255, 0), 5);
                Mat pointMat = new Mat(1, 1, DepthType.Cv32F, 2);
                pointMat.SetTo(new float[] {pointToTrack.X, pointToTrack.Y });
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

                if (processCheckBox.Checked)
                {
                    process = gray.ToBitmap().ToImage<Bgr, byte>();
                    process.Draw(new CircleF(topLeft, 20), new Bgr(0, 0, 255), 10);
                    process.Draw(new CircleF(topRight, 20), new Bgr(0, 255, 0), 10);
                    process.Draw(new CircleF(bottomRight, 20), new Bgr(255, 0, 0), 10);
                    process.Draw(new CircleF(bottomLeft, 20), new Bgr(0, 255, 255), 10);
                    process.Draw(new CircleF(new PointF(pointToTrack.X, pointToTrack.Y), 10), new Bgr(255, 255, 0), 5);


                    pictureBox2.Image = process.ToBitmap();
                    
                }
               
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

                    // Apply the perspective transformation
                   
                     CvInvoke.PerspectiveTransform(pointMat, transformedPointMat, matrix);
                    // Convert the transformed Mat back to PointF
                    float[] transformedPointValues = new float[2];
                    Marshal.Copy(transformedPointMat.DataPointer, transformedPointValues, 0, 2);
                    PointF transformedPoint = new PointF((transformedPointValues[0] / ixres) * xres, (transformedPointValues[1] / iyres) * yres);


                    bool outsideX = transformedPoint.X < 0 || transformedPoint.X > xres;
                    bool outsideY = transformedPoint.Y < 0 || transformedPoint.Y > yres;
                    if ( outsideX||outsideY)
                    {
                       
                        outSideOfScreen = true;
                        if (movable)
                            ahk.UnSuspend();
                        else
                            ahk.Suspend();
                    }
                    else
                    {
                        outSideOfScreen = false;
                      
                        ahk.ExecFunction("StopTimer");
                        ahk.Suspend();
                    }


                    return transformedPoint;
                }

            }
            else
            {
                if (processCheckBox.Checked)
                {
                    var process = gray.ToBitmap();
                    pictureBox2.Image = process;
                }
     
                outSideOfScreen = true;
                if (movable)
                    ahk.UnSuspend();
                else
                    ahk.Suspend();
            }
           
            outSideOfScreen = true;
            if (movable)
                ahk.UnSuspend();
            else
                ahk.Suspend();
            return PointF.Empty;




        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Path to the JSON file
            string jsonFilePath = ".\\setting.json";
            // Serialize the object back to a JSON string
            string updatedJsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON string to a file
            File.WriteAllText(jsonFilePath, updatedJsonString);
        }

        private void up10Button_Click(object sender, EventArgs e)
        {
            yOffset -= 10;
            settings.Yoffset = (int)yOffset;
        }

        private void down10Button_Click(object sender, EventArgs e)
        {
            yOffset += 10;
            settings.Yoffset = (int)yOffset;
        }

        private void left10Button_Click(object sender, EventArgs e)
        {
            xOffset -= 10;
            settings.Xoffset = (int)xOffset;
        }

        private void right10Button_Click(object sender, EventArgs e)
        {
            xOffset += 10;
            settings.Xoffset = (int)xOffset;
        }

        private void up1Button_Click(object sender, EventArgs e)
        {
            yOffset -= 1;
            settings.Yoffset = (int)yOffset;
        }

        private void down1Button_Click(object sender, EventArgs e)
        {
            yOffset += 1;
            settings.Yoffset = (int)yOffset;
        }

        private void left1Button_Click(object sender, EventArgs e)
        {
            xOffset -= 1;
            settings.Xoffset = (int)xOffset;
        }

        private void right1Button_Click(object sender, EventArgs e)
        {
            xOffset += 1;
            settings.Xoffset = (int)xOffset;
        }





        public static void OpenBorder()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            if (border != null && border.Visible)
            {
                border.Dispose();
                border = null;
                xres = screenWidth;
                yres = screenHeight;
            }
            else
            {
                border = new TransparentForm(sWidth, screenWidth, screenHeight);
                border.Show();
                xres = screenWidth;
                yres = screenHeight;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            LoadWebcams();
        }


    }
}
