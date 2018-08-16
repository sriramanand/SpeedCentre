using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    class TreeAtrib
    {
        public string Atrib_Val { get; set; }
        public string Atrib_Typ { get; set; }

        public TreeAtrib(string val, string t)
        {
            this.Atrib_Val = val;
            this.Atrib_Typ = t;
        }
    }
}
