using slotmachine.Librerias;
using slotmachine.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SystemTray;

namespace slotmachine.Forms
{
    public partial class Inicio : Form
    {
        ConfigManager configmanager = new ConfigManager();
        SerialObserver so = new SerialObserver();
        public Inicio()
        {

            
            InitializeComponent();
            txtCoinMinute.Text = configmanager.ReadSetting("CoinMinute");
            txtNotificationMinute.Text = configmanager.ReadSetting("NotificationMinute");
            txtNotificationMessage.Text = configmanager.ReadSetting("NotificationMessage");
            txtBackgroundImage.Text = configmanager.ReadSetting("BackgroundImage");
            txtBackgroundMessage.Text = configmanager.ReadSetting("BackgroundMessage");
            txtBackgroundColor.Text = configmanager.ReadSetting("BackgroundColor");
            cbxSlotPort.Text = configmanager.ReadSetting("SlotPort");
            try
            {
                picBackgroundColor.BackColor = Color.FromArgb(int.Parse(configmanager.ReadSetting("BackgroundColor")));
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

        private void button5_Click(object sender, EventArgs e)
        {
            configmanager.AddUpdateAppSettings("CoinMinute", txtCoinMinute.Text);
            configmanager.AddUpdateAppSettings("NotificationMinute", txtNotificationMinute.Text);
            configmanager.AddUpdateAppSettings("NotificationMessage", txtNotificationMessage.Text);
            configmanager.AddUpdateAppSettings("BackgroundImage", txtBackgroundImage.Text);
            configmanager.AddUpdateAppSettings("BackgroundMessage", txtBackgroundMessage.Text);
            configmanager.AddUpdateAppSettings("BackgroundColor", txtBackgroundColor.Text);
            configmanager.AddUpdateAppSettings("SlotPort", cbxSlotPort.Text);
            FormTimer f1 = new FormTimer(so);
            f1.Show();
            this.Close();
        }

        private void btnDetectar_Click(object sender, EventArgs e)
        {
            SerialObserver serials = new SerialObserver();
            List<Device> devices= serials.GetSerials();
            cbxSlotPort.DataSource = devices;
            cbxSlotPort.DisplayMember = "Name";
            cbxSlotPort.ValueMember = "Port";
            cbxSlotPort.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void btnConectar_Click(object sender, EventArgs e)
        {
            so.Connect(((Device)cbxSlotPort.SelectedItem).Port);
        }
        
    }
}
