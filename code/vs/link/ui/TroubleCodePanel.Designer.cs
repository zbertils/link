namespace link.ui
{
    partial class TroubleCodePanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TroubleCodePanel));
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBoxDtcInfo = new System.Windows.Forms.GroupBox();
            this.groupBoxDtc = new System.Windows.Forms.GroupBox();
            this.listViewDtc = new System.Windows.Forms.ListView();
            this.troubleCodeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.descriptionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listViewCodeStatuses = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxDtc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCheck
            // 
            this.buttonCheck.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCheck.Enabled = false;
            this.buttonCheck.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheck.Image")));
            this.buttonCheck.Location = new System.Drawing.Point(15, 0);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(100, 100);
            this.buttonCheck.TabIndex = 1;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCheck.UseVisualStyleBackColor = false;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.SystemColors.Control;
            this.buttonClear.Enabled = false;
            this.buttonClear.Image = ((System.Drawing.Image)(resources.GetObject("buttonClear.Image")));
            this.buttonClear.Location = new System.Drawing.Point(121, 0);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(100, 100);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Clear";
            this.buttonClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBoxDtcInfo
            // 
            this.groupBoxDtcInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDtcInfo.Location = new System.Drawing.Point(227, 3);
            this.groupBoxDtcInfo.Name = "groupBoxDtcInfo";
            this.groupBoxDtcInfo.Size = new System.Drawing.Size(214, 96);
            this.groupBoxDtcInfo.TabIndex = 3;
            this.groupBoxDtcInfo.TabStop = false;
            this.groupBoxDtcInfo.Text = "DTC Info";
            // 
            // groupBoxDtc
            // 
            this.groupBoxDtc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDtc.Controls.Add(this.tabControl1);
            this.groupBoxDtc.Location = new System.Drawing.Point(15, 106);
            this.groupBoxDtc.Name = "groupBoxDtc";
            this.groupBoxDtc.Size = new System.Drawing.Size(427, 231);
            this.groupBoxDtc.TabIndex = 4;
            this.groupBoxDtc.TabStop = false;
            this.groupBoxDtc.Text = "DTC Info";
            // 
            // listViewDtc
            // 
            this.listViewDtc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.troubleCodeColumn,
            this.typeColumn,
            this.descriptionColumn});
            this.listViewDtc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDtc.Location = new System.Drawing.Point(3, 3);
            this.listViewDtc.MultiSelect = false;
            this.listViewDtc.Name = "listViewDtc";
            this.listViewDtc.Size = new System.Drawing.Size(407, 180);
            this.listViewDtc.TabIndex = 4;
            this.listViewDtc.UseCompatibleStateImageBehavior = false;
            this.listViewDtc.View = System.Windows.Forms.View.Details;
            // 
            // troubleCodeColumn
            // 
            this.troubleCodeColumn.Text = "Trouble Code";
            this.troubleCodeColumn.Width = 100;
            // 
            // typeColumn
            // 
            this.typeColumn.Text = "Type";
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.Text = "Description";
            this.descriptionColumn.Width = 275;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(421, 212);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listViewDtc);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(413, 186);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Active Codes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listViewCodeStatuses);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(413, 186);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Code Statuses";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewCodeStatuses
            // 
            this.listViewCodeStatuses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewCodeStatuses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCodeStatuses.Location = new System.Drawing.Point(3, 3);
            this.listViewCodeStatuses.MultiSelect = false;
            this.listViewCodeStatuses.Name = "listViewCodeStatuses";
            this.listViewCodeStatuses.Size = new System.Drawing.Size(407, 180);
            this.listViewCodeStatuses.TabIndex = 5;
            this.listViewCodeStatuses.UseCompatibleStateImageBehavior = false;
            this.listViewCodeStatuses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Trouble Code";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status";
            this.columnHeader2.Width = 275;
            // 
            // TroubleCodePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxDtc);
            this.Controls.Add(this.groupBoxDtcInfo);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonCheck);
            this.Name = "TroubleCodePanel";
            this.Size = new System.Drawing.Size(445, 340);
            this.groupBoxDtc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.GroupBox groupBoxDtcInfo;
        private System.Windows.Forms.GroupBox groupBoxDtc;
        private System.Windows.Forms.ListView listViewDtc;
        private System.Windows.Forms.ColumnHeader troubleCodeColumn;
        private System.Windows.Forms.ColumnHeader descriptionColumn;
        private System.Windows.Forms.ColumnHeader typeColumn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listViewCodeStatuses;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
