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
        public void ComboBoxChangeButton(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            int id = int.Parse(comboBox.Name.Substring("comboBox".Length));
            switch (CheckZone(id))
            {
                case AssignmentZone.P1Normal:
                    lightGunP1.SaveButtonSetting(id - 1, (int)Type.Normal, comboBox.SelectedIndex);
                    if (!lightGunP1.SetButton((int)Type.Normal, id - 1, ButtonCodeConvert(comboBox.SelectedItem.ToString())))
                    {
                        comboBox.SelectedIndexChanged -= ComboBoxChangeButton;
                        comboBox.SelectedIndex = int.Parse(comboBox.Tag.ToString());
                        comboBox.SelectedIndexChanged += ComboBoxChangeButton;
                    }

                    break;
                case AssignmentZone.P1Offscreen:
                    lightGunP1.SaveButtonSetting(id - 1 - 22, (int)Type.Offscreen, comboBox.SelectedIndex);
                    if (!lightGunP1.SetButton((int)Type.Offscreen, id - 1 - 22, ButtonCodeConvert(comboBox.SelectedItem.ToString())))
                    {
                        comboBox.SelectedIndexChanged -= ComboBoxChangeButton;
                        comboBox.SelectedIndex = int.Parse(comboBox.Tag.ToString());
                        comboBox.SelectedIndexChanged += ComboBoxChangeButton;
                    }
                    break;
                case AssignmentZone.P2Normal:
                    lightGunP2.SaveButtonSetting(id - 1 - 44, (int)Type.Normal, comboBox.SelectedIndex);
                    if (!lightGunP2.SetButton((int)Type.Normal, id - 1 - 44, ButtonCodeConvert(comboBox.SelectedItem.ToString())))
                    {
                        comboBox.SelectedIndexChanged -= ComboBoxChangeButton;
                        comboBox.SelectedIndex = int.Parse(comboBox.Tag.ToString());
                        comboBox.SelectedIndexChanged += ComboBoxChangeButton;
                    }
                    break;
                case AssignmentZone.P2Offscreen:
                    lightGunP2.SaveButtonSetting(id - 1 - 66, (int)Type.Offscreen, comboBox.SelectedIndex);
                    if (!lightGunP2.SetButton((int)Type.Offscreen, id - 1 - 66, ButtonCodeConvert(comboBox.SelectedItem.ToString())))
                    {
                        comboBox.SelectedIndexChanged -= ComboBoxChangeButton;
                        comboBox.SelectedIndex = int.Parse(comboBox.Tag.ToString());
                        comboBox.SelectedIndexChanged += ComboBoxChangeButton;
                    }
                    break;
            }
        }
        private byte ButtonCodeConvert(string value)
        {

            switch (value)
            {
                case "None": return 0;
                case "Click": return 1;
                case "Click Left": return 1;
                case "Click Right": return 2;
                case "Click Middle": return 3;
                case "Border On/Off": return 254;
                case "a": return 4;
                case "b": return 5;
                case "c": return 6;
                case "d": return 7;
                case "e": return 8;
                case "f": return 9;
                case "g": return 10;
                case "h": return 11;
                case "i": return 12;
                case "j": return 13;
                case "k": return 14;
                case "l": return 15;
                case "m": return 16;
                case "n": return 17;
                case "o": return 18;
                case "p": return 19;
                case "q": return 20;
                case "r": return 21;
                case "s": return 22;
                case "t": return 23;
                case "u": return 24;
                case "v": return 25;
                case "w": return 26;
                case "x": return 27;
                case "y": return 28;
                case "z": return 29;
                case "0": return 39;
                case "1": return 30;
                case "2": return 31;
                case "3": return 32;
                case "4": return 33;
                case "5": return 34;
                case "6": return 35;
                case "7": return 36;
                case "8": return 37;
                case "9": return 38;
                case "F1": return 58;
                case "F2": return 59;
                case "F3": return 60;
                case "F4": return 61;
                case "F5": return 62;
                case "F6": return 63;
                case "F7": return 64;
                case "F8": return 65;
                case "F9": return 66;
                case "F10": return 67;
                case "F11": return 68;
                case "F12": return 69;
                case "Esc": return 41;
                case "Tab": return 43;
                case "Space": return 44;
                case "Enter": return 40;
                case "Backspace": return 42;
                case "Up": return 82;
                case "Down": return 81;
                case "Left": return 80;
                case "Right": return 79;
                default: return 0;
            }
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
        public void SetButton(string comboBoxValue, int playerIndex, int type, int index)
        {
            if (playerIndex == 0)
                lightGunP1.SetButton(type, index, ButtonCodeConvert(comboBoxValue));
            else
                lightGunP2.SetButton(type, index, ButtonCodeConvert(comboBoxValue));
        }
    }
}
