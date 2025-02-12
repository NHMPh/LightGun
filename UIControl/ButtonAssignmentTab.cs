using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGun.UIControl
{
    internal class ButtonAssignmentTab
    {
        private LightGunCompoment.LightGun lightGunP1;
        private LightGunCompoment.LightGun lightGunP2;
        public ButtonAssignmentTab(LightGunCompoment.LightGun lightGunP1, LightGunCompoment.LightGun lightGunP2)
        {
            this.lightGunP1 = lightGunP1;
            this.lightGunP2 = lightGunP2;
        }
    }
}
