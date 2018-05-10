namespace link.ui
{
    partial class DataPanel
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Engine", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Transmission", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Body", System.Windows.Forms.HorizontalAlignment.Left);
            this.listViewRealTimeValues = new System.Windows.Forms.ListView();
            this.reserved0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnParameter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnUnits = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControlData = new System.Windows.Forms.TabControl();
            this.tabListView = new System.Windows.Forms.TabPage();
            this.tabGraphs = new System.Windows.Forms.TabPage();
            this.splitContainerData = new System.Windows.Forms.SplitContainer();
            this.treeViewPids = new System.Windows.Forms.TreeView();
            this.timerUpdateListView = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripTreeViewRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editPIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlData.SuspendLayout();
            this.tabListView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerData)).BeginInit();
            this.splitContainerData.Panel1.SuspendLayout();
            this.splitContainerData.Panel2.SuspendLayout();
            this.splitContainerData.SuspendLayout();
            this.contextMenuStripTreeViewRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewRealTimeValues
            // 
            this.listViewRealTimeValues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.reserved0,
            this.columnParameter,
            this.columnValue,
            this.columnUnits});
            this.listViewRealTimeValues.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Engine";
            listViewGroup1.Name = "listViewGroupEngine";
            listViewGroup2.Header = "Transmission";
            listViewGroup2.Name = "listViewGroupTransmission";
            listViewGroup3.Header = "Body";
            listViewGroup3.Name = "listViewGroupBody";
            this.listViewRealTimeValues.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.listViewRealTimeValues.Location = new System.Drawing.Point(3, 3);
            this.listViewRealTimeValues.Name = "listViewRealTimeValues";
            this.listViewRealTimeValues.Size = new System.Drawing.Size(488, 488);
            this.listViewRealTimeValues.TabIndex = 2;
            this.listViewRealTimeValues.UseCompatibleStateImageBehavior = false;
            this.listViewRealTimeValues.View = System.Windows.Forms.View.Details;
            // 
            // reserved0
            // 
            this.reserved0.Text = "";
            this.reserved0.Width = 0;
            // 
            // columnParameter
            // 
            this.columnParameter.Text = "Parameter";
            this.columnParameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnParameter.Width = 180;
            // 
            // columnValue
            // 
            this.columnValue.Text = "Value";
            this.columnValue.Width = 80;
            // 
            // columnUnits
            // 
            this.columnUnits.Text = "Units";
            this.columnUnits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabControlData
            // 
            this.tabControlData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlData.Controls.Add(this.tabListView);
            this.tabControlData.Controls.Add(this.tabGraphs);
            this.tabControlData.Location = new System.Drawing.Point(0, 0);
            this.tabControlData.Name = "tabControlData";
            this.tabControlData.SelectedIndex = 0;
            this.tabControlData.Size = new System.Drawing.Size(502, 520);
            this.tabControlData.TabIndex = 3;
            // 
            // tabListView
            // 
            this.tabListView.Controls.Add(this.listViewRealTimeValues);
            this.tabListView.Location = new System.Drawing.Point(4, 22);
            this.tabListView.Name = "tabListView";
            this.tabListView.Padding = new System.Windows.Forms.Padding(3);
            this.tabListView.Size = new System.Drawing.Size(494, 494);
            this.tabListView.TabIndex = 0;
            this.tabListView.Text = "Current";
            this.tabListView.UseVisualStyleBackColor = true;
            // 
            // tabGraphs
            // 
            this.tabGraphs.Location = new System.Drawing.Point(4, 22);
            this.tabGraphs.Name = "tabGraphs";
            this.tabGraphs.Padding = new System.Windows.Forms.Padding(3);
            this.tabGraphs.Size = new System.Drawing.Size(494, 494);
            this.tabGraphs.TabIndex = 1;
            this.tabGraphs.Text = "Graph";
            this.tabGraphs.UseVisualStyleBackColor = true;
            // 
            // splitContainerData
            // 
            this.splitContainerData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerData.Location = new System.Drawing.Point(0, 0);
            this.splitContainerData.Name = "splitContainerData";
            // 
            // splitContainerData.Panel1
            // 
            this.splitContainerData.Panel1.Controls.Add(this.tabControlData);
            // 
            // splitContainerData.Panel2
            // 
            this.splitContainerData.Panel2.Controls.Add(this.treeViewPids);
            this.splitContainerData.Size = new System.Drawing.Size(840, 520);
            this.splitContainerData.SplitterDistance = 500;
            this.splitContainerData.SplitterWidth = 8;
            this.splitContainerData.TabIndex = 4;
            this.splitContainerData.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainerData_Paint);
            // 
            // treeViewPids
            // 
            this.treeViewPids.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewPids.CheckBoxes = true;
            this.treeViewPids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewPids.Location = new System.Drawing.Point(0, 0);
            this.treeViewPids.Name = "treeViewPids";
            this.treeViewPids.Size = new System.Drawing.Size(332, 520);
            this.treeViewPids.TabIndex = 0;
            this.treeViewPids.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.pidsTreeView_AfterCheck);
            this.treeViewPids.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewPids_MouseClick);
            // 
            // timerUpdateListView
            // 
            this.timerUpdateListView.Interval = 1000;
            this.timerUpdateListView.Tick += new System.EventHandler(this.timerUpdateListView_Tick);
            // 
            // contextMenuStripTreeViewRightClick
            // 
            this.contextMenuStripTreeViewRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPIDToolStripMenuItem});
            this.contextMenuStripTreeViewRightClick.Name = "contextMenuStripTreeViewRightClick";
            this.contextMenuStripTreeViewRightClick.Size = new System.Drawing.Size(153, 48);
            // 
            // editPIDToolStripMenuItem
            // 
            this.editPIDToolStripMenuItem.Name = "editPIDToolStripMenuItem";
            this.editPIDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editPIDToolStripMenuItem.Text = "Edit PID";
            this.editPIDToolStripMenuItem.Click += new System.EventHandler(this.editPIDToolStripMenuItem_Click);
            // 
            // DataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerData);
            this.DoubleBuffered = true;
            this.Name = "DataPanel";
            this.Size = new System.Drawing.Size(840, 520);
            this.Load += new System.EventHandler(this.DataPanel_Load);
            this.tabControlData.ResumeLayout(false);
            this.tabListView.ResumeLayout(false);
            this.splitContainerData.Panel1.ResumeLayout(false);
            this.splitContainerData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerData)).EndInit();
            this.splitContainerData.ResumeLayout(false);
            this.contextMenuStripTreeViewRightClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewRealTimeValues;
        private System.Windows.Forms.TabControl tabControlData;
        private System.Windows.Forms.TabPage tabListView;
        private System.Windows.Forms.TabPage tabGraphs;
        private System.Windows.Forms.ColumnHeader reserved0;
        private System.Windows.Forms.ColumnHeader columnParameter;
        private System.Windows.Forms.ColumnHeader columnValue;
        private System.Windows.Forms.ColumnHeader columnUnits;
        private System.Windows.Forms.SplitContainer splitContainerData;
        private System.Windows.Forms.TreeView treeViewPids;
        private System.Windows.Forms.Timer timerUpdateListView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeViewRightClick;
        private System.Windows.Forms.ToolStripMenuItem editPIDToolStripMenuItem;
    }
}
