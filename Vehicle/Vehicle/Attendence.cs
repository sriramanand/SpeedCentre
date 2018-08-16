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
    public partial class Attendence : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");
        SqlCommand cmd;

        public Attendence()
        {
            InitializeComponent();
            DisplayData();
        }

        public void Clear2()
        {
            //CLEAR THE COMPONANTS 
            eid.Text = string.Empty;
            pos.Text = string.Empty;
            sft.Text = String.Empty;
            name.Text = string.Empty;
            inntime.Text = null;
            dat.Text = null;

        }
        public void Clear()
        {
            //CLEAR THE COMPONANTS 
            eiod.Text = string.Empty;
            name1.Text = string.Empty;
            intm.Text = null;
            outime.Text = null;
            wdu.Text = null;
            min.Text = null;

        }

        private void gettime_Click(object sender, EventArgs e)
        {
            dat.Text = DateTime.Now.ToString("d/M/yyyy");

            inntime.Text = DateTime.Now.ToString("HH:mm:ss tt");
            gettime.Enabled = false;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            string edid = eid.Text;
            String ps = pos.Text;
            DateTime inn = DateTime.Parse(inntime.Text);

            string fn = name.Text;
            string attStatus = "Present";

            if (eid.Text == "" || pos.Text == ""|| sft.Text == "" || inntime.Text == "" || dat.Text == "" || name.Text == "")
            {
                MessageBox.Show("All Values Should Be Given!");
            }
            else
            {

                try
                {

                    String query = "select Dates,EmpID from Attendance where EmpID='" + eid.Text + "' and Dates = '" + theDate + "'";
                    if (!function.hasData(query, con))
                    {

                        string Query = "INSERT INTO Attendance(EmpID,EmpFName,InTime,Shifts,EmpRole,Dates,AttendanceStatus)VALUES('" + edid + "','" + fn + "','" + inn + "','" + sft.Text + "','" + ps + "','" + theDate + "','" + attStatus + "')";
                        function.adddetails(Query, con);
                        Debug.WriteLine(Query);
                        MessageBox.Show("submission Success!");
                        Clear2();
                        gettime.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Choose The Correct Employee!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
                DisplayData();
            }
        }

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select  * from Attendance", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            Clear2();
            gettime.Enabled = true;
        }

        private void eid_MouseLeave(object sender, EventArgs e)
        {
            try
            {

                String query = "select EmpFName ,EmpRole  from Employee where EmpID='" + eid.Text + "'";
                function.getcol(name, "EmpFName", query, con);
                function.getcol(pos, "EmpRole", query, con);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            con.Close();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            DisplayData();
            label43.Visible = false;
            noOfpresent.Visible = false;
        }

        private void Attendence_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void button3_Click(object sender, EventArgs e)
        {



            outime.Text = DateTime.Now.ToString("HH:mm:ss ");
            String startTime = outime.Text = DateTime.Now.ToString("HH:mm:ss ");
            String endTime = intm.Text.ToString();          //for working hours

            TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            wdu.Text = duration.ToString("hh");
            min.Text = duration.ToString("mm");

            button3.Enabled = false;

            int wdhour = int.Parse(wdu.Text.ToString());
            if (wdhour <= 9)
            {
                OT.Text = "0";
            }
            else if (wdhour > 9)
            {
                int oth = wdhour - 9;
                OT.Text = oth.ToString();
            }

            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime outtime = DateTime.Parse(outime.Text);
            DateTime intime = DateTime.Parse(intm.Text);

            String wdud = wdu.Text;
            String wmin = min.Text;


            try
            {


                String query = "select OutTime from Attendance where EmpID='" + eiod.Text + "' and Dates = '" + DateTime.Now.ToString("yyyy/MM/dd") + "'";
                Debug.WriteLine(query);
                if (function.getVal("OutTime", query, con) == "")
                {

                    con.Open();
                    string Query = " UPDATE Attendance set OutTime = '" + outtime + "',duration = '" + wdud + ':' + wmin + "',OverTime='" + OT.Text + "' where EmpID = '" + eiod.Text + "' and Dates = '" + DateTime.Now.ToString("yyyy/MM/dd") + "'";
                    Debug.WriteLine(Query);
                    cmd = new SqlCommand(Query, con);
                    int N = cmd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("submission Success!");
                    Clear2();

                    DisplayData();
                    button3.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Choose The Correct Employee!");



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void eid_Leave(object sender, EventArgs e)
        {
            
        }

        private void eiod_MouseLeave(object sender, EventArgs e)
        {
            try
            {

                String query = "select EmpFName,InTime from Attendance where EmpID='" + eiod.Text + "'";
                function.getcol(name1, "EmpFName", query, con);
                function.getcol(intm, "InTime", query, con);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            con.Close();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {


                string Query = "select * from Attendance where Dates = '" + this.dateTimePicker2.Text + "' ";
                function.DisplayData(Query, dataGridView1, con);

                string stat = "Present";
                string qry = "select count(EmpID) as count from Attendance where AttendanceStatus='" + stat + "' and Dates = '" + this.dateTimePicker2.Text + "' ";

                function.getcount(noOfpresent, "count", qry, con);  //count the working employees
                label43.Visible = true;
                noOfpresent.Visible = true;

            }
            catch (Exception ex)
            {
                Console.Write(ex);
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
