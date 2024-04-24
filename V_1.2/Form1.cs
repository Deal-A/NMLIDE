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
        private TrainingSampleForm TrainingSampleForm;
        private ANNForm ANNForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _addWidnows();
            ModeChoosingStartForm = new ModeChoosingStartForm();


            ModeChoosingStartForm.Show();

            ModeChoosingStartForm.LearnModeClick += LearnModeOpenMainForm;
            ModeChoosingStartForm.ExperimentModeClick += ExperimentModeOpenMainForm;
            ModeChoosingStartForm.ModelingModeClick += ModelingModeOpenMainForm;

            // При запуске прячет главную форму, чтобы показать форму входа
            Shown += MainForm_Shown;

            KnowledgeTesting.TestFinished += ChooseTrainDataStage;

        }

        private void ChooseTrainDataStage()
        {
            MessageBox.Show("Тест пройден! Подготовка данных.");
        }

        private void ModelingModeOpenMainForm()
        {
            ModeChoosingStartForm.Hide();
            Show();
            stageControlPanel.Visible = false;

        }

        private void ExperimentModeOpenMainForm()
        {
            ModeChoosingStartForm.Hide();
            Show();
            stageControlPanel.Visible = false;
        }


        private void LearnModeOpenMainForm()
        {
            stageControlPanel.Visible = true;
            ModeChoosingStartForm.Hide();
            Show();
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            Hide();
        }


        private void _addWidnows()
        {
            KnowledgeTesting = new KnowledgeTesting();
         //   KnowledgeTesting.MdiParent = this;
            KnowledgeTesting.Hide();

            TrainingSampleForm = new TrainingSampleForm();
            TrainingSampleForm.Hide();

            ANNForm = new ANNForm();
            ANNForm.Show();// Чтобы сразу прогрузить, затеб быстрее будет
            ANNForm.Hide();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void загрузитьВыборкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingSampleForm.Show();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KnowledgeTesting?.Show();
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

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void promptButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            Hide();
            ModeChoosingStartForm.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ANNForm_Show();
        }

        private void ANNForm_Show()
        {
            if (null == ANNForm) 
            {
                ANNForm = new ANNForm();
            }

            loadAnnFormByModel();
            ANNForm.Show();
        }

        private void loadAnnFormByModel()
        {
            
        }
    }
}
