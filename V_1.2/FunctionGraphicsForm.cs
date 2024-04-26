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
    public partial class FunctionGraphicsForm : Form
    {
        public FunctionGraphicsForm()
        {
            InitializeComponent();

            _fillMAEGraph();

        }

        private void _fillMAEGraph()
        {


            double a = 0.1, b = 11, h = 0.05, x, y;

            x = a;

            Func<double, double, double> mae = (xx, aa) => aa/xx;

            while (x <= b)
            {
                y = mae(x,10);
                chart1.Series[0].Points.AddXY(x, y);

                x += h;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void FunctionGraphicsForm_Load(object sender, EventArgs e)
        {

        }

        internal void ShowMLMode()
        {
            Show();
        }
    }
}
