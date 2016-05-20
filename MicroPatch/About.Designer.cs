namespace MicroPatch
{
    partial class About
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
            this.closeButton = new System.Windows.Forms.Button();
            this.infoBox = new System.Windows.Forms.Label();
            this.windowTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.BackgroundImage = global::MicroPatch.Properties.Resources.close;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(378, 2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(20, 20);
            this.closeButton.TabIndex = 2;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // infoBox
            // 
            this.infoBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.infoBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.infoBox.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoBox.ForeColor = System.Drawing.Color.White;
            this.infoBox.Location = new System.Drawing.Point(0, 62);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(400, 100);
            this.infoBox.TabIndex = 5;
            this.infoBox.Text = "MicroPatch v1.1 r01\r\nProudly created by PaNtHer\r\n\r\nd99496b9c4c1b011bde8ef27b33ccc" +
    "7e";
            this.infoBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.windowTitle.Size = new System.Drawing.Size(129, 16);
            this.windowTitle.TabIndex = 6;
            this.windowTitle.Text = "About MicroPatch";
            this.windowTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MicroPatch.Properties.Resources.bg1;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.windowTitle);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.Opacity = 0.9D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label infoBox;
        private System.Windows.Forms.Label windowTitle;
    }
}