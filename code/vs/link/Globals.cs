using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using link.ui;
using OBD2;
using Utilities;

namespace link
{
    public static class Globals
    {
        // make sure these are not null, they can be empty but not null
        public static ParameterIdentificationCollection pids = new ParameterIdentificationCollection();
        public static List<Utilities.IniFileEntry> dtcs = new List<Utilities.IniFileEntry>();
        public static IniFile makes = null;

        public static HomePanel homePanel = new HomePanel();
        public static TroubleCodePanel troubleCodePanel = new TroubleCodePanel();
        public static DataPanel dataPanel = new DataPanel();
        public static SettingsPanel settingsPanel = new SettingsPanel();

        public static OBD2.Cables.Cable cable = null;

        public static ListViewGroup realTimeListViewUnknownGroup = new ListViewGroup() { Header = "Other", Name = "listViewGroupUnknown" };
        public static List<ListViewGroup> realTimeListViewGroups = new List<ListViewGroup>();
        public static List<ListViewItem> realTimeListViewItems = new List<ListViewItem>();

        public static OBD2.Vehicle.Vehicle vehicle = null;

    }
}
