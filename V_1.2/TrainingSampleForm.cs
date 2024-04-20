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
    public partial class TrainingSampleForm : Form
    {

        private int _curListPreviewIndex = -1;

        private ViewCurrentSampleForm ViewCurrentSampleForm;

        public TrainingSampleForm()
        {
            InitializeComponent();

            ViewCurrentSampleForm = new ViewCurrentSampleForm();
            ViewCurrentSampleForm.Hide();

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

        private void viewTrainButton_Click(object sender, EventArgs e)
        {
            ViewCurrentSampleForm.Show();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 1;

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
            }
        }
    }
}
