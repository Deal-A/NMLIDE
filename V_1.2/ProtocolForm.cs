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
    public partial class ProtocolForm : Form
    {
        public ProtocolForm()
        {
            InitializeComponent();

            this.VisibleChanged += ProtocolForm_VisibleChanged;

            insertPicture();
            insertAnotherText();

        }

        private void insertAnotherText()
        {
            var t = "\n\n\nПроверка на тестовом множестве:\n"
            + "Среднеквадратичная ошибка: 0,17\n"
            + "Средная ошибка: 0,5\n"
            + "Максимальаня абсолютная ошибка: 48,12\n";

            //richTextBox1.SelectionStart = richTextBox1.Text.Length;


            richTextBox1.SelectionStart = richTextBox1.Rtf.Length;   //richTextBox1.Rtf.Length
            
            Clipboard.SetText(t);

            richTextBox1.Paste();

            //richTextBox1.Rtf += t;

        }

        private void ProtocolForm_VisibleChanged(object sender, EventArgs e)
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        protected override void OnShown(EventArgs e)
        {
            richTextBox1.ReadOnly = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
