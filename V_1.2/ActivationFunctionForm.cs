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

            textBox2.Text = "0,1";
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

        }

        private void _alphaUpdated() 
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

            double.TryParse(textBox1.Text, out _curFModel.a);

            _updateGraphByModel();
        }

        private void _updateGraphByModel()
        {
            _curFModel.activationFucntion = HidenLayersSettings._aFHumanMachineRelDict[comboBox1.Text];

            double a = -11, b = 11, h = 0.05, x, y;

            double.TryParse(textBox2.Text, out h);

            chart1.Series[0].Points.Clear();
            
            x = a;

            var aF = HidenLayersSettings._aFHumanMachineRelDict[comboBox1.Text];
            var func = HidenLayersSettings._aFCalculateRelDict[aF];

            if (_curFModel.activationFucntion == HidenLayersSettings.ActivationFucntion.step) 
            {
                // В режиме сплайна появляются артефакты, устранить - в режим линии и увеличить шаг сетки
                // для всех - медленно, тольк для одно ступени.

                h = 0.01;

                while (x <= b)
                {
                    y = func(_curFModel.a, _curFModel.e, x);

                    chart1.Series[0].Points.AddXY(x, y);
                    x += h;
                }

                return;
            }


            while (x <= b)
            {
                y = func(_curFModel.a, _curFModel.e, x);
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

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                _alphaUpdated();
            }
        }
    }
}
