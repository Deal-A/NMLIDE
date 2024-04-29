

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CsvHelper;

namespace V_1._2
{
    public partial class TrainingSampleForm : Form
    {

        private int _curListPreviewIndex = -1;

        private ViewCurrentSampleForm ViewCurrentSampleForm;

        private List<List<string>> _trainSourceModel = new List<List<string>>();
        private List<List<string>> _trainModel = new List<List<string>>();
        private List<List<string>> _validationModel = new List<List<string>>();
        private List<List<string>> _testModel = new List<List<string>>();

        private string _filePath;
        private const char _sep = ';';

        private Dictionary<char, RadioButton> _csvSep = new Dictionary<char, RadioButton>();

        private int _sourceCount = 0;
        public TrainingSampleForm()
        {
            InitializeComponent();

            ViewCurrentSampleForm = new ViewCurrentSampleForm();
            ViewCurrentSampleForm.Hide();

            _csvSep.Add(';', radioButton1);
            _csvSep.Add(',', radioButton2);

            
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            ViewCurrentSampleForm.Hide();
        }

        private void listView2_MouseDown(object sender, MouseEventArgs e)
        {
            _curListPreviewIndex = 0;
        }



        private void openButton_Click(object sender, EventArgs e)
        {
            try 
            {
                fillFilePath();
                loadFileToModel();
                updateAllViewsByModel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void fillFilePath()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 1;

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _filePath = openFileDialog.FileName;
            }
        }

        private void loadFileToModel()
        {
            _trainSourceModel.Clear();

            string[] lines = File.ReadAllLines(_filePath);

            string[] values;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].Split(_csvSep.FirstOrDefault(a=>a.Value.Checked).Key);
                _trainSourceModel.Add(values.ToList());
            }

            _sourceCount = _trainSourceModel.Count-1;

        }

        private void addHeadersToView(List<List<string>> model, ListView view)
        {
            if (model.Count == 0) 
            {
                return;
            }

            var c = model[0];
            for (int i = 0; i < c.Count; i++)
            {
                view.Columns.Add(new ColumnHeader());
                view.Columns[i].Text = c[i];
                view.Columns[i].Width = 100;
            }


        }
        private void addDataToView(List<List<string>> model, ListView view)
        {
            if (model.Count == 0)
            {
                return;
            }

            for (int i = 1; i < model.Count; i++)
            {
                view.Items.Add(new ListViewItem(model[i].ToArray()));
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Автозаполнение выборок
            if (_trainModel.Count > 0) 
            {
                // уже заполняли
                loadFileToModel();
                _trainModel.Clear();
                _validationModel.Clear();
                _testModel.Clear();
            }
            

            int _count = _trainSourceModel.Count;
            int trainCount = (int)(_count * 0.7);
            int validationCount = (int)(_count*0.2);

            _trainModel.Add(_trainSourceModel[0]);

            for (int i = 0; i < trainCount; i++)
            {
                _trainModel.Add(getRandomSampleFromSource());
            }

            _validationModel.Add(_trainSourceModel[0]);

            for (int i = 0; i < validationCount; i++)
            {
                _validationModel.Add(getRandomSampleFromSource());
            }

            _testModel.Add(_trainSourceModel[0]);

            int lim = _trainSourceModel.Count - 1;

            for (int i = 0; i < lim; i++)
            {
                _testModel.Add(getRandomSampleFromSource());
            }

            updateAllViewsByModel();
        }

        private void updateAllViewsByModel()
        {
            updateLabel(label4, "Исходная", _trainSourceModel.Count - 1, _sourceCount);
            updateViewByModel(_trainSourceModel,listView1);

            updateLabel(label1, "Обучающая", _trainModel.Count != 0 ? _trainModel.Count - 1 : 0, _sourceCount);
            updateViewByModel(_trainModel,listView2);

            updateLabel(label2, "Валидационная", _validationModel.Count != 0 ? _validationModel.Count - 1 : 0, _sourceCount);
            updateViewByModel(_validationModel,listView3);

            updateLabel(label3, "Тестовая", _testModel.Count != 0 ? _testModel.Count - 1 : 0, _sourceCount);
            updateViewByModel(_testModel,listView4);
        }

        private void updateLabel(Label label, string text, int c1, int c2)
        {
            label.Text = $"{text}: {c1} ({Math.Round((c2 != 0 ? (double)(c1) / (double)(c2) : 0) * 100,2)}%)";
        }

        private void updateViewByModel(List<List<string>> model, ListView view)
        {
            
            view.Columns.Clear();
            view.Items.Clear();
            view.Clear();
            addHeadersToView(model, view);
            addDataToView(model, view);
        }

        private List<string> getRandomSampleFromSource()
        {
            int i = new Random().Next(1, _trainSourceModel.Count);
            var res = _trainSourceModel[i];
            _trainSourceModel.RemoveAt(i);
            
            return res;
        }

        private void viewTrainButton_Click(object sender, EventArgs e)
        {
            ViewCurrentSampleForm.UpdateListView(_trainModel);
            ViewCurrentSampleForm.Show();
        }

        private void viewValidationButton_Click(object sender, EventArgs e)
        {
            ViewCurrentSampleForm.UpdateListView(_validationModel);
            ViewCurrentSampleForm.Show();
        }

        private void viewTestButton_Click(object sender, EventArgs e)
        {
            ViewCurrentSampleForm.UpdateListView(_testModel);
            ViewCurrentSampleForm.Show();
        }

        private void viewResourceButton_Click(object sender, EventArgs e)
        {
            ViewCurrentSampleForm.UpdateListView(_trainSourceModel);
            ViewCurrentSampleForm.Show();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool growth = true;
            if (listView1.Sorting == SortOrder.Ascending)
            {
                listView1.Sorting = SortOrder.Descending;
                growth = false;
            }
            else 
            {
                listView1.Sorting = SortOrder.Ascending;
                growth = true;
            }
                

            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, growth);

            listView1.Sort();
        }


    }

    public class ListViewItemComparer : IComparer
    {
        private int col;
        bool growth;
        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column)
        {
            col = column;
        }

        public ListViewItemComparer(int column, bool growth)
        {
            col = column;
            this.growth = growth;
        }
        public int Compare(object x, object y)
        {
            //if (x is double && y is double)
            //{
            double numX = double.Parse(((ListViewItem)x).SubItems[col].Text.Replace('.', ','));
            double numY = double.Parse(((ListViewItem)y).SubItems[col].Text.Replace('.', ','));
            return growth ? numX.CompareTo(numY) : -numX.CompareTo(numY);
            // }

            //return 0;
        }
    }
}
