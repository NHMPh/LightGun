using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGun.LightGunCompoment
{
    public class ArduinoMouse
    {
        private SerialPort port = new SerialPort();
        public void OpenPort(string port)
        {
            ClosePort();
            this.port = new SerialPort(port, 115200);
            try
            {
                this.port.Open();
                MessageBox.Show("Open");
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
            if (port.BytesToWrite == 0)
            {
                port.Write(data);
            }
        }
    }
}
