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
    public partial class HidenLayersSettingsForm : Form
    {
        private int _currentRowIndex = 0;
        private int _currentRowShownIndex = 0;

        public delegate void ApplyDelegate();

        public event ApplyDelegate HasApplied;


        protected List<ActivationFunctionModel> _activationFunctionModelList = new List<ActivationFunctionModel>();

        public List<string> _aFHumanNames = new List<string> {
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

        public enum ActivationFucntion {   sigmoid,
                                            tanh,
                                            ReLU,
                                            leakyReLU,
                                            ELU,
                                            smoothReLU,
                                            softsign,
                                            step
        };

        private Dictionary<ActivationFucntion, ActivationFunctionModel> defaultAFDict = new Dictionary<ActivationFucntion, ActivationFunctionModel>();

        private ActivationFunctionForm activationFunctionForm;


        public HidenLayersSettingsForm()
        {
            InitializeComponent();

            // Динамически добавляем паенели в flowlayout вертикально.
            // Таблицая для быстрого редактирования

            // Заполнить словарь defaultAFDict

            activationFunctionForm = new ActivationFunctionForm();

            activationFunctionForm.HasAppliedFunctionChanges += ActivationFunctionForm_HasAppliedFunctionChanges;


            dataGridView1.MouseWheel += dataGridView1_MouseWheel;

            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;

            dataGridView1.UserAddedRow += DataGridView1_UserAddedRow;
            dataGridView1.UserDeletedRow += DataGridView1_UserDeletedRow;

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

            dataGridView1.Rows.CollectionChanged += Rows_CollectionChanged;

        }

        private void ActivationFunctionForm_HasAppliedFunctionChanges(ActivationFunctionModel activationFunctionModel)
        {
            _activationFunctionModelList[_currentRowShownIndex] = activationFunctionModel;

            _updateDataGridCellByModel(_currentRowShownIndex);
            // обновить таблицу datagridview

        }

        private void _updateDataGridCellByModel(int index)
        {
            //for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            //{
                //if (null == dataGridView1.Rows[i].Cells["activationFunction"].Value) 
                //{

                //}
                dataGridView1.Rows[index].Cells["activationFunction"].Value = HidenLayersSettings._aFHumanMachineRelDict.FirstOrDefault(af => af.Value == _activationFunctionModelList[index].activationFucntion).Key; 
            //}
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
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

            if (rows[0].Cells["layerNeuronCount"].Value == null) 
            {
                return;
            }
            
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
                return;
                //dataGridView1.Rows.RemoveAt(11);
            }

            // При добавлении строки в таблицу добавить модель активационной функции по умолчанию
            // далее при нажатии на кнопку настроить модель обновляется в соотвествии с данными пользователя

            _activationFunctionModelList.Add(new ActivationFunctionModel());

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
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewButtonCell buttonCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                var ncv = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                var afv = dataGridView1.Rows[e.RowIndex].Cells[2].Value;


                if (buttonCell != null && (ncv != null || afv != null))
                {
                    //MessageBox.Show("Настройка функции");

                    _updateAFModel();
                    _showFunctionByModelIndex(e.RowIndex);

                }
            }
        }

        private void _showFunctionByModelIndex(int rowIndex)
        {
            // На вход индекс строки, на выход - отобразить 

            _currentRowShownIndex = rowIndex;

            activationFunctionForm.Show(_activationFunctionModelList[rowIndex]);
        }

        private void _updateAFModel()
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                // Элемент с данным индексом гарантирован.

                var fn = (string)dataGridView1.Rows[i].Cells["activationFunction"].Value;

                if (null == fn) 
                {
                    continue;
                }

                _activationFunctionModelList[i].activationFucntion = HidenLayersSettings._aFHumanMachineRelDict[fn];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Поднять событие применения
            // Сформировать подстроку из массива для файла пример "[3,7,5]" 

            if (_hasDataGridNullCells()) 
            {
                MessageBox.Show("Каждый слой описывается количеством нейронов и функцией актиации. Не все слои правильно заданы.");
                return;
            }
            
            _createHidenNueronsModel(dataGridView1.Rows);


            HasApplied();
            Hide();
        }

        private bool _hasDataGridNullCells()
        {
            var result = true;

            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                var lnc = dataGridView1.Rows[i].Cells["layerNeuronCount"].Value;
                var af = dataGridView1.Rows[i].Cells["activationFunction"].Value;
                if (null == lnc || null == af) 
                {
                    return result;
                }
            }

            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Сброс всех настроек - необратимая операция с потерей данных о структуре скрытых слоев. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            _updateAFModel();

            dataGridView1.Rows.Clear();


            dataGridView1.AllowUserToAddRows = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Установка по умолчанию - необратимая операция с потерей данных о структуре скрытых слоев. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            dataGridView1.Rows.Clear();

            dataGridView1.Rows.Add();

            dataGridView1.Rows[0].Cells["layerNeuronCount"].Value = "1";
            dataGridView1.Rows[0].Cells["activationFunction"].Value = HidenLayersSettings._aFHumanMachineRelDict.Keys.ToList()[7];

            //DataGridViewComboBoxCell tst = (DataGridViewComboBoxCell)dataGridView1.Rows[0].Cells["activationFunction"];

            _updateAFModel();

            dataGridView1.AllowUserToAddRows = true;
        }

        private void dataGridView1_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            // При фокусе данная ячейка текущая
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                dataGridView1.BeginEdit(true);
            }
        }
        private void dataGridView1_MouseWheel(object sender, MouseEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            // А коли она текущая, то меняем значение быстро!
            if (dataGridView.CurrentCell is DataGridViewComboBoxCell)
            {
                dataGridView.BeginEdit(true);
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) 
            {
                return;
            }

            if (e.ColumnIndex < 0)
            {
                return;
            }

            // При фокусе данная ячейка текущая
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                dataGridView1.BeginEdit(true);
            }
        }
    }

    public class ActivationFunctionModel
    {
        public double e;
        public double a;
        public HidenLayersSettings.ActivationFucntion activationFucntion;

        public ActivationFunctionModel(double e, double a, HidenLayersSettings.ActivationFucntion activationFucntion)
        {
            this.a = a;
            this.e = e;
            this.activationFucntion = activationFucntion;
        }

        public ActivationFunctionModel()
        {
            this.a = 0.5;
            this.e = 0.5;
            this.activationFucntion = HidenLayersSettings.ActivationFucntion.sigmoid;
        }

    }

    public static class HidenLayersSettings
    {

        public delegate double ActivationFunctionDelegate(double a, double e, double x);

        public enum ActivationFucntion
        {
            sigmoid,
            tanh,
            ReLU,
            leakyReLU,
            ELU,
            smoothReLU,
            softsign,
            step
        };

        public static Dictionary<string, ActivationFucntion> _aFHumanMachineRelDict = new Dictionary<string, ActivationFucntion>
        {
            {"сигмоид",ActivationFucntion.sigmoid },
            {"гиперболический тангенс", ActivationFucntion.tanh},
            {"ReLU", ActivationFucntion.ReLU},
            {"leaky ReLU", ActivationFucntion.leakyReLU},
            {"ELU", ActivationFucntion.ELU},
            {"SmoothReLU", ActivationFucntion.smoothReLU},
            {"softsign", ActivationFucntion.softsign},
            {"ступенчатая", ActivationFucntion.step}
        };

        public static Dictionary<ActivationFucntion, ActivationFunctionDelegate> _aFCalculateRelDict = new Dictionary<ActivationFucntion, ActivationFunctionDelegate>
        {
            {ActivationFucntion.sigmoid, sigmoid},
            {ActivationFucntion.tanh,tanh},
            {ActivationFucntion.ReLU,ReLU},
            {ActivationFucntion.leakyReLU, leakyReLU},
            {ActivationFucntion.ELU, ELU},
            {ActivationFucntion.smoothReLU, smoothReLU},
            {ActivationFucntion.softsign,softSign},
            {ActivationFucntion.step,step}
        };

        public static Dictionary<ActivationFucntion, bool> aFHasParams = new Dictionary<ActivationFucntion, bool>
        {
            {ActivationFucntion.sigmoid, true},
            {ActivationFucntion.tanh, false},
            {ActivationFucntion.ReLU,false },
            {ActivationFucntion.leakyReLU, true},
            {ActivationFucntion.ELU, true},
            {ActivationFucntion.smoothReLU, false},
            {ActivationFucntion.softsign,false},
            {ActivationFucntion.step,false}
        };

        public static double sigmoid(double a, double e, double x)
        {
            return 1 / (1 + a * Math.Pow(Math.E, -x));
        }

        public static double tanh(double a, double e, double x)
        {
            return (Math.Pow(Math.E, 2 * x) - 1) / (Math.Pow(Math.E, 2 * x) + 1);
        }

        public static double ReLU(double a, double e, double x)
        {
            return x > 0 ? x : 0;
        }

        public static double leakyReLU(double a, double e, double x)
        {
            return x > 0 ? x : a * x;
        }

        public static double ELU(double a, double e, double x)
        {
            return x > 0 ? x : a *(Math.Pow(Math.E,x)-1);
        }

        public static double smoothReLU(double a, double e, double x)
        {
            return Math.Log(1+Math.Pow(Math.E,x));
        }
        public static double softSign(double a, double e, double x)
        {
            return x/(1+Math.Abs(x));
        }

        public static double step(double a, double e, double x)
        {
            return x >= 0 ? 1 : -1;
        }

    }
}
