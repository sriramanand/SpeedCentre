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
    public partial class AddSales : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");
        string qry1 = "SELECT * FROM PurchaseProduct";

        private string tempPID;
        private double tempTotal;
        private string tempUnitPrice;
        private string tempItem;
        private DateTime tempDate;
        private double count = 0;
        public static string tempBuyerId;
        private string tempAvailable;
        private int tempAvailableInt;

        private string tempCost;
        private double tempCostTotal;
        private double unitCount = 0;

        public AddSales()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainForm m1 = new MainForm();
            this.Hide();
            m1.Show();

        }

        private void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.RowCount > 1)
            //{
            //    DialogResult dialogResult = MessageBox.Show("Do you wish to confirm the order?", "Confirm Order", MessageBoxButtons.YesNo);

            //    if (dialogResult == DialogResult.Yes)
            //    {
                    
            //        float totalBill = float.Parse(txtGrandTotal.Text);
            //        double totalCost = this.unitCount;

            //        try
            //        {
            //            con.Open();
            //            SqlCommand cmd = con.CreateCommand();
            //            cmd.CommandType = CommandType.Text;
            //            cmd.CommandText = "INSERT INTO Orders (CusFName, CusLName, NIC, ProductId, BrandName, Model, QuantityAvailable, price,PurDate,CusAddress,CusLContact,CusMContact,CusEmail) VALUES ("
            //               + "'" + this.textBox1 + "', "
            //               + "'" + this.textBox3 + "', "
            //               + "'" + this.textBox2 + "', "
            //               + "'" + this.textBox8 + "', "
            //               + "'" + this.comboBox1 + "', "
            //               + "'" + this.comboBox2 + "', "
            //               + "'" + this.txtQuantity + "', "
            //               + "'" + totalBill + "', "
            //               + "'" + this.lblDate + "', "
            //               + "'" + this.textBox4 + "', "
            //               + "'" + this.textBox5 + "', "
            //               + "'" + this.textBox6 + "', "
            //               + "'" + this.textBox7 + "', "
            //               + totalBill + ","
            //               + totalCost + ")";
            //            cmd.ExecuteNonQuery();
            //            con.Close();
            //            MessageBox.Show("Item is entered");

                            



            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(this, "Error", "Invoice duplicating is not allowed\n" + ex);

            //        }
            //    }
            //}

            //else
            //{
            //    MessageBox.Show("Please add some item before checking out.");
            //}
        }

        private void AddSales_Load(object sender, EventArgs e)
        {
            this.tempDate = DateTime.Now;
            lblDate.Text = this.tempDate.ToShortDateString();
            LoadComboBoxes("SELECT DISTINCT ProductName FROM Product", comboItems, "ProductName");
            LoadComboBoxes("SELECT DISTINCT BrandName FROM Product", comboBox1, "BrandName");
            LoadComboBoxes("SELECT DISTINCT Model FROM Product", comboBox2, "Model");
            //txtInvoiceNo.Text = this.tempOrderId = function.getNextID("PPId", "PurchaseProduct", "INV");
            lblQuantityTotal.Text = "0.00";
            string query2 = "select * from PurchaseProduct ";
            function.loadTableQuery(dataGridView1, query2);
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            /*if (txtQuantity.Text == "0" || String.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show(this, "Error", "Select quantity");
            }

            else if (Int32.Parse(txtQuantity.Text) > this.tempAvailableInt)
            {
                MessageBox.Show(this, "Enter a valid quantity");
            }
            else
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = this.textBox8.Text;
                row.Cells[1].Value = this.comboItems.Text;
                row.Cells[2].Value = this.comboBox1.Text;
                row.Cells[3].Value = this.comboBox2.Text;
                row.Cells[4].Value = this.tempUnitPrice;
                row.Cells[5].Value = txtQuantity.Text;
                row.Cells[6].Value = this.tempTotal;
                dataGridView1.Rows.Add(row);

                this.count = this.count + this.tempTotal;
                this.unitCount = this.unitCount + this.tempCostTotal;
                txtGrandTotal.Text = string.Concat(this.count.ToString(), ".00");
                ClearAfterAdd();

            }*/

            //float totalBill = float.Parse(txtGrandTotal.Text);
            //double totalCost = this.unitCount;

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO PurchaseProduct (CusFName, CusLName, NIC, ProductId, ProductName, BrandName, Model, QuantityAvailable, price,PurDate,CusAddress,CusLContact,CusMContact,CusEmail) VALUES ('" + this.textBox1.Text + "', '" + this.textBox3.Text + "','" + this.textBox2.Text + "','" + this.textBox8.Text + "', '"+this.comboItems.Text+"','" + this.comboBox1.Text + "', '" + this.comboBox2.Text + "', '" + txtQuantity.Text + "', '" + this.lblQuantityTotal.Text + "', '" + this.lblDate.Text + "', '" + this.textBox4.Text + "', '" + this.textBox5.Text + "','" + this.textBox6.Text + "', '" + this.textBox7.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Item is entered");
            string query2 = "select * from PurchaseProduct ";
            function.loadTableQuery(dataGridView1, query2);
        }

        public void LoadComboBoxes(string query, ComboBox combo, String colName)
        {
            try
            {
                combo.Items.Clear();
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter(query, con);
                DataSet ds1 = new DataSet();
                sda1.Fill(ds1, "Table");
                DataTable dt1 = ds1.Tables["Table"];

                foreach (DataRow dr in dt1.Rows)
                {
                    combo.Items.Add(dr[colName].ToString());
                }
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox2.SelectedIndex != -1)
            {
                txtQuantity.ResetText();

                string choice = null;
                choice = comboBox2.Text;
                this.tempItem = choice;

                txtMRP.Text = this.tempUnitPrice = getValue("Product", "Model", choice, "Price");
                this.tempCost = getValue("Product", "Model", choice, "Price");
                txtAvailable.Text = this.tempAvailable = getValue("Product", "Model", choice, "QuantityAvailable");
                this.tempAvailableInt = Int32.Parse(this.tempAvailable);
                this.tempPID = getValue("Product", "ProductName", choice, "ProductId");
            }
        }

        public  String getValue(String table, String col, String choice, String wanted)
        {
            String query_ = "SELECT * FROM " + table + " WHERE " + col + "='" + choice + "'";
            String output = "";
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query_, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                output = dt.Rows[0][wanted].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
            return output;
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            string qty = (txtQuantity.Text);

            if (String.IsNullOrEmpty(qty))
            {
                lblQuantityTotal.Text = "0.00";
            }
            else
            {
                int qtyInt = 0;
                bool yes = int.TryParse(qty, out qtyInt);

                if (yes)
                {
                    double unitCost = Double.Parse(txtMRP.Text);
                    this.tempTotal = unitCost * (Int32.Parse(qty));
                    lblQuantityTotal.Text = string.Concat(tempTotal.ToString(), ".00");

                    double buyingCost = Double.Parse(this.tempCost);
                    this.tempCostTotal = buyingCost * (Int32.Parse(qty));
                }
                else
                {
                    MessageBox.Show(this, "Invalid input", "Enter valid quantity amount");
                }
            }
        }

        private void ClearAfterAdd()
        {
            txtMRP.Text = "";
            textBox8.Text = "";
            //comboBox1.Text = "";
            //comboBox2.Text = "";
            txtAvailable.Text = "";
            txtQuantity.Text = "";
            lblQuantityTotal.Text = "0";
           LoadComboBoxes("SELECT * FROM Product", comboItems, "ProductName");
           LoadComboBoxes("SELECT * FROM Product", comboBox1, "BrandName");
           LoadComboBoxes("SELECT * FROM Product", comboBox2, "Model");
        }

        private void btnRemoveCart_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you wish to remove this item from cart?", "Removing item from cart", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string qry = "DELETE FROM PurchaseProduct WHERE PPId ='" + txtInvoiceNo.Text + "'";
                    function.executesqlquerey(qry, con);
                    function.tableload(qry1, con, dataGridView1, "PurchaseProduct");
                    
                }
            }
        }

        private void comboItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            string choice = null;
            choice = comboItems.Text;
            this.tempItem = choice;
            textBox8.Text = this.tempUnitPrice = getValue("Product", "ProductName", choice, "ProductId");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtInvoiceNo.Text = selectedRow.Cells[0].Value.ToString();
                textBox1.Text = selectedRow.Cells[1].Value.ToString();
                textBox3.Text = selectedRow.Cells[2].Value.ToString();
                textBox2.Text = selectedRow.Cells[3].Value.ToString();
                textBox8.Text = selectedRow.Cells[4].Value.ToString();
                comboItems.Text = selectedRow.Cells[5].Value.ToString();
                comboBox1.Text = selectedRow.Cells[6].Value.ToString();
                comboBox2.Text = selectedRow.Cells[7].Value.ToString();
                txtQuantity.Text = selectedRow.Cells[8].Value.ToString();
                lblQuantityTotal.Text = selectedRow.Cells[9].Value.ToString();
                lblDate.Text = selectedRow.Cells[10].Value.ToString();
                textBox4.Text = selectedRow.Cells[11].Value.ToString();
                textBox5.Text = selectedRow.Cells[12].Value.ToString();
                textBox6.Text = selectedRow.Cells[13].Value.ToString();
                textBox7.Text = selectedRow.Cells[14].Value.ToString();
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

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
