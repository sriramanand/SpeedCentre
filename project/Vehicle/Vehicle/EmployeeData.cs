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
    public partial class EmployeeData : Form
    {

        private List<String> lBoxItems = new List<string>();


        public EmployeeData()
        {
            InitializeComponent();
            InitializeDataComponents();
            button2.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void InitializeDataComponents()
        {
            this.DesktopLocation = new Point(base.Location.X, base.Location.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            if (System.IO.File.Exists(textBox1.Text))
            {
                int num;
                if (int.TryParse(domainUpDown1.Text, out num) && num != 0)
                {
                    TimeSchedule.SchedulingDays = num;
                    TimeSchedule.FileLocation = textBox1.Text;
                    TimeSchedule.Shifts = new String[lBoxItems.Count];

                    for (int x = 0; x < lBoxItems.Count; x++)
                        TimeSchedule.Shifts[x] = lBoxItems.ToArray()[x];
                }
                else
                    MessageBox.Show("Not a valid work days", "Invalid Work Days");
            }
            else
            {
                MessageBox.Show("Not a valid file.", "Invalid File");
            }

            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
                lBoxItems.Add("Morning");
                lBoxItems.Add("Night");
                listBox1.DataSource = null;
                listBox1.DataSource = lBoxItems;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            try
            {
                lBoxItems.RemoveAt(selectedIndex);  // Remove the item in the List.
            }
            catch
            {
            }

            listBox1.DataSource = null;
            listBox1.DataSource = lBoxItems;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                String filename = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((filename = openFileDialog1.FileName) != null)
                        {
                            textBox1.Text = filename;
                        }
                        else
                        {
                            textBox1.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
                else
                {
                    textBox1.Text = "";
                }
            
        }

        private void EmployeeData_Load(object sender, EventArgs e)
        {
            domainUpDown1.Enabled = false;
        }
    }
}
