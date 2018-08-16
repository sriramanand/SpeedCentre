using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    class EmployeeQueue
    {
        private Employee[] emp;

        public EmployeeQueue(Employee[] emp)
        {
            this.emp = emp;
        }

        public Queue<Employee> queueRandom(Random rand)                    //queue the employee
        {
            Queue<Employee> q = new Queue<Employee>();
            List<int> l = new List<int>();

            for (int a = rand.Next(emp.Length); q.Count < emp.Length; a = rand.Next(emp.Length))
            {
                if (!l.Contains(a))
                {
                    q.Enqueue(emp[a]);
                    l.Add(a);
                }
            }
            return q;
        }
    }
}
