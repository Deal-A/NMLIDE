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
    public partial class KnowledgeTestProtocolForm : Form
    {
        private Color _rightColour = Color.FromArgb(255, 0, 173, 0);
        private Color _ignorCorrectColour = Color.FromArgb(255, 177, 4, 255);
        private Color _wrongColour = Color.FromArgb(201, 3, 0);

        public delegate void NextDelegate();

        public event NextDelegate NextDelegateClick;

        public KnowledgeTestProtocolForm()
        {
            InitializeComponent();

            _setPresentationProtocolStyles();

            _statAdd();


        }

        private void _statAdd()
        {
            listView1.Items.Add(new ListViewItem(new[] { "Количество вопросов", "3" }));
            listView1.Items.Add(new ListViewItem(new[] { "Процент выполнения", "87,5" }));
        }

        private void _setPresentationProtocolStyles()
        {
            richTextBox1.Focus();


            int selCursor = this.richTextBox1.SelectionStart + richTextBox1.Lines[0].Length;

            //richTextBox1.SelectionStart = selCursor;
            //richTextBox1.SelectionLength = richTextBox1.Lines[1].Length +1;
            //richTextBox1.SelectionFont = _getCorrectAnswerFont();
            //richTextBox1.SelectionColor = Color.FromArgb(255, 0, 173, 0);

            _paintLine(_rightColour, _getCorrectAnswerFont(), 1);
            _paintLine(_rightColour, _getCorrectAnswerFont(), 6);
            _paintLine(_rightColour, _getCorrectAnswerFont(), 7);
            _paintLine(_ignorCorrectColour, _getWrongAnswerFont(), 8);

            _paintLine(_wrongColour, _getWrongAnswerFont(), 9);

            _paintLine(_rightColour, _getCorrectAnswerFont(), 14);
            _paintLine(_rightColour, _getCorrectAnswerFont(), 13);

        }

        private void _paintLine(Color color, Font font, int lineIndex) 
        {
            int selCursor = 0;

            for (int i = 0; i < lineIndex;i++) 
            {
                selCursor += richTextBox1.Lines[i].Length + 1;
            }

            richTextBox1.SelectionStart = selCursor;
            richTextBox1.SelectionLength = richTextBox1.Lines[lineIndex].Length ;
            richTextBox1.SelectionFont = font;
            richTextBox1.SelectionColor = color;
        }

        private Font _getCorrectAnswerFont()
        {
            var rbf = richTextBox1.Font;
            return new Font(rbf.FontFamily, rbf.Size+1,FontStyle.Bold,GraphicsUnit.Point);
        }

        private Font _getWrongAnswerFont()
        {
            var rbf = richTextBox1.Font;
            return new Font(rbf.FontFamily, rbf.Size + 1, FontStyle.Regular, GraphicsUnit.Point);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void KnowledgeTestProtocolForm_Load(object sender, EventArgs e)
        {

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            this.NextDelegateClick();
        }
    }
}
