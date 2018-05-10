namespace link.ui
{
    partial class SettingsPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.groupBoxLogging = new System.Windows.Forms.GroupBox();
            this.textBoxLogTo = new System.Windows.Forms.TextBox();
            this.labelSaveTo = new System.Windows.Forms.Label();
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.groupBoxLayout = new System.Windows.Forms.GroupBox();
            this.checkBoxSaveLocation = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveSize = new System.Windows.Forms.CheckBox();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.groupBoxDebug = new System.Windows.Forms.GroupBox();
            this.comboBoxProtocol = new System.Windows.Forms.ComboBox();
            this.checkBoxSimulated = new System.Windows.Forms.CheckBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.groupBoxLogging.SuspendLayout();
            this.groupBoxLayout.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.groupBoxDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSettings.Controls.Add(this.tabPageGeneral);
            this.tabControlSettings.Controls.Add(this.tabPageAdvanced);
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(522, 415);
            this.tabControlSettings.TabIndex = 2;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.groupBoxLogging);
            this.tabPageGeneral.Controls.Add(this.groupBoxLayout);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(514, 389);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBoxLogging
            // 
            this.groupBoxLogging.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLogging.Controls.Add(this.textBoxLogTo);
            this.groupBoxLogging.Controls.Add(this.labelSaveTo);
            this.groupBoxLogging.Controls.Add(this.checkBoxLog);
            this.groupBoxLogging.Location = new System.Drawing.Point(6, 83);
            this.groupBoxLogging.Name = "groupBoxLogging";
            this.groupBoxLogging.Size = new System.Drawing.Size(502, 71);
            this.groupBoxLogging.TabIndex = 1;
            this.groupBoxLogging.TabStop = false;
            this.groupBoxLogging.Text = "Logging";
            // 
            // textBoxLogTo
            // 
            this.textBoxLogTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogTo.Location = new System.Drawing.Point(78, 42);
            this.textBoxLogTo.Name = "textBoxLogTo";
            this.textBoxLogTo.Size = new System.Drawing.Size(418, 20);
            this.textBoxLogTo.TabIndex = 2;
            this.textBoxLogTo.Text = "C:\\temp\\";
            this.textBoxLogTo.TextChanged += new System.EventHandler(this.textBoxSaveLogsTo_TextChanged);
            // 
            // labelSaveTo
            // 
            this.labelSaveTo.AutoSize = true;
            this.labelSaveTo.Location = new System.Drawing.Point(6, 45);
            this.labelSaveTo.Name = "labelSaveTo";
            this.labelSaveTo.Size = new System.Drawing.Size(66, 13);
            this.labelSaveTo.TabIndex = 1;
            this.labelSaveTo.Text = "Save logs to";
            // 
            // checkBoxLog
            // 
            this.checkBoxLog.AutoSize = true;
            this.checkBoxLog.Location = new System.Drawing.Point(6, 20);
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.Size = new System.Drawing.Size(168, 17);
            this.checkBoxLog.TabIndex = 0;
            this.checkBoxLog.Text = "Start logging when connected";
            this.checkBoxLog.UseVisualStyleBackColor = true;
            this.checkBoxLog.CheckedChanged += new System.EventHandler(this.checkBoxLog_CheckedChanged);
            // 
            // groupBoxLayout
            // 
            this.groupBoxLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLayout.Controls.Add(this.checkBoxSaveLocation);
            this.groupBoxLayout.Controls.Add(this.checkBoxSaveSize);
            this.groupBoxLayout.Location = new System.Drawing.Point(6, 6);
            this.groupBoxLayout.Name = "groupBoxLayout";
            this.groupBoxLayout.Size = new System.Drawing.Size(502, 71);
            this.groupBoxLayout.TabIndex = 0;
            this.groupBoxLayout.TabStop = false;
            this.groupBoxLayout.Text = "Layout";
            // 
            // checkBoxSaveLocation
            // 
            this.checkBoxSaveLocation.AutoSize = true;
            this.checkBoxSaveLocation.Location = new System.Drawing.Point(6, 43);
            this.checkBoxSaveLocation.Name = "checkBoxSaveLocation";
            this.checkBoxSaveLocation.Size = new System.Drawing.Size(217, 17);
            this.checkBoxSaveLocation.TabIndex = 1;
            this.checkBoxSaveLocation.Text = "Save window location between sessions";
            this.checkBoxSaveLocation.UseVisualStyleBackColor = true;
            this.checkBoxSaveLocation.CheckedChanged += new System.EventHandler(this.checkBoxSaveLocation_CheckedChanged);
            // 
            // checkBoxSaveSize
            // 
            this.checkBoxSaveSize.AutoSize = true;
            this.checkBoxSaveSize.Location = new System.Drawing.Point(6, 20);
            this.checkBoxSaveSize.Name = "checkBoxSaveSize";
            this.checkBoxSaveSize.Size = new System.Drawing.Size(198, 17);
            this.checkBoxSaveSize.TabIndex = 0;
            this.checkBoxSaveSize.Text = "Save window size between sessions";
            this.checkBoxSaveSize.UseVisualStyleBackColor = true;
            this.checkBoxSaveSize.CheckedChanged += new System.EventHandler(this.checkBoxSaveSize_CheckedChanged);
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.groupBoxDebug);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdvanced.Size = new System.Drawing.Size(514, 389);
            this.tabPageAdvanced.TabIndex = 1;
            this.tabPageAdvanced.Text = "Advanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // groupBoxDebug
            // 
            this.groupBoxDebug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDebug.Controls.Add(this.comboBoxProtocol);
            this.groupBoxDebug.Controls.Add(this.checkBoxSimulated);
            this.groupBoxDebug.Location = new System.Drawing.Point(6, 6);
            this.groupBoxDebug.Name = "groupBoxDebug";
            this.groupBoxDebug.Size = new System.Drawing.Size(502, 46);
            this.groupBoxDebug.TabIndex = 1;
            this.groupBoxDebug.TabStop = false;
            this.groupBoxDebug.Text = "Debug";
            // 
            // comboBoxProtocol
            // 
            this.comboBoxProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProtocol.FormattingEnabled = true;
            this.comboBoxProtocol.Items.AddRange(new object[] {
            "SAE J1850 VPW (GM)",
            "SAE J1850 PWM (Ford)",
            "ISO 15765-4 CAN (11/500)",
            "ISO 15765-4 CAN (29/500)",
            "ISO 15765-4 CAN (11/250)",
            "ISO 15765-4 CAN (29/250)"});
            this.comboBoxProtocol.Location = new System.Drawing.Point(176, 16);
            this.comboBoxProtocol.Name = "comboBoxProtocol";
            this.comboBoxProtocol.Size = new System.Drawing.Size(184, 21);
            this.comboBoxProtocol.TabIndex = 1;
            this.comboBoxProtocol.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocol_SelectedIndexChanged);
            // 
            // checkBoxSimulated
            // 
            this.checkBoxSimulated.AutoSize = true;
            this.checkBoxSimulated.Location = new System.Drawing.Point(6, 20);
            this.checkBoxSimulated.Name = "checkBoxSimulated";
            this.checkBoxSimulated.Size = new System.Drawing.Size(164, 17);
            this.checkBoxSimulated.TabIndex = 0;
            this.checkBoxSimulated.Text = "Use simulated ELM327 cable";
            this.checkBoxSimulated.UseVisualStyleBackColor = true;
            this.checkBoxSimulated.CheckedChanged += new System.EventHandler(this.checkBoxSimulated_CheckedChanged);
            // 
            // SettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlSettings);
            this.Name = "SettingsPanel";
            this.Size = new System.Drawing.Size(522, 415);
            this.Load += new System.EventHandler(this.SettingsPanel_Load);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.groupBoxLogging.ResumeLayout(false);
            this.groupBoxLogging.PerformLayout();
            this.groupBoxLayout.ResumeLayout(false);
            this.groupBoxLayout.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.groupBoxDebug.ResumeLayout(false);
            this.groupBoxDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.GroupBox groupBoxLayout;
        private System.Windows.Forms.CheckBox checkBoxSaveLocation;
        private System.Windows.Forms.CheckBox checkBoxSaveSize;
        private System.Windows.Forms.GroupBox groupBoxDebug;
        private System.Windows.Forms.CheckBox checkBoxSimulated;
        private System.Windows.Forms.GroupBox groupBoxLogging;
        private System.Windows.Forms.CheckBox checkBoxLog;
        private System.Windows.Forms.TextBox textBoxLogTo;
        private System.Windows.Forms.Label labelSaveTo;
        private System.Windows.Forms.ComboBox comboBoxProtocol;
    }
}
