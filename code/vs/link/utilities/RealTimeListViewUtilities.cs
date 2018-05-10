using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utilities
{
    public static class RealTimeListViewUtilities
    {

        /// <summary>
        /// Enumerated values representing columns in the real time list view.
        /// </summary>
        public enum Columns : int
        {
            Reserved0 = 0,
            ParameterName = 1,
            Value = 2,
            Unit = 3
        }


        /// <summary>
        /// Enumerated groups that each 
        /// </summary>
        public enum Groups : int
        {
            Engine = 0,
            Transmission = 1,
            Body = 2
        }
        

        /// <summary>
        /// The column widths in percent for the real time list view.
        /// </summary>
        public static double[] ColumnWidthPercentages = new double[] { 0.0, 0.60, 0.15, 0.20 };


        /// <summary>
        /// Adds a given pid to the list view items.
        /// </summary>
        /// <param name="listview"> The list view to add the pid to. </param>
        /// <param name="pid"> The given pid to add to the list view. </param>
        /// <param name="group"> The group the pid belongs to. </param>
        /// <returns> The item added to the group and list view items. </returns>
        public static ListViewItem AddPid(this ListView listview, OBD2.ParameterIdentification pid, ListViewGroup group)
        {
            if (pid != null && group != null)
            {
                // add the newly checked pid to the real time list view
                ListViewItem item = new ListViewItem();
                item.Name = item.Text = pid.Name;
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, pid.Name));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, "NaN"));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, pid.Units));
                item.Group = group;
                listview.Items.Add(item);
                
                return item;
            }

            return null;
        }

        /// <summary>
        /// Removes a given pid from the list view items.
        /// </summary>
        /// <param name="listview"> The ListView object to remove the pid from. </param>
        /// <param name="pid"> The pid to remove, if in the ListView.Items list. </param>
        public static ListViewItem RemovePid(this ListView listview, OBD2.ParameterIdentification pid)
        {
            for (int i = listview.Items.Count - 1; i >= 0; --i)
            {
                if (listview.Items[i].Name == pid.Name)
                {
                    ListViewItem removedItem = listview.Items[i];
                    listview.Items.RemoveAt(i);
                    return removedItem;
                }
            }

            return null;
        }

    }
}
