using AForge;
using AForge.Video.DirectShow;
using LightGun.LightGunCompoment;
using LightGun.UIControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace LightGun
{
    public partial class MainWindow : Form
    {

        private Master master;

        public MainWindow()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            KeyPreview = true;
            master = new Master();

            picBoxRawP1.SizeMode = PictureBoxSizeMode.StretchImage;
            picBoxRawP2.SizeMode = PictureBoxSizeMode.StretchImage;
            picBoxProP1.SizeMode = PictureBoxSizeMode.StretchImage;
            picBoxProP2.SizeMode = PictureBoxSizeMode.StretchImage;

            //Main Tab UI
            comBoxCamP1.SelectedIndexChanged += master.mainTab.ComBoxCamP1;
            comBoxCamP1.SelectedIndexChanged += ComBoxCamP1_SelectedIndexChanged;

            comBoxCamP2.SelectedIndexChanged += master.mainTab.ComBoxCamP2;
            comBoxCamP2.SelectedIndexChanged += ComBoxCamP2_SelectedIndexChanged;

            comBoxArP1.SelectedIndexChanged += master.mainTab.ComBoxArP1;
            comBoxArP2.SelectedIndexChanged += master.mainTab.ComBoxArP2;

            btnRefreshCamP1.Click += BtnRefresh;
            btnRefreshCamP2.Click += BtnRefresh;

            btnRefreshArP1.Click += BtnRefresh;
            btnRefreshArP2.Click += BtnRefresh;

            btnStart.Click += master.mainTab.Start;
            btnSave.Click += master.mainTab.SaveSetting;

            // Camera Slider
            tTrackBarP1.ValueChanged += master.mainTab.tTrackBarP1;
            tTrackBarP1.ValueChanged += TTrackBarP1_ValueChanged;

            bTrackBarP1.ValueChanged += master.mainTab.bTrackBarP1;
            bTrackBarP1.ValueChanged += BTrackBarP1_ValueChanged;

            cTrackBarP1.ValueChanged += master.mainTab.cTrackBarP1;
            cTrackBarP1.ValueChanged += CTrackBarP1_ValueChanged;

            gTrackBarP1.ValueChanged += master.mainTab.gTrackBarP1;
            gTrackBarP1.ValueChanged += GTrackBarP1_ValueChanged;

            eTrackBarP1.ValueChanged += master.mainTab.eTrackBarP1;
            eTrackBarP1.ValueChanged += ETrackBarP1_ValueChanged;

            tTrackBarP2.ValueChanged += master.mainTab.tTrackBarP2;
            tTrackBarP2.ValueChanged += TTrackBarP2_ValueChanged;

            bTrackBarP2.ValueChanged += master.mainTab.bTrackBarP2;
            bTrackBarP2.ValueChanged += BTrackBarP2_ValueChanged;

            cTrackBarP2.ValueChanged += master.mainTab.cTrackBarP2;
            cTrackBarP2.ValueChanged += CTrackBarP2_ValueChanged;

            gTrackBarP2.ValueChanged += master.mainTab.gTrackBarP2;
            gTrackBarP2.ValueChanged += GTrackBarP2_ValueChanged;

            eTrackBarP2.ValueChanged += master.mainTab.eTrackBarP2;
            eTrackBarP2.ValueChanged += ETrackBarP2_ValueChanged;
        }

       

        private void BtnRefresh(object? sender, EventArgs e)
        {

            comBoxCamP1.Items.Clear(); 
            comBoxCamP2.Items.Clear();
            comBoxArP1.Items.Clear();
            comBoxArP2.Items.Clear();
            // Create a collection to hold the video devices
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            string[] ports = SerialPort.GetPortNames();

            // Check if any video devices are found
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No webcams found.");
                return;
            }
            comBoxCamP1.Sorted = false;
            comBoxCamP2.Sorted = false;
            comBoxArP1.Sorted = false;
            comBoxArP2.Sorted=false;
            // Add each video device to the ComboBox
            foreach (FilterInfo device in videoDevices)
            {
                comBoxCamP1.Items.Add(device.Name);
                comBoxCamP2.Items.Add(device.Name);
            }
            comBoxCamP1.Sorted = false;
            comBoxCamP2.Sorted = false;

            foreach(var port in ports)
            {
                comBoxArP1.Items.Add((string)port);
                comBoxArP2.Items.Add((string)port);
            }
            comBoxArP1.Sorted = false;
            comBoxArP2.Sorted = false;
        }

        private void BTrackBarP1_ValueChanged(object? sender, EventArgs e)
        {
            bTextBoxP1.Text = bTrackBarP1.Value.ToString();
        }

        private void TTrackBarP1_ValueChanged(object? sender, EventArgs e)
        {
            tTextBoxP1.Text = tTrackBarP1.Value.ToString();
        }

        private void CTrackBarP1_ValueChanged(object? sender, EventArgs e)
        {
            cTextBoxP1.Text = cTrackBarP1.Value.ToString();
        }

        private void GTrackBarP1_ValueChanged(object? sender, EventArgs e)
        {
            gTextBoxP1.Text = gTrackBarP1.Value.ToString();
        }

        private void ETrackBarP1_ValueChanged(object? sender, EventArgs e)
        {
            eTextBoxP1.Text = eTrackBarP1.Value.ToString();
        }

        private void BTrackBarP2_ValueChanged(object? sender, EventArgs e)
        {
            bTextBoxP2.Text = bTrackBarP2.Value.ToString();
        }

        private void TTrackBarP2_ValueChanged(object? sender, EventArgs e)
        {
            tTextBoxP2.Text = tTrackBarP2.Value.ToString();
        }

        private void CTrackBarP2_ValueChanged(object? sender, EventArgs e)
        {
            cTextBoxP2.Text = cTrackBarP2.Value.ToString();
        }

        private void GTrackBarP2_ValueChanged(object? sender, EventArgs e)
        {
            gTextBoxP2.Text = gTrackBarP2.Value.ToString();
        }

        private void ETrackBarP2_ValueChanged(object? sender, EventArgs e)
        {
            eTextBoxP2.Text = eTrackBarP2.Value.ToString();
        }

        private void ComBoxCamP2_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Task.Run(async () => await FetchVideoP2());
        }

        private void ComBoxCamP1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Task.Run(async () => await FetchVideoP1());
        }
        private async Task FetchVideoP1()
        {
            await Task.Delay(5000);
            while (true)
            {
                //Display video
                if (rawCheckBox.Checked)
                    master.mainTab.picBoxRawP1(picBoxRawP1);
                if (processCheckBox.Checked)
                    master.mainTab.picBoxProP1(picBoxProP1);
                await Task.Delay(16);

            }
        }
        private async Task FetchVideoP2()
        {
            while (true)
            {
                //Display video
                if(rawCheckBox.Checked)
                master.mainTab.picBoxRawP2(picBoxRawP2);
                if(processCheckBox.Checked)
                master.mainTab.picBoxProP2(picBoxProP2);
                await Task.Delay(16);
            }
        }
    }
}
