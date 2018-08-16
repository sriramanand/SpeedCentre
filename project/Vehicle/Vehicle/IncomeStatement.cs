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
    public partial class IncomeStatement : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");
        string qry1 = "SELECT  TOP 12 * FROM income ORDER BY month DESC";

        public IncomeStatement()
        {
            InitializeComponent();
        }

        private void IncomeStatement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.income' table. You can move, or remove it, as needed.
            this.incomeTableAdapter.Fill(this.DataSet1.income);
            exp_id.Enabled = false;
            textBox3.Enabled = false;
            textBox5.Enabled = false;
            c.Format = DateTimePickerFormat.Custom;
            c.CustomFormat = "MMMM yyyy";
            c.ShowUpDown = true;

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MMMM yyyy";
            dateTimePicker2.ShowUpDown = true;

            


            //exp_id.Text = function.getNextID("incomeID", "income", "IN", con);
            //DateTime tdate = DateTime.Now;
            //label22.Text = tdate.Date.ToString();
            string qry = "SELECT  TOP 12 * FROM income ORDER BY month desc";
            function.tableload(qry, con, dataGridView1, "income");
            //this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || c.Text == "")
            {
                MessageBox.Show("Fill the empty fields");
            }
            else
            {
                try
                {

                    if (MessageBox.Show("Do you want to add this row?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        string qry = "INSERT INTO income(month,revenue,sold,grossProfit,expenses,netIncome)VALUES('" + this.c.Text + "','" + textBox1.Text + "','" + float.Parse(textBox2.Text) + "','" + float.Parse(textBox3.Text) + "','" + float.Parse(textBox4.Text) + "','" + float.Parse(textBox5.Text) + "')";
                        function.executesqlquerey(qry, con);
                        // string q = "INSERT INTO Payment(PaymentID,OtherEx,Date,AmountPaid,TotalAmount,Discount,NetAmount,Balance)VALUES('" + function.getNextID("PaymentID", "Payment", "PA", con) + "','" + exp_id.Text + "','" + this.dateTimePicker1.Text + "','" + float.Parse(amounts.Text) * -1 + "','" + float.Parse(amounts.Text) * -1 + "',0,'" + float.Parse(amounts.Text) * -1 + "',0)";
                        // function.executesqlquerey(q, con);
                        //MessageBox.Show("Success!");
                        function.tableload(qry1, con, dataGridView1, "income");
                        empty();
                        //exp_id.Text = function.getNextID("incomeID", "income", "IN", con);

                    }
                    else
                    {
                        MessageBox.Show("Insert failed try again");
                    }
                }
                catch (SqlException ex)
                {
                    //var sqlException = ex.InnerException as System.Data.SqlClient.SqlException;

                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Cannot insert duplicate Month/Year.");

                    }
                    else
                    {
                        MessageBox.Show("Error while saving data.");
                    }

                }
                finally
                {
                    MessageBox.Show("Success!");
                }
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" ||  textBox4.Text == "")
            {
                MessageBox.Show("Fill the empty fields");
            }

            else
            {
                double a1 = Convert.ToDouble(textBox1.Text);
                double a2 = Convert.ToDouble(textBox2.Text);
                //double a3 = Convert.ToInt32(textBox3.Text);
                double a4 = Convert.ToDouble(textBox4.Text);

                double gross = a1 - a2;
                textBox3.Text = gross.ToString();
                double net = gross - a4;
                textBox5.Text = net.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String sql = "SELECT * FROM income WHERE month = '" + this.dateTimePicker2.Text + "' ";
            function.tableload(sql, con, dataGridView1, "income");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                exp_id.Text = selectedRow.Cells[0].Value.ToString();
                c.Text = selectedRow.Cells[1].Value.ToString();
                textBox1.Text = selectedRow.Cells[2].Value.ToString();
                textBox2.Text = selectedRow.Cells[3].Value.ToString();
                textBox3.Text = selectedRow.Cells[4].Value.ToString();
                textBox4.Text = selectedRow.Cells[5].Value.ToString();
                textBox5.Text = selectedRow.Cells[6].Value.ToString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Amount = float.Parse(amounts.Text);
            string qry = "UPDATE income SET month='" + this.c.Text + "',revenue='" + textBox1.Text + "',sold='" + textBox2.Text + "',grossProfit='" + textBox3.Text + "',expenses='" + textBox4.Text + "',netIncome='" + textBox5.Text + "' WHERE incomeID='" + exp_id.Text + "'";
            function.executesqlquerey(qry, con);
            //string q = "UPDATE payment SET AmountPaid='"+ float.Parse(amounts.Text)*-1 + "',TotalAmount='" + float.Parse(amounts.Text) * -1 + "',NetAmount='" + float.Parse(amounts.Text) * -1 + "' WHERE OtherEx='"+exp_id.Text+"' ";
            //Debug.WriteLine(q);
            //function.executesqlquerey(q,con);
            function.tableload(qry1, con, dataGridView1, "income");
            empty();
        }

        void empty()
        {
            exp_id.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this row?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string qry = "DELETE FROM income WHERE incomeID ='" + exp_id.Text + "'";
                function.executesqlquerey(qry, con);
                function.tableload(qry1, con, dataGridView1, "income");
                empty();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            IncomeReport i1 = new IncomeReport();
            i1.Show();
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

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
