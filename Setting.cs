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
        public int Threshold { get; set; } = 85;
        public int Xoffset { get; set; } = 0;
        public int Yoffset { get; set; } = 0;
        public int Brightness { get; set; } = 64;
        public int Contrast { get; set; } = 0;
        public int Gamma { get; set; } = 300;
        public int Exposure { get; set; } = -8;

        public ButtonSelection[] NormalButton { get; set; }

        public ButtonSelection[] OffscreenButton { get; set; }
    }

    public class Settings
    {
        public PlayerSettings[] Players { get; set; } = new PlayerSettings[2];
        public float Border { get; set; }
        public bool IsRawCheck { get; set; }

        public bool IsProcessCheck { get; set; }

        public bool Is43BorderCheck { get; set; }

        public bool IsJoyCheck { get; set; }

        public bool IsZAxisCheck { get; set; }

        public bool IsAntiDriftCheck { get;set; }
    }
}
