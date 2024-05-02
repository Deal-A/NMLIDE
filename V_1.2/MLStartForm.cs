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
    public partial class MLStartForm : Form
    {
        public MLStartForm()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void learningMLP() 
        {
            int inputSize = 3;
            int[] hiddenLayerSizes = { 3, 5, 7 };
            int outputSize = 1;

            MLP network = new MLP(inputSize, hiddenLayerSizes, outputSize, Math.Tanh);

            double[] input = { 0.5, -0.3, 0.8 };

            double[] output = network.FeedForward(input);

            Console.WriteLine("До обучения\n");
            foreach (var value in output)
            {
                Console.WriteLine(value);
            }

            double[] target = { 0.9 };
            double learningRate = 0.1;

            network.Backpropagation(target, learningRate);

            output = network.FeedForward(input);

            Console.WriteLine("\nПосле обучения");
            foreach (var value in output)
            {
                Console.WriteLine(value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //learningMLP();
            Hide();
        }
    }
}
