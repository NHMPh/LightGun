
using ArduinoUploader.Hardware;
using LightGun.LightGunCompoment;
using LightGun.UIControl;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Management;
using static LightGun.UIControl.ButtonAssignmentTab;
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
            master = new Master(this);

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
            comBoxArP1.SelectedIndexChanged += AssignAllButtonP1;

            comBoxArP2.SelectedIndexChanged += master.mainTab.ComBoxArP2;
            comBoxArP2.SelectedIndexChanged += AssignAllButtonP2;

            btnRefreshCamP1.Click += BtnRefresh;
            btnRefreshCamP2.Click += BtnRefresh;

            btnRefreshArP1.Click += BtnRefresh;
            btnRefreshArP2.Click += BtnRefresh;

            btnUpFirP1.Click += BtnUpFirP1_Click;
            btnUpFirP2.Click += BtnUpFirP2_Click;

            BtnRefresh(null, null);

            btnStartP1.Click += StartStopP1;
            btnStartP2.Click += StartStopP2;
            btnSave.Click += master.SaveSetting;
            btnSaveCali.Click += master.SaveSetting;
            btnSaveBuAssign.Click += master.SaveSetting;

            tTrackBarP1.ValueChanged += TTrackBarP1_ValueChanged;
            bTrackBarP1.ValueChanged += BTrackBarP1_ValueChanged;
            cTrackBarP1.ValueChanged += CTrackBarP1_ValueChanged;
            gTrackBarP1.ValueChanged += GTrackBarP1_ValueChanged;
            eTrackBarP1.ValueChanged += ETrackBarP1_ValueChanged;
            tTrackBarP2.ValueChanged += TTrackBarP2_ValueChanged;
            bTrackBarP2.ValueChanged += BTrackBarP2_ValueChanged;
            cTrackBarP2.ValueChanged += CTrackBarP2_ValueChanged;
            gTrackBarP2.ValueChanged += GTrackBarP2_ValueChanged;
            eTrackBarP2.ValueChanged += ETrackBarP2_ValueChanged;
            LoadSetting();
            tTrackBarP1.ValueChanged += master.mainTab.tTrackBarP1;
            bTrackBarP1.ValueChanged += master.mainTab.bTrackBarP1;
            cTrackBarP1.ValueChanged += master.mainTab.cTrackBarP1;
            gTrackBarP1.ValueChanged += master.mainTab.gTrackBarP1;
            eTrackBarP1.ValueChanged += master.mainTab.eTrackBarP1;
            tTrackBarP2.ValueChanged += master.mainTab.tTrackBarP2;
            bTrackBarP2.ValueChanged += master.mainTab.bTrackBarP2;
            cTrackBarP2.ValueChanged += master.mainTab.cTrackBarP2;
            gTrackBarP2.ValueChanged += master.mainTab.gTrackBarP2;
            eTrackBarP2.ValueChanged += master.mainTab.eTrackBarP2;


            rawCheckBox.CheckedChanged += RawCheckBox_CheckedChanged;
            processCheckBox.CheckedChanged += ProcessCheckBox_CheckedChanged;
            _43CheckBox.CheckedChanged += _43CheckBox_CheckedChanged;
            //Calibration Tab UI
            up10ButtonP1.Click += master.calibrationTab.up10ButtonP1;
            down10ButtonP1.Click += master.calibrationTab.down10ButtonP1;
            left10ButtonP1.Click += master.calibrationTab.left10ButtonP1;
            right10ButtonP1.Click += master.calibrationTab.right10ButtonP1;

            up1ButtonP1.Click += master.calibrationTab.up1ButtonP1;
            down1ButtonP1.Click += master.calibrationTab.down1ButtonP1;
            left1ButtonP1.Click += master.calibrationTab.left1ButtonP1;
            right1ButtonP1.Click += master.calibrationTab.right1ButtonP1;

            up10ButtonP2.Click += master.calibrationTab.up10ButtonP2;
            down10ButtonP2.Click += master.calibrationTab.down10ButtonP2;
            left10ButtonP2.Click += master.calibrationTab.left10ButtonP2;
            right10ButtonP2.Click += master.calibrationTab.right10ButtonP2;

            up1ButtonP2.Click += master.calibrationTab.up1ButtonP2;
            down1ButtonP2.Click += master.calibrationTab.down1ButtonP2;
            left1ButtonP2.Click += master.calibrationTab.left1ButtonP2;
            right1ButtonP2.Click += master.calibrationTab.right1ButtonP2;
            //GuideButton
            guideBorder.Click += master.guideMessageBoxMessage.BorderMessage;
            guideButtonAssign.Click += master.guideMessageBoxMessage.ButtonAssignMessage;
            guideCalibration.Click += master.guideMessageBoxMessage.CalibrationMessage;
            guideCam.Click += master.guideMessageBoxMessage.CamMessage;
            guideFirmware.Click += master.guideMessageBoxMessage.FirmwareMessage;
            guideSelect.Click += master.guideMessageBoxMessage.SelectMessage;
            guideOverlay.Click += master.guideMessageBoxMessage.OverlayTabMessage1;
            guideOverlay2.Click += master.guideMessageBoxMessage.OverlayTabMessage2;
            guideSelect2.Click += master.guideMessageBoxMessage.SelectMessage;




            //Button AssignmentTab UI

            for (int i = 0; i < 88; i++)
            {

                if (this.Controls.Find($"comboBox{i + 1}", true)[0] is ComboBox comboBox)
                {
                    if (i >= 0 && i <= 21)
                    {
                        comboBox.SelectedIndex = master.Settings.Players[0].NormalButton[i].SelectedIndex;
                        comboBox.Tag = master.Settings.Players[0].NormalButton[i].SelectedIndex;
                    }

                    else if (i >= 22 && i <= 43)
                    {
                        comboBox.SelectedIndex = master.Settings.Players[0].OffscreenButton[i - 22].SelectedIndex;
                        comboBox.Tag = master.Settings.Players[0].OffscreenButton[i - 22].SelectedIndex;
                    }

                    else if (i >= 44 && i <= 65)
                    {
                        comboBox.SelectedIndex = master.Settings.Players[1].NormalButton[i - 44].SelectedIndex;
                        comboBox.Tag = master.Settings.Players[1].NormalButton[i - 44].SelectedIndex;
                    }

                    else if (i >= 66 && i <= 87)
                    {
                        comboBox.SelectedIndex = master.Settings.Players[1].OffscreenButton[i - 66].SelectedIndex;
                        comboBox.Tag = master.Settings.Players[1].OffscreenButton[i - 66].SelectedIndex;
                    }


                    comboBox.SelectedIndexChanged += master.buttonAssignmentTab.ComboBoxChangeButton;
                    comboBox.SelectedIndexChanged += Unforcus;

                }
            }

            comBoxArSelP2.SelectedIndex = 0;
            comBoxArSelP1.SelectedIndex = 0;
            comBoxArSelP1.Enabled = false;
            comBoxArSelP2.Enabled = false;

        }

        private void _43CheckBox_CheckedChanged(object? sender, EventArgs e)
        {
           master.Settings.Is43BorderCheck = _43CheckBox.Checked;
        }

        private void ProcessCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            master.Settings.IsProcessCheck = processCheckBox.Checked;
        }

        private void RawCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            master.Settings.IsRawCheck = rawCheckBox.Checked;
        }

        private void BtnUpFirP2_Click(object? sender, EventArgs e)
        {
            if(comBoxArSelP2.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a model");
                return;
            }
            master.firmwareUploadTab.UploadFirmwareP2((ArduinoModel)Enum.Parse(typeof(ArduinoModel), comBoxArSelP2.SelectedItem.ToString()));
            BtnRefresh(null, null);
        }

        private void BtnUpFirP1_Click(object? sender, EventArgs e)
        {
            if (comBoxArSelP1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a model");
                return;
            }
            master.firmwareUploadTab.UploadFirmwareP1((ArduinoModel)Enum.Parse(typeof(ArduinoModel),comBoxArSelP1.SelectedItem.ToString()));
            BtnRefresh(null, null);
        }

        private void Unforcus(object? sender, EventArgs e)
        {
            label114.Focus();
        }

        public void AssignAllButtonP1(object? sender, EventArgs e)
        {

            if (comBoxArP1.SelectedIndex == -1) return;
            if (master.overlayPanel.CheckArduinoConnectP1())
            {
                checkBoxArP1.Checked = true;
                if (checkBoxCamP1.Checked)
                {
                    btnStartP1.BackColor = Color.Green;
                    btnStartP1.Text = "Start";
                }
            }
            else
            {
                checkBoxArP1.Checked = false;
                btnStartP1.BackColor = Color.DarkGray;
                btnStartP1.Text = "Not ready";
                comBoxArP1.SelectedIndex = -1;
                return;
            }

            for (int i = 0; i < 43; i++)
            {

                if (this.Controls.Find($"comboBox{i + 1}", true)[0] is ComboBox comboBox)
                {
                    if (comboBox.SelectedIndex == 0) continue;

                    if (i >= 0 && i <= 21)
                    {
                        master.buttonAssignmentTab.SetButton(comboBox.SelectedItem.ToString(), 0, 0, i);
                    }
                    else if (i >= 22 && i <= 43)
                    {
                        master.buttonAssignmentTab.SetButton(comboBox.SelectedItem.ToString(), 0, 1, i - 22);
                    }

                }
            }
        }
        public void AssignAllButtonP2(object? sender, EventArgs e)
        {
            if (comBoxArP2.SelectedIndex == -1) return;
            if (master.overlayPanel.CheckArduinoConnectP2())
            {
                checkBoxArP2.Checked = true;
                if (checkBoxCamP2.Checked)
                {
                    btnStartP2.BackColor = Color.Green;
                    btnStartP2.Text = "Start";
                }
            }
            else
            {
                checkBoxArP2.Checked = false;
                btnStartP2.BackColor = Color.DarkGray;
                btnStartP2.Text = "Not ready";
                comBoxArP2.SelectedIndex = -1;
                return;
            }

            for (int i = 44; i < 88; i++)
            {

                if (this.Controls.Find($"comboBox{i + 1}", true)[0] is ComboBox comboBox)
                {
                    if (comboBox.SelectedIndex == 0) continue;


                    if (i >= 44 && i <= 65)
                    {
                        master.buttonAssignmentTab.SetButton(comboBox.SelectedItem.ToString(), 1, 0, i - 44);

                    }
                    else if (i >= 66 && i <= 87)
                    {
                        master.buttonAssignmentTab.SetButton(comboBox.SelectedItem.ToString(), 1, 1, i - 66);
                    }
                }
            }
        }
        private void LoadSetting()
        {
            tTrackBarP1.Value = master.Settings.Players[0].Threshold;
            bTrackBarP1.Value = master.Settings.Players[0].Brightness;
            cTrackBarP1.Value = master.Settings.Players[0].Contrast;
            gTrackBarP1.Value = master.Settings.Players[0].Gamma;
            eTrackBarP1.Value = master.Settings.Players[0].Exposure;

            tTrackBarP2.Value = master.Settings.Players[1].Threshold;
            bTrackBarP2.Value = master.Settings.Players[1].Brightness;
            cTrackBarP2.Value = master.Settings.Players[1].Contrast;
            gTrackBarP2.Value = master.Settings.Players[1].Gamma;
            eTrackBarP2.Value = master.Settings.Players[1].Exposure;

            _43CheckBox.Checked = master.Settings.Is43BorderCheck;
            rawCheckBox.Checked = master.Settings.IsRawCheck;
            processCheckBox.Checked = master.Settings.IsProcessCheck;

            borderTextBox.Text = master.Settings.Border.ToString();
        }


        private void BtnRefresh(object? sender, EventArgs e)
        {

            string[] ports = SerialPort.GetPortNames();
            var webcams = master.mainTab.GetSortedWebcamsByLastArrivalDate();

            // Check if any video devices are found
            if (webcams.Count == 0)
            {
                MessageBox.Show("No webcams found.");
                return;
            }
            comBoxArP1.Items.Clear();
            comBoxArP2.Items.Clear();
            comBoxCamP1.Items.Clear();
            comBoxCamP2.Items.Clear();

            comBoxCamP1.Sorted = false;
            comBoxCamP2.Sorted = false;
            comBoxArP1.Sorted = false;
            comBoxArP2.Sorted = false;

            // Add each video device to the ComboBox
            foreach (var device in webcams)
            {
                comBoxCamP1.Items.Add(device);
                comBoxCamP2.Items.Add(device);

            }

            foreach (var port in ports)
            {
                foreach (var item in comBoxArP1.Items)
                {
                    if (item.ToString() == port)
                    {
                        comBoxArP1.Items.Remove(port);
                    }
                }
                foreach (var item in comBoxArP2.Items)
                {
                    if (item.ToString() == port)
                    {
                        comBoxArP2.Items.Remove(port);
                    }
                }
                comBoxArP1.Items.Add(port);
                comBoxArP2.Items.Add(port);

            }




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
            if (master.overlayPanel.CheckCameraConnectP2())
            {
                checkBoxCamP2.Checked = true;
                if (checkBoxArP2.Checked)
                {
                    btnStartP2.BackColor = Color.Green;
                    btnStartP2.Text = "Start";
                }
            }


            Task.Run(async () => await FetchVideoP2());

        }

        private void ComBoxCamP1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (master.overlayPanel.CheckCameraConnectP1())
            {
                checkBoxCamP1.Checked = true;
                if (checkBoxArP1.Checked)
                {
                    btnStartP1.BackColor = Color.Green;
                    btnStartP1.Text = "Start";
                }
            }


            Task.Run(async () => await FetchVideoP1());

        }
        private async Task FetchVideoP1()
        {
            while (true)
            {
                //Display video

                if (rawCheckBox.Checked)
                {
                    var image = master.mainTab.picBoxRawP1();
                    picBoxRawP1.Invoke((Action)(() => picBoxRawP1.Image = image));
                }
                if (processCheckBox.Checked)
                {
                    var image = master.mainTab.picBoxProP1();
                    picBoxProP1.Invoke((Action)(() => picBoxProP1.Image = image));
                }


                await Task.Delay(16);
            }
        }

        private async Task FetchVideoP2()
        {
            while (true)
            {

                //Display video
                if (rawCheckBox.Checked)
                {
                    var image = master.mainTab.picBoxRawP2();
                    picBoxRawP2.Invoke((Action)(() => picBoxRawP2.Image = image));
                }
                if (processCheckBox.Checked)
                {
                    var image = master.mainTab.picBoxProP2();
                    picBoxProP2.Invoke((Action)(() => picBoxProP2.Image = image));
                }


                await Task.Delay(16);
            }
        }

        private TransparentForm border;
        private int currentState = 0; // 0: off, 1: 16:9, 2: 4:3

        public void OpenBorder()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            if (border != null && border.Visible)
            {
                border.Dispose();
                border = null;
            }
            if (_43CheckBox.Checked)
            {
                currentState = (currentState + 1) % 3;

            }
            else
            {
                if(currentState == 2)
                {
                    currentState = -1;
                }
                currentState = (currentState + 1) % 2;
            }

            if (currentState == 1 )
            {
                border = new TransparentForm(int.Parse(borderTextBox.Text), screenWidth, screenHeight);
                border.Show();
            }
            else if (currentState == 2)
            {
                border = new TransparentForm(int.Parse(borderTextBox.Text), screenWidth, screenHeight);
                border.Is43 = true;
                border.Show();
            }
        }
        public void StartStopP1(object? sender, EventArgs e)
        {
            if (!checkBoxCamP1.Checked && !checkBoxArP1.Checked)
            {
                MessageBox.Show("Camera is not found\nArduino is not found", "Can not start player 1", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!checkBoxCamP1.Checked)
            {
                MessageBox.Show("Camera is not found.", "Can not start player 1", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!checkBoxArP1.Checked)
            {
                MessageBox.Show("Arduino is not found", "Can not start player 1", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            master.overlayPanel.StartP1(this.btnStartP1, null);
        }

        public void StartStopP2(object? sender, EventArgs e)
        {
            if (!checkBoxCamP2.Checked && !checkBoxArP2.Checked)
            {
                MessageBox.Show("Camera is not found\nArduino is not found", "Can not start player 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!checkBoxCamP2.Checked)
            {
                MessageBox.Show("Camera is not found.", "Can not start player 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!checkBoxArP2.Checked)
            {
                MessageBox.Show("Arduino is not found.", "Can not start player 2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            master.overlayPanel.StartP2(this.btnStartP2, null);
        }


        private void borderTextBox_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                master.Settings.Border = int.Parse(borderTextBox.Text);
            }

        }
        public void DisconnectCameraP1(object? sender, EventArgs e)
        {
        
            checkBoxCamP1.Checked = false;
            btnStartP1.BackColor = Color.DarkGray;
            btnStartP1.Text = "Not ready";
            comBoxCamP1.SelectedIndex = -1;

        }
        public void DisconnectArduinoP1(object? sender, EventArgs e)
        {
            checkBoxArP1.Checked = false;
            btnStartP1.BackColor = Color.DarkGray;
            btnStartP1.Text = "Not ready";
            comBoxArP1.SelectedIndex = -1;

        }
        public void DisconnectCameraP2(object? sender, EventArgs e)
        {
            checkBoxCamP2.Checked = false;
            btnStartP2.BackColor = Color.DarkGray;
            btnStartP2.Text = "Not ready";
            comBoxCamP2.SelectedIndex = -1;

        }
        public void DisconnectArduinoP2(object? sender, EventArgs e)
        {
            checkBoxArP2.Checked = false;
            btnStartP2.BackColor = Color.DarkGray;
            btnStartP2.Text = "Not ready";
            comBoxArP2.SelectedIndex = -1;
        }


    }
}
