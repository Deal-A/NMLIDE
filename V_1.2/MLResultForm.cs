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
    public partial class MLResultForm : Form
    {
        public MLResultForm()
        {
            InitializeComponent();

            this.VisibleChanged += MLResultForm_VisibleChanged;

            insertPicture();

        }
        protected override void OnShown(EventArgs e)
        {
            richTextBox1.ReadOnly = true;
        }
        private void MLResultForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                richTextBox1.ReadOnly = false;
            }
        }

        private void insertPicture()
        {

            Image imageToInsert = Image.FromFile("D:\\_1Study\\ВКР\\P\\V_1\\V_1.2\\Sources\\MAE_.png");


            Clipboard.SetImage(imageToInsert);



            richTextBox1.SelectionStart = richTextBox1.Text.Length;

            richTextBox1.Paste();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
