using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gecko;

namespace V_1._2
{
    public partial class ANNForm : Form
    {
        public ANNForm()
        {
            InitializeComponent();

            string our = "D:\\Веб\\Обучение\\Vyatka_bank_local\\index.html";

            Xpcom.Initialize("Firefox64");

            var geckoWebBrowser = new GeckoWebBrowser { Dock = DockStyle.Fill };

            panel1.Controls.Add(geckoWebBrowser);

            geckoWebBrowser.Navigate(our);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
