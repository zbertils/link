using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace link.ui
{
    public partial class TroubleCodePanel : UserControl
    {

        public TroubleCodePanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets if the user interface is enabled.
        /// </summary>
        public bool EnableUserInterface
        {
            get 
            {
                return _enableUserInterface;
            }
            set
            {
                _enableUserInterface = value;
                buttonCheck.Enabled
                    = buttonClear.Enabled
                    = _enableUserInterface;
            }
        }
        private bool _enableUserInterface = false;

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            listViewDtc.Items.Clear();
            listViewCodeStatuses.Items.Clear();

            if (Globals.cable != null &&
                Globals.cable.IsInitialized &&
                Globals.cable.IsOpen)
            {
                // no communications can be happening when asking for trouble codes
                Globals.dataPanel.StopCommunication();

                // get active and pending trouble codes as reported by the computer
                List<OBD2.DiagnosticTroubleCode> troubleCodes = Globals.cable.RequestTroubleCodes();
                if (troubleCodes != null)
                {
                    if (troubleCodes.Count > 0)
                    {
                        foreach (OBD2.DiagnosticTroubleCode code in troubleCodes)
                        {
                            ListViewItem newTroubleCode = new ListViewItem(code.Code);
                            newTroubleCode.SubItems.Add(code.Type.ToString());
                            newTroubleCode.SubItems.Add(code.Description);

                            // add the item to the list displayed
                            listViewDtc.Items.Add(newTroubleCode);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No trouble codes present!", "DTC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Looks like communication with the vehicle didn't work as expected.", "No DTCs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // get the status for trouble codes as well
                List<Tuple<OBD2.DiagnosticTroubleCode, string>> codeStatuses = Globals.cable.RequestAllDtcStatuses();
                if (codeStatuses != null && codeStatuses.Count > 0)
                {
                    foreach (Tuple<OBD2.DiagnosticTroubleCode, string> code in codeStatuses)
                    {
                        ListViewItem newTroubleCode = new ListViewItem(code.Item1.Code);
                        newTroubleCode.SubItems.Add(code.Item2);

                        // add the item to the list displayed
                        listViewCodeStatuses.Items.Add(newTroubleCode);
                    }
                }
                else
                {
                    Diagnostics.DiagnosticLogger.Log("Could not get trouble code statuses");
                }

                // restart the data communications
                Globals.dataPanel.StartCommunication();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (Globals.cable != null &&
                Globals.cable.IsInitialized &&
                Globals.cable.IsOpen)
            {
               DialogResult userChoice = MessageBox.Show("In order to clear trouble codes, the vehicle should be on but not running.\n\nPlease put the key in the ON position with the engine OFF. You may need to reconnect if the engine was already running.\n\nPress OK to continue when ready.",
                "Ready?",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);

                if (userChoice == DialogResult.OK)
                {
                    // no communications can be happening when asking for trouble codes
                    Globals.dataPanel.StopCommunication();

                    Globals.cable.ClearTroubleCodes();

                    // restart the data communications
                    Globals.dataPanel.StartCommunication();
                }
            }
            else
            {
                MessageBox.Show("Please connect the cable before attempting to clear trouble codes!", "Connect First", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
