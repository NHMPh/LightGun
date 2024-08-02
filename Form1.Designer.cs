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
            label1 = new Label();
            trackBar1 = new TrackBar();
            trackBar2 = new TrackBar();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            trackBar3 = new TrackBar();
            textBox3 = new TextBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
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
            button1.Location = new Point(569, 369);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(754, 356);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(781, 393);
            trackBar1.Maximum = 300;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(366, 45);
            trackBar1.TabIndex = 3;
            trackBar1.ValueChanged += TrackBar1_ValueChanged;
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(35, 384);
            trackBar2.Maximum = 300;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(366, 45);
            trackBar2.TabIndex = 4;
            trackBar2.ValueChanged += TrackBar2_ValueChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(139, 338);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 5;
            // 
            // textBox2
            // //
            textBox2.Location = new Point(928, 353);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 6;
            // 
            // trackBar3
            // 
            trackBar3.Location = new Point(413, 442);
            trackBar3.Maximum = 300;
            trackBar3.Name = "trackBar3";
            trackBar3.Size = new Size(366, 45);
            trackBar3.TabIndex = 7;
            trackBar3.ValueChanged += TrackBar3_ValueChanged;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(553, 415);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 8;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(721, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(651, 315);
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1437, 499);
            Controls.Add(pictureBox2);
            Controls.Add(textBox3);
            Controls.Add(trackBar3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(trackBar2);
            Controls.Add(trackBar1);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }





        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private Label label1;
        private TrackBar trackBar1;
        private TrackBar trackBar2;
        private TextBox textBox1;
        private TextBox textBox2;
        private TrackBar trackBar3;
        private TextBox textBox3;
        private PictureBox pictureBox2;
    }
}
