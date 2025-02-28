using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace LightGun
{
    public class TransparentForm : Form
    {


        private float width = 0;
        private int resX;
        private int resY;
        private bool is43 = false;
        public bool Is43
        {
            get { return is43; }
            set { is43 = value; }
        }

        public TransparentForm(float width,int resX, int resY )
        {
            // Set the form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.BackColor = Color.Lime;
            this.TransparencyKey = Color.Lime;
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            this.ShowInTaskbar = false;
            this.AllowTransparency = true;
            this.width = width;
            this.resX = resX;
            this.resY = resY;
     
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!is43)
            {
                // Draw 16:9 aspect ratio
                float thickness = (float)Math.Ceiling((resX * 1.12) * (width / 100));
                // Draw a rectangle on the form
                using (Pen pen = new Pen(Color.White, thickness))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, resX, resY));
                }
            }
            else
            {
                // Draw 4:3 aspect ratio horizontally
                float thickness = (float)Math.Ceiling((resX * 1.12) * (width / 100));
                int width43 = (int)(resY * 4.0 / 3.0);
                int horizontalPadding = (resX - width43) / 2;

                // Paint the extra space black
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(0, 0, horizontalPadding, resY)); // Left black bar
                    e.Graphics.FillRectangle(brush, new Rectangle(resX - horizontalPadding, 0, horizontalPadding, resY)); // Right black bar
                }

                // Draw a rectangle on the form
                using (Pen pen = new Pen(Color.White, thickness))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(horizontalPadding, 0, width43, resY));
                }
            }
         
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTTRANSPARENT = -1;

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
                return;
            }

            base.WndProc(ref m);
        }
    }
}
