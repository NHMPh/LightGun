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

        public enum AssignmentZone
        {
            P1Normal,
            P1Offscreen,
            P2Normal,
            P2Offscreen
        }
        public enum Type
        {
            Normal,
            Offscreen,
           
        }
        private ComboBox[] normalP1 = new ComboBox[22];
        public ButtonAssignmentTab(LightGunCompoment.LightGun lightGunP1, LightGunCompoment.LightGun lightGunP2)
        {
            this.lightGunP1 = lightGunP1;
            this.lightGunP2 = lightGunP2;
        }
        public void ComboBoxChangeButton(object? sender, EventArgs e)
        {
            int id = int.Parse((sender as ComboBox).Name.Substring("comboBox".Length));
            switch (CheckZone(id))
            {
                case AssignmentZone.P1Normal:
                    lightGunP1.SetButton((int)Type.Normal,id-1,ButtonCodeConvert((sender as ComboBox).SelectedValue.ToString()));
                    break;
                case AssignmentZone.P1Offscreen:
                    lightGunP1.SetButton((int)Type.Offscreen, id - 1, ButtonCodeConvert((sender as ComboBox).SelectedValue.ToString()));
                    break;
                case AssignmentZone.P2Normal:
                    lightGunP2.SetButton((int)Type.Normal, id - 1, ButtonCodeConvert((sender as ComboBox).SelectedValue.ToString()));
                    break;
                case AssignmentZone.P2Offscreen:
                    lightGunP2.SetButton((int)Type.Offscreen, id - 1, ButtonCodeConvert((sender as ComboBox).SelectedValue.ToString()));
                    break;
            }
        }
        public byte ButtonCodeConvert(string value)
        {
            throw new NotImplementedException();

            return 0;
        }
        private AssignmentZone CheckZone(int value)
        {
            if (value >= 1 && value <= 22)
                return AssignmentZone.P1Normal;
            if (value >= 23 && value <= 44)
                return AssignmentZone.P1Offscreen;
            if (value >= 45 && value <= 66)
                return AssignmentZone.P2Normal;
            return AssignmentZone.P2Offscreen;
        }
    }
}
