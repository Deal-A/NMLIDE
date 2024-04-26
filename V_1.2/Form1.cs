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
        private AugmentationForm augmentationForm;
        private ANNKindChoisingForm aNNKindChoisingForm;
        private MLParametersForm mLParametersForm;
        private FunctionGraphicsForm functionGraphicsForm;
        private MLStartForm mLStartForm;


        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _addWidnows();

            // При запуске прячет главную форму, чтобы показать форму входа
            Shown += MainForm_Shown;

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
            KnowledgeTesting.Hide();
            KnowledgeTesting.TestFinished += ChooseTrainDataStage;

            TrainingSampleForm = new TrainingSampleForm();
            TrainingSampleForm.Hide();

            ANNForm = new ANNForm();
            ANNForm.Show();// Чтобы сразу прогрузить, затем быстрее будет
            ANNForm.Hide();

            ModeChoosingStartForm = new ModeChoosingStartForm();
            ModeChoosingStartForm.Show();
            ModeChoosingStartForm.LearnModeClick += LearnModeOpenMainForm;
            ModeChoosingStartForm.ExperimentModeClick += ExperimentModeOpenMainForm;
            ModeChoosingStartForm.ModelingModeClick += ModelingModeOpenMainForm;

            augmentationForm = new AugmentationForm();

            aNNKindChoisingForm = new ANNKindChoisingForm();

            mLParametersForm = new MLParametersForm();
            mLParametersForm.Hide();

            functionGraphicsForm = new FunctionGraphicsForm();
            functionGraphicsForm.Hide();

            mLStartForm = new MLStartForm();
            mLStartForm.Hide();

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void загрузитьВыборкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrainingSampleForm.ShowDialog();
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
            ANNForm.ShowDialog();
        }

        private void loadAnnFormByModel()
        {
            
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            augmentationForm.ShowDialog();
        }

        private void выборИНСToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aNNKindChoisingForm.ShowDialog();
        }

        private void дейсвтиеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void параметрыОбученияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mLParametersForm.Show();
        }

        private void графикиФункцийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            functionGraphicsForm.ShowDialog();
        }

        private void обучениеНачатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mLStartForm.ShowDialog();
            functionGraphicsForm.ShowMLMode();
        }
    }
}
