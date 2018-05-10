namespace link.ui
{
    partial class HomePanel
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
            this.buttonCarInfo = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelVinTitle = new System.Windows.Forms.Label();
            this.labelYearTitle = new System.Windows.Forms.Label();
            this.labelModelTitle = new System.Windows.Forms.Label();
            this.labelMakeTitle = new System.Windows.Forms.Label();
            this.groupBoxVehicleInfo = new System.Windows.Forms.GroupBox();
            this.labelStatusTitle = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.labelElmVersionTitle = new System.Windows.Forms.Label();
            this.labelElmVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelChosenProtocol = new System.Windows.Forms.Label();
            this.labelAutoProtocolTitle = new System.Windows.Forms.Label();
            this.labelAutoProtocol = new System.Windows.Forms.Label();
            this.labelEchoTitle = new System.Windows.Forms.Label();
            this.labelEcho = new System.Windows.Forms.Label();
            this.labelVin = new System.Windows.Forms.Label();
            this.labelYear = new System.Windows.Forms.Label();
            this.labelMake = new System.Windows.Forms.Label();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.groupBoxVehicleInfo.SuspendLayout();
            this.groupBoxConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCarInfo
            // 
            this.buttonCarInfo.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCarInfo.Image = global::link.Properties.Resources.CarInfo;
            this.buttonCarInfo.Location = new System.Drawing.Point(15, 106);
            this.buttonCarInfo.Name = "buttonCarInfo";
            this.buttonCarInfo.Size = new System.Drawing.Size(100, 100);
            this.buttonCarInfo.TabIndex = 1;
            this.buttonCarInfo.Text = "Save Vehicle Info";
            this.buttonCarInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCarInfo.UseVisualStyleBackColor = false;
            this.buttonCarInfo.Click += new System.EventHandler(this.buttonCarInfo_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.SystemColors.Control;
            this.buttonConnect.Image = global::link.Properties.Resources.NotConnected;
            this.buttonConnect.Location = new System.Drawing.Point(15, 0);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(100, 100);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelVinTitle
            // 
            this.labelVinTitle.AutoSize = true;
            this.labelVinTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVinTitle.Location = new System.Drawing.Point(6, 22);
            this.labelVinTitle.Name = "labelVinTitle";
            this.labelVinTitle.Size = new System.Drawing.Size(33, 16);
            this.labelVinTitle.TabIndex = 2;
            this.labelVinTitle.Text = "VIN:";
            // 
            // labelYearTitle
            // 
            this.labelYearTitle.AutoSize = true;
            this.labelYearTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYearTitle.Location = new System.Drawing.Point(6, 48);
            this.labelYearTitle.Name = "labelYearTitle";
            this.labelYearTitle.Size = new System.Drawing.Size(40, 16);
            this.labelYearTitle.TabIndex = 3;
            this.labelYearTitle.Text = "Year:";
            // 
            // labelModelTitle
            // 
            this.labelModelTitle.AutoSize = true;
            this.labelModelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModelTitle.Location = new System.Drawing.Point(6, 103);
            this.labelModelTitle.Name = "labelModelTitle";
            this.labelModelTitle.Size = new System.Drawing.Size(49, 16);
            this.labelModelTitle.TabIndex = 4;
            this.labelModelTitle.Text = "Model:";
            // 
            // labelMakeTitle
            // 
            this.labelMakeTitle.AutoSize = true;
            this.labelMakeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMakeTitle.Location = new System.Drawing.Point(6, 75);
            this.labelMakeTitle.Name = "labelMakeTitle";
            this.labelMakeTitle.Size = new System.Drawing.Size(45, 16);
            this.labelMakeTitle.TabIndex = 5;
            this.labelMakeTitle.Text = "Make:";
            // 
            // groupBoxVehicleInfo
            // 
            this.groupBoxVehicleInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVehicleInfo.Controls.Add(this.textBoxModel);
            this.groupBoxVehicleInfo.Controls.Add(this.labelMake);
            this.groupBoxVehicleInfo.Controls.Add(this.labelYear);
            this.groupBoxVehicleInfo.Controls.Add(this.labelVin);
            this.groupBoxVehicleInfo.Controls.Add(this.labelVinTitle);
            this.groupBoxVehicleInfo.Controls.Add(this.labelMakeTitle);
            this.groupBoxVehicleInfo.Controls.Add(this.labelYearTitle);
            this.groupBoxVehicleInfo.Controls.Add(this.labelModelTitle);
            this.groupBoxVehicleInfo.Location = new System.Drawing.Point(121, 3);
            this.groupBoxVehicleInfo.Name = "groupBoxVehicleInfo";
            this.groupBoxVehicleInfo.Size = new System.Drawing.Size(347, 203);
            this.groupBoxVehicleInfo.TabIndex = 6;
            this.groupBoxVehicleInfo.TabStop = false;
            this.groupBoxVehicleInfo.Text = "Vehicle Info";
            // 
            // labelStatusTitle
            // 
            this.labelStatusTitle.AutoSize = true;
            this.labelStatusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatusTitle.Location = new System.Drawing.Point(6, 26);
            this.labelStatusTitle.Name = "labelStatusTitle";
            this.labelStatusTitle.Size = new System.Drawing.Size(48, 16);
            this.labelStatusTitle.TabIndex = 7;
            this.labelStatusTitle.Text = "Status:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(120, 26);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(17, 16);
            this.labelStatus.TabIndex = 8;
            this.labelStatus.Text = "...";
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConnection.Controls.Add(this.labelElmVersionTitle);
            this.groupBoxConnection.Controls.Add(this.labelElmVersion);
            this.groupBoxConnection.Controls.Add(this.label4);
            this.groupBoxConnection.Controls.Add(this.labelChosenProtocol);
            this.groupBoxConnection.Controls.Add(this.labelAutoProtocolTitle);
            this.groupBoxConnection.Controls.Add(this.labelAutoProtocol);
            this.groupBoxConnection.Controls.Add(this.labelEchoTitle);
            this.groupBoxConnection.Controls.Add(this.labelEcho);
            this.groupBoxConnection.Controls.Add(this.labelStatusTitle);
            this.groupBoxConnection.Controls.Add(this.labelStatus);
            this.groupBoxConnection.Location = new System.Drawing.Point(15, 212);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(453, 160);
            this.groupBoxConnection.TabIndex = 9;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Connection";
            // 
            // labelElmVersionTitle
            // 
            this.labelElmVersionTitle.AutoSize = true;
            this.labelElmVersionTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelElmVersionTitle.Location = new System.Drawing.Point(6, 133);
            this.labelElmVersionTitle.Name = "labelElmVersionTitle";
            this.labelElmVersionTitle.Size = new System.Drawing.Size(87, 16);
            this.labelElmVersionTitle.TabIndex = 15;
            this.labelElmVersionTitle.Text = "ELM Version:";
            // 
            // labelElmVersion
            // 
            this.labelElmVersion.AutoSize = true;
            this.labelElmVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelElmVersion.Location = new System.Drawing.Point(120, 133);
            this.labelElmVersion.Name = "labelElmVersion";
            this.labelElmVersion.Size = new System.Drawing.Size(17, 16);
            this.labelElmVersion.TabIndex = 16;
            this.labelElmVersion.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Chosen Protocol:";
            // 
            // labelChosenProtocol
            // 
            this.labelChosenProtocol.AutoSize = true;
            this.labelChosenProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChosenProtocol.Location = new System.Drawing.Point(120, 108);
            this.labelChosenProtocol.Name = "labelChosenProtocol";
            this.labelChosenProtocol.Size = new System.Drawing.Size(17, 16);
            this.labelChosenProtocol.TabIndex = 14;
            this.labelChosenProtocol.Text = "...";
            // 
            // labelAutoProtocolTitle
            // 
            this.labelAutoProtocolTitle.AutoSize = true;
            this.labelAutoProtocolTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAutoProtocolTitle.Location = new System.Drawing.Point(6, 81);
            this.labelAutoProtocolTitle.Name = "labelAutoProtocolTitle";
            this.labelAutoProtocolTitle.Size = new System.Drawing.Size(91, 16);
            this.labelAutoProtocolTitle.TabIndex = 11;
            this.labelAutoProtocolTitle.Text = "Auto Protocol:";
            // 
            // labelAutoProtocol
            // 
            this.labelAutoProtocol.AutoSize = true;
            this.labelAutoProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAutoProtocol.Location = new System.Drawing.Point(120, 81);
            this.labelAutoProtocol.Name = "labelAutoProtocol";
            this.labelAutoProtocol.Size = new System.Drawing.Size(17, 16);
            this.labelAutoProtocol.TabIndex = 12;
            this.labelAutoProtocol.Text = "...";
            // 
            // labelEchoTitle
            // 
            this.labelEchoTitle.AutoSize = true;
            this.labelEchoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEchoTitle.Location = new System.Drawing.Point(6, 53);
            this.labelEchoTitle.Name = "labelEchoTitle";
            this.labelEchoTitle.Size = new System.Drawing.Size(42, 16);
            this.labelEchoTitle.TabIndex = 9;
            this.labelEchoTitle.Text = "Echo:";
            // 
            // labelEcho
            // 
            this.labelEcho.AutoSize = true;
            this.labelEcho.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEcho.Location = new System.Drawing.Point(120, 53);
            this.labelEcho.Name = "labelEcho";
            this.labelEcho.Size = new System.Drawing.Size(17, 16);
            this.labelEcho.TabIndex = 10;
            this.labelEcho.Text = "...";
            // 
            // labelVin
            // 
            this.labelVin.AutoSize = true;
            this.labelVin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVin.Location = new System.Drawing.Point(56, 22);
            this.labelVin.Name = "labelVin";
            this.labelVin.Size = new System.Drawing.Size(17, 16);
            this.labelVin.TabIndex = 6;
            this.labelVin.Text = "...";
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYear.Location = new System.Drawing.Point(56, 48);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(17, 16);
            this.labelYear.TabIndex = 7;
            this.labelYear.Text = "...";
            // 
            // labelMake
            // 
            this.labelMake.AutoSize = true;
            this.labelMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMake.Location = new System.Drawing.Point(56, 75);
            this.labelMake.Name = "labelMake";
            this.labelMake.Size = new System.Drawing.Size(17, 16);
            this.labelMake.TabIndex = 9;
            this.labelMake.Text = "...";
            // 
            // textBoxModel
            // 
            this.textBoxModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxModel.Location = new System.Drawing.Point(59, 99);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(282, 20);
            this.textBoxModel.TabIndex = 10;
            this.textBoxModel.TextChanged += new System.EventHandler(this.textBoxModel_TextChanged);
            // 
            // HomePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxConnection);
            this.Controls.Add(this.groupBoxVehicleInfo);
            this.Controls.Add(this.buttonCarInfo);
            this.Controls.Add(this.buttonConnect);
            this.Name = "HomePanel";
            this.Size = new System.Drawing.Size(471, 375);
            this.groupBoxVehicleInfo.ResumeLayout(false);
            this.groupBoxVehicleInfo.PerformLayout();
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxConnection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonCarInfo;
        private System.Windows.Forms.Label labelVinTitle;
        private System.Windows.Forms.Label labelYearTitle;
        private System.Windows.Forms.Label labelModelTitle;
        private System.Windows.Forms.Label labelMakeTitle;
        private System.Windows.Forms.GroupBox groupBoxVehicleInfo;
        private System.Windows.Forms.Label labelStatusTitle;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.GroupBox groupBoxConnection;
        private System.Windows.Forms.Label labelEchoTitle;
        private System.Windows.Forms.Label labelEcho;
        private System.Windows.Forms.Label labelAutoProtocolTitle;
        private System.Windows.Forms.Label labelAutoProtocol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelChosenProtocol;
        private System.Windows.Forms.Label labelElmVersionTitle;
        private System.Windows.Forms.Label labelElmVersion;
        private System.Windows.Forms.Label labelMake;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.Label labelVin;
        private System.Windows.Forms.TextBox textBoxModel;
    }
}
