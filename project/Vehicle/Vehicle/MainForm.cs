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

namespace Vehicle
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddInventory a1 = new AddInventory();
            this.Hide();
            a1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEmployee a1 = new AddEmployee();
            this.Hide();
            a1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Schedule s1 = new Schedule();
            this.Hide();
            s1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
          AddData a2 = new AddData();
          this.Hide();
          a2.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Attendence a1 = new Attendence();
            this.Hide();
            a1.Show();
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OtherExpense a1 = new OtherExpense();
            this.Hide();
            a1.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            TimeSchedule t1 = new TimeSchedule();
            this.Hide();
            t1.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AddSales a1 = new AddSales();
            this.Hide();
            a1.Show();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            IncomeStatement i1 = new IncomeStatement();
            this.Hide();
            i1.Show();
        }

    }
}
