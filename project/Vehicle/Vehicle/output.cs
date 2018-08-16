using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vehicle
{
    class output
    {
        public void start(Label lb6,TextBox tB1,TextBox tB2,TextBox tB3)
        {
            
                string outType = tB2.Text;                  //outcome type
                string outSuccessVal = tB3.Text;          //outcome success val

                List<TreeData> data;
                List<string> possTypes;

                data = exData(
                    tB1.Text,
                    outType,
                    outSuccessVal,
                    out possTypes);

                possTypes.Remove(outType);

                DecisionTree dt = new DecisionTree();
                lb6.Text += ("Decision Tree") + Environment.NewLine;
                lb6.Text += ("===============================================") + Environment.NewLine;
                lb6.Text += Environment.NewLine;
                DecisionTree.DTAlg(data, outType, possTypes, false, lb6);
                lb6.Text += Environment.NewLine;
                lb6.Text += ("================================================") + Environment.NewLine;

        }

        public static List<TreeData> exData(string fName, string outcomeName, string successlOutName, out List<string> possTypes)   //extract data
        {
            List<TreeData> outp = new List<TreeData>();

            StreamReader sr = new StreamReader(fName);
            string line = "";
            possTypes = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                line = line.Replace(",", string.Empty); //Remove Commas
                string[] words = line.Split(' ');


                if (possTypes.Count == 0)  //Only true for the first loop
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        possTypes.Add(words[i]);
                    }
                }
                else
                {
                    List<TreeAtrib> attList = new List<TreeAtrib>();
                    for (int i = 0; i < words.Length; i++)
                    {
                        TreeAtrib attribute = new TreeAtrib(words[i], possTypes[i]);
                        attList.Add(attribute);
                    }
                    TreeData data = new TreeData(attList, outcomeName, successlOutName);
                    outp.Add(data);
                }
            }
            return outp;
        }

        public static List<string> GetPossAtribValues(List<TreeData> dList, string atribType)  // Returns a list of possible values for a given attributeType from the given data list
        {
            List<string> ValuesList = new List<string>();

            foreach (TreeData data in dList)
            {
                TreeAtrib atrib = data.GetAttributeByType(atribType);
                ValuesList.Add(atrib.Atrib_Val);
            }

            List<string> distin_valList = ValuesList.Distinct().ToList();
            return distin_valList;
        }

        public static double countAtribValueOccuran(List<TreeData> dataList, string attributeValue, string attributeType)  // Returns the number of occurances of the given value of given given type within the given data list
        {
            List<string> ValuesList = new List<string>();

            foreach (TreeData data in dataList)
            {
                TreeAtrib attribute = data.GetAttributeByType(attributeType);
                ValuesList.Add(attribute.Atrib_Val);
            }
            int count = ((from temp in ValuesList where temp.Equals(attributeValue) select temp).Count());
            return count;
        }

        public static double countFailByAtribVal(List<TreeData> dataList, string attributeValue, string attributeType)
        {
            double c = 0;               //count
            foreach (TreeData data in dataList)
            {
                TreeAtrib atrib = data.GetAttributeByType(attributeType);   //Get attribute from the given data
                if (atrib.Atrib_Val.Equals(attributeValue))   //Is the attribute's value equals to the value we are counting?
                {
                    if (!data.isSuccess)  //if this data failed with the given attribute value, increment count
                    {
                        c++;
                    }
                }
            }
            return c;
        }

        public static double countSuccessByAtribVal(List<TreeData> dataList, string attributeValue, string attributeType)
        {
            double c = 0;           //count
            foreach (TreeData data in dataList)
            {
                TreeAtrib attribute = data.GetAttributeByType(attributeType);   //Get attribute from the given data
                if (attribute.Atrib_Val.Equals(attributeValue))   //Is the attribute's value equals to the value we are counting?
                {
                    if (data.isSuccess)  //if this data failed with the given attribute value, increment count
                    {
                        c++;
                    }
                }
            }
            return c;
        }

        public static List<TreeData> getAtribValOccuran(List<TreeData> dataList, string attributeValue, string attributeType) // Gets the data with given value of the given attribute type
        {
            List<TreeData> FilDataList = new List<TreeData>();          //filetred data

            foreach (TreeData data in dataList)
            {
                if (data.GetAttributeByType(attributeType).Atrib_Val.Equals(attributeValue))
                {
                    FilDataList.Add(data);
                }
            }
            return FilDataList;
        }
    }
}
