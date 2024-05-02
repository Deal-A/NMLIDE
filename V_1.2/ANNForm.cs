using System;
using System.IO;
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

        HidenLayersSettingsForm HidenLayersSettingsForm;

        private string graphUrl = "http://localhost:5173/";
        private string vueAppPath = "D:\\_1Study\\ВКР\\P\\V_1\\test_vue\\vue-project\\src\\App.vue";
        private string _initNeuronLStruct = "[1]";

        public int inputsCount = 1;
        public int outputsCount = 1;
        public ANNForm()
        {
            InitializeComponent();

            c_br = new ChromiumWebBrowser() { Dock = DockStyle.Fill };

            panel1.Controls.Add(c_br);

            //c_br.LoadUrl(graphUrl);

            HidenLayersSettingsForm = new HidenLayersSettingsForm();


            HidenLayersSettingsForm.HasApplied += HidenLayersSettingsForm_HasApplied;
        }

        private void HidenLayersSettingsForm_HasApplied()
        {
            //string templ = $"  hidenLayersArr : {HidenLayersSettingsForm.neuronNodel}";
            //updateLineReload(templ, 22);
            _updateByModel();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            c_br.LoadUrl(checkButton.Text);
        }

        public static void ChangeLine(string filePath, int lineIndex, string newLine)
        {
            string[] lines = File.ReadAllLines(filePath);
            lines[lineIndex - 1] = newLine;
            File.WriteAllLines(filePath, lines);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _updateByModel();

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            _updateByModel();

        }

        private void updateLineReload(string l, int n) 
        {
            ChangeLine(vueAppPath, n, l);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HidenLayersSettingsForm.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (null == HidenLayersSettingsForm.neuronNodel) 
            {
                MessageBox.Show("Не заполнены скрытые слои");
                return;
            }

            
            c_br.LoadUrl(graphUrl);
        }

        private void _updateByModel()
        {
            if (null == HidenLayersSettingsForm.neuronNodel) 
            {
                return;
            }

            updateLineReload($"  inputs:{numericUpDown1.Value},", 20);
            updateLineReload($"  outputs:{numericUpDown2.Value},", 21);
            updateLineReload($"  hidenLayersArr : {HidenLayersSettingsForm.neuronNodel}", 22);

            c_br.LoadUrl(graphUrl);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
