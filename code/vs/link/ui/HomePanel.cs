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
    public partial class HomePanel : UserControl
    {
        public HomePanel()
        {
            InitializeComponent();
        }

        private void UpdateConnectionStatus(int step, OBD2.Cables.Elm327Cable.CableInfo info)
        {
            switch (step)
            {
                case 1:
                    {
                        buttonConnect.Image = Properties.Resources.Connecting1;
                    }
                    break;

                case 2:
                    {
                        buttonConnect.Image = Properties.Resources.Connecting2;
                        // TODO: display info
                    }
                    break;

                case 3:
                    {
                        buttonConnect.Image = Properties.Resources.Connecting3;
                        // TODO: display info
                    }
                    break;

                case 4:
                    {
                        buttonConnect.Image = Properties.Resources.Connected;
                        // TODO: display all set label
                    }
                    break;

                default:
                    {
                        buttonConnect.Image = Properties.Resources.NotConnected;
                        // default to not connected
                    }
                    break;
            }

            labelStatus.Text = info.Description;
            labelChosenProtocol.Text = info.Protocol.ToString();
            labelElmVersion.Text = info.Version;

            if (info.AutoProtocolSet)
            {
                labelAutoProtocol.Text = "ON";
                labelAutoProtocol.ForeColor = Color.Green;
                labelAutoProtocol.Font = new Font(labelEcho.Font, FontStyle.Bold);
            }
            else
            {
                labelAutoProtocol.Text = "OFF";
                labelAutoProtocol.ForeColor = Color.Red;
                labelAutoProtocol.Font = new Font(labelEcho.Font, FontStyle.Bold);
            }

            if (info.EchoOff)
            {
                labelEcho.Text = "OFF";
                labelEcho.ForeColor = Color.Green;
                labelEcho.Font = new Font(labelEcho.Font, FontStyle.Bold);
            }
            else
            {
                labelEcho.Text = "ON";
                labelEcho.ForeColor = Color.Red;
                labelEcho.Font = new Font(labelEcho.Font, FontStyle.Bold);
            }

            // force an immediate refresh of the screen for the user
            labelStatus.Refresh(); Application.DoEvents();
            labelEcho.Refresh(); Application.DoEvents();
            labelAutoProtocol.Refresh(); Application.DoEvents();
            labelChosenProtocol.Refresh(); Application.DoEvents();
            labelElmVersion.Refresh(); Application.DoEvents();
            buttonConnect.Refresh(); Application.DoEvents();
        }

        private void SetupConnectState()
        {
            labelStatus.Text
                = labelEcho.Text
                = labelAutoProtocol.Text 
                = labelChosenProtocol.Text
                = labelElmVersion.Text
                = string.Empty;

            labelStatus.Refresh(); Application.DoEvents();
            labelEcho.Refresh(); Application.DoEvents();
            labelAutoProtocol.Refresh(); Application.DoEvents();
            labelChosenProtocol.Refresh(); Application.DoEvents();
            labelElmVersion.Refresh(); Application.DoEvents();

            buttonConnect.Text = "Connect";
            buttonConnect.Click += buttonConnect_Click;
            buttonConnect.Image = Properties.Resources.NotConnected;
        }

        public void SetupVehicleInfo()
        {
            if (Globals.vehicle != null)
            {
                labelVin.Text = Globals.vehicle.VIN;
                labelMake.Text = Globals.vehicle.Manufacturer;
                labelYear.Text = Globals.vehicle.Year.ToString();
                // TODO: fill in the rest of the known vehicle info
            }
            else
            {
                labelVin.Text = "...";
                labelMake.Text = "...";
                labelYear.Text = "...";
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            buttonConnect.Click -= buttonConnect_Click;
            buttonConnect.Text = "Connecting...";

            if (Properties.Settings.Default.SimulatedCable)
            {
                Globals.cable = new OBD2.Cables.Elm327CableSimulator("COM16", 3000, UpdateConnectionStatus);
            }
            else
            {
                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    try
                    {
                        Globals.cable = new OBD2.Cables.Elm327Cable(port, 3000, UpdateConnectionStatus);
                        if (Globals.cable.IsInitialized)
                        {
                            break;
                        }

                        Globals.cable.Close();
                        Globals.cable.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Diagnostics.DiagnosticLogger.Log("Attempting to open cable " + port + " resulted in an exception", ex);
                    }
                }
            }

            if (Globals.cable.IsInitialized &&
                Globals.cable.IsOpen)
            {
                // regardless of the cable, set the trouble code descriptions loaded from file
                Globals.cable.TroubleCodeDescriptions = Globals.dtcs;

                // connected, allow user to disconnect
                buttonConnect.Text = "Disconnect";
                buttonConnect.Click += buttonDisconnect_Click;

                // get the vehicle information before starting normal communication
                string vin = Globals.cable.RequestVIN();
                Globals.vehicle = new OBD2.Vehicle.Vehicle(vin, Globals.makes);

                // the worker for the data panel can start
                Globals.dataPanel.RestartCommunication();

                // enable the dtc panel user interface
                Globals.troubleCodePanel.EnableUserInterface = true;
            }
            else
            {
                MessageBox.Show("Could not fully initialize the cable communications. Try unplugging it, and plugging it back in to both the vehicle and computer.",
                    "Cable Connection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                SetupConnectState();
            }

            SetupVehicleInfo();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (Globals.cable.IsInitialized ||
                Globals.cable.IsOpen)
            {
                buttonConnect.Click -= buttonDisconnect_Click;
                buttonConnect.Text = "Disconnecting...";

                // disable the dtc panel user interface
                Globals.troubleCodePanel.EnableUserInterface = false;

                Globals.dataPanel.StopCommunication();
                System.Threading.Thread.Sleep(500); // give the data panel some time to close its worker

                Globals.cable.Close();
            }

            SetupConnectState(); // setup the button to be ready to connect no matter what
            SetupVehicleInfo();
        }

        private void buttonCarInfo_Click(object sender, EventArgs e)
        {
            DialogResult userChoice = MessageBox.Show("This feature is for your own benefit to print, import to other applications, or work with outside of this application. Knowing this, do you still want to save vehicle info?",
                "Vehicle Info",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (userChoice == DialogResult.Yes)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.FileName = "VehicleInfo.vin";
                saveDialog.Filter = "Vehicle Info (*.vin)|*.vin|All Files (*.*)|*.*";
                saveDialog.OverwritePrompt = true;

                userChoice = saveDialog.ShowDialog();
                if (userChoice == DialogResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllText(saveDialog.FileName, Globals.vehicle.ToString());
                    }
                    catch (Exception ex)
                    {
                        Diagnostics.DiagnosticLogger.Log("Could not save vehicle info", ex);
                        MessageBox.Show("Could not save vehicle information!\r\n\r\n" + ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void textBoxModel_TextChanged(object sender, EventArgs e)
        {
            // no need to check for anything, all strings are valid for the model
            Globals.vehicle.Model = textBoxModel.Text;
        }
    }
}
