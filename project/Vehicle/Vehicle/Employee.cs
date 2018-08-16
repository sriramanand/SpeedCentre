using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    public class Employee
    {
        
        private String n;                           //emp name
        private int wLen = 7;                       // length of the week
        private bool[] avai;                        //availability
        private String[] s;                         //emp shift
        private String[] pos = new String[0];       //emp position
        private String[] wSchedule;                 //work schedule
        private int[] maxShift;                     //max no of shifts per day

        
        public Employee(String n, String pos, bool[] avai, String[] s)    
        {
            this.n = n;
            this.avai = avai;
            this.s = s;
            this.wSchedule = new String[wLen];
            this.maxShift = getDefaultShiftsPerDay();
            setPos(pos);
            
        }

        
        public Employee(String n, String pos, bool[] avail, String[] s, int[] max)   
        {
            this.n = n;
            this.avai = avail;
            this.s = s;
            this.wSchedule = new String[wLen];
            this.maxShift = max;
            setPos(pos);
            
        }

        private int[] getDefaultShiftsPerDay()          //find the default shifts per day
        {
            int[] a = new int[wLen];

            for (int x = 0; x < wLen; x++)
            {
                if (avai[x])
                {
                    if (this.s[x].Contains("Any"))
                        a[x] = 1;
                    else if (this.s[x].Contains("none"))
                        a[x] = 0;
                    else
                        a[x] = 1;
                }
                else
                    a[x] = 0;
            }

            return a;
        }

        public int getShiftCount()    //Gets the shift count.
        {
            int a = 0;
            foreach (String s in wSchedule)
                if (!s.Contains("None"))
                    a += s.Split('/').Length;
            return a;
        }

        private void setPos(String pos)        //Sets the positions.
        {
            this.pos = pos.Split('/');
        }

        private void setPos(String[] pos)    //set position
        {
            this.pos = pos;
        }

       
        public bool isWork(int day, String time, String posit)     //Can the Employee work shift in question
        {
            foreach (String s in pos)
                if (s.Contains(posit) && avai[day] && wSchedule[day].Equals("None") && (s[day].Equals(time) || s[day].Equals("Any")))
                    return true;
            return false;
        }


        public bool isWork(int day, String time, String posit, bool multiply, int maxDay)    //Can the Employee work shift in question
        {
            if (pos.Cast<String>().Contains(posit))
            {
                if (this.avai[day])
                {
                    if (s[day].Contains(time) || s[day].Equals("Any"))
                    {
                        if (this.wSchedule[day].Equals("None"))
                        {
                            if (this.getShiftCount() < maxDay)
                                return true;
                        }
                        else if (!this.wSchedule[day].Contains(time) && multiply)
                            return true;
                    }
                }
            }
            return false;
        }


        public void reSche()                     //resetSchedule
        {
            for (int a = 0; a < wLen; a++)
            {
                wSchedule[a] = "None";
            }
        }


        public void setSche(int day, String time, String posit)           //set schedule
        {
            if (wSchedule[day].Equals("None"))
                wSchedule[day] = time + "[" + posit.ToCharArray()[0] + "]";
            else
                wSchedule[day] += "/" + time + "[" + posit.ToCharArray()[0] + "]";
        }


        public int[] maxShifts()                           // find max shifts per day
        {
            return this.maxShift;
        }


        public String[] getSchedule()                   //find work schedule
        {
            return wSchedule;
        }


        public String getName()                             //get name
        {
            return n;
        }


        public String[] getPositions()                      //get the positions
        {
            return pos;
        }


        public int weekLength()                          //find the length of the week
        {
            return wLen;
        }


        public bool[] getAvail()                    //find availability
        {
            return avai;
        }


        public String[] findShift()                          //find the shift
        {
            return s;
        }

        public static int comp(Employee e)             //compare to
        {
            return 0;
        }
    }
}
