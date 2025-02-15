using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGun.UIControl
{
    internal class OverlayPanel
    {
        private LightGunCompoment.LightGun lightGunP1;
        private LightGunCompoment.LightGun lightGunP2;

        public OverlayPanel(LightGunCompoment.LightGun lightGunP1, LightGunCompoment.LightGun lightGunP2)
        {
            this.lightGunP1 = lightGunP1;
            this.lightGunP2 = lightGunP2;
        }

        public void StartP1(object? sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            bool isStartP1 = lightGunP1.GetIsStart();

            lightGunP1.SetIsStart(!isStartP1);
           

            if (!isStartP1)
            {
                button.BackColor = Color.Red;
                button.Text = "Stop";
            }
            else
            {
                button.BackColor = Color.Green;
                button.Text = "Start";
            }
        }
        public void StartP2(object? sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            bool isStartP2 = lightGunP2.GetIsStart();
   
            lightGunP2.SetIsStart(!isStartP2);

            if (!isStartP2)
            {
                button.BackColor = Color.Red;
                button.Text = "Stop";
            }
            else
            {
                button.BackColor = Color.Green;
                button.Text = "Start";
            }
        }
    }
}
