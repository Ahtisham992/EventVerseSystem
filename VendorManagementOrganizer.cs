﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventVerse
{
    public partial class VendorManagementOrganizer : Form
    {
        private Form prev;
        public VendorManagementOrganizer(Form prev)
        {
            InitializeComponent();
            this.prev = prev;
        }

        private void VendorManagementOrganizer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'eventverseOriginal.VENDOR' table. You can move, or remove it, as needed.

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            prev.Show();
        }
    }
}