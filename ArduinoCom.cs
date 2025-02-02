using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace LightGun
{
    public  class ArduinoCom
    {  
        SerialPort port = new SerialPort();
        public void OpenPort(string port){
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
        public void ClosePort()
        {
            this.port.Close();
        }
        public void SendCursorPos(int x, int y)
        {
            
           

            String data = $"0 {x} {y}\n";
            if(port.BytesToWrite == 0)
            {
                port.Write(data);
               
              
            }
        }

    }
}
