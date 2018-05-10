using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using link.ui;

namespace link
{
    public partial class Main : Form
    {
        private void InitializeCustomComponent()
        {
            // set the correct parents
            Globals.homePanel.Parent = dockPanel;
            Globals.troubleCodePanel.Parent = dockPanel;
            Globals.dataPanel.Parent = dockPanel;
            Globals.settingsPanel.Parent = dockPanel;

            // set the correct docking
            Globals.homePanel.Dock = DockStyle.Fill;
            Globals.troubleCodePanel.Dock = DockStyle.Fill;
            Globals.dataPanel.Dock = DockStyle.Fill;
            Globals.settingsPanel.Dock = DockStyle.Fill;

            // set the tags for each button
            buttonHome.Tag = Globals.homePanel;
            buttonDtc.Tag = Globals.troubleCodePanel;
            buttonData.Tag = Globals.dataPanel;
            buttonSettings.Tag = Globals.settingsPanel;
        }

        public Main()
        {
            InitializeComponent();
            InitializeCustomComponent();

            // the home button is selected by default
            buttonHome.BackColor = Color.LightBlue;
        }

        private void UpdateButtonSelection(Button button)
        {
            // reset 
            buttonHome.BackColor = Color.White;
            buttonDtc.BackColor = Color.White;
            buttonData.BackColor = Color.White;
            buttonSettings.BackColor = Color.White;

            // set selected button
            button.BackColor = Color.LightBlue;
        }

        private void UpdatePanelSelection(UserControl control)
        {
            // hide all
            Globals.homePanel.Hide();
            Globals.troubleCodePanel.Hide();
            Globals.dataPanel.Hide();
            Globals.settingsPanel.Hide();

            // show the given one
            control.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "Link - v" + Application.ProductVersion;
            
            if (Properties.Settings.Default.SaveSize)
            {
                this.Size = Properties.Settings.Default.Size;
            }

            if (Properties.Settings.Default.SaveLocation)
            {
                this.Location = Properties.Settings.Default.Location;
            }

            // get names of specific files
            string dataFolder = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "data");
            string pidsFile = System.IO.Path.Combine(dataFolder, "pids.xml");
            string dtcsFile = System.IO.Path.Combine(dataFolder, "dtcs.ini");
            string makesFile = System.IO.Path.Combine(dataFolder, "makes.ini");

            Globals.pids.LoadPIDs(pidsFile); // load the pids first
            Globals.dtcs = OBD2.ParameterIdentificationCollection.LoadDtcDescriptions(dtcsFile);

            // load the available makes
            Globals.makes = new Utilities.IniFile(makesFile);
            Globals.makes.Read();

            Globals.dataPanel.SetTreeViewPids(); // update the tree view and set the check states
            Globals.dataPanel.SetListViewPids(); // the list view needs to be updated after pids are loaded and tree view set
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.dataPanel.StopCommunication();
            
            if (Properties.Settings.Default.SaveSize)
            {
                Properties.Settings.Default.Size = this.Size;
            }

            if (Properties.Settings.Default.SaveLocation)
            {
                Properties.Settings.Default.Location = this.Location;
            }

            Properties.Settings.Default.Save();
        }

        private void navigationButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                UpdateButtonSelection(button);
                UpdatePanelSelection(button.Tag as UserControl);
            }
        }
    }
}
