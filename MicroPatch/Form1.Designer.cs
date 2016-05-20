namespace MicroPatch
{
    partial class MicroPatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MicroPatch));
            this.patch = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.windowTitle = new System.Windows.Forms.Label();
            this.infoBox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // patch
            // 
            this.patch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.patch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.patch.Font = new System.Drawing.Font("Source Sans Pro", 12F);
            this.patch.Location = new System.Drawing.Point(150, 152);
            this.patch.Name = "patch";
            this.patch.Size = new System.Drawing.Size(100, 32);
            this.patch.TabIndex = 0;
            this.patch.Text = "Patch";
            this.patch.UseVisualStyleBackColor = false;
            this.patch.Click += new System.EventHandler(this.patch_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackgroundImage = global::MicroPatch.Properties.Resources.close;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(378, 2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(20, 20);
            this.closeButton.TabIndex = 1;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.closeButton_MouseDown);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Red;
            this.progressBar1.Location = new System.Drawing.Point(12, 123);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(376, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 2;
            // 
            // windowTitle
            // 
            this.windowTitle.AutoSize = true;
            this.windowTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.windowTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.windowTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(36)))), ((int)(((byte)(0)))));
            this.windowTitle.Location = new System.Drawing.Point(2, 2);
            this.windowTitle.Name = "windowTitle";
            this.windowTitle.Size = new System.Drawing.Size(188, 16);
            this.windowTitle.TabIndex = 3;
            this.windowTitle.Text = "MicroPatch v1.1 | PaNtHer";
            this.windowTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // infoBox
            // 
            this.infoBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.infoBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.infoBox.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoBox.ForeColor = System.Drawing.Color.White;
            this.infoBox.Location = new System.Drawing.Point(2, 26);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(396, 94);
            this.infoBox.TabIndex = 4;
            this.infoBox.Text = "Click here to select a patch file.\r\n\r\nStatus: Waiting";
            this.infoBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infoBox.Click += new System.EventHandler(this.label1_Click);
            // 
            // MicroPatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.BackgroundImage = global::MicroPatch.Properties.Resources.bg1;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.windowTitle);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.patch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MicroPatch";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MicroPatch v1.1";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button patch;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label windowTitle;
        private System.Windows.Forms.Label infoBox;
    }
}

