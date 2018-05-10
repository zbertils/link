namespace link
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.buttonHome = new System.Windows.Forms.Button();
            this.buttonDtc = new System.Windows.Forms.Button();
            this.buttonData = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.dockPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonHome
            // 
            this.buttonHome.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHome.Image = ((System.Drawing.Image)(resources.GetObject("buttonHome.Image")));
            this.buttonHome.Location = new System.Drawing.Point(12, 12);
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.Size = new System.Drawing.Size(100, 100);
            this.buttonHome.TabIndex = 1;
            this.buttonHome.Text = "Home";
            this.buttonHome.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonHome.UseVisualStyleBackColor = false;
            this.buttonHome.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // buttonDtc
            // 
            this.buttonDtc.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonDtc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDtc.Image = ((System.Drawing.Image)(resources.GetObject("buttonDtc.Image")));
            this.buttonDtc.Location = new System.Drawing.Point(12, 118);
            this.buttonDtc.Name = "buttonDtc";
            this.buttonDtc.Size = new System.Drawing.Size(100, 100);
            this.buttonDtc.TabIndex = 3;
            this.buttonDtc.Text = "DTC";
            this.buttonDtc.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonDtc.UseVisualStyleBackColor = false;
            this.buttonDtc.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // buttonData
            // 
            this.buttonData.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonData.Image = ((System.Drawing.Image)(resources.GetObject("buttonData.Image")));
            this.buttonData.Location = new System.Drawing.Point(12, 224);
            this.buttonData.Name = "buttonData";
            this.buttonData.Size = new System.Drawing.Size(100, 100);
            this.buttonData.TabIndex = 4;
            this.buttonData.Text = "Data";
            this.buttonData.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonData.UseVisualStyleBackColor = false;
            this.buttonData.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonSettings.Image")));
            this.buttonSettings.Location = new System.Drawing.Point(12, 330);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(100, 100);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.navigationButton_Click);
            // 
            // dockPanel
            // 
            this.dockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dockPanel.Location = new System.Drawing.Point(118, 12);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(536, 418);
            this.dockPanel.TabIndex = 6;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(666, 455);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonData);
            this.Controls.Add(this.buttonDtc);
            this.Controls.Add(this.buttonHome);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Link";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonHome;
        private System.Windows.Forms.Button buttonDtc;
        private System.Windows.Forms.Button buttonData;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Panel dockPanel;
    }
}

