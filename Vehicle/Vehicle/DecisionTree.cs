using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vehicle
{
    class DecisionTree
    {
        public static TreeDrawer d { get; set; }        //drawer

        public DecisionTree()
        {
            d = new TreeDrawer();
        }

        
        public static TreeNode DTAlg(List<TreeData> eg, string TarAtrib_Type, List<string> Atrib_Types, bool ExtraLog, Label label6)   //decision tree algorithm
        {
            TreeNode r = new TreeNode("Unlabeled");           //root

            if (!d.isSigned) d.Sign(TarAtrib_Type, Atrib_Types);

            string typestring;
            if (checkOutcome(eg, TarAtrib_Type, out typestring))
            {
                
                label6.Text += (" Outcome : " + typestring) + Environment.NewLine;
                r.lb = typestring;
                r.Dec_AtribType = TarAtrib_Type;
                r.isLeaf = true;
                d.AddNode(r);
                d.GoUp();
                
                if (ExtraLog) label6.Text += ("\nTree Finalized for Type: " + eg[0].AttributesList[0].Atrib_Typ + " With general value of: " + eg[0].AttributesList[0].Atrib_Val + " With outcome of: " + typestring) + Environment.NewLine;
                return r;
            }

            if (Atrib_Types.Count == 0)
            {
                List<string> possible_TargetAttribute_Types = output.GetPossAtribValues(eg, TarAtrib_Type);


                var most = possible_TargetAttribute_Types.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();        //taken from: https://stackoverflow.com/questions/355945/find-the-most-occurring-number-in-a-listint

                r.isLeaf = true;
                r.Dec_AtribType = TarAtrib_Type;
                r.lb = most;
                
                label6.Text += (" Outcome : " + most) + Environment.NewLine;
                d.AddNode(r);
                d.GoUp();
                
                if (ExtraLog) label6.Text += ("\nTree Finalized for: " + typestring);
                return r;
            }
            
            string atribType_BestGain = "";    //Find the  Attribute with the best gain
            double g = -1;                      //best gain

            foreach (string AttributeType in Atrib_Types)
            {
                double calGain = CalGain(eg, AttributeType, TarAtrib_Type);     //Finds the gain for the given attribute type in examples
                
                if (ExtraLog) label6.Text += ("\nInformation Gain : " + calGain + " from attribute: " + AttributeType);
                if (calGain > g)
                {
                    g = calGain;
                    atribType_BestGain = AttributeType;

                }
            }
            
            if (ExtraLog) label6.Text += ("Selected Attribute : '" + atribType_BestGain + "' with information gain of :" + g);
            r.lb = atribType_BestGain;
            r.StaticGain = g;
            d.AddNode(r);
            r.Dec_AtribType = atribType_BestGain;


            List<string> posValues = output.GetPossAtribValues(eg, atribType_BestGain);   //For each possible value vi of attributeType_WithBestGain
            foreach (string vi in posValues)
            {


                TreeBranch b = new TreeBranch(vi);   //Create a new Branch below Root, corresponding to the test attributeType_WithBestGain = vi

                //Let Examplesvi be the subset of examples that have value vi for attributeType_WithBestGain
                List<TreeData> Examplesvi = output.getAtribValOccuran(eg, vi, atribType_BestGain); //EXAMPLESvi subset list


                if (Examplesvi.Count == 0)    //if Examplesvi is empty
                {
                    //Below this new branch add a leaf node with label = most common value of Target_attribute in Examples
                    List<string> pos_TarAtrib_Types = output.GetPossAtribValues(eg, TarAtrib_Type);
                    var most = pos_TarAtrib_Types.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();  //taken from: https://stackoverflow.com/questions/355945/find-the-most-occurring-number-in-a-listint

                    r.Branches.Add(b);
                    d.handleBranches(r);
                }
                //Else below this new branch add the subtree
                else
                {
          
                    label6.Text += (atribType_BestGain + "= " + vi + "->") ;
                    if (ExtraLog) label6.Text += ("\nRemoving current Attribute for next subtree: " + atribType_BestGain);
                    List<string> Atrib_TypesCopy = new List<string>(Atrib_Types);
                    Atrib_TypesCopy.Remove(atribType_BestGain);  //Remove A(attribute with best gain ) from the list
                    if (ExtraLog) label6.Text += ("\nStarting new Sub-tree with rule: " + atribType_BestGain + " = " + vi);
                    d.GoDown();
                    TreeNode subtree = DTAlg(Examplesvi, TarAtrib_Type, Atrib_TypesCopy, ExtraLog, label6);

                    b.ConnNode = subtree;
                    r.Branches.Add(b);
                    d.handleBranches(r);
                }
            }
            d.GoUp();
            d.Save();
            return r;

        }

        
        private static bool checkOutcome(List<TreeData> eg, string TarAtrib_Type, out string o)  //check for all same outcome
        {
            bool allSame = !eg.Select(item => item.OutValue)  //If all outcome values are the same in the given list, return true
                      .Where(x => !string.IsNullOrEmpty(x))
                      .Distinct()
                      .Skip(1)
                      .Any();

            if (allSame)
            {
                o = eg[0].OutValue;
                return true;
            }
            else
            {
                o = null;
                return false;
            }
        }

        
        
        
        public static double CalEntropy(double[] outCounts)           //calculate the entropy (double positive, double negative)
        {
            double tot = 0;                                            //total
            foreach (double i in outCounts)
            {
                tot += i;
            }

            double outputValue = 0;
            foreach (double i in outCounts)                         //take the +ve and -ve outcomes as parameters
            {
                double val = -1 * (i / tot) * Math.Log(i / tot, 2);

                if (double.IsNaN(val)) { val = 0.0; }

                outputValue += val;
            }

            if (double.IsNaN(outputValue))      //absolute success check
            {
                return 0.0;
            }
            return outputValue;
        }

        public static double CalGain(List<TreeData> dList, string AtribType, string outType)   // Calculates gain of the attribute type in the given dataset values in the notepad
        {
            List<string> possOutValues = new List<string>();
            possOutValues = output.GetPossAtribValues(dList, outType);

            double[] outcomeCounts = new double[possOutValues.Count];

            for (int i = 0; i < possOutValues.Count; i++)
            {
                outcomeCounts[i] = output.countAtribValueOccuran(dList, possOutValues[i], outType);
            }

            double gCount = CalEntropy(outcomeCounts);           //gains
            List<string> PossibleValues = output.GetPossAtribValues(dList, AtribType);

            foreach (string possibleValue in PossibleValues)    //Possible value : A, B, C,
            {
                List<TreeData> occurances = output.getAtribValOccuran(dList, possibleValue, AtribType);

                double[] occuranceOutcomeCounts = new double[possOutValues.Count];

                foreach (TreeData data in occurances)   //For every treedata object where possiblevalue exists 
                {
                    for (int i = 0; i < possOutValues.Count; i++)
                    {
                        if (data.OutValue.Equals(possOutValues[i]))
                        {
                            occuranceOutcomeCounts[i]++;
                            break;
                        }
                    }
                }
                double entropyValue = CalEntropy(occuranceOutcomeCounts);
                double GainForPossibleValue = (Convert.ToDouble(occurances.Count) / Convert.ToDouble(dList.Count)) * entropyValue;
                gCount -= GainForPossibleValue;
            }

            return gCount;
        }
    }
}
