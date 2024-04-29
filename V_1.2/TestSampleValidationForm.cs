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
    public partial class TestSampleValidationForm : Form
    {
        public TestSampleValidationForm()
        {
            InitializeComponent();
            insertPicture();
        }

        private void insertPicture()
        {

            Image imageToInsert = Image.FromFile("D:\\_1Study\\ВКР\\P\\V_1\\V_1.2\\Sources\\Нормальное_распределение.png");


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
    }
}
