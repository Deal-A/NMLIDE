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
    public partial class ViewCurrentSampleForm : Form
    {
        public ViewCurrentSampleForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        internal void UpdateListView(List<List<string>> model)
        {
            listView1.Clear();
            addHeadersToView(model, listView1);
            addDataToView(model, listView1);
        }

        private void addHeadersToView(List<List<string>> model, ListView view)
        {
            if (model.Count == 0)
            {
                return;
            }

            var c = model[0];
            for (int i = 0; i < c.Count; i++)
            {
                view.Columns.Add(new ColumnHeader());
                view.Columns[i].Text = c[i];
                view.Columns[i].Width = 100;
            }
        }
        private void addDataToView(List<List<string>> model, ListView view)
        {
            if (model.Count == 0)
            {
                return;
            }

            for (int i = 1; i < model.Count; i++)
            {
                view.Items.Add(new ListViewItem(model[i].ToArray()));
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool growth = true;
            if (listView1.Sorting == SortOrder.Ascending)
            {
                listView1.Sorting = SortOrder.Descending;
                growth = false;
            }
            else
            {
                listView1.Sorting = SortOrder.Ascending;
                growth = true;
            }


            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, growth);

            listView1.Sort();
        }
    }
}
