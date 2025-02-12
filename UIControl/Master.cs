using LightGun.LightGunCompoment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGun.UIControl
{
    internal class Master
    {

        private LightGunCompoment.LightGun lightGunP1;
        private LightGunCompoment.LightGun lightGunP2;

       
        public MainTab mainTab;
        public ButtonAssignmentTab buttonAssignmentTab;
        public CalibrationTab calibrationTab;
        public Master()
        {
            lightGunP1 = new LightGunCompoment.LightGun(0);
            lightGunP2 = new LightGunCompoment.LightGun(1);
            ////////
            mainTab = new MainTab(lightGunP1,lightGunP2);
            buttonAssignmentTab = new ButtonAssignmentTab( lightGunP1, lightGunP2);
            calibrationTab = new CalibrationTab(lightGunP1, lightGunP2);
        }
    }
}
