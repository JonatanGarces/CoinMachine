namespace Forms
{
    partial class ScreenSaverForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenSaverForm));
            this.textLabel = new System.Windows.Forms.Label();
            this.picBackgroundImage = new System.Windows.Forms.PictureBox();
            this.moveTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundImage)).BeginInit();
            this.SuspendLayout();
            // 
            // textLabel
            // 
            this.textLabel.AutoSize = true;
            this.textLabel.BackColor = System.Drawing.Color.Transparent;
            this.textLabel.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLabel.ForeColor = System.Drawing.Color.Gray;
            this.textLabel.Location = new System.Drawing.Point(51, 9);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(576, 55);
            this.textLabel.TabIndex = 0;
            this.textLabel.Text = "Favor de insertar moneda";
            // 
            // picBackgroundImage
            // 
            this.picBackgroundImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBackgroundImage.Image = ((System.Drawing.Image)(resources.GetObject("picBackgroundImage.Image")));
            this.picBackgroundImage.Location = new System.Drawing.Point(85, 32);
            this.picBackgroundImage.Margin = new System.Windows.Forms.Padding(2);
            this.picBackgroundImage.Name = "picBackgroundImage";
            this.picBackgroundImage.Size = new System.Drawing.Size(415, 217);
            this.picBackgroundImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBackgroundImage.TabIndex = 1;
            this.picBackgroundImage.TabStop = false;
            // 
            // ScreenSaverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(830, 260);
            this.Controls.Add(this.textLabel);
            this.Controls.Add(this.picBackgroundImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScreenSaverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormTimer";
            this.Load += new System.EventHandler(this.ScreenSaverForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScreenSaverForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.PictureBox picBackgroundImage;
        private System.Windows.Forms.Timer moveTimer;
    }
}

