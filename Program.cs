using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.XObjdetect;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LightGun
{
    internal static class Program
    {

       
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            Console.WriteLine("Running");
            
          

        }
        private static void Test2()
        {
            // Load the image where you want to find the television
            Image<Bgr, byte> sceneImage = null;
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                 sceneImage = new Image<Bgr, byte>(dialog.FileName).Resize(640, 480, Inter.Cubic);
            }
          

            // Load an image of the television for reference
            Mat televisionImage = CvInvoke.Imread("D:\\Project\\5.jpg", ImreadModes.Color);

            // Initialize the ORB detector
            int nFeatures = 500;
            ORB orbDetector = new ORB(nFeatures);

            // Detect keypoints and compute descriptors for both images
            VectorOfKeyPoint keypointsScene, keypointsTelevision;
            Mat descriptorsScene = new Mat(), descriptorsTelevision = new Mat();
            keypointsScene = new VectorOfKeyPoint();
            keypointsTelevision = new VectorOfKeyPoint();
            orbDetector.DetectAndCompute(sceneImage, null, keypointsScene, descriptorsScene, false);
            orbDetector.DetectAndCompute(televisionImage, null, keypointsTelevision, descriptorsTelevision, false);

            // Match descriptors using BFMatcher
            BFMatcher matcher = new BFMatcher(DistanceType.Hamming);
            VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();
            matcher.KnnMatch(descriptorsTelevision, descriptorsScene, matches, 2);

            // Filter matches using the Lowe's ratio test
            var goodMatches = new VectorOfDMatch();
            for (int i = 0; i < matches.Size; i++)
            {
                if (matches[i][0].Distance < 0.95 * matches[i][1].Distance)
                {
                    goodMatches.Push(new[] { matches[i][0] });
                }
            }

            // Find homography and corners of the television in the scene
            if (goodMatches.Size >= 4) // Need at least 4 matches
            {
                PointF[] ptsTelevision = new PointF[goodMatches.Size];
                PointF[] ptsScene = new PointF[goodMatches.Size];
                for (int i = 0; i < goodMatches.Size; i++)
                {
                    ptsTelevision[i] = keypointsTelevision[goodMatches[i].QueryIdx].Point;
                    ptsScene[i] = keypointsScene[goodMatches[i].TrainIdx].Point;
                }

                Mat homography = CvInvoke.FindHomography(ptsTelevision, ptsScene, RobustEstimationAlgorithm.Ransac, 5);
                if (homography != null)
                {
                    // Draw the detected corners on the scene image
                    Rectangle rect = new Rectangle(Point.Empty, televisionImage.Size);
                    PointF[] corners = new PointF[]
                    {
                    new PointF(rect.Left, rect.Top),
                    new PointF(rect.Right, rect.Top),
                    new PointF(rect.Right, rect.Bottom),
                    new PointF(rect.Left, rect.Bottom)
                    };
                    corners = CvInvoke.PerspectiveTransform(corners, homography);

                    for (int i = 0; i < 4; i++)
                        CvInvoke.Line(sceneImage, Point.Round(corners[i]), Point.Round(corners[(i + 1) % 4]), new MCvScalar(0, 255, 0), 2);
                }
            }

            // Show the result
            CvInvoke.Imshow("Detected Television", sceneImage);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
        }
        private static void Test()
        {
            Mat roiImage = new Mat();
            Console.WriteLine("Running2");
            // Path to the input image
            Image<Bgr,byte> image = null;
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Image<Bgr, byte>(dialog.FileName).Resize(640, 480, Inter.Cubic);
            }
          

            // Load the image
           
            

            // Initialize the YOLO model
            string model = "D:\\Project\\Yolo\\yolov3.weights"; // Path to the weights file
            string cfg = "D:\\Project\\Yolo\\yolov3.cfg"; // Path to the cfg file
            string classNamesFile = "D:\\Project\\Yolo\\coco.names"; // Path to the COCO class names file

            // Load the class names
            string[] classNames = System.IO.File.ReadAllLines(classNamesFile);

            // Load the network
            Net net = DnnInvoke.ReadNetFromDarknet(cfg, model);

            // Prepare the image for the model
            Mat inputBlob = DnnInvoke.BlobFromImage(image, 1 / 255.0, new Size(640, 480), swapRB: true);

            // Set the input to the network
            net.SetInput(inputBlob);
            //set prefer backend

            //  net.SetPreferableBackend(Emgu.CV.Dnn.Backend.OpenCV);

            //  net.SetPreferableTarget(Target.Cpu);

            // Perform the detection
            VectorOfMat outputs = new VectorOfMat();
            net.Forward(outputs, net.UnconnectedOutLayersNames);

            Console.WriteLine(outputs);
            // Process the results
            for (int i = 0; i < outputs.Size; i++)
            {
                Mat output = outputs[i];
                float[,] data = output.GetData(true) as float[,];
                float maxConfident = 0;
                var dataToEvaluate = 0;
                for (int j = 0; j < data.GetLength(0); j++)
                {
                    float confidence = data[j, 4];
                    if (confidence > maxConfident)
                    {
                        maxConfident = confidence;
                        dataToEvaluate = j;
                    }                
                }
               
                    // Find the class with the highest score
                    int classId = 0;
                    float maxScore = 0;
                    for (int k = 5; k < data.GetLength(1); k++)
                    {
                        if (data[dataToEvaluate, k] > maxScore)
                        {
                            maxScore = data[dataToEvaluate, k];
                            classId = k - 5;
                        }
                    }
                    // Check if the detected class is a television
                    if (classNames[classId] == "tvmonitor")
                    {
                        // Extract the bounding box
                        int centerX = (int)(data[dataToEvaluate, 0] * image.Width);
                        int centerY = (int)(data[dataToEvaluate, 1] * image.Height);
                        int width = (int)(data[dataToEvaluate, 2] * image.Width);
                        int height = (int)(data[dataToEvaluate, 3] * image.Height);
                        Rectangle rect = new Rectangle(centerX - width / 2, centerY - height / 2, width+10, height+10);

                    // Create a new image containing only the ROI
                         roiImage = new Mat(image.Mat, rect);
                    // Draw the bounding box
                    //  CvInvoke.Rectangle(image, rect, new MCvScalar(0, 255, 0), 2);
                }
                
            }
           
            // Show the image
            CvInvoke.Imshow("Detected Televisions", roiImage);
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyAllWindows();
 
      
        }
       
    }
}