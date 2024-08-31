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
            label8 = new Label();
            comboBox2 = new ComboBox();
            label10 = new Label();
            label11 = new Label();
            button11 = new Button();
            button12 = new Button();
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
            pictureBox1.Location = new Point(210, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(317, 217);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(937, 57);
            button1.Name = "button1";
            button1.Size = new Size(155, 41);
            button1.TabIndex = 1;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox2
            // 
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
            tTextBox.Size = new Size(100, 23);
            tTextBox.TabIndex = 14;
            // 
            // bTextBox
            // 
            bTextBox.Location = new Point(507, 346);
            bTextBox.Name = "bTextBox";
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
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "0", "1", "2", "3", "4" });
            comboBox1.Location = new Point(33, 75);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 35;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 57);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 36;
            label1.Text = "Select light gun";
            // 
            // hTextBox
            // 
            hTextBox.Location = new Point(507, 422);
            hTextBox.Name = "hTextBox";
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
            eTrackBar.Minimum = -7;
            eTrackBar.Name = "eTrackBar";
            eTrackBar.Size = new Size(367, 45);
            eTrackBar.TabIndex = 55;
            eTrackBar.ValueChanged += ETrackBar_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(937, 117);
            label8.Name = "label8";
            label8.Size = new Size(94, 15);
            label8.TabIndex = 59;
            label8.Text = "Game resolution";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "2560x1440", "1920x1080", "1366x768", "1280x720", "1920x1200", "1680x1050", "1440x900", "1280x800", "1024x768", "800x600", "640x480" });
            comboBox2.Location = new Point(937, 135);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 23);
            comboBox2.TabIndex = 58;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
            comboBox2.KeyDown += ComboBox2_KeyDown;
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
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(683, 18);
            label11.Name = "label11";
            label11.Size = new Size(93, 15);
            label11.TabIndex = 61;
            label11.Text = "Proceesed video";
            // 
            // button11
            // 
            button11.Location = new Point(221, 650);
            button11.Name = "button11";
            button11.Size = new Size(93, 25);
            button11.TabIndex = 68;
            button11.Text = "Save";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button12
            // 
            button12.Location = new Point(1134, 627);
            button12.Name = "button12";
            button12.Size = new Size(93, 25);
            button12.TabIndex = 69;
            button12.Text = "Save";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
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
            up1Button.Location = new Point(979, 384);
            up1Button.Name = "up1Button";
            up1Button.Size = new Size(65, 61);
            up1Button.TabIndex = 72;
            up1Button.Text = "+1";
            up1Button.UseVisualStyleBackColor = true;
            up1Button.Click += up1Button_Click;
            // 
            // left1Button
            // 
            left1Button.Location = new Point(908, 444);
            left1Button.Name = "left1Button";
            left1Button.Size = new Size(65, 61);
            left1Button.TabIndex = 73;
            left1Button.Text = "-1";
            left1Button.UseVisualStyleBackColor = true;
            left1Button.Click += left1Button_Click;
            // 
            // right1Button
            // 
            right1Button.Location = new Point(1050, 444);
            right1Button.Name = "right1Button";
            right1Button.Size = new Size(65, 61);
            right1Button.TabIndex = 74;
            right1Button.Text = "+1";
            right1Button.UseVisualStyleBackColor = true;
            right1Button.Click += right1Button_Click;
            // 
            // down1Button
            // 
            down1Button.Location = new Point(979, 507);
            down1Button.Name = "down1Button";
            down1Button.Size = new Size(65, 61);
            down1Button.TabIndex = 75;
            down1Button.Text = "-1";
            down1Button.UseVisualStyleBackColor = true;
            down1Button.Click += down1Button_Click;
            // 
            // down10Button
            // 
            down10Button.Location = new Point(957, 574);
            down10Button.Name = "down10Button";
            down10Button.Size = new Size(106, 88);
            down10Button.TabIndex = 79;
            down10Button.Text = "-10";
            down10Button.UseVisualStyleBackColor = true;
            down10Button.Click += down10Button_Click;
            // 
            // right10Button
            // 
            right10Button.Location = new Point(1121, 430);
            right10Button.Name = "right10Button";
            right10Button.Size = new Size(106, 88);
            right10Button.TabIndex = 78;
            right10Button.Text = "+10";
            right10Button.UseVisualStyleBackColor = true;
            right10Button.Click += right10Button_Click;
            // 
            // left10Button
            // 
            left10Button.Location = new Point(799, 430);
            left10Button.Name = "left10Button";
            left10Button.Size = new Size(106, 88);
            left10Button.TabIndex = 77;
            left10Button.Text = "-10";
            left10Button.UseVisualStyleBackColor = true;
            left10Button.Click += left10Button_Click;
            // 
            // up10Button
            // 
            up10Button.Location = new Point(957, 290);
            up10Button.Name = "up10Button";
            up10Button.Size = new Size(106, 88);
            up10Button.TabIndex = 76;
            up10Button.Text = "+10";
            up10Button.UseVisualStyleBackColor = true;
            up10Button.Click += up10Button_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDarkDark;
            panel1.Location = new Point(799, 289);
            panel1.Name = "panel1";
            panel1.Size = new Size(428, 380);
            panel1.TabIndex = 80;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1268, 687);
            Controls.Add(down10Button);
            Controls.Add(right10Button);
            Controls.Add(left10Button);
            Controls.Add(up10Button);
            Controls.Add(down1Button);
            Controls.Add(right1Button);
            Controls.Add(left1Button);
            Controls.Add(up1Button);
            Controls.Add(label13);
            Controls.Add(button12);
            Controls.Add(button11);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label8);
            Controls.Add(comboBox2);
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
            Name = "Form1";
            Text = "Phu's Light Gun";
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

        private void ComboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                string value = (sender as ComboBox).Text.ToString();
                var res = value.Split('x');
                xres = int.Parse(res[0]);
                yres = int.Parse(res[1]);

                }
                catch
                {

                }
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = (sender as ComboBox).Text.ToString();
            var res = value.Split('x');
            xres = int.Parse(res[0]);
            yres = int.Parse(res[1]);
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
        private Button button1;
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
        private Label label8;
        private ComboBox comboBox2;
        private Label label10;
        private Label label11;
        private Button button11;
        private Button button12;
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
    }
}
