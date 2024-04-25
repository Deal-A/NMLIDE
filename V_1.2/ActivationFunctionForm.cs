using System;
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
    public partial class ActivationFunctionForm : Form
    {
        public ActivationFunctionForm()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(HidenLayersSettings._aFHumanMachineRelDict.Keys.ToArray());
        }

        protected void Show(double a, double e, HidenLayersSettings.ActivationFucntion activationFucntion) 
        {

            Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
