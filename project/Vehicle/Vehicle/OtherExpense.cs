using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vehicle
{
    public partial class OtherExpense : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");
        string qry1 = "SELECT  TOP 10 * FROM otherExpenses ORDER BY date DESC";
        public static float Amount;

        public OtherExpense()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (description.Text == "" || dateTimePicker1.Text == "" || amounts.Text == "" )
            {
                MessageBox.Show("Fill the empty fields");
            }

            else if (MessageBox.Show("Do you want to add this row?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                    string qry = "INSERT INTO otherExpenses(expID,description,date,amount)VALUES('" + exp_id.Text + "','" + description.Text + "','" + this.dateTimePicker1.Text + "','" + float.Parse(amounts.Text) + "')";
                    function.executesqlquerey(qry, con);
                   // string q = "INSERT INTO Payment(PaymentID,OtherEx,Date,AmountPaid,TotalAmount,Discount,NetAmount,Balance)VALUES('" + function.getNextID("PaymentID", "Payment", "PA", con) + "','" + exp_id.Text + "','" + this.dateTimePicker1.Text + "','" + float.Parse(amounts.Text) * -1 + "','" + float.Parse(amounts.Text) * -1 + "',0,'" + float.Parse(amounts.Text) * -1 + "',0)";
                   // function.executesqlquerey(q, con);
                    MessageBox.Show("Succes!");
                    function.tableload(qry1, con, dataGridView1, "otherExpenses");
                    empty();
                    exp_id.Text = function.getNextID("expID", "otherExpenses", "EX", con);

            }
            else
            {
                    MessageBox.Show("Insert failed try again");
            }
            
        }

        void empty()
        {
            description.Text = string.Empty;
            
            amounts.Text = string.Empty;
            
        }

        private void OtherExpense_Load(object sender, EventArgs e)
        {
            exp_id.Text = function.getNextID("expID", "otherExpenses", "EX", con);
            DateTime tdate = DateTime.Now;
            label11.Text = tdate.Date.ToString();
            string qry = "SELECT  TOP 10 * FROM otherExpenses ORDER BY date desc";
            function.tableload(qry, con, dataGridView1, "otherExpenses");
        }

        private void button2_Click(object sender, EventArgs e)
        {
                    Amount = float.Parse(amounts.Text);
                    string qry = "UPDATE otherExpenses SET description='" + description.Text + "',date='" + this.dateTimePicker1.Text + "',amount='" + Amount + "' WHERE expID='" + exp_id.Text + "'";
                    function.executesqlquerey(qry, con);
                    //string q = "UPDATE payment SET AmountPaid='"+ float.Parse(amounts.Text)*-1 + "',TotalAmount='" + float.Parse(amounts.Text) * -1 + "',NetAmount='" + float.Parse(amounts.Text) * -1 + "' WHERE OtherEx='"+exp_id.Text+"' ";
                    //Debug.WriteLine(q);
                    //function.executesqlquerey(q,con);
                    function.tableload(qry1, con, dataGridView1, "otherExpenses");
                    empty();
                    exp_id.Text = function.getNextID("expID", "otherExpenses", "EX", con);
                
            }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                exp_id.Text = selectedRow.Cells[0].Value.ToString();
                description.Text = selectedRow.Cells[1].Value.ToString();
                dateTimePicker1.Text = selectedRow.Cells[2].Value.ToString();
                amounts.Text = selectedRow.Cells[3].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this row?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string qry = "DELETE FROM otherExpenses WHERE expID ='" + exp_id.Text + "'";
                function.executesqlquerey(qry, con);
                function.tableload(qry1, con, dataGridView1, "otherExpenses");
                empty();
                exp_id.Text = function.getNextID("expID", "otherExpenses", "EX", con);
            }
        
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            string q = "select * from otherExpenses WHERE description LIKE '" + '%' + search.Text + '%' + "'";
            function.tableload(q, con, dataGridView1, "otherExpenses");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String sql = "SELECT * FROM otherExpenses WHERE date BETWEEN '" + this.dateTimePicker2.Text + "' AND '" + this.dateTimePicker3.Text + "'";
            function.tableload(sql, con, dataGridView1, "otherExpenses");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
