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
    public enum ANNType { nd, MLP, RBF, recurrent, convolution, LVQ };
    public partial class ANNKindChoisingForm : Form
    {

        public delegate void HasTypeNMApplyedDelagate(ANNType aNNType);

        public event HasTypeNMApplyedDelagate HasTypeNMApplyed;

        private Dictionary<ANNType, RadioButton> viewModelMapping = new Dictionary<ANNType, RadioButton>();

        private ANNType _currentANNType;

        private bool flag = false;

        public ANNKindChoisingForm()
        {
            InitializeComponent();

            Shown += ANNKindChoisingForm_Shown;

            viewModelMapping.Add(ANNType.MLP, radioButton1);
            viewModelMapping.Add(ANNType.RBF, radioButton2);
            viewModelMapping.Add(ANNType.recurrent, radioButton3);
            viewModelMapping.Add(ANNType.convolution, radioButton4);
            viewModelMapping.Add(ANNType.LVQ, radioButton5);


            foreach (var rb in viewModelMapping.Values.ToArray()) 
            {
                rb.CheckedChanged += Rb_CheckedChanged;
            }
        }

        private void Rb_CheckedChanged(object sender, EventArgs e)
        {

            var arr = viewModelMapping.Values.ToArray();
            for (var i=0;i< arr.Length;i++)
            {
                if (arr[i].Checked) 
                {
                    _currentANNType = viewModelMapping.ToArray()[i].Key;
                }
            }
            //_currentANNType =  viewModelMapping.FirstOrDefault(vmm => vmm.Value == (RadioButton)sender).Key;
        }

        private void ANNKindChoisingForm_Shown(object sender, EventArgs e)
        {


            if (ANNType.nd == _currentANNType) 
            {
                return;
            }

            viewModelMapping[_currentANNType].Checked = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HasTypeNMApplyed(_currentANNType);
            Hide();
        }

        private void ANNKindChoisingForm_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
