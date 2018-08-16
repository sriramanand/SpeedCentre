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
    public partial class AddEmployee : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");

        public AddEmployee()
        {
            InitializeComponent();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainForm m1 = new MainForm();
            this.Hide();
            m1.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeeTable e1 = new EmployeeTable();
            this.Hide();
            e1.Show();
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || dateTimePicker1.Text == "")
            {
                MessageBox.Show("Fill the empty fields");
            }
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Employee(EmpFName,EmpLName,Gender,EmpDOB,Age,NIC,EmpRole,EmpAddress,EmpLContact,EmpMContact,EmpEmail,EmpBasicSalary) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + theDate + "','" + comboBox2.Text + "','" + textBox3.Text + "','" + comboBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Item is entered");
                //clear();
                //display();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Fill the empty fields");
            }
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Employee set EmpFName ='" + textBox1.Text + "', EmpLName ='" + textBox2.Text + "', Gender ='" + comboBox1.Text + "',EmpDOB ='" + theDate + "', Age ='" + comboBox2.Text + "', NIC  ='" + textBox3.Text + "', EmpRole  ='" + comboBox3.Text + "',EmpAddress  ='" + textBox4.Text + "', EmpLContact  ='" + textBox5.Text + "', EmpMContact  ='" + textBox6.Text + "', EmpEmail  ='" + textBox7.Text + "',EmpBasicSalary  ='" + textBox8.Text + "' where EmpId ='" + label22.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Item is updated.");
            }
        }
    }
}
