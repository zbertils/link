using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;

using Utilities;
using OBD2;

namespace link.ui
{
    public partial class DataPanel : UserControl
    {
        private BlockingCollection<Tuple<double, ParameterIdentification>> decodedValuesQueue;
        private UpdatePidsWorker communication;
        private SavePidsWorker logger;

        public DataPanel()
        {
            InitializeComponent();
            ListViewHelper.EnableDoubleBuffer(listViewRealTimeValues);

            decodedValuesQueue = new BlockingCollection<Tuple<double, ParameterIdentification>>();
            communication = new UpdatePidsWorker(null, null, decodedValuesQueue);
            logger = new SavePidsWorker(decodedValuesQueue);
        }

        /// <summary>
        /// Starts communications with the vehicle.
        /// </summary>
        public void StartCommunication()
        {
            if (!communication.IsAlive)
            {
                communication.SetCable(Globals.cable);
                communication.SetPids(Globals.pids);
                communication.Start();

                timerUpdateListView.Start();
            }

            if (!logger.IsAlive)
            {
                // set the pids for the logger and start if the user set the settings accordingly
                if (Properties.Settings.Default.LogToFile)
                {
                    logger.Start();
                }
            }
        }

        /// <summary>
        /// Stops communication with the vehicle.
        /// </summary>
        public void StopCommunication()
        {
            if (communication.IsAlive)
            {
                communication.Stop();
                communication.Join();
                timerUpdateListView.Stop();
            }

            if (logger.IsAlive)
            {
                logger.Stop();
                logger.Join();
            }
        }

        /// <summary>
        /// Restarts communication with the vehicle. This does not disconnect the cable.
        /// </summary>
        public void RestartCommunication()
        {
            StopCommunication();
            StartCommunication();
        }

        public bool CommunicationActive { get { return communication.IsAlive; } }

        /// <summary>
        /// Determines if all nodes are checked for a given tree node collection.
        /// </summary>
        /// <param name="treeViewNodes"> The level of tree nodes to begin the checking from. </param>
        /// <param name="checkedNodeCount"> The number of nodes that are checked. </param>
        /// <returns> True if all the nodes and child nodes are checked and false otherwise. </returns>
        private bool AllNodesChecked(TreeNodeCollection treeViewNodes, ref int checkedNodeCount)
        {
            bool allChecked = true; // by default all nodes are assumed checked

            foreach (TreeNode node in treeViewNodes)
            {
                // if this node is checked then increase checkedCount
                if (node.Checked)
                {
                    checkedNodeCount++;
                }
                else
                {
                    allChecked = false;
                }

                // check all child nodes for checked state
                allChecked = AllNodesChecked(node.Nodes, ref checkedNodeCount) && allChecked;
            }

            return allChecked;
        }

