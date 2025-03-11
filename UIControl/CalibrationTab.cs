using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGun.UIControl
{
    internal class CalibrationTab
    {
        private LightGunCompoment.LightGun lightGunP1;
        private LightGunCompoment.LightGun lightGunP2;
        public CalibrationTab(LightGunCompoment.LightGun lightGunP1, LightGunCompoment.LightGun lightGunP2)
        {
            this.lightGunP1 = lightGunP1;
            this.lightGunP2 = lightGunP2;
        }
        public void up10ButtonP1(object sender, EventArgs e)
        {
            lightGunP1.SetProcessorOffset(0, -10);
        }

        public void down10ButtonP1(object sender, EventArgs e)
        {
            lightGunP1.SetProcessorOffset(0, 10);
        }

        public void left10ButtonP1(object sender, EventArgs e)
        {
            lightGunP1.SetProcessorOffset(-10, 0);
        }

        public void right10ButtonP1(object sender, EventArgs e)
        {
            lightGunP1.SetProcessorOffset(10, 0);
        }

        public void up1ButtonP1(object sender, EventArgs e)
        {
            lightGunP1.SetProcessorOffset(0, -1);
        }

        public void down1ButtonP1(object sender, EventArgs e)
        {
            lightGunP1.SetProcessorOffset(0, 1);
        }

        public void left1ButtonP1(object sender, EventArgs e)
        {
            lightGunP1.SetProcessorOffset(-1, 0);
        }

        public void right1ButtonP1(object sender, EventArgs e)
        {
            lightGunP1.SetProcessorOffset(1, 0);
        }

        public void up10ButtonP2(object sender, EventArgs e)
        {
            lightGunP2.SetProcessorOffset(0, -10);
        }

        public void down10ButtonP2(object sender, EventArgs e)
        {
            lightGunP2.SetProcessorOffset(0, 10);
        }

        public void left10ButtonP2(object sender, EventArgs e)
        {
            lightGunP2.SetProcessorOffset(-10, 0);
        }

        public void right10ButtonP2(object sender, EventArgs e)
        {
            lightGunP2.SetProcessorOffset(10, 0);
        }

        public void up1ButtonP2(object sender, EventArgs e)
        {
            lightGunP2.SetProcessorOffset(0, -1);
        }

        public void down1ButtonP2(object sender, EventArgs e)
        {
            lightGunP2.SetProcessorOffset(0, 1);
        }

        public void left1ButtonP2(object sender, EventArgs e)
        {
            lightGunP2.SetProcessorOffset(-1, 0);
        }

        public void right1ButtonP2(object sender, EventArgs e)
        {
            lightGunP2.SetProcessorOffset(1, 0);
        }
    }
}
