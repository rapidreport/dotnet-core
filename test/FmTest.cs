using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace test
{
    public partial class FmTest : Form
    {
        public FmTest()
        {
            InitializeComponent();
        }

        private void FmTest_Load(object sender, EventArgs e)
        {
            CmbTest.Items.AddRange(MainModule.Tests.ToArray());
            CmbTest.SelectedIndex = CmbTest.Items.Count - 1;
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            var i = CmbTest.SelectedItem;
            if (i != null)
            {
                i.GetType().GetMethod("Run").Invoke(i, null);
            }
        }

        private void BtnOpenOut_Click(object sender, EventArgs e)
        {
            Process.Start("EXPLORER.EXE", @"out");
        }
    }
}