        /// <summary>
        /// Sets the check state of all child notes to true.
        /// </summary>
        /// <param name="parentNode"> The parent node that contains the child nodes to check. </param>
        /// <param name="nodeChecked"> True if the child nodes should be checked and false otherwise. </param>
        private void CheckAllChildNodes(TreeNode parentNode, bool nodeChecked)
        {
            // if there are no children then just return
            if (parentNode.Nodes.Count == 0)
            {
                return;
            }

            foreach (TreeNode node in parentNode.Nodes)
            {
                node.Checked = nodeChecked;
                ParameterIdentification pid = node.Tag as ParameterIdentification;
                if (pid != null)
                {
                    pid.LogThisPID = nodeChecked;
                    if (node.Checked)
                    {
                        ListViewGroup group = Globals.realTimeListViewGroups.Find(g => g.Header == pid.Group) ?? Globals.realTimeListViewUnknownGroup;
                        ListViewItem addedItem = Globals.dataPanel.listViewRealTimeValues.AddPid(pid, group);
                        Globals.realTimeListViewItems.Add(addedItem);
                    }
                    else
                    {
                        ListViewItem removedItem = Globals.dataPanel.listViewRealTimeValues.RemovePid(pid);
                        Globals.realTimeListViewItems.Remove(removedItem);
                    }
                }
                else
                {
                    Diagnostics.DiagnosticLogger.Log("Node has a null tag value, " + node.Text);
                }

                if (node.Nodes.Count > 0)
                {
                    // if the current node has child nodes, call the CheckAllChildNodes method recursively
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        /// <summary>
        /// Checks if a parent of the given node should be checked or unchecked based on the child nodes siblings check state.
        /// </summary>
        /// <param name="childNode"> The child node of the parent node to be checked or unchecked. </param>
        private void CheckParentNode(TreeNode childNode)
        {
            // if the node has no parent then just return
            if (childNode.Parent == null)
            {
                return;
            }

            bool hasCheckedChildren = false;

            // go through each sibling node of the current child node,
            // if any of them are checked then the parent node must stay checked,
            // if none of them are checked then the parent must be unchecked
            foreach (TreeNode node in childNode.Parent.Nodes)
            {
                if (node.Checked)
                {
                    hasCheckedChildren = true;
                    break;
                }
            }

            childNode.Parent.Checked = hasCheckedChildren;
        }

        /// <summary>
        /// Updates the available PID tree view control.
        /// </summary>
        /// <remarks> This is expected to be called on start and when the pids are updated. </remarks>
        public void SetTreeViewPids()
        {
            treeViewPids.Nodes.Clear(); // remove any previous nodes
            foreach (ParameterIdentification pid in Globals.pids)
            {
                TreeNode[] nodes = treeViewPids.Nodes.Find(pid.PidType, false);
                TreeNode node = null;

                // if the parent nodes do not exist, or there is no length to the array
                if (nodes == null || nodes.Length == 0)
                {
                    node = new TreeNode();
                    node.Name = pid.PidType.ToString();
                    node.Text = pid.PidType.ToString();
                    treeViewPids.Nodes.Add(node);
                }

                // the parent node exists then grab the first node in the array
                else
                {
                    node = nodes[0];
                }

                // add the pid node to the mode tree node
                TreeNode newPidNode = new TreeNode();
                newPidNode.Text = pid.Name;
                newPidNode.Checked = pid.LogThisPID;
                newPidNode.ToolTipText = pid.Description;
                newPidNode.Tag = pid;
                node.Nodes.Add(newPidNode);

                // if this pid is suppose to be logged then check the parent node as well
                if (pid.LogThisPID)
                {
                    node.Checked = true;
                }
            }
        }

        /// <summary>
        /// Updates the list view with pids that are to be displayed.
        /// </summary>
        /// <remarks> This is expected to be called at start and when the pids are updated. </remarks>
        public void SetListViewPids()
        {
            foreach (ParameterIdentification pid in Globals.pids)
            {
                if (pid.LogThisPID)
                {
                    ListViewGroup group = Globals.realTimeListViewGroups.Find(g => g.Header == pid.Group) ?? Globals.realTimeListViewUnknownGroup;
                    ListViewItem addedItem = Globals.dataPanel.listViewRealTimeValues.AddPid(pid, group);
                    Globals.realTimeListViewItems.Add(addedItem);
                }
            }
        }

        private void pidsTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // the code only executes if the user caused the checked state to change. 
            if (e.Action != TreeViewAction.Unknown)
            {
                // pid node
                if (e.Node.Tag != null)
                {
                    // check or uncheck the parent node depending on how many children nodes are selected
                    CheckParentNode(e.Node);

                    // enable the pid that was checked
                    ParameterIdentification pid = e.Node.Tag as ParameterIdentification;
                    if (pid != null)
                    {
                        if (e.Node.Checked)
                        {
                            ListViewGroup group = Globals.realTimeListViewGroups.Find(g => g.Header == pid.Group) ?? Globals.realTimeListViewUnknownGroup;
                            ListViewItem addedItem = Globals.dataPanel.listViewRealTimeValues.AddPid(pid, group);
                            Globals.realTimeListViewItems.Add(addedItem);
                        }
                        else
                        {
                            ListViewItem removedItem = Globals.dataPanel.listViewRealTimeValues.RemovePid(pid);
                            Globals.realTimeListViewItems.Remove(removedItem);
                        }

                        // do this last, the update worker looks at this value as does the list view update timer
                        pid.LogThisPID = e.Node.Checked;
                    }
                    else
                    {
                        MessageBox.Show("Oh man, this is embarrassing. The selected PID doesn't exist in memory!",
                            "Invalid PID",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                // parent pid type node, programmatically check all children nodes
                else
                {
                    // check all children nodes if a root node was selected
                    CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        // do special painting to get the splitter grab handle visible
        private void splitContainerData_Paint(object sender, PaintEventArgs e)
        {
            var control = sender as SplitContainer;
            //paint the three dots'
            Point[] points = new Point[3];
            var w = control.Width;
            var h = control.Height;
            var d = control.SplitterDistance;
            var sW = control.SplitterWidth;

            //calculate the position of the points'
            if (control.Orientation == Orientation.Horizontal)
            {
                points[0] = new Point((w / 2), d + (sW / 2));
                points[1] = new Point(points[0].X - 10, points[0].Y);
                points[2] = new Point(points[0].X + 10, points[0].Y);
            }
            else
            {
                points[0] = new Point(d + (sW / 2), (h / 2));
                points[1] = new Point(points[0].X, points[0].Y - 10);
                points[2] = new Point(points[0].X, points[0].Y + 10);
            }

            foreach (Point p in points)
            {
                p.Offset(-2, -2);
                e.Graphics.FillEllipse(SystemBrushes.ControlDark, new Rectangle(p, new Size(3, 3)));

                p.Offset(1, 1);
                e.Graphics.FillEllipse(SystemBrushes.ControlLight, new Rectangle(p, new Size(3, 3)));
            }
        }

        private void DataPanel_Load(object sender, EventArgs e)
        {
            // gather all of the groups from the real time list view to a list
            foreach (ListViewGroup group in Globals.dataPanel.listViewRealTimeValues.Groups)
            {
                Globals.realTimeListViewGroups.Add(group);
            }

            // always add a hard coded unknown group for new pids
            Globals.dataPanel.listViewRealTimeValues.Groups.Add(Globals.realTimeListViewUnknownGroup);
            Globals.realTimeListViewGroups.Add(Globals.realTimeListViewUnknownGroup);
        }

        private void timerUpdateListView_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < Globals.pids.Count; i++)
            {
                ParameterIdentification pid = Globals.pids[i];
                if (pid.LogThisPID)
                {
                    ListViewItem item = Globals.realTimeListViewItems.Find(p => p.SubItems[(int)RealTimeListViewUtilities.Columns.ParameterName].Text == pid.Name);
                    if (pid != null && item != null)
                    {
                        item.SubItems[(int)RealTimeListViewUtilities.Columns.Value].Text = pid.LastDecodedValue.ToString("0.0");
                    }
                }
            }

            listViewRealTimeValues.Invalidate();
        }

        private void treeViewPids_MouseClick(object sender, MouseEventArgs e)
        {
            // if right clicked on a tree view node
            if (e.Button == MouseButtons.Right)
            {
                // the ability to edit pids should only be allowed if the cable is not initialized and data being collected
                editPIDToolStripMenuItem.Enabled = Globals.cable == null || !Globals.cable.IsInitialized;

                // show the context menu so users know they can or cannot edit pids
                contextMenuStripTreeViewRightClick.Show(treeViewPids.PointToScreen(e.Location));
            }
        }

        private void editPIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewPids.SelectedNode != null)
            {
                TreeNode selectedNode = treeViewPids.SelectedNode;
                EditPidForm editPid = new EditPidForm();
                DialogResult result = editPid.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // TODO: save the updated xml of pid data
                    MessageBox.Show("User pressed OK");
                }
                else
                {
                    MessageBox.Show("User pressed CANCEL");
                }
            }
        }
    }
}
