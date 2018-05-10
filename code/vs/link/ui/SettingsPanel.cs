using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace link.ui
{
    public partial class SettingsPanel : UserControl
    {
        public SettingsPanel()
        {
            InitializeComponent();
        }

        private void SettingsPanel_Load(object sender, EventArgs e)
        {
            checkBoxSaveSize.Checked = Properties.Settings.Default.SaveSize;
            checkBoxSaveLocation.Checked = Properties.Settings.Default.SaveLocation;
            checkBoxSimulated.Checked = Properties.Settings.Default.SimulatedCable;
            checkBoxLog.Checked = Properties.Settings.Default.LogToFile;
            textBoxLogTo.Text = Properties.Settings.Default.LogToDirectory;
            comboBoxProtocol.SelectedIndex = comboBoxProtocol.Items.IndexOf(Properties.Settings.Default.SimulatedProtocol);
        }

        private void checkBoxSaveSize_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveSize = checkBoxSaveSize.Checked;
        }

        private void checkBoxSaveLocation_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveLocation = checkBoxSaveLocation.Checked;
        }

        private void checkBoxSimulated_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SimulatedCable = checkBoxSimulated.Checked;
        }

        private void checkBoxLog_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // if starting logging then make sure the path is valid and save it to the settings variable
                if (checkBoxLog.Checked)
                {
                    string fullPath = Path.GetFullPath(textBoxLogTo.Text);

                    // should throw an exception before getting here if the path is improperly formatted
                    Properties.Settings.Default.LogToDirectory = fullPath;
                }

                Properties.Settings.Default.LogToFile = checkBoxLog.Checked;

                // only restart if already running
                if (Globals.dataPanel.CommunicationActive)
                {
                    Globals.dataPanel.RestartCommunication();
                }
            }
            catch
            {
                MessageBox.Show("The chosen path for placing log files is not valid. Please choose a valid path and try enabling again.",
                    "Invalid Path",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void textBoxSaveLogsTo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string fullPath = Path.GetFullPath(textBoxLogTo.Text);

                // should throw an exception before getting here if the path is improperly formatted
                Properties.Settings.Default.LogToDirectory = fullPath;
            }
            catch
            {
                MessageBox.Show("The chosen path for placing log files is not valid. Please choose a valid path and try enabling again.",
                    "Invalid Path",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void comboBoxProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SimulatedProtocol = comboBoxProtocol.SelectedItem.ToString();
        }
    }
}
