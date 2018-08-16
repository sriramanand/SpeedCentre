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
    public partial class AddData : Form
    {
        public AddData()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Fill the empty fields");
            }
            else
            {
                output o1 = new output();
                o1.start(label6, textBox1, textBox2, textBox3);
                Image i = Image.FromFile("out.png");                    // read in image
                pictureBox5.Size = new Size(i.Width, i.Height);         //set label to correct size
                pictureBox5.Image = i;                                  // put image on label
                flowLayoutPanel1.Controls.Add(pictureBox5);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label6.Text = "";
            pictureBox5.Image = null;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainForm m1 = new MainForm();
            this.Hide();
            m1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void AddData_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
