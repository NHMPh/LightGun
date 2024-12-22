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


        float width = 39;
        int resX;
        int resY;
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
            float thickness = (float)Math.Ceiling((resX*1.12) * (width / 100)) ;
            // Draw a rectangle on the form
            using (Pen pen = new Pen(Color.White, thickness))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, resX, resY));
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
