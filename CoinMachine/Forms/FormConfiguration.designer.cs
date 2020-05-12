namespace Forms
{
    partial class Inicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCoinMinute = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNotificationTitle = new System.Windows.Forms.TextBox();
            this.lblNotificationTitle = new System.Windows.Forms.Label();
            this.txtNotificationMinute = new System.Windows.Forms.NumericUpDown();
            this.txtNotificationMessage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtBackgroundMessageColor = new System.Windows.Forms.TextBox();
            this.picBackgroundMessageColor = new System.Windows.Forms.PictureBox();
            this.btnBackgroundMessageColor = new System.Windows.Forms.Button();
            this.lblBackgroundMessageColor = new System.Windows.Forms.Label();
            this.txtBackgroundColor = new System.Windows.Forms.TextBox();
            this.picBackgroundColor = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.picBackgroundImage = new System.Windows.Forms.PictureBox();
            this.btnBackgroundImage = new System.Windows.Forms.Button();
            this.txtBackgroundMessage = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBackgroundImage = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel4 = new System.Windows.Forms.Panel();
            this.picSlotOk = new System.Windows.Forms.PictureBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.cbxSlotPort = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCoinMinute)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotificationMinute)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundMessageColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundImage)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSlotOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "1 peso es igual a";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "minuto(s)";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtCoinMinute);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 42);
            this.panel1.TabIndex = 10;
            // 
            // txtCoinMinute
            // 
            this.txtCoinMinute.Location = new System.Drawing.Point(125, 14);
            this.txtCoinMinute.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtCoinMinute.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCoinMinute.Name = "txtCoinMinute";
            this.txtCoinMinute.Size = new System.Drawing.Size(33, 20);
            this.txtCoinMinute.TabIndex = 10;
            this.txtCoinMinute.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtNotificationTitle);
            this.panel2.Controls.Add(this.lblNotificationTitle);
            this.panel2.Controls.Add(this.txtNotificationMinute);
            this.panel2.Controls.Add(this.txtNotificationMessage);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(12, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 119);
            this.panel2.TabIndex = 11;
            // 
            // txtNotificationTitle
            // 
            this.txtNotificationTitle.Location = new System.Drawing.Point(127, 92);
            this.txtNotificationTitle.Name = "txtNotificationTitle";
            this.txtNotificationTitle.Size = new System.Drawing.Size(100, 20);
            this.txtNotificationTitle.TabIndex = 14;
            // 
            // lblNotificationTitle
            // 
            this.lblNotificationTitle.AutoSize = true;
            this.lblNotificationTitle.Location = new System.Drawing.Point(53, 86);
            this.lblNotificationTitle.Name = "lblNotificationTitle";
            this.lblNotificationTitle.Size = new System.Drawing.Size(62, 26);
            this.lblNotificationTitle.TabIndex = 13;
            this.lblNotificationTitle.Text = "Titulo de la \r\nnotificacion";
            // 
            // txtNotificationMinute
            // 
            this.txtNotificationMinute.Location = new System.Drawing.Point(127, 6);
            this.txtNotificationMinute.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtNotificationMinute.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNotificationMinute.Name = "txtNotificationMinute";
            this.txtNotificationMinute.Size = new System.Drawing.Size(33, 20);
            this.txtNotificationMinute.TabIndex = 12;
            this.txtNotificationMinute.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtNotificationMessage
            // 
            this.txtNotificationMessage.Location = new System.Drawing.Point(127, 30);
            this.txtNotificationMessage.Multiline = true;
            this.txtNotificationMessage.Name = "txtNotificationMessage";
            this.txtNotificationMessage.Size = new System.Drawing.Size(221, 57);
            this.txtNotificationMessage.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "Mensaje de \r\nnotificacion";
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tiempo restante para \r\nmostrar la notificacion";
            this.label3.UseMnemonic = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "minuto(s)";
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 26);
            this.label6.TabIndex = 7;
            this.label6.Text = "Seleccionar\r\nImagen";
            this.label6.UseMnemonic = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtBackgroundMessageColor);
            this.panel3.Controls.Add(this.picBackgroundMessageColor);
            this.panel3.Controls.Add(this.btnBackgroundMessageColor);
            this.panel3.Controls.Add(this.lblBackgroundMessageColor);
            this.panel3.Controls.Add(this.txtBackgroundColor);
            this.panel3.Controls.Add(this.picBackgroundColor);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.picBackgroundImage);
            this.panel3.Controls.Add(this.btnBackgroundImage);
            this.panel3.Controls.Add(this.txtBackgroundMessage);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtBackgroundImage);
            this.panel3.Location = new System.Drawing.Point(12, 205);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(363, 234);
            this.panel3.TabIndex = 13;
            // 
            // txtBackgroundMessageColor
            // 
            this.txtBackgroundMessageColor.Location = new System.Drawing.Point(244, 170);
            this.txtBackgroundMessageColor.Name = "txtBackgroundMessageColor";
            this.txtBackgroundMessageColor.Size = new System.Drawing.Size(34, 20);
            this.txtBackgroundMessageColor.TabIndex = 21;
            this.txtBackgroundMessageColor.Visible = false;
            // 
            // picBackgroundMessageColor
            // 
            this.picBackgroundMessageColor.BackColor = System.Drawing.Color.Red;
            this.picBackgroundMessageColor.Location = new System.Drawing.Point(128, 164);
            this.picBackgroundMessageColor.Name = "picBackgroundMessageColor";
            this.picBackgroundMessageColor.Size = new System.Drawing.Size(25, 25);
            this.picBackgroundMessageColor.TabIndex = 20;
            this.picBackgroundMessageColor.TabStop = false;
            // 
            // btnBackgroundMessageColor
            // 
            this.btnBackgroundMessageColor.Location = new System.Drawing.Point(163, 166);
            this.btnBackgroundMessageColor.Name = "btnBackgroundMessageColor";
            this.btnBackgroundMessageColor.Size = new System.Drawing.Size(75, 23);
            this.btnBackgroundMessageColor.TabIndex = 19;
            this.btnBackgroundMessageColor.Text = "Seleccionar";
            this.btnBackgroundMessageColor.UseVisualStyleBackColor = true;
            this.btnBackgroundMessageColor.Click += new System.EventHandler(this.btnBackgroundMessageColor_Click);
            // 
            // lblBackgroundMessageColor
            // 
            this.lblBackgroundMessageColor.AutoSize = true;
            this.lblBackgroundMessageColor.Location = new System.Drawing.Point(11, 163);
            this.lblBackgroundMessageColor.Name = "lblBackgroundMessageColor";
            this.lblBackgroundMessageColor.Size = new System.Drawing.Size(107, 26);
            this.lblBackgroundMessageColor.TabIndex = 18;
            this.lblBackgroundMessageColor.Text = "Seleccionar Color\r\nde Mensaje de fondo";
            // 
            // txtBackgroundColor
            // 
            this.txtBackgroundColor.Location = new System.Drawing.Point(244, 206);
            this.txtBackgroundColor.Name = "txtBackgroundColor";
            this.txtBackgroundColor.Size = new System.Drawing.Size(34, 20);
            this.txtBackgroundColor.TabIndex = 17;
            this.txtBackgroundColor.Visible = false;
            // 
            // picBackgroundColor
            // 
            this.picBackgroundColor.BackColor = System.Drawing.Color.Red;
            this.picBackgroundColor.Location = new System.Drawing.Point(128, 200);
            this.picBackgroundColor.Name = "picBackgroundColor";
            this.picBackgroundColor.Size = new System.Drawing.Size(25, 25);
            this.picBackgroundColor.TabIndex = 16;
            this.picBackgroundColor.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(163, 202);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Seleccionar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 26);
            this.label8.TabIndex = 14;
            this.label8.Text = "Seleccionar \r\nColor de Fondo";
            // 
            // picBackgroundImage
            // 
            this.picBackgroundImage.Location = new System.Drawing.Point(128, 5);
            this.picBackgroundImage.Name = "picBackgroundImage";
            this.picBackgroundImage.Size = new System.Drawing.Size(91, 89);
            this.picBackgroundImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBackgroundImage.TabIndex = 13;
            this.picBackgroundImage.TabStop = false;
            // 
            // btnBackgroundImage
            // 
            this.btnBackgroundImage.Location = new System.Drawing.Point(225, 31);
            this.btnBackgroundImage.Name = "btnBackgroundImage";
            this.btnBackgroundImage.Size = new System.Drawing.Size(77, 23);
            this.btnBackgroundImage.TabIndex = 12;
            this.btnBackgroundImage.Text = "Seleccionar";
            this.btnBackgroundImage.UseVisualStyleBackColor = true;
            this.btnBackgroundImage.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBackgroundMessage
            // 
            this.txtBackgroundMessage.Location = new System.Drawing.Point(128, 100);
            this.txtBackgroundMessage.Multiline = true;
            this.txtBackgroundMessage.Name = "txtBackgroundMessage";
            this.txtBackgroundMessage.Size = new System.Drawing.Size(220, 60);
            this.txtBackgroundMessage.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 26);
            this.label7.TabIndex = 10;
            this.label7.Text = "Mensaje\r\nde fondo";
            // 
            // txtBackgroundImage
            // 
            this.txtBackgroundImage.Location = new System.Drawing.Point(225, 74);
            this.txtBackgroundImage.Name = "txtBackgroundImage";
            this.txtBackgroundImage.Size = new System.Drawing.Size(72, 20);
            this.txtBackgroundImage.TabIndex = 6;
            this.txtBackgroundImage.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.picSlotOk);
            this.panel4.Controls.Add(this.btnConectar);
            this.panel4.Controls.Add(this.cbxSlotPort);
            this.panel4.Location = new System.Drawing.Point(12, 459);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(363, 42);
            this.panel4.TabIndex = 16;
            // 
            // picSlotOk
            // 
            this.picSlotOk.Image = global::CoinMachine.Properties.Resources.cross;
            this.picSlotOk.Location = new System.Drawing.Point(266, 9);
            this.picSlotOk.Name = "picSlotOk";
            this.picSlotOk.Size = new System.Drawing.Size(22, 24);
            this.picSlotOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSlotOk.TabIndex = 20;
            this.picSlotOk.TabStop = false;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(205, 11);
            this.btnConectar.Margin = new System.Windows.Forms.Padding(0);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(58, 19);
            this.btnConectar.TabIndex = 2;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // cbxSlotPort
            // 
            this.cbxSlotPort.FormattingEnabled = true;
            this.cbxSlotPort.Location = new System.Drawing.Point(12, 9);
            this.cbxSlotPort.Name = "cbxSlotPort";
            this.cbxSlotPort.Size = new System.Drawing.Size(190, 21);
            this.cbxSlotPort.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(277, 507);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(98, 24);
            this.button5.TabIndex = 19;
            this.button5.Text = "Guardar e Iniciar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::CoinMachine.Properties.Resources.coinslotter;
            this.pictureBox6.Location = new System.Drawing.Point(345, 445);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(37, 45);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 17;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::CoinMachine.Properties.Resources.iconmonstr_computer_3_240;
            this.pictureBox3.Location = new System.Drawing.Point(350, 193);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 34);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CoinMachine.Properties.Resources.bell_ringing;
            this.pictureBox2.Location = new System.Drawing.Point(350, 60);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CoinMachine.Properties.Resources.coing;
            this.pictureBox1.Location = new System.Drawing.Point(350, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 33);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(13, 508);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(387, 538);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Inicio";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tragamonedas";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCoinMinute)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotificationMinute)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundMessageColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundImage)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSlotOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNotificationMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtBackgroundMessage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBackgroundImage;
        private System.Windows.Forms.Button btnBackgroundImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox picBackgroundImage;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox picBackgroundColor;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.ComboBox cbxSlotPort;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtBackgroundColor;
        private System.Windows.Forms.NumericUpDown txtCoinMinute;
        private System.Windows.Forms.NumericUpDown txtNotificationMinute;
        private System.Windows.Forms.PictureBox picSlotOk;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtNotificationTitle;
        private System.Windows.Forms.Label lblNotificationTitle;
        private System.Windows.Forms.TextBox txtBackgroundMessageColor;
        private System.Windows.Forms.PictureBox picBackgroundMessageColor;
        private System.Windows.Forms.Button btnBackgroundMessageColor;
        private System.Windows.Forms.Label lblBackgroundMessageColor;
        private System.Windows.Forms.ColorDialog colorDialog2;
    }
}