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
    public partial class AddSupplier : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");

        public AddSupplier()
        {
            InitializeComponent();
            SupID.Enabled = false;
            
        }

        private void SupAdd_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (SupName.Text == "" || SupAddress.Text == "" || SupContact.Text == "" )
            {
                MessageBox.Show("Fill the empty fields");
            }
            else if (SupName.Text != "" && function.IsName(SupName.Text))
            {
                if (SupAddress.Text != "")
                {
                    if (SupContact.Text != "" && function.IsDigit(SupContact.Text))
                    {
                        string supid = SupID.Text.ToString();
                        string supname = SupName.Text.ToString();
                        string supaddress = SupAddress.Text.ToString();
                        string supcontact = SupContact.Text.ToString();

                        string query = "insert into Supplier(SupplierID,SupplierName,Address,Contact,status)values('" + supid + "','" + supname + "','" + supaddress + "','" + supcontact + "','Available')";
                        //string query1 = "insert into SupplierCredits(SupplierID,CreditBal)values('" + supid + "','" + 0 + "')";
                        function.insertQuery(query);
                        //function.insertQuery(query1);

                        string query2 = "select SupplierID,SupplierName,Address,Contact from Supplier "
                                       + "where status='Available' ";
                        function.loadTableQuery(Supplierdgv, query2);
                        SupID.Text = function.getNextID("SupplierID", "Supplier", "SP");
                        SupName.Clear();
                        SupAddress.Clear();
                        SupContact.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Please type the Contact Correctly");
                        SupContact.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Please type the Address Correctly");
                    SupAddress.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please type the Name Correctly");
                SupName.Clear();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SupName.Text != "" && function.IsName(SupName.Text))
            {
                if (SupAddress.Text != "")
                {
                    if (SupContact.Text != "" && function.IsDigit(SupContact.Text))
                    {
                        string supid = SupID.Text.ToString();
                        string supname = SupName.Text.ToString();
                        string supaddress = SupAddress.Text.ToString();
                        string supcontact = SupContact.Text.ToString();
                        string sts = "Available";

                        string query = "insert into Supplier(SupplierID,SupplierName,Address,Contact,status)values('" + supid + "','" + supname + "','" + supaddress + "','" + supcontact + "','" + sts + "')";
                        //string query1 = "insert into SupplierCredits(SupplierID,CreditBal)values('" + supid + "',0)";
                        function.insertQuery(query);
                        //function.insertQuery(query1);

                        //AddNewGrocer ang = new AddNewGrocer();

                        //ang.Show();
                        //this.Hide();


                    }
                    else
                    {
                        MessageBox.Show("Please type the Contact Correctly");
                        SupContact.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Please type the Address Correctly");
                    SupAddress.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please type the Name Correctly");
                SupName.Clear();

            }
        }

        private void AddSupplier_Load(object sender, EventArgs e)
        {
            SupID.Text = function.getNextID("SupplierID", "Supplier", "SP");

            string query2 = "select SupplierID,SupplierName,Address,Contact from Supplier "
                                       + "where status='Available' ";
            function.loadTableQuery(Supplierdgv, query2);
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
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Supplier set SupplierName='" + SupName.Text + "', Address='" + SupAddress.Text + "', Contact='" + SupContact.Text + "' where SupplierID='" + SupID.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            display();
            MessageBox.Show("Item is entered");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Supplier where SupplierID='" + SupID.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            display();
            MessageBox.Show("Item is deleted successfully.");
            clear();
        }

        public void display()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Supplier";
            cmd.ExecuteNonQuery();
            DataTable d1 = new DataTable();
            SqlDataAdapter s1 = new SqlDataAdapter(cmd);
            s1.Fill(d1);
            Supplierdgv.DataSource = d1;

            con.Close();
            naming();

        }

        public void naming()
        {
            Supplierdgv.Columns[0].HeaderText = "Supplier ID";
            Supplierdgv.Columns[1].HeaderText = "Supplier Name";
            Supplierdgv.Columns[2].HeaderText = "Address";
            Supplierdgv.Columns[3].HeaderText = "Contact No";
        }

        public void clear()
        {
            SupID.Text = "";
            SupName.Text = "";
            SupAddress.Text = "";
            SupContact.Text = "";
            
        }

        private void Supplierdgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SupID.Text = this.Supplierdgv.CurrentRow.Cells[0].Value.ToString();
            SupName.Text = this.Supplierdgv.CurrentRow.Cells[1].Value.ToString();
            SupAddress.Text = this.Supplierdgv.CurrentRow.Cells[2].Value.ToString();
            SupContact.Text = this.Supplierdgv.CurrentRow.Cells[3].Value.ToString();
           
        }
    }
}
