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
    public partial class MainForm : Form
    {
        private ModeChoosingStartForm ModeChoosingStartForm;
        private KnowledgeTesting KnowledgeTesting;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _addMdiWidnows();
            ModeChoosingStartForm = new ModeChoosingStartForm();


            this.ModeChoosingStartForm.Show();

            this.ModeChoosingStartForm.click += OpenMainForm;

            this.Shown += MainForm_Shown;
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void OpenMainForm()
        {
            this.ModeChoosingStartForm.Hide();
            this.Show();
        }

        private void _addMdiWidnows()
        {
            KnowledgeTesting = new KnowledgeTesting();
         //   KnowledgeTesting.MdiParent = this;
            KnowledgeTesting.Hide();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void загрузитьВыборкуToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KnowledgeTesting.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void помощьToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }
    }
}
