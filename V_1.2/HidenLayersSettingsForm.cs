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
    public partial class HidenLayersSettingsForm : Form
    {
        private int _currentRowIndex = 0;

        public delegate void ApplyDelegate();

        public event ApplyDelegate HasApplied;

        private List<ActivationFunctionModel> _activationFunctionModelList = new List<ActivationFunctionModel>();

        private List<string> _aFHumanNames = new List<string> {
            "сигмоид",
            "гиперболический тангенс",
            "ReLU",
            "leaky ReLU",
            "ELU",
            "SmoothReLU",
            "softsign",
            "ступенчатая"
        };

        public string neuronNodel;

        private enum ActivationFucntion {   sigmoid,
                                            tanh,
                                            ReLU,
                                            leakyReLU,
                                            ELU,
                                            smoothReLU,
                                            softsign,
                                            step
        };

        private Dictionary<ActivationFucntion, ActivationFunctionModel> defaultAFDict = new Dictionary<ActivationFucntion, ActivationFunctionModel>();
        private Dictionary<string, ActivationFucntion> _aFHumanMachineRelDict = new Dictionary<string, ActivationFucntion>();

        private class ActivationFunctionModel
        {
            public double e;
            public double a;
            public ActivationFucntion activationFucntion;

            public ActivationFunctionModel(double e, double a, ActivationFucntion activationFucntion) 
            {
                this.a = a;
                this.e = e;
                this.activationFucntion = activationFucntion;
            }

        }
        public HidenLayersSettingsForm()
        {
            InitializeComponent();

            // Динамически добавляем паенели в flowlayout вертикально.
            // Таблицая для быстрого редактирования

            // Заполнить словарь defaultAFDict
            _mapHumanAFNToMachine();
            _fillAFDictByDefault();

            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;

            dataGridView1.UserAddedRow += DataGridView1_UserAddedRow;
            dataGridView1.UserDeletedRow += DataGridView1_UserDeletedRow;

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

            dataGridView1.Rows.CollectionChanged += Rows_CollectionChanged;

        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void _mapHumanAFNToMachine()
        {
            _aFHumanMachineRelDict.Add("сигмоид",ActivationFucntion.sigmoid);
            _aFHumanMachineRelDict.Add("гиперболический тангенс", ActivationFucntion.tanh);
            _aFHumanMachineRelDict.Add("ReLU", ActivationFucntion.ReLU);
            _aFHumanMachineRelDict.Add("leaky ReLU", ActivationFucntion.leakyReLU);
            _aFHumanMachineRelDict.Add("ELU", ActivationFucntion.ELU);
            _aFHumanMachineRelDict.Add("SmoothReLU", ActivationFucntion.smoothReLU);
            _aFHumanMachineRelDict.Add("softsign", ActivationFucntion.softsign);
            _aFHumanMachineRelDict.Add("ступенчатая", ActivationFucntion.step);
        }

        private void _fillAFDictByDefault()
        {
            foreach (var aFEnum in _aFHumanMachineRelDict.Values)
            {
                defaultAFDict.Add(aFEnum, new ActivationFunctionModel(0.5, 0.5, aFEnum));
            }
        }

        private void DataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            int rowCount = dataGridView1.Rows.Count;
            if (rowCount <= 10)
            {
                //MessageBox.Show("Не более 10 скрытых слоев.");
                dataGridView1.AllowUserToAddRows = true;
                //dataGridView1.Rows.RemoveAt(11);
            }
        }

        private void Rows_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["layerNumberColumn"].Value = i + 1;
            }

        }

        private void _createHidenNueronsModel(DataGridViewRowCollection rows)
        {
            
            string res = "[";

            int d = dataGridView1.AllowUserToAddRows ? 2 : 1;

            if (rows.Count > 2) 
            {
                for (int i = 0; i < rows.Count - d; i++)
                {
                    res += rows[i].Cells["layerNeuronCount"].Value;
                    res += ",";
                }
            }



            res += rows[rows.Count - d].Cells["layerNeuronCount"].Value;
            res += "]";

            neuronNodel = res;

        }

        private void DataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int rowCount = dataGridView1.Rows.Count;
            if (rowCount > 10)
            {
                //MessageBox.Show("Не более 10 скрытых слоев.");
                dataGridView1.AllowUserToAddRows = false;
                //dataGridView1.Rows.RemoveAt(11);
            }


        }

        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Поднять событие применения
            // Сформировать подстроку из массива для файла пример "[3,7,5]" 
            _createHidenNueronsModel(dataGridView1.Rows);
            HasApplied();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // заполнить только один скрытый слой тремя нейронами с сигмоидой
        }
    }
}
