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
            eTextBox = new TextBox();
            eLabel = new Label();
            eTrackBar = new TrackBar();
            gTextBox = new TextBox();
            gLabel = new Label();
            gTrackBar = new TrackBar();
            bTextBox = new TextBox();
            bLabel = new Label();
            bTrackBar = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)eTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bTrackBar).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(651, 315);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(651, 333);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(721, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(651, 315);
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // tTrackBar
            // 
            tTrackBar.Location = new Point(99, 353);
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
            tLable.Location = new Point(26, 353);
            tLable.Name = "tLable";
            tLable.Size = new Size(67, 15);
            tLable.TabIndex = 11;
            tLable.Text = "Threadhold";
            // 
            // tTextBox
            // 
            tTextBox.Location = new Point(472, 353);
            tTextBox.Name = "tTextBox";
            tTextBox.Size = new Size(100, 23);
            tTextBox.TabIndex = 14;
            // 
            // eTextBox
            // 
            eTextBox.Location = new Point(472, 404);
            eTextBox.Name = "eTextBox";
            eTextBox.Size = new Size(100, 23);
            eTextBox.TabIndex = 17;
            // 
            // eLabel
            // 
            eLabel.AutoSize = true;
            eLabel.Location = new Point(26, 404);
            eLabel.Name = "eLabel";
            eLabel.Size = new Size(55, 15);
            eLabel.TabIndex = 16;
            eLabel.Text = "Exposure";
            // 
            // eTrackBar
            // 
            eTrackBar.Location = new Point(99, 404);
            eTrackBar.Maximum = 10000;
            eTrackBar.Name = "eTrackBar";
            eTrackBar.Size = new Size(367, 45);
            eTrackBar.TabIndex = 15;
            eTrackBar.ValueChanged += ETrackBar_ValueChanged;
            // 
            // gTextBox
            // 
            gTextBox.Location = new Point(472, 455);
            gTextBox.Name = "gTextBox";
            gTextBox.Size = new Size(100, 23);
            gTextBox.TabIndex = 20;
            // 
            // gLabel
            // 
            gLabel.AutoSize = true;
            gLabel.Location = new Point(26, 455);
            gLabel.Name = "gLabel";
            gLabel.Size = new Size(31, 15);
            gLabel.TabIndex = 19;
            gLabel.Text = "Gain";
            // 
            // gTrackBar
            // 
            gTrackBar.Location = new Point(99, 455);
            gTrackBar.Maximum = 255;
            gTrackBar.Name = "gTrackBar";
            gTrackBar.Size = new Size(367, 45);
            gTrackBar.TabIndex = 18;
            gTrackBar.ValueChanged += GTrackBar_ValueChanged;
            // 
            // bTextBox
            // 
            bTextBox.Location = new Point(472, 499);
            bTextBox.Name = "bTextBox";
            bTextBox.Size = new Size(100, 23);
            bTextBox.TabIndex = 23;
            // 
            // bLabel
            // 
            bLabel.AutoSize = true;
            bLabel.Location = new Point(26, 499);
            bLabel.Name = "bLabel";
            bLabel.Size = new Size(62, 15);
            bLabel.TabIndex = 22;
            bLabel.Text = "Brightness";
            // 
            // bTrackBar
            // 
            bTrackBar.Location = new Point(99, 499);
            bTrackBar.Maximum = 255;
            bTrackBar.Name = "bTrackBar";
            bTrackBar.Size = new Size(367, 45);
            bTrackBar.TabIndex = 21;
            bTrackBar.ValueChanged += BTrackBar_ValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1437, 556);
            Controls.Add(bTextBox);
            Controls.Add(bLabel);
            Controls.Add(bTrackBar);
            Controls.Add(gTextBox);
            Controls.Add(gLabel);
            Controls.Add(gTrackBar);
            Controls.Add(eTextBox);
            Controls.Add(eLabel);
            Controls.Add(eTrackBar);
            Controls.Add(tTextBox);
            Controls.Add(tLable);
            Controls.Add(tTrackBar);
            Controls.Add(pictureBox2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)tTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)eTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)gTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)bTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void BTrackBar_ValueChanged(object sender, EventArgs e)
        {
           brightness = (sender as TrackBar).Value;
           bTextBox.Text =brightness.ToString();
            capture.Set(CapProp.Brightness, brightness);
        }

        private void GTrackBar_ValueChanged(object sender, EventArgs e)
        {
            gain = (sender as TrackBar).Value;
            gTextBox.Text = gain.ToString();
            capture.Set(CapProp.Gain, gain);
        }

        private void ETrackBar_ValueChanged(object sender, EventArgs e)
        {
            exposure = (sender as TrackBar).Value;
            eTextBox.Text = exposure.ToString();
            capture.Set(CapProp.Exposure, exposure);
        }

        private void TTrackBar_ValueChanged(object sender, EventArgs e)
        {
            threadhold = (sender as TrackBar).Value;
            tTextBox.Text = threadhold.ToString();
        }





        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private PictureBox pictureBox2;
        private TrackBar tTrackBar;
        private Label tLable;
        private TextBox tTextBox;
        private TextBox eTextBox;
        private Label eLabel;
        private TrackBar eTrackBar;
        private TextBox gTextBox;
        private Label gLabel;
        private TrackBar gTrackBar;
        private TextBox bTextBox;
        private Label bLabel;
        private TrackBar bTrackBar;
    }
}
