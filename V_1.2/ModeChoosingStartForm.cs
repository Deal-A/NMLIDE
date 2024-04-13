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
    public enum Mode {Learning, Experiment, Free}

    public partial class ModeChoosingStartForm : Form
    {
        
        public Mode CurrentMode;

        public delegate void OpenDelegate();

        public event OpenDelegate click;
        public ModeChoosingStartForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.CurrentMode = Mode.Learning;
            this.click();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.CurrentMode = Mode.Experiment;
            this.click();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.CurrentMode = Mode.Free;
            this.click();
        }

        private void ModeChoosingStartForm_Load(object sender, EventArgs e)
        {

        }
    }
}
