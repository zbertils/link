using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utilities
{
    public enum ListViewExtendedStyles
    {
        /// <summary>
        /// LVS_EX_GRIDLINES
        /// </summary>
        GridLines = 0x00000001,
        /// <summary>
        /// LVS_EX_SUBITEMIMAGES
        /// </summary>
        SubItemImages = 0x00000002,
        /// <summary>
        /// LVS_EX_CHECKBOXES
        /// </summary>
        CheckBoxes = 0x00000004,
        /// <summary>
        /// LVS_EX_TRACKSELECT
        /// </summary>
        TrackSelect = 0x00000008,
        /// <summary>
        /// LVS_EX_HEADERDRAGDROP
        /// </summary>
        HeaderDragDrop = 0x00000010,
        /// <summary>
        /// LVS_EX_FULLROWSELECT
        /// </summary>
        FullRowSelect = 0x00000020,
        /// <summary>
        /// LVS_EX_ONECLICKACTIVATE
        /// </summary>
        OneClickActivate = 0x00000040,
        /// <summary>
        /// LVS_EX_TWOCLICKACTIVATE
        /// </summary>
        TwoClickActivate = 0x00000080,
        /// <summary>
        /// LVS_EX_FLATSB
        /// </summary>
        FlatsB = 0x00000100,
        /// <summary>
        /// LVS_EX_REGIONAL
        /// </summary>
        Regional = 0x00000200,
        /// <summary>
        /// LVS_EX_INFOTIP
        /// </summary>
        InfoTip = 0x00000400,
        /// <summary>
        /// LVS_EX_UNDERLINEHOT
        /// </summary>
        UnderlineHot = 0x00000800,
        /// <summary>
        /// LVS_EX_UNDERLINECOLD
        /// </summary>
        UnderlineCold = 0x00001000,
        /// <summary>
        /// LVS_EX_MULTIWORKAREAS
        /// </summary>
        MultilWorkAreas = 0x00002000,
        /// <summary>
        /// LVS_EX_LABELTIP
        /// </summary>
        LabelTip = 0x00004000,
        /// <summary>
        /// LVS_EX_BORDERSELECT
        /// </summary>
        BorderSelect = 0x00008000,
        /// <summary>
        /// LVS_EX_DOUBLEBUFFER
        /// </summary>
        DoubleBuffer = 0x00010000,
        /// <summary>
        /// LVS_EX_HIDELABELS
        /// </summary>
        HideLabels = 0x00020000,
        /// <summary>
        /// LVS_EX_SINGLEROW
        /// </summary>
        SingleRow = 0x00040000,
        /// <summary>
        /// LVS_EX_SNAPTOGRID
        /// </summary>
        SnapToGrid = 0x00080000,
        /// <summary>
        /// LVS_EX_SIMPLESELECT
        /// </summary>
        SimpleSelect = 0x00100000
    }

    public enum ListViewMessages
    {
        First = 0x1000,
        SetExtendedStyle = (First + 54),
        GetExtendedStyle = (First + 55),
    }

    /// <summary>
    /// Contains helper methods to change extended styles on ListView, including enabling double buffering.
    /// </summary>
    /// <remarks>
    /// Based on Giovanni Montrone's article on <see cref="http://www.codeproject.com/KB/list/listviewxp.aspx"/>
    /// </remarks>
    public static class ListViewHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr handle, int msg, int wparam, int lparam);


        /// <summary>
        /// Sets the extended style for a given control.
        /// </summary>
        /// <param name="control"> The control to set the extended style for. </param>
        /// <param name="exStyle"> The extended style to set. </param>
        public static void SetExtendedStyle(Control control, ListViewExtendedStyles exStyle)
        {
            ListViewExtendedStyles styles;
            styles = (ListViewExtendedStyles)SendMessage(control.Handle, (int)ListViewMessages.GetExtendedStyle, 0, 0);
            styles |= exStyle;
            SendMessage(control.Handle, (int)ListViewMessages.SetExtendedStyle, 0, (int)styles);
        }


        /// <summary>
        /// Enables double buffering for a given control.
        /// </summary>
        /// <param name="control"> The control to enable double buffer for. </param>
        public static void EnableDoubleBuffer(Control control)
        {
            ListViewExtendedStyles styles;
            styles = (ListViewExtendedStyles)SendMessage(control.Handle, (int)ListViewMessages.GetExtendedStyle, 0, 0); // read current style
            styles |= ListViewExtendedStyles.DoubleBuffer | ListViewExtendedStyles.BorderSelect; // enable double buffer and border select
            SendMessage(control.Handle, (int)ListViewMessages.SetExtendedStyle, 0, (int)styles); // write new style
        }


        /// <summary>
        /// Disables double buffering for a given control.
        /// </summary>
        /// <param name="control"> The control to disable the double buffer for. </param>
        public static void DisableDoubleBuffer(Control control)
        {
            ListViewExtendedStyles styles;
            styles = (ListViewExtendedStyles)SendMessage(control.Handle, (int)ListViewMessages.GetExtendedStyle, 0, 0); // read current style
            styles -= styles & ListViewExtendedStyles.DoubleBuffer; // disable double buffer and border select
            styles -= styles & ListViewExtendedStyles.BorderSelect; // write new style
            SendMessage(control.Handle, (int)ListViewMessages.SetExtendedStyle, 0, (int)styles);
        }
    }
}
