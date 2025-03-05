
using ArduinoUploader;
using ArduinoUploader.Hardware;
using System.IO.Ports;

namespace LightGun.LightGunCompoment
{
    public class ArduinoMouse
    {
        private SerialPort port = new SerialPort();

        public ArduinoMouse()
        {

        }
        public void OpenPort(string port)
        {
            ClosePort();
            this.port = new SerialPort(port, 115200);
            try
            {
                this.port.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool isOpen()
        {
            return port.IsOpen;
        }
        public void ClosePort()
        {
            port.Close();
        }
        public void SendCursorPos(Point point)
        {
            string data = $"0 {point.X} {point.Y}\n";

            while (port.BytesToWrite != 0) ;

            port.Write(data);
        }
        public void SendNewButtonAssignment(int type, int index, byte buttonCode)
        {

            //type = 0 normal type = 1 offscreen  
            string data = $"1 {type} {index} {buttonCode}\n";

            while (port.BytesToWrite != 0) ;

            port.Write(data);

        }
        public void UploadFirmware(ArduinoModel model,int playerIndex)
        {
            port.Close();
            var uploader = new ArduinoSketchUploader(
                    new ArduinoSketchUploaderOptions()
                    {
                        PortName = port.PortName,
                        ArduinoModel = model,
                        
                    });
            try
            {
                if(playerIndex ==0)
                uploader.UploadFile(".\\ArduinoMouseFirmware\\ArduinoMouseFirmwareP1.ino.hex");
                else
                uploader.UploadFile(".\\ArduinoMouseFirmware\\ArduinoMouseFirmwareP2.ino.hex");

                MessageBox.Show("Upload success! Please reconnect your arduino");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());         
            }
           

        }

    }
}
