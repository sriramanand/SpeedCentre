using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    class TreeData
    {


        public List<TreeAtrib> AttributesList { get; set; }     // List of attributes this data contains (Contains 1 attribute for each datatype(a row in the given dataset))
        public bool isSuccess { get; set; }                     // Is this treedata outcome succesful
        public string OutValue { get; set; }                    // Value of the outcome

        public TreeData(List<TreeAtrib> atrib, string outType, string successWord)
        {
            AttributesList = atrib;

            this.OutValue = GetAttributeByType(outType).Atrib_Val;

            if (GetAttributeByType(outType).Atrib_Val.Equals(successWord))     //Determine if this data is succesful
            { 
                isSuccess = true; 
            }
            else 
            { 
                isSuccess = false; 
            } 
        }

       
        public TreeAtrib GetAttributeByType(string Atrib_Type)                              // Finds the attribute with given Attribute Type and returns it
        {
            foreach (TreeAtrib att in AttributesList)
            {
                if (att.Atrib_Typ.Equals(Atrib_Type))
                {
                    return att;
                }
            }
            return null;
        }
    }
}
