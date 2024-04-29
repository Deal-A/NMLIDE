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
    public partial class FunctionGraphicsForm : Form
    {
        private int _delay = 250;
        private bool _mlMode = false;
        public FunctionGraphicsForm()
        {
            InitializeComponent();

           // DocumentFormat.OpenXml.Drawing.Charts.LineChart lineChart

            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Interval = 1000; 
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Start();

            //timer.

            _fillMAEGraph();
            _fillMSEGraph(0);
            _fillMAPEGraph(0);
        }

        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    // Обновляем данные в компоненте Chart
        //    chart1.Series["Series1"].Points.AddXY(DateTime.Now.Second, rand.Next(100));
        //}

        private void _fillMAPEGraph(int delay)
        {
            double a = 1, b = 11, h = 0.05, x, y;

            x = a;

            Func<double, double, double> mae = (xx, aa) => Math.Pow(aa, -(xx - 10));

            while (x <= b)
            {
                y = mae(x, 3);
                chart3.Series[0].Points.AddXY(x, y);

                x += h;

                //Thread.Sleep(delay);
            }
        }

        private void _fillMSEGraph(int delay)
        {
            double a = 0.1, b = 11, h = 0.05, x, y;

            x = a;

            Func<double, double, double> mae = (xx, aa) => aa / xx;

            while (x <= b)
            {
                y = mae(x, 10);
                chart2.Series[0].Points.AddXY(x, y);

                x += h;

                //Thread.Sleep(delay);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }


        public delegate void UpdateChartDelegate(double x, double y);



        public void _fillMAEGraphLine(double x, double y) 
        {
            chart1.Series[0].Points.AddXY(x, y);
        }

        public void _fillMSEGraphLine(double x, double y)
        {
            chart2.Series[0].Points.AddXY(x, y);
        }

        public void _fillMAPEGraphLine(double x, double y)
        {
            chart3.Series[0].Points.AddXY(x, y);
        }

        public void _fillMAEGraph()
        {
            int delay = 0;

            if (_mlMode)
            {
                delay = _delay;
            }

            double a = 0.1, b = 11, h = 0.05, x, y;

            x = a;

            Func<double, double, double> mae = (xx, aa) => aa / xx;

            while (x <= b)
            {
                y = mae(x, 10);
                chart1.Series[0].Points.AddXY(x, y);

                x += h;

                //Thread.Sleep(delay);
            }
        }

        private void TaskMAE() 
        {
            UpdateChartDelegate fillMAEUpdateGraphDelegate = new UpdateChartDelegate(_fillMAEGraphLine);
            
            double a = 0.1, b = 5, h = 0.05, x, y;

            x = a;

            Func<double, double, double> mae = (xx, aa) => aa / xx;

            while (x <= b)
            {
                y = mae(x, 10);
                this.Invoke(fillMAEUpdateGraphDelegate, new object[] {x, y});

                x += h;

                Thread.Sleep(_delay);
            }

        }

        private void TaskMAPE()
        {
            UpdateChartDelegate fillMAPEUpdateGraphDelegate = new UpdateChartDelegate(_fillMAPEGraphLine);

            double a = 0.5, b = 5, h = 0.05, x, y;

            x = a;

            Func<double, double, double> mae = (xx, aa) => Math.Pow(aa, -(xx - 5));

            while (x <= b)
            {
                y = mae(x, 3);
                this.Invoke(fillMAPEUpdateGraphDelegate, new object[] { x, y });

                x += h;

                Thread.Sleep(_delay);
            }

        }

        private void TaskMSE()
        {
            UpdateChartDelegate fillMSEUpdateGraphDelegate = new UpdateChartDelegate(_fillMSEGraphLine);

            double a = 0.1, b = 5, h = 0.05, x, y;

            x = a;

            Func<double, double, double> mae = (xx, aa) => aa / xx;

            while (x <= b)
            {
                y = mae(x, 5);
                this.Invoke(fillMSEUpdateGraphDelegate, new object[] { x, y });

                x += h;

                Thread.Sleep(_delay);
            }

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void FunctionGraphicsForm_Load(object sender, EventArgs e)
        {

        }

        internal void ShowMLMode()
        {
            Show();

            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();

            int delay = 500;

            _mlMode = true;

            ThreadStart threadTask = new ThreadStart(TaskMAE);
            Thread t = new Thread(threadTask);
            t.Start();

            (new Thread(new ThreadStart(TaskMAPE))).Start();
            (new Thread(new ThreadStart(TaskMSE))).Start();

            //ThreadStart threadTask = new ThreadStart(TaskMAE);

            //chart1.BeginInvoke((MethodInvoker) delegate 
            //{
            //    int d = 500;

            //    double a = 0.1, b = 11, h = 0.05, x, y;

            //    x = a;

            //    Func<double, double, double> mae = (xx, aa) => aa / xx;

            //    while (x <= b)
            //    {
            //        y = mae(x, 10);
            //        chart1.Series[0].Points.AddXY(x, y);

            //        x += h;

            //        Thread.Sleep(d);
            //    }
            //});

            //Thread t = new Thread(() => _fillMAEGraph(delay));

            //t.Start();


            //_fillMAPEGraph(delay);
            //_fillMSEGraph(delay);
            //_fillMAEGraph(delay);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
