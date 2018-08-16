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
    public partial class Schedule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");
        string qry1 = "SELECT  * FROM schedule ";

        public Schedule()
        {
            InitializeComponent();
            textBox1.Enabled = false;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            dateTimePicker2.Text = monthCalendar1.SelectionStart.ToString();
        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            //dateTimePicker2.Enabled = false;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MMMM dd, yyyy - dddd";
            //dateTimePicker3 = new DateTimePicker();
            dateTimePicker3.Format = DateTimePickerFormat.Time;
            dateTimePicker3.ShowUpDown = true;

            string qry = "SELECT  * FROM schedule";
            function.tableload(qry, con, dataGridView1, "schedule");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || textBox2.Text == "" || dateTimePicker2.Text == "" || dateTimePicker3.Text == "" || textBox4.Text == "" )
            {
                MessageBox.Show("Fill the empty fields");
            }
            else if (MessageBox.Show("Do you want to add this row?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string qry = "INSERT INTO schedule(category,title,date,time,notes)VALUES('" + comboBox1.Text + "','" + textBox2.Text + "','" + this.dateTimePicker2.Text + "','" + this.dateTimePicker3.Text + "','"+textBox4.Text+"')";
                function.executesqlquerey(qry, con);
                // string q = "INSERT INTO Payment(PaymentID,OtherEx,Date,AmountPaid,TotalAmount,Discount,NetAmount,Balance)VALUES('" + function.getNextID("PaymentID", "Payment", "PA", con) + "','" + exp_id.Text + "','" + this.dateTimePicker1.Text + "','" + float.Parse(amounts.Text) * -1 + "','" + float.Parse(amounts.Text) * -1 + "',0,'" + float.Parse(amounts.Text) * -1 + "',0)";
                // function.executesqlquerey(q, con);
                MessageBox.Show("Success!");
                function.tableload(qry1, con, dataGridView1, "schedule");
                empty();

            }
            else
            {
                MessageBox.Show("Insert failed try again");
            }
            
        }

        void empty()
        {
            comboBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            dateTimePicker2.Text = string.Empty;
            dateTimePicker2.Text = string.Empty;
            textBox4.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string qry = "UPDATE schedule SET category='" + comboBox1.Text + "',title='" + textBox2.Text + "',date='" + this.dateTimePicker2.Text + "',time='" + this.dateTimePicker3.Text + "',notes='" + textBox4.Text + "' WHERE scheduleID ='" + textBox1.Text + "'";
            function.executesqlquerey(qry, con);
            //string q = "UPDATE payment SET AmountPaid='"+ float.Parse(amounts.Text)*-1 + "',TotalAmount='" + float.Parse(amounts.Text) * -1 + "',NetAmount='" + float.Parse(amounts.Text) * -1 + "' WHERE OtherEx='"+exp_id.Text+"' ";
            //Debug.WriteLine(q);
            //function.executesqlquerey(q,con);
            function.tableload(qry1, con, dataGridView1, "schedule");
            empty();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                textBox1.Text = selectedRow.Cells[0].Value.ToString();
                comboBox1.Text = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[2].Value.ToString();
                monthCalendar1.Text = selectedRow.Cells[3].Value.ToString();
                dateTimePicker3.Text = selectedRow.Cells[4].Value.ToString();
                textBox4.Text = selectedRow.Cells[5].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this row?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string qry = "DELETE FROM schedule WHERE scheduleID ='" + textBox1.Text + "'";
                function.executesqlquerey(qry, con);
                function.tableload(qry1, con, dataGridView1, "schedule");
                empty();
                
            }
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
        
    }
}
