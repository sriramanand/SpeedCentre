using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vehicle
{
    public partial class IncomeReport : Form
    {
        public IncomeReport()
        {
            InitializeComponent();
        }

        private void IncomeReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.income' table. You can move, or remove it, as needed.
            this.incomeTableAdapter.Fill(this.DataSet1.income);
            this.reportViewer1.RefreshReport();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
