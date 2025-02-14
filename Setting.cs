using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGun
{
    public class ButtonSelection
    {
        public int SelectedIndex { get; set; }
    }
    public class PlayerSettings
    {
        public int Threshold { get; set; }
        public int Xoffset { get; set; }
        public int Yoffset { get; set; }
        public int Brightness { get; set; }
        public int Contrast { get; set; }
        public int Gamma { get; set; }
        public int Exposure { get; set; }

        public ButtonSelection[] NormalButton { get; set; }

        public ButtonSelection[] OffscreenButton { get; set; }
    }

    public class Settings
    {
        public PlayerSettings[] Players { get; set; }
        public float Border { get; set; }
    }
}
