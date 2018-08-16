using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    class Scheduler
    {
        private static Employee[] emp;                               //employee
        private Random rand = new Random();
        private String[] sTime = { "Morning", "Night" };           //shifts time
        private int[] hour;                                        //week hours
        private String[] posit;                                  //all position
        private double[] ra;                                       //fixed ratio
        private double[] predic;                                     //predic of sales
        private static int workWeek = 0;                                //work week
        private static HashSet<String> position = new HashSet<String>();       //positions
        
        public Scheduler(double[] ratio, double[] predictionSales, int w)           //initiate scheduler
        {
            this.ra = ratio;
            Scheduler.setWorkWeek(w);
            predic = predictionSales;
            emp = TimeSchedule.employees.ToArray();
            sTime = TimeSchedule.Shifts;
            resetSchedule();
            setPosit();
            this.hour = getHours();
        }

        public static Employee[] Group
        {
            get { return emp; }
            set { emp = value; }
        }

        public bool isSchedulePos()                                                     //is schedule possible
        {
            for (int a = 0; a < this.posit.Length; a++)
            {
                for (int b = 0; b < empPerShift().GetLength(0); b++)
                {
                    int totalShifts = 0;

                    foreach (Employee e in emp)
                    {
                        if (e.getPositions().Contains(this.posit[a]))
                        {
                            int[] y = e.maxShifts();
                            
                            totalShifts += e.maxShifts()[b];

                        }
                    }
                    if (totalShifts < empPerShift()[b,a])
                        return false;
                }
            }
            return true;
        }

        public double[] seRatio()                                    //get set ratio
        {
            return this.ra;
        }

        public void setSeRatio(double[] seRatio)
        {
            this.ra = seRatio;
        }

        private int[] getHours()                                   //get week hours
        {
            ra = new double[] { 50, 100 };                               //ratio
            predic = new double[] { 150, 200, 201, 202, 250, 300, 350 }; //predic sales
            int[] shifts = new int[2];                                  //week shifts
            for (int a = 0; a < 2; a++)
            {
                shifts[a] = 0;
                for (int y = 0; y < 7; y++)
                {
                    shifts[a] += (int)Math.Round(predic[y] / ra[a]);
                }
                shifts[a] = (int)Math.Round(shifts[a] / getEmp(posit[a]));
            }
            return shifts;
        }

        public double getEmp(String allPositions2)                          //get people for shifts
        {
            int pCount = 0;                                          //position count
            foreach (Employee e in emp)
                foreach (String s in e.getPositions())
                    if (s.Contains(allPositions2))
                        pCount++;
            return pCount;
        }

        private void setPosit()                                                  //set all positions
        {
            String temp = "";                                                    //temp posit
            foreach (Employee e in emp)
            {
                foreach (String s in e.getPositions())
                {
                    if (!temp.Contains(s) && !temp.Equals(""))
                        temp += "," + s;
                    else if (!temp.Contains(s))
                        temp += s;
                }
            }
            posit = temp.Split(',');
        }

        public void resetSchedule()                                                     //reset schedule
        {
            foreach (Employee e in emp)
            {
                e.reSche();
            }
        }

        public void displaySched()                                        //display schedule by position
        {
            foreach (String s in position)
            {
                Console.WriteLine("\n\nName                    Monday             Tuesday             Wednesday             Thursday             Friday             Saturday             Sunday");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------");

                foreach (Employee e in emp)
                {
                    if (e.getPositions().Contains(s))
                    {
                        Console.Write("%n%-24s", e.getName());
                        for (int x = 0; x < getWorkWeek(); x++)
                        {
                            if (e.getSchedule()[x].Contains("" + s.ToCharArray()[0]))
                                Console.Write("%-20s", e.getSchedule()[x]);
                            else
                                Console.Write("%-20s", "None");
                        }
                    }
                }
            }
        }

        public void displaySchedule()           //display schedule
        {
            Console.WriteLine("Name                    Monday             Tuesday             Wednesday             Thursday             Friday             Saturday             Sunday");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------");

            for (int a = 0; a < emp.Length; a++)
            {
                Console.Write("%-24s%-19s%-20s%-22s%-21s%-19s%-21s%-19s%n", emp[a].getName(), emp[a].getSchedule()[0],
                        emp[a].getSchedule()[1], emp[a].getSchedule()[1],
                        emp[a].getSchedule()[3], emp[a].getSchedule()[4],
                        emp[a].getSchedule()[5], emp[a].getSchedule()[6]);
            }
        }

        public int[] getWeek()              //get week by volume
        {

            
            double[] wSales = new double[workWeek];      //week sales
            double[] un = new double[workWeek];       //unsorted
            int[] vOrder = new int[workWeek];          //order of volume

            wSales = predic;
            un = predic;
            
            Array.Sort<double>(wSales);
            int index = 0;

            for (int a = 0; a < wSales.Length; a++)
            {
                for (int b = 0; b < un.Length; b++)
                {
                    if (wSales[a] == un[b])
                    {
                        vOrder[index] = b;
                        index++;
                    }
                }
            }
            return vOrder;
        }

        public Employee[] createSched()                      //create schedule by week volume
        {
            if (isSchedulePos())
            {
                EmployeeQueue empQue = new EmployeeQueue(emp);
                Queue<Employee> que = empQue.queueRandom(rand);
                long brokenCount = 0;

                foreach (int i in getWeek())
                {
                    for (int x = 0; x < posit.Length; x++)
                    {
                        for (int y = 0; y < sTime.Length; y++)
                        {
                            for (int z = 0; z < empPerShift()[i, x]; z++)
                            {
                                bool shiftFilled = false;
                                bool checkDoubles = false;
                                do
                                {
                                    Employee tempEmp = que.Dequeue();
                                    if (tempEmp.getName().Equals("Alex Four"))
                                    {
                                        shiftFilled = false;
                                        int a = 1;
                                        a++;
                                    }
                                    if (tempEmp.isWork(i, sTime[y], posit[x], checkDoubles, hour[x]))
                                    {

                                        foreach (Employee e in emp)
                                        {
                                            if (e.getName().Equals(tempEmp.getName()))
                                            {
                                                e.setSche(i, sTime[y], posit[x]);
                                                shiftFilled = true;
                                            }
                                        }
                                    }
                                    if (que.Count == 0 && !shiftFilled)
                                    {
                                        brokenCount++;
                                        if (brokenCount > 1000)
                                        {
                                            return null;
                                        }
                                        que = empQue.queueRandom(rand);
                                        checkDoubles = true;
                                        int b = 0;
                                        int totalEmployeesPosition = 0;
                                        foreach (Employee e in emp)
                                        {
                                            int a = 0;
                                            foreach (String positions in e.getPositions())
                                            {
                                                if (positions.Equals(posit[x]))
                                                {
                                                    totalEmployeesPosition++;
                                                    foreach (String s in e.getSchedule())
                                                    {
                                                        if (!s.Equals("None"))
                                                        {
                                                            a += s.Split('/').Length;
                                                            if (a == hour[x])
                                                                b++;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (b == totalEmployeesPosition)
                                        {
                                            hour[x]++;
                                        }
                                    }
                                    if (shiftFilled)
                                        que = empQue.queueRandom(rand);


                                } while (!shiftFilled);
                            }
                        }
                    }
                }
            }
            return emp;
        }

        public Employee[] createSchedule2()                     //create the schedule
        {
            if (isSchedulePos())
            {
                EmployeeQueue empQue = new EmployeeQueue(emp);
                Queue<Employee> que = empQue.queueRandom(rand);
                long brokenCount = 0;

                for (int w = 0; w < getWorkWeek(); w++)
                {
                    for (int p = 0; p < posit.Length; p++)
                    {
                        for (int l = 0; l < sTime.Length; l++)
                        {
                            for (int z = 0; z < empPerShift()[w,p]; z++)
                            {
                                bool shiftFilled = false;
                                bool checkDoubles = false;
                                do
                                {
                                    Employee tempEmployee = que.Dequeue();
                                    if (tempEmployee.getName().Equals("Alex Four"))
                                    {
                                        shiftFilled = false;
                                        int a = 1;
                                        a++;
                                    }
                                    if (tempEmployee.isWork(w, sTime[l], posit[p], checkDoubles, hour[p]))
                                    {
                                        
                                        foreach (Employee e in emp)
                                        {
                                            if (e.getName().Equals(tempEmployee.getName()))
                                            {
                                                e.setSche(w, sTime[l], posit[p]);
                                                shiftFilled = true;
                                            }
                                        }
                                    }
                                    if (que.Count == 0 && !shiftFilled)
                                    {
                                        brokenCount++;
                                        if (brokenCount > 1000)
                                        {
                                            return null;
                                        }
                                        que = empQue.queueRandom(rand);
                                        checkDoubles = true;
                                        int b = 0;
                                        int totalEmployeesPosition = 0;
                                        foreach (Employee e in emp)
                                        {
                                            int a = 0;
                                            foreach (String positions in e.getPositions())
                                            {
                                                if (positions.Equals(posit[p]))
                                                {
                                                    totalEmployeesPosition++;
                                                    foreach (String s in e.getSchedule())
                                                    {
                                                        if (!s.Equals("None"))
                                                        {
                                                            a += s.Split('/').Length;
                                                            if (a == hour[p])
                                                                b++;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (b == totalEmployeesPosition)
                                        {
                                            hour[p]++;
                                        }
                                    }
                                    if (shiftFilled)
                                        que = empQue.queueRandom(rand);


                                } while (!shiftFilled);
                            }
                        }
                    }
                }
            }
            return emp;
        }

        private int[,] empPerShift()                 //people per shift
        {
            int[,] perShifts = new int[getWorkWeek(), seRatio().Length];

            int x = perShifts.GetLength(0);
            int y = perShifts.GetLength(1);

            for (int a = 0; a < perShifts.GetLength(0); a++)
            {
                for (int b = 0; b < perShifts.GetLength(1); b++)
                {
                    if (((predic[a] / ra[b]) / sTime.Length) < 1)
                        perShifts[a,b] = 1;
                    else
                        perShifts[a,b] = (int)Math.Round(((predic[a] / ra[b]) / sTime.Length), MidpointRounding.AwayFromZero);
                }
            }

            return perShifts;
        }

        public static int getWorkWeek()                 //get work week
        {
            return workWeek;
        }

        public static void setWorkWeek(int workWeek)        //set work week
        {
            Scheduler.workWeek = workWeek;
        }

        public void saveSchedule()                      //save full schedule
        {
            Console.WriteLine("Name                    Monday             Tuesday             Wednesday             Thursday             Friday             Saturday             Sunday");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------");

            for (int y = 0; y < emp.Length; y++)
            {
                Console.Write("%-24s%-19s%-20s%-22s%-21s%-19s%-21s%-19s%n", emp[y].getName(), emp[y].getSchedule()[0],
                        emp[y].getSchedule()[1], emp[y].getSchedule()[1],
                        emp[y].getSchedule()[3], emp[y].getSchedule()[4],
                        emp[y].getSchedule()[5], emp[y].getSchedule()[6]);
            }
        }
    }
}
