using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp.DevTools.IO;
using CefSharp.WinForms;

namespace V_1._2
{

    public partial class ANNForm : Form
    {

        ChromiumWebBrowser c_br;
        public ANNForm()
        {
            InitializeComponent();

            c_br = new ChromiumWebBrowser() { Dock = DockStyle.Fill };

            panel1.Controls.Add(c_br);

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            c_br.LoadUrl(textBox1.Text);
        }
    }
}
