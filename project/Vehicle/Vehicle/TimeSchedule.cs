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
    public partial class TimeSchedule : Form
    {

        public static List<Employee> employees = new List<Employee>();
        private static String fileLocation = "";
        private static double[] seRatio;
        private static int schedulingDays = 7;
        private static double[] predictedSales;
        private static List<String> positions = new List<string>();
        private EmployeeData sid = new EmployeeData();
        private static String[] shifts;

        public TimeSchedule()
        {
            InitializeComponent();
        }

        public static String[] Shifts
        {
            get { return shifts; }
            set { shifts = value; }
        }

        public static double[] PredictedSales
        {
            get { return predictedSales; }
            set { predictedSales = value; }
        }

        public static String FileLocation
        {
            get { return fileLocation; }
            set { fileLocation = value; }
        }

        public static String[] Positions
        {
            get
            {
                return positions.ToArray();
            }
        }

        public static double[] seRatioValue
        {
            get { return seRatio; }
            set { seRatio = value; }
        }

        public static int SchedulingDays
        {
            get { return schedulingDays; }
            set { schedulingDays = value; }
        }

        private void openCtrlOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sid.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                loadEmployees(fileLocation);
            }
        }

        public void resetEmployees()
        {
            if (MessageBox.Show("Are you sure you wish to reset the employee data?", "Reset Employees?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                employees = new List<Employee>();
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
            }
        }

        public void loadEmployees(String filename)
        {
            if (employees.Count != 0)
            {
                resetEmployees();
            }

            String line;
            System.IO.StreamReader sr = new System.IO.StreamReader(filename);

            while ((line = sr.ReadLine()) != null)
            {
                String[] employeeInfo = line.Split(',');
                String[] shift = new String[7];
                bool[] availability = new bool[7];
                for (int x = 2; x < employeeInfo.Length; x++)
                {
                    if (x < shift.Length + 2)
                        availability[x - 2] = Boolean.Parse(employeeInfo[x]);
                    else
                        shift[x - (7 + 2)] = employeeInfo[x];
                }

                if (!positions.Contains(employeeInfo[1]))
                    positions.Add(employeeInfo[1]);


                employees.Add(new Employee(employeeInfo[0], employeeInfo[1], availability, shift));

            }
            displayEmployees();
            sr.Close();
        }

        public void displayEmployees()
        {
            dataGridView1.Rows.Clear();
            foreach (Employee e in employees)
            {
                List<String> tempString = new List<String>();
                tempString.Add(e.getName());

                String positions = "";
                foreach (String s in e.getPositions())
                {
                    if (positions != "")
                    {
                        positions += "/" + s;
                    }
                    else
                        positions += s;
                }
                tempString.Add(positions);

                for (int x = 0; x < e.getAvail().Length; x++)
                {
                    if (e.getAvail()[x])
                        tempString.Add("Yes");
                    else
                        tempString.Add("No");
                }
                dataGridView1.Rows.Add(tempString.ToArray());
            }
        }

        public void createCustomColumns()
        {
            DateTime today = DateTime.Today;
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextTuesday = today.AddDays(daysUntilMonday);

            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("Name", "Name");
            for (int x = 0; x < schedulingDays; x++)
            {
                dataGridView2.Columns.Add(today.AddDays(x).ToShortDateString().ToString(), today.AddDays(x).ToShortDateString().ToString());
            }
            dataGridView2.Refresh();
        }

        public void displaySchedule(Employee[] employee)
        {

            foreach (Employee e in employee)
            {
                List<String> tempString = new List<String>();
                tempString.Add(e.getName());
                foreach (String s in e.getSchedule())
                {
                    tempString.Add(s);
                }
                dataGridView2.Rows.Add(tempString.ToArray());
            }
            dataGridView2.Refresh();
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (employees.Count != 0)
            {
                if (predictedSales == null)
                {
                    //DailySalesDialog dsd = new DailySalesDialog();
                    //if (dsd.ShowDialog(this) == DialogResult.OK)
                    {
                        Scheduler mainConfig = new Scheduler(seRatio, predictedSales, schedulingDays);
                        if (mainConfig.isSchedulePos())
                        {
                            int a = 0;
                            int b = 0;
                            Employee[] employee = mainConfig.createSched();//Employee[] employee = mainConfig.createSchedule2();
                            if (employee != null)
                            {
                                Console.WriteLine("Succeeded " + ++a + " times.");
                                createCustomColumns();
                                employees = new List<Employee>(employee);
                                displaySchedule(employees.ToArray());
                            }
                            else
                                MessageBox.Show("Failed " + ++b + " times.");
                        }
                        else
                            MessageBox.Show("Schedule Not Possible", "Not Possible");
                    }
                }
                else
                {
                    Scheduler mainConfig = new Scheduler(seRatio, predictedSales, schedulingDays);
                    if (mainConfig.isSchedulePos())
                    {
                        int a = 0;
                        int b = 0;
                        Employee[] employee = mainConfig.createSchedule2();
                        if (employee != null)
                        {
                            Console.WriteLine("Succeeded " + ++a + " times.");
                            createCustomColumns();
                            employees = new List<Employee>(employee);
                            displaySchedule(employees.ToArray());
                        }
                        else
                            MessageBox.Show("Failed " + ++b + " times.");
                    }
                    else
                        MessageBox.Show("Schedule Not Possible", "Not Possible");
                }

            }
            else
            {
                if (MessageBox.Show("No employees loaded. Would you like to load the employee file?", "No Employees Found",
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    sid = new EmployeeData();
                    if (sid.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        loadEmployees(fileLocation);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //NewEmployeeDialog ned = new NewEmployeeDialog();
            //if (ned.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            //{
            //    displayEmployees();
            //}
        }

        private void openCtrlOToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (sid.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                loadEmployees(fileLocation);
            }
        }

        private void saveCtrlSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String filename = null;
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.InitialDirectory = "C:\\";
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;

                if ((filename = sfd.FileName) != null)
                {
                    if (!System.IO.File.Exists(filename))
                        System.IO.File.Create(filename);
                    else
                    {
                        System.IO.File.Delete(filename);
                        System.IO.File.Create(filename);
                    }
                }
            }
            catch { MessageBox.Show("Could not select file."); }
        }

        private void saveEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = "C:\\";
            sfd.RestoreDirectory = true;
            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.AddExtension = true;

            if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                String filename = sfd.FileName;
                try
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename))
                    {

                        foreach (Employee e1 in employees)
                        {
                            String tempPositions = "";
                            foreach (String s in e1.getPositions())
                            {
                                if (tempPositions.Equals(""))
                                    tempPositions += s;
                                else
                                    tempPositions += "/" + s;
                            }

                            String tempString = e1.getName() + "," + tempPositions;
                            String tempWage = "";

                            //foreach (Double d in e1.getWage())
                            //{
                            //    if (tempWage.Equals(""))
                            //        tempWage += d;
                            //    else
                            //        tempWage += "/" + d;
                            //}

                            tempString += "," + tempWage;

                            for (int x = 0; x < e1.getAvail().Length; x++)
                                tempString += "," + e1.getAvail()[x];

                            foreach (String s in e1.findShift())
                                tempString += "," + s;

                            sw.WriteLine(tempString);
                        }

                        sw.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("File is already open");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                    for (int i = 0; i < dataGridView2.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1] = dataGridView2.Columns[i].HeaderText;
                    }
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView2.Columns.Count; j++)
                        {
                            if (dataGridView2.Rows[i].Cells[j].Value != null)
                            {
                                worksheet.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
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

        private void TimeSchedule_Load(object sender, EventArgs e)
        {

        }

    }
}
