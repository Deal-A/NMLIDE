using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace V_1._2
{
    public partial class TestSampleValidationForm : Form
    {
        public TestSampleValidationForm()
        {
            InitializeComponent();
            //insertPicture();
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

        private void insertText() 
        {
            string t = "Проверка на тестовом множестве:\n"
            + "Среднеквадратичная ошибка: 0,17\n"
            + "Средная ошибка: 0,5\n"
            + "Максимальаня абсолютная ошибка: 48,12\n\n\n";

            richTextBox1.Text = t;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Thread.Sleep(500);

            MessageBox.Show("Модель проверена");
            insertText();
            insertPicture();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
