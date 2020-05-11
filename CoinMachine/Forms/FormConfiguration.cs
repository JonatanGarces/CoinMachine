using Forms;
using Library;
using Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace Forms
{
    public partial class Inicio : Form
    {
        private ConfigManager configmanager = new ConfigManager();
        private SerialObserver so = new SerialObserver();

        public Inicio()
        {
            InitializeComponent();
            txtCoinMinute.Text = configmanager.ReadSetting("CoinMinute");
            txtNotificationMinute.Text = configmanager.ReadSetting("NotificationMinute");
            txtNotificationMessage.Text = configmanager.ReadSetting("NotificationMessage");
            txtBackgroundImage.Text = configmanager.ReadSetting("BackgroundImage");
            txtBackgroundMessage.Text = configmanager.ReadSetting("BackgroundMessage");
            txtBackgroundColor.Text = configmanager.ReadSetting("BackgroundColor");

            txtBackgroundMessageColor.Text = configmanager.ReadSetting("BackgroundMessageColor");

            txtNotificationTitle.Text = configmanager.ReadSetting("NotificationTitle");

            if (File.Exists(configmanager.ReadSetting("BackgroundImage"))) { btnBackgroundImage.Text = "Cambiar"; } else { btnBackgroundImage.Text = "Seleccionar"; }

            //List<Device> devices = new List<Device>();
            // devices.Add(new Device(configmanager.ReadSetting("SlotName"), configmanager.ReadSetting("SlotPort")));

            try
            {
                picBackgroundColor.BackColor = Color.FromArgb(int.Parse(configmanager.ReadSetting("BackgroundColor")));
                picBackgroundMessageColor.BackColor = Color.FromArgb(int.Parse(configmanager.ReadSetting("BackgroundMessageColor")));
            }
            catch (ArgumentNullException) { }
            catch (ArgumentOutOfRangeException) { }
            catch (ArgumentException) { }
            catch (FormatException) { }
            catch (FileNotFoundException) { }
            try
            {
                picBackgroundImage.Image = Image.FromFile(configmanager.ReadSetting("BackgroundImage"));
            }
            catch (ArgumentNullException) { }
            catch (ArgumentOutOfRangeException) { }
            catch (ArgumentException) { }
            catch (FileNotFoundException) { }

            cbxSlotPort.DataSource = so.GetSerials();
            cbxSlotPort.DisplayMember = "Name";
            cbxSlotPort.ValueMember = "Port";
            cbxSlotPort.DropDownStyle = ComboBoxStyle.DropDownList;

            so.Changed += () =>
            {
                // after we've done all the processing,
                this.Invoke(new MethodInvoker(delegate
                {
                    // load the control with the appropriate data
                    cbxSlotPort.DataSource = so.GetSerials();
                    cbxSlotPort.DisplayMember = "Name";
                    cbxSlotPort.ValueMember = "Port";
                    cbxSlotPort.DropDownStyle = ComboBoxStyle.DropDownList;
                }));

                //  cbxSlotPort.Refresh();
            };

            // TCPObserver tcp = new TCPObserver();
            // tcp.TcpOverride += () =>
            // {
            //     Console.WriteLine("lo lograste puta ");
            //  };
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            SerialPort connected = so.Connect(((Device)cbxSlotPort.SelectedItem).Port);
            if (connected.IsOpen)
            {
                this.picSlotOk.Image = global::CoinMachine.Properties.Resources.check;
            }
            else
            {
                this.picSlotOk.Image = global::CoinMachine.Properties.Resources.check;
            }
        }

        private void btnDetectar_Click(object sender, EventArgs e)
        {
            cbxSlotPort.DataSource = so.GetSerials();
            cbxSlotPort.DisplayMember = "Name";
            cbxSlotPort.ValueMember = "Port";
            cbxSlotPort.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picBackgroundImage.Image = new Bitmap(openFileDialog1.FileName);
                txtBackgroundImage.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            picBackgroundColor.BackColor = colorDialog1.Color;
            int code = colorDialog1.Color.ToArgb();
            configmanager.AddUpdateAppSettings("BackgroundColor", code.ToString());
            txtBackgroundColor.Text = code.ToString();
        }

        private void btnBackgroundMessageColor_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            picBackgroundMessageColor.BackColor = colorDialog2.Color;
            int code = colorDialog2.Color.ToArgb();
            configmanager.AddUpdateAppSettings("BackgroundMessageColor", code.ToString());
            txtBackgroundMessageColor.Text = code.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtCoinMinute.Text.Trim().Equals("")) { MessageBox.Show("Falta de llenar la primera seccion"); return; }
            if (txtNotificationMinute.Text.Trim().Equals("")) { MessageBox.Show("Falta de llenar la segunda seccion"); return; }
            if (txtNotificationMessage.Text.Trim().Equals("")) { MessageBox.Show("Falta de llenar la segunda seccion"); return; }
            if (txtNotificationTitle.Text.Trim().Equals("")) { MessageBox.Show("Falta de llenar la segunda seccion"); return; }

            if (txtBackgroundImage.Text.Trim().Equals("")) { MessageBox.Show("Falta de llenar la tercera seccion"); return; }
            if (txtBackgroundMessage.Text.Trim().Equals("")) { MessageBox.Show("Falta de llenar la tercera seccion"); return; }
            if (txtBackgroundColor.Text.Trim().Equals("")) { MessageBox.Show("Falta de llenar la tercera seccion"); return; }
            if (txtBackgroundMessageColor.Text.Trim().Equals("")) { MessageBox.Show("Falta de llenar la tercera seccion"); return; }
            if (((Device)cbxSlotPort.SelectedItem).Port.Equals("")) { MessageBox.Show("Falta de llenar la cuarta seccion"); return; }

            if (!so.serialport.IsOpen) { MessageBox.Show("No ha conectado el dispositivo"); return; }

            configmanager.AddUpdateAppSettings("CoinMinute", txtCoinMinute.Text);
            configmanager.AddUpdateAppSettings("NotificationMinute", txtNotificationMinute.Text);
            configmanager.AddUpdateAppSettings("NotificationMessage", txtNotificationMessage.Text);
            configmanager.AddUpdateAppSettings("NotificationTitle", txtNotificationTitle.Text);

            configmanager.AddUpdateAppSettings("BackgroundImage", txtBackgroundImage.Text);
            configmanager.AddUpdateAppSettings("BackgroundMessage", txtBackgroundMessage.Text);
            configmanager.AddUpdateAppSettings("BackgroundColor", txtBackgroundColor.Text);

            configmanager.AddUpdateAppSettings("BackgroundMessageColor", txtBackgroundMessageColor.Text);

            configmanager.AddUpdateAppSettings("SlotPort", ((Device)cbxSlotPort.SelectedItem).Port);
            configmanager.AddUpdateAppSettings("SlotPortName", ((Device)cbxSlotPort.SelectedItem).Name);

            FormTimer f1 = new FormTimer(so);
            f1.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}