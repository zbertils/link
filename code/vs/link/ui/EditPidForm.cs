using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace link.ui
{
    public partial class EditPidForm : Form
    {
        public EditPidForm()
        {
            InitializeComponent();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void EditPidForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
