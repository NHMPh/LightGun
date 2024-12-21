using AutoHotkey.Interop;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace LightGun
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            button1 = new Button();
            pictureBox2 = new PictureBox();
            tTrackBar = new TrackBar();
            tLable = new Label();
            tTextBox = new TextBox();
            bTextBox = new TextBox();
            bLabel = new Label();
            bTrackBar = new TrackBar();
            rawCheckBox = new CheckBox();
            processCheckBox = new CheckBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            hTextBox = new TextBox();
            label2 = new Label();
            hTrackBar = new TrackBar();
            cTextBox = new TextBox();
            label3 = new Label();
            cTrackBar = new TrackBar();
            shTextBox = new TextBox();
            label4 = new Label();
            shTrackBar = new TrackBar();
            saTextBox = new TextBox();
            label5 = new Label();
            saTrackBar = new TrackBar();
            wTextBox = new TextBox();
            label6 = new Label();
            wTrackBar = new TrackBar();
            gTextBox = new TextBox();
            label7 = new Label();
            gTrackBar = new TrackBar();
            eTextBox = new TextBox();
            label9 = new Label();
            eTrackBar = new TrackBar();
            label10 = new Label();
            button11 = new Button();
            label13 = new Label();
            up1Button = new Button();
            left1Button = new Button();
            right1Button = new Button();
            down1Button = new Button();
            down10Button = new Button();
            right10Button = new Button();
            left10Button = new Button();
            up10Button = new Button();
            panel1 = new Panel();
            label8 = new Label();
            borderTextBox = new TextBox();
            label12 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            clickOutComboBox = new ComboBox();
            holdOutComboBox = new ComboBox();
            label18 = new Label();
            button2 = new Button();
            label19 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)shTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)saTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eTrackBar).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ActiveBorder;
            pictureBox1.Location = new Point(210, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(317, 217);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Green;
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(980, 46);
            button1.Name = "button1";
            button1.Size = new Size(155, 41);
            button1.TabIndex = 1;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.ActiveBorder;
            pictureBox2.Location = new Point(573, 47);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(317, 217);
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // tTrackBar
            // 
            tTrackBar.Location = new Point(134, 308);
            tTrackBar.Maximum = 255;
            tTrackBar.Name = "tTrackBar";
            tTrackBar.Size = new Size(367, 45);
            tTrackBar.TabIndex = 10;
            tTrackBar.Value = 200;
            tTrackBar.ValueChanged += TTrackBar_ValueChanged;
            // 
            // tLable
            // 
            tLable.AutoSize = true;
            tLable.Location = new Point(71, 308);
            tLable.Name = "tLable";
            tLable.Size = new Size(67, 15);
            tLable.TabIndex = 11;
            tLable.Text = "Threadhold";
            // 
            // tTextBox
            // 
            tTextBox.Location = new Point(507, 308);
            tTextBox.Name = "tTextBox";
            tTextBox.ReadOnly = true;
            tTextBox.Size = new Size(100, 23);
            tTextBox.TabIndex = 14;
            // 
            // bTextBox
            // 
            bTextBox.Location = new Point(507, 346);
            bTextBox.Name = "bTextBox";
            bTextBox.ReadOnly = true;
            bTextBox.Size = new Size(100, 23);
            bTextBox.TabIndex = 23;
            // 
            // bLabel
            // 
            bLabel.AutoSize = true;
            bLabel.Location = new Point(71, 346);
            bLabel.Name = "bLabel";
            bLabel.Size = new Size(62, 15);
            bLabel.TabIndex = 22;
            bLabel.Text = "Brightness";
            // 
            // bTrackBar
            // 
            bTrackBar.Location = new Point(134, 346);
            bTrackBar.Maximum = 64;
            bTrackBar.Minimum = -64;
            bTrackBar.Name = "bTrackBar";
            bTrackBar.Size = new Size(367, 45);
            bTrackBar.TabIndex = 21;
            bTrackBar.ValueChanged += BTrackBar_ValueChanged;
            // 
            // rawCheckBox
            // 
            rawCheckBox.AutoSize = true;
            rawCheckBox.Checked = true;
            rawCheckBox.CheckState = CheckState.Checked;
            rawCheckBox.Location = new Point(328, 270);
            rawCheckBox.Name = "rawCheckBox";
            rawCheckBox.Size = new Size(77, 19);
            rawCheckBox.TabIndex = 33;
            rawCheckBox.Text = "Show raw";
            rawCheckBox.UseVisualStyleBackColor = true;
            // 
            // processCheckBox
            // 
            processCheckBox.AutoSize = true;
            processCheckBox.Checked = true;
            processCheckBox.CheckState = CheckState.Checked;
            processCheckBox.Location = new Point(683, 270);
            processCheckBox.Name = "processCheckBox";
            processCheckBox.Size = new Size(111, 19);
            processCheckBox.TabIndex = 34;
            processCheckBox.Text = "Show processed";
            processCheckBox.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(33, 75);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 35;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 57);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 36;
            label1.Text = "Select light gun";
            // 
            // hTextBox
            // 
            hTextBox.Location = new Point(507, 422);
            hTextBox.Name = "hTextBox";
            hTextBox.ReadOnly = true;
            hTextBox.Size = new Size(100, 23);
            hTextBox.TabIndex = 42;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(71, 422);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 41;
            label2.Text = "Hue";
            // 
            // hTrackBar
            // 
            hTrackBar.LargeChange = 100;
            hTrackBar.Location = new Point(134, 422);
            hTrackBar.Maximum = 2000;
            hTrackBar.Minimum = -2000;
            hTrackBar.Name = "hTrackBar";
            hTrackBar.Size = new Size(367, 45);
            hTrackBar.SmallChange = 100;
            hTrackBar.TabIndex = 40;
            hTrackBar.ValueChanged += HTrackBar_ValueChanged;
            // 
            // cTextBox
            // 
            cTextBox.Location = new Point(507, 384);
            cTextBox.Name = "cTextBox";
            cTextBox.ReadOnly = true;
            cTextBox.Size = new Size(100, 23);
            cTextBox.TabIndex = 39;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(71, 384);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 38;
            label3.Text = "Contrast";
            // 
            // cTrackBar
            // 
            cTrackBar.Location = new Point(134, 384);
            cTrackBar.Maximum = 95;
            cTrackBar.Name = "cTrackBar";
            cTrackBar.Size = new Size(367, 45);
            cTrackBar.TabIndex = 37;
            cTrackBar.ValueChanged += CTrackBar_ValueChanged;
            // 
            // shTextBox
            // 
            shTextBox.Location = new Point(507, 494);
            shTextBox.Name = "shTextBox";
            shTextBox.ReadOnly = true;
            shTextBox.Size = new Size(100, 23);
            shTextBox.TabIndex = 48;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(71, 494);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 47;
            label4.Text = "Sharpness";
            // 
            // shTrackBar
            // 
            shTrackBar.Location = new Point(134, 494);
            shTrackBar.Maximum = 7;
            shTrackBar.Minimum = 1;
            shTrackBar.Name = "shTrackBar";
            shTrackBar.Size = new Size(367, 45);
            shTrackBar.TabIndex = 46;
            shTrackBar.Value = 1;
            shTrackBar.ValueChanged += ShTrackBar_ValueChanged;
            // 
            // saTextBox
            // 
            saTextBox.Location = new Point(507, 456);
            saTextBox.Name = "saTextBox";
            saTextBox.ReadOnly = true;
            saTextBox.Size = new Size(100, 23);
            saTextBox.TabIndex = 45;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(71, 456);
            label5.Name = "label5";
            label5.Size = new Size(61, 15);
            label5.TabIndex = 44;
            label5.Text = "Saturation";
            // 
            // saTrackBar
            // 
            saTrackBar.Location = new Point(134, 456);
            saTrackBar.Maximum = 100;
            saTrackBar.Name = "saTrackBar";
            saTrackBar.Size = new Size(367, 45);
            saTrackBar.TabIndex = 43;
            saTrackBar.Value = 100;
            saTrackBar.ValueChanged += SaTrackBar_ValueChanged;
            // 
            // wTextBox
            // 
            wTextBox.Location = new Point(507, 568);
            wTextBox.Name = "wTextBox";
            wTextBox.ReadOnly = true;
            wTextBox.Size = new Size(100, 23);
            wTextBox.TabIndex = 54;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(57, 571);
            label6.Name = "label6";
            label6.Size = new Size(76, 15);
            label6.TabIndex = 53;
            label6.Text = "White blance";
            // 
            // wTrackBar
            // 
            wTrackBar.Location = new Point(134, 568);
            wTrackBar.Maximum = 6500;
            wTrackBar.Minimum = 2800;
            wTrackBar.Name = "wTrackBar";
            wTrackBar.Size = new Size(367, 45);
            wTrackBar.TabIndex = 52;
            wTrackBar.Value = 2800;
            wTrackBar.ValueChanged += WTrackBar_ValueChanged;
            // 
            // gTextBox
            // 
            gTextBox.Location = new Point(507, 530);
            gTextBox.Name = "gTextBox";
            gTextBox.ReadOnly = true;
            gTextBox.Size = new Size(100, 23);
            gTextBox.TabIndex = 51;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(71, 530);
            label7.Name = "label7";
            label7.Size = new Size(49, 15);
            label7.TabIndex = 50;
            label7.Text = "Gamma";
            // 
            // gTrackBar
            // 
            gTrackBar.Location = new Point(134, 530);
            gTrackBar.Maximum = 300;
            gTrackBar.Minimum = 100;
            gTrackBar.Name = "gTrackBar";
            gTrackBar.Size = new Size(367, 45);
            gTrackBar.TabIndex = 49;
            gTrackBar.Value = 200;
            gTrackBar.ValueChanged += GTrackBar_ValueChanged;
            // 
            // eTextBox
            // 
            eTextBox.Location = new Point(507, 605);
            eTextBox.Name = "eTextBox";
            eTextBox.ReadOnly = true;
            eTextBox.Size = new Size(100, 23);
            eTextBox.TabIndex = 57;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(67, 608);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 56;
            label9.Text = "Exposure";
            // 
            // eTrackBar
            // 
            eTrackBar.LargeChange = 1;
            eTrackBar.Location = new Point(134, 605);
            eTrackBar.Maximum = 0;
            eTrackBar.Minimum = -8;
            eTrackBar.Name = "eTrackBar";
            eTrackBar.Size = new Size(367, 45);
            eTrackBar.TabIndex = 55;
            eTrackBar.Value = -7;
            eTrackBar.ValueChanged += ETrackBar_ValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(328, 18);
            label10.Name = "label10";
            label10.Size = new Size(61, 15);
            label10.TabIndex = 60;
            label10.Text = "Raw video";
            // 
            // button11
            // 
            button11.BackColor = SystemColors.Highlight;
            button11.ForeColor = SystemColors.ControlLight;
            button11.Location = new Point(622, 629);
            button11.Name = "button11";
            button11.Size = new Size(154, 46);
            button11.TabIndex = 68;
            button11.Text = "Save";
            button11.UseVisualStyleBackColor = false;
            button11.Click += button11_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(725, 290);
            label13.Name = "label13";
            label13.Size = new Size(68, 15);
            label13.TabIndex = 70;
            label13.Text = "Calibration:";
            // 
            // up1Button
            // 
            up1Button.Location = new Point(980, 383);
            up1Button.Name = "up1Button";
            up1Button.Size = new Size(65, 61);
            up1Button.TabIndex = 72;
            up1Button.Text = "+1";
            up1Button.UseVisualStyleBackColor = true;
            up1Button.Click += up1Button_Click;
            // 
            // left1Button
            // 
            left1Button.Location = new Point(909, 443);
            left1Button.Name = "left1Button";
            left1Button.Size = new Size(65, 61);
            left1Button.TabIndex = 73;
            left1Button.Text = "-1";
            left1Button.UseVisualStyleBackColor = true;
            left1Button.Click += left1Button_Click;
            // 
            // right1Button
            // 
            right1Button.Location = new Point(1051, 443);
            right1Button.Name = "right1Button";
            right1Button.Size = new Size(65, 61);
            right1Button.TabIndex = 74;
            right1Button.Text = "+1";
            right1Button.UseVisualStyleBackColor = true;
            right1Button.Click += right1Button_Click;
            // 
            // down1Button
            // 
            down1Button.Location = new Point(980, 506);
            down1Button.Name = "down1Button";
            down1Button.Size = new Size(65, 61);
            down1Button.TabIndex = 75;
            down1Button.Text = "-1";
            down1Button.UseVisualStyleBackColor = true;
            down1Button.Click += down1Button_Click;
            // 
            // down10Button
            // 
            down10Button.Location = new Point(958, 573);
            down10Button.Name = "down10Button";
            down10Button.Size = new Size(106, 88);
            down10Button.TabIndex = 79;
            down10Button.Text = "-10";
            down10Button.UseVisualStyleBackColor = true;
            down10Button.Click += down10Button_Click;
            // 
            // right10Button
            // 
            right10Button.Location = new Point(1122, 429);
            right10Button.Name = "right10Button";
            right10Button.Size = new Size(106, 88);
            right10Button.TabIndex = 78;
            right10Button.Text = "+10";
            right10Button.UseVisualStyleBackColor = true;
            right10Button.Click += right10Button_Click;
            // 
            // left10Button
            // 
            left10Button.Location = new Point(800, 429);
            left10Button.Name = "left10Button";
            left10Button.Size = new Size(106, 88);
            left10Button.TabIndex = 77;
            left10Button.Text = "-10";
            left10Button.UseVisualStyleBackColor = true;
            left10Button.Click += left10Button_Click;
            // 
            // up10Button
            // 
            up10Button.Location = new Point(958, 289);
            up10Button.Name = "up10Button";
            up10Button.Size = new Size(106, 88);
            up10Button.TabIndex = 76;
            up10Button.Text = "+10";
            up10Button.UseVisualStyleBackColor = true;
            up10Button.Click += up10Button_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaptionText;
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Location = new Point(800, 288);
            panel1.Name = "panel1";
            panel1.Size = new Size(428, 380);
            panel1.TabIndex = 80;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(941, 111);
            label8.Name = "label8";
            label8.Size = new Size(104, 15);
            label8.TabIndex = 81;
            label8.Text = "Border Percentage";
            // 
            // borderTextBox
            // 
            borderTextBox.Location = new Point(1064, 108);
            borderTextBox.Name = "borderTextBox";
            borderTextBox.Size = new Size(100, 23);
            borderTextBox.TabIndex = 82;
            borderTextBox.TextChanged += TextBox1_TextChanged;
            borderTextBox.KeyPress += TextBox1_KeyPress;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(941, 149);
            label12.Name = "label12";
            label12.Size = new Size(112, 15);
            label12.TabIndex = 83;
            label12.Text = "Click outside screen";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(941, 190);
            label14.Name = "label14";
            label14.Size = new Size(112, 15);
            label14.TabIndex = 85;
            label14.Text = "Hold outside screen";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(941, 222);
            label15.MaximumSize = new Size(250, 0);
            label15.Name = "label15";
            label15.Size = new Size(0, 15);
            label15.TabIndex = 87;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(1170, 111);
            label16.Name = "label16";
            label16.Size = new Size(61, 15);
            label16.TabIndex = 89;
            label16.Text = "(LCtrl + B)";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(1141, 59);
            label17.Name = "label17";
            label17.Size = new Size(66, 15);
            label17.TabIndex = 90;
            label17.Text = "(LShift + B)";
            // 
            // clickOutComboBox
            // 
            clickOutComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            clickOutComboBox.FormattingEnabled = true;
            clickOutComboBox.Items.AddRange(new object[] { "Click Left", "Click Right", "Click Middle", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "Esc", "Tab", "Shift", "Ctrl", "Alt", "Space", "Enter", "Backspace", "Up", "Down", "Left", "Right", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "Numpad0", "Numpad1", "Numpad2", "Numpad3", "Numpad4", "Numpad5", "Numpad6", "Numpad7", "Numpad8", "Numpad9" });
            clickOutComboBox.Location = new Point(1064, 146);
            clickOutComboBox.Name = "clickOutComboBox";
            clickOutComboBox.Size = new Size(100, 23);
            clickOutComboBox.TabIndex = 91;
            clickOutComboBox.SelectedIndexChanged += clickOutComboBox_SelectedIndexChanged;
            // 
            // holdOutComboBox
            // 
            holdOutComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            holdOutComboBox.FormattingEnabled = true;
            holdOutComboBox.Items.AddRange(new object[] { "Click Left", "Click Right", "Click Middle", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "Esc", "Tab", "Shift", "Ctrl", "Alt", "Space", "Enter", "Backspace", "Up", "Down", "Left", "Right", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "Numpad0", "Numpad1", "Numpad2", "Numpad3", "Numpad4", "Numpad5", "Numpad6", "Numpad7", "Numpad8", "Numpad9" });
            holdOutComboBox.Location = new Point(1064, 187);
            holdOutComboBox.Name = "holdOutComboBox";
            holdOutComboBox.Size = new Size(100, 23);
            holdOutComboBox.TabIndex = 92;
            holdOutComboBox.SelectedIndexChanged += holdOutComboBox_SelectedIndexChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(1004, 237);
            label18.Name = "label18";
            label18.Size = new Size(112, 15);
            label18.TabIndex = 93;
            label18.Text = "Created by MHMPh";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Control;
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Location = new Point(52, 104);
            button2.Name = "button2";
            button2.Size = new Size(71, 23);
            button2.TabIndex = 94;
            button2.Text = "Refresh";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(664, 18);
            label19.Name = "label19";
            label19.Size = new Size(129, 15);
            label19.TabIndex = 96;
            label19.Text = "MPCV Proceesed video";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 687);
            Controls.Add(label19);
            Controls.Add(button2);
            Controls.Add(label18);
            Controls.Add(holdOutComboBox);
            Controls.Add(clickOutComboBox);
            Controls.Add(label17);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(label12);
            Controls.Add(borderTextBox);
            Controls.Add(label8);
            Controls.Add(down10Button);
            Controls.Add(right10Button);
            Controls.Add(left10Button);
            Controls.Add(up10Button);
            Controls.Add(down1Button);
            Controls.Add(right1Button);
            Controls.Add(left1Button);
            Controls.Add(up1Button);
            Controls.Add(label13);
            Controls.Add(button11);
            Controls.Add(label10);
            Controls.Add(eTextBox);
            Controls.Add(label9);
            Controls.Add(eTrackBar);
            Controls.Add(wTextBox);
            Controls.Add(label6);
            Controls.Add(wTrackBar);
            Controls.Add(gTextBox);
            Controls.Add(label7);
            Controls.Add(gTrackBar);
            Controls.Add(shTextBox);
            Controls.Add(label4);
            Controls.Add(shTrackBar);
            Controls.Add(saTextBox);
            Controls.Add(label5);
            Controls.Add(saTrackBar);
            Controls.Add(hTextBox);
            Controls.Add(label2);
            Controls.Add(hTrackBar);
            Controls.Add(cTextBox);
            Controls.Add(label3);
            Controls.Add(cTrackBar);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(processCheckBox);
            Controls.Add(rawCheckBox);
            Controls.Add(bTextBox);
            Controls.Add(bLabel);
            Controls.Add(bTrackBar);
            Controls.Add(tTextBox);
            Controls.Add(tLable);
            Controls.Add(tTrackBar);
            Controls.Add(pictureBox2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Cursor = Cursors.Cross;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "NHMPh's Light Gun";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)tTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)bTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)hTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)cTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)shTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)saTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)wTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)gTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)eTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void clickOutComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            clickOutside = (sender as ComboBox).SelectedItem.ToString();
            ahk.SetVar("clickAction", $"{clickOutside}");
            settings.ClickOutSide = clickOutside;
            this.ActiveControl = null;
        }
        private void holdOutComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            holdOutside = (sender as ComboBox).SelectedItem.ToString();
            ahk.SetVar("holdAction", $"{holdOutside}");
            settings.HoldOutSide = holdOutside;
            this.ActiveControl = null;
        }
        private void ClickOutTextBox_TextChanged(object sender, EventArgs e)
        {

            if ((sender as TextBox).Text == string.Empty)
            {
                return;
            }
           

        }

        private void HoldOutTextBox_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text == string.Empty)
            {
                return;
            }

            holdOutside = (sender as TextBox).Text;
            settings.HoldOutSide = holdOutside;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

            if ((sender as TextBox).Text == string.Empty)
            {
                (sender as TextBox).Text = "0";
            }
            sWidth = float.Parse((sender as TextBox).Text.ToString());
            settings.Border = sWidth;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (backspace, delete, etc.)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow digits only
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;


            }

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            streamVideo = false;
            capture.Release();
            cameraIndex = (sender as ComboBox).SelectedIndex;
            capture = new VideoCapture(cameraIndex);
            streamVideo = true;
            Task.Run(async () => await StreamVideo());
        }

        private void ETrackBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            settings.Exposure = value;
            eTextBox.Text = value.ToString();
            capture.Set(CapProp.Exposure, value);
        }

        private void GTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            settings.Gamma = value;
            gTextBox.Text = value.ToString();
            capture.Set(CapProp.Gamma, value);
        }

        private void WTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            settings.WhiteBalance = value;
            wTextBox.Text = value.ToString();
            capture.Set(CapProp.WhiteBalanceRedV, value);
        }

        private void SaTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            settings.Saturation = value;
            saTextBox.Text = value.ToString();
            capture.Set(CapProp.Saturation, value);
        }

        private void ShTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            settings.Sharpness = value;
            shTextBox.Text = value.ToString();
            capture.Set(CapProp.Sharpness, value);
        }

        private void CTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            settings.Contrast = value;
            cTextBox.Text = value.ToString();
            capture.Set(CapProp.Contrast, value);
        }

        private void HTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            settings.Hue = value;
            hTextBox.Text = value.ToString();
            capture.Set(CapProp.Hue, value);
        }

        private void BTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            settings.Brightness = value;
            bTextBox.Text = value.ToString();
            capture.Set(CapProp.Brightness, value);
        }

        private void TTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            threadhold = value;
            settings.Threadhold = value;
            tTextBox.Text = value.ToString();
        }



        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TrackBar tTrackBar;
        private Label tLable;
        private TextBox tTextBox;
        private TextBox bTextBox;
        private Label bLabel;
        private TrackBar bTrackBar;
        private CheckBox rawCheckBox;
        private CheckBox processCheckBox;
        private ComboBox comboBox1;
        private Label label1;
        private TextBox hTextBox;
        private Label label2;
        private TrackBar hTrackBar;
        private TextBox cTextBox;
        private Label label3;
        private TrackBar cTrackBar;
        private TextBox shTextBox;
        private Label label4;
        private TrackBar shTrackBar;
        private TextBox saTextBox;
        private Label label5;
        private TrackBar saTrackBar;
        private TextBox wTextBox;
        private Label label6;
        private TrackBar wTrackBar;
        private TextBox gTextBox;
        private Label label7;
        private TrackBar gTrackBar;
        private TextBox eTextBox;
        private Label label9;
        private TrackBar eTrackBar;
        private Label label10;
        private Button button11;
        private Label label13;
        private Button up1Button;
        private Button left1Button;
        private Button right1Button;
        private Button down1Button;
        private Button down10Button;
        private Button right10Button;
        private Button left10Button;
        private Button up10Button;
        private Panel panel1;
        private Label label8;
        private TextBox borderTextBox;
        private Label label12;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private ComboBox clickOutComboBox;
        private ComboBox holdOutComboBox;
        private Label label18;
        private Button button2;
        private Label label19;
        private static Button button1;
    }
}
