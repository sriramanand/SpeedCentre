using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Vehicle
{
    public partial class AddInventory : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");

        public AddInventory()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" ||  textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" )
            {
                MessageBox.Show("Fill the empty fields");
            }
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Product(ProductName,BrandName,Model,Price,QuantityAvailable) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + textBox4.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Item is entered");
                clear();
                display();
            }
       
        }

        public void display()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Product";
            cmd.ExecuteNonQuery();
            DataTable d1 = new DataTable();
            SqlDataAdapter s1 = new SqlDataAdapter(cmd);
            s1.Fill(d1);
            dataGridView1.DataSource = d1;
            
            con.Close();
            naming();
            
        }

        public void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            label5.Text = "";
        }

        public void naming()
        {
            dataGridView1.Columns[0].HeaderText = "Product No";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Brand Name";
            dataGridView1.Columns[3].HeaderText = "Model";
            dataGridView1.Columns[4].HeaderText = "Price";
            dataGridView1.Columns[5].HeaderText = "Quantity";
        }

        private void AddInventory_Load(object sender, EventArgs e)
        {
            display();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Product where ProductId='" + label5.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            display();
            MessageBox.Show("Item is deleted successfully.");
            clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Product set ProductName='" + textBox1.Text + "', BrandName='" + textBox2.Text + "', Model='" + textBox3.Text + "',Price='" + textBox5.Text + "', QuantityAvailable='" + textBox4.Text + "' where ProductId='" + label5.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            display();
            MessageBox.Show("Item is entered");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label5.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox4.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            button1.Enabled = false;
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                app.Visible = true;
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Records";

                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value != null)
                            {
                                worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                worksheet.Cells[i + 2, j + 1] = "";
                            }
                        }
                    }

                    //Getting the location and file name of the excel to save from user. 
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveDialog.FilterIndex = 2;

                    if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        workbook.SaveAs(saveDialog.FileName);
                        MessageBox.Show("Export Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    app.Quit();
                    workbook = null;
                    worksheet = null;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddSupplier a1 = new AddSupplier();
            this.Hide();
            a1.Show();
        }

    }
}
