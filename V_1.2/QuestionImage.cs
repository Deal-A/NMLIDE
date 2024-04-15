using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V_1._2
{
    public partial class QuestionImageForm : Form
    {
        public QuestionImageForm()
        {
            InitializeComponent();
        }

        public QuestionImageForm(byte [] imageByteArr)
        {

            InitializeComponent();

            pictureBox1.Image = null;
            if (imageByteArr.Length > 0) 
            {
                using (MemoryStream ms = new MemoryStream(imageByteArr))
                {
                    // Загружаем изображение из потока памяти
                    Image image = Image.FromStream(ms);

                    // Устанавливаем изображение в PictureBox
                    pictureBox1.Image = image;
                }
            }


        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
