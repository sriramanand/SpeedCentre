using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vehicle
{
    public partial class EmployeeTable : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");

        public EmployeeTable()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainForm m1 = new MainForm();
            this.Hide();
            m1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddEmployee a1 = new AddEmployee();
            a1.button3.Enabled = false;
            this.Hide();
            a1.Show();
        }

        private void EmployeeTable_Load(object sender, EventArgs e)
        {
            display();
        }

        public void display()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Employee";
            cmd.ExecuteNonQuery();
            DataTable d1 = new DataTable();
            SqlDataAdapter s1 = new SqlDataAdapter(cmd);
            s1.Fill(d1);
            dataGridView1.DataSource = d1;

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEmployee a1 = new AddEmployee();
            a1.label22.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            a1.textBox1.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            a1.textBox2.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            a1.comboBox1.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            a1.dateTimePicker1.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            a1.comboBox2.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            a1.textBox3.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
            a1.comboBox3.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
            a1.textBox4.Text = this.dataGridView1.CurrentRow.Cells[8].Value.ToString();
            a1.textBox5.Text = this.dataGridView1.CurrentRow.Cells[9].Value.ToString();
            a1.textBox6.Text = this.dataGridView1.CurrentRow.Cells[10].Value.ToString();
            a1.textBox7.Text = this.dataGridView1.CurrentRow.Cells[11].Value.ToString();
            a1.textBox8.Text = this.dataGridView1.CurrentRow.Cells[12].Value.ToString();
            a1.button2.Enabled = false;
            a1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Employee where EmpId ='" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            display();
            MessageBox.Show("Employee is deleted successfully.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Employee where EmpId  like '" + textBox1.Text + "%' ", con);
                // MessageBox.Show(query);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

    }
}
