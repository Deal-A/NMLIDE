﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V_1._2
{
    public partial class MLParametersForm : Form
    {
        public MLParametersForm()
        {
            InitializeComponent();
        }

        public void SetParamModel() 
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
