using CoinMachine.Library;
using Library;
using Newtonsoft.Json;
using Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using virtualPrinter;

namespace Forms
{
    public partial class Inicio : Form
    {
        private ConfigManager configmanager = new ConfigManager();
        private Serial so = new Serial();
        private SerialObserver SerialObserver = new SerialObserver();

        //private myPrinterClass getPrinters = new myPrinterClass();
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

            //txtPrinterColorCoin
            //  txtPrinterGreyScaleCoin

            //listPrinterPrinters.Text = configmanager.ReadSetting("PrinterPrinters");
            string StringPrintersSaved = configmanager.ReadSetting("PrintersSaved");
            string StringPrintersInstalled = JsonConvert.SerializeObject(myPrinterClass.getPrinterNames());
            Console.WriteLine(StringPrintersInstalled);

            List<string> ListPrintersSaved = JsonConvert.DeserializeObject<List<string>>(StringPrintersSaved);
            List<string> ListPrintersInstalled = JsonConvert.DeserializeObject<List<string>>(StringPrintersInstalled);

            listPrintersSaved.ClearSelected();
            listPrintersSaved.SelectionMode = SelectionMode.MultiExtended;
            int i = 0;
            foreach (String item in ListPrintersInstalled)
            {
                listPrintersSaved.Items.Add(item);
                if (ListPrintersSaved != null)
                {
                    if (ListPrintersSaved.Contains(item))
                    {
                        listPrintersSaved.SetSelected(i, true);
                    }
                }

                i++;
            }

            ///txtPrinterPrinters.Text = configmanager.ReadSetting("PrinterPrinters");

            if (File.Exists(configmanager.ReadSetting("BackgroundImage"))) { btnBackgroundImage.Text = "Cambiar"; } else { btnBackgroundImage.Text = "Seleccionar"; }
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

            cbxSlotPort.DataSource = SerialObserver.GetSerials();
            cbxSlotPort.DisplayMember = "Name";
            cbxSlotPort.ValueMember = "Port";
            cbxSlotPort.DropDownStyle = ComboBoxStyle.DropDownList;

            chkPrinterModuleEnabled.Checked = configmanager.ReadSetting("PrinterModuleEnabled") == "true" ? true : false;

            txtPrinterGreyScaleCoin.Text = configmanager.ReadSetting("PrinterGreyScaleCoin");
            txtPrinterGreyScaleCent.Text = configmanager.ReadSetting("PrinterGreyScaleCent");
            txtPrinterColorCoin.Text = configmanager.ReadSetting("PrinterColorCoin");
            txtPrinterColorCent.Text = configmanager.ReadSetting("PrinterColorCent");

            SerialObserver.Changed += () =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    cbxSlotPort.DataSource = SerialObserver.GetSerials();
                    cbxSlotPort.DisplayMember = "Name";
                    cbxSlotPort.ValueMember = "Port";
                    cbxSlotPort.DropDownStyle = ComboBoxStyle.DropDownList;
                }));
            };
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
            cbxSlotPort.DataSource = SerialObserver.GetSerials();
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

            configmanager.AddUpdateAppSettings("PrinterGreyScaleCoin", txtPrinterGreyScaleCoin.Text);
            configmanager.AddUpdateAppSettings("PrinterGreyScaleCent", txtPrinterGreyScaleCent.Text);
            configmanager.AddUpdateAppSettings("PrinterColorCoin", txtPrinterColorCoin.Text);
            configmanager.AddUpdateAppSettings("PrinterColorCent", txtPrinterColorCent.Text);

            configmanager.AddUpdateAppSettings("PrinterModuleEnabled", chkPrinterModuleEnabled.Checked ? "true" : "false");

            if (listPrintersSaved.SelectedItems.Count <= 0) { MessageBox.Show("No ha seleccionado imrpesoram"); return; }

            string StringPrintersInstalled = JsonConvert.SerializeObject(listPrintersSaved.SelectedItems);
            configmanager.AddUpdateAppSettings("PrintersSaved", StringPrintersInstalled);

            //Console.WriteLine(StringPrintersInstalled);
            new FormCountDownTimer1(so).Show();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkPrinterModuleEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrinterModuleEnabled.Checked == true)
            {
            }
        }
    }
}