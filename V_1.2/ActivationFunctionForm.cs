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

        public delegate void ApplyDelegateFunctionChanges(ActivationFunctionModel activationFunctionModel);

        public event ApplyDelegateFunctionChanges HasAppliedFunctionChanges;

        private ActivationFunctionModel _curFModel; 

        public ActivationFunctionForm()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(HidenLayersSettings._aFHumanMachineRelDict.Keys.ToArray());
            chart1.Series[0].Points.Clear();

        }

        public void Show(ActivationFunctionModel aFM) 
        {

            _curFModel = aFM;

            var hAF = HidenLayersSettings._aFHumanMachineRelDict.FirstOrDefault(af => af.Value == _curFModel.activationFucntion).Key;

            comboBox1.Text = hAF;

            textBox1.Text = _curFModel.a.ToString();

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
            double low = -50, high = 50, defaultValue = 0.5;

            if (!double.TryParse(textBox1.Text, out _curFModel.a)) 
            {
                MessageBox.Show("Ошибка ввода");
                textBox1.Text = defaultValue.ToString();
            }

            if (_curFModel.a < low)
            {
                MessageBox.Show($"Значение должно быть больше или равно {low}");
                textBox1.Text = defaultValue.ToString();
            }
            if (_curFModel.a > high)
            {
                MessageBox.Show($"Значение должно меншье или равно {high}");
                textBox1.Text = defaultValue.ToString();
            }

            _updateGraphByModel();

        }

        private void _updateGraphByModel()
        {
            _curFModel.activationFucntion = HidenLayersSettings._aFHumanMachineRelDict[comboBox1.Text];

            double a = -11, b = 11, h = 0.05, x, y;

            chart1.Series[0].Points.Clear();

            x = a;
            while (x <= b)
            {
                var aF = HidenLayersSettings._aFHumanMachineRelDict[comboBox1.Text];
                y = HidenLayersSettings._aFCalculateRelDict[aF](_curFModel.a, _curFModel.e, x);

                chart1.Series[0].Points.AddXY(x, y);

                x += h;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            HasAppliedFunctionChanges(_curFModel);
            Hide();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            _updateGraphByModel();
        }
    }
}
