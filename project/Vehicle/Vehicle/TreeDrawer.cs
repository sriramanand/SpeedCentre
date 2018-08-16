using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    class TreeDrawer
    {
        int[] nCount;                       //count no of nodes
        int cDepth;                         //current depth
        Bitmap i;                           //image
        Graphics grap;

        public int hPadding { get; set; }               //horizontal padding
        public int vPadding { get; set; }               //vertical padding
        public int dMultiply { get; set; }              //distance multiply
        public bool isSigned { get; set; }              //is signed

        public TreeDrawer()
        {
            hPadding = 175;
            vPadding = 175;
            dMultiply = 165;

            nCount = new int[99999];
            cDepth = 1;
            i = new Bitmap(2000, 2000);
            grap = Graphics.FromImage(i);

            grap.FillRectangle(Brushes.Black, 0, 0, 2000, 2000);        //Paint background black for better readablity
        }

        public void Save()
        {
            i.Save("out.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        public void AddNode(TreeNode n)
        {
            int NodesOnThisDepth = nCount[cDepth];
            nCount[cDepth]++;

            grap.FillRectangle(Brushes.Gray, 5 + hPadding + (NodesOnThisDepth * dMultiply) + NodesOnThisDepth * 20, 5 + 100 + cDepth * dMultiply, 103, 78);
            grap.FillRectangle(Brushes.LightGray, hPadding + (NodesOnThisDepth * dMultiply) + NodesOnThisDepth * 20, 100 + cDepth * dMultiply, 100, 75);

            Font drawFont = new Font("Arial", 9);
            SolidBrush drawBrush = new SolidBrush(Color.MediumBlue);
            PointF drawPoint = new PointF(hPadding + (NodesOnThisDepth * dMultiply) + NodesOnThisDepth * 20, 100 + cDepth * dMultiply);  // Create point for upper-left corner of drawing.


            grap.DrawString(n.lb + "\nGain: " + (n.StaticGain).ToString("#0.000"), drawFont, drawBrush, drawPoint);   // Draw string to screen.
        }

        public void handleBranches(TreeNode n)
        {
            int NodesOnThisDepth = nCount[cDepth];

            Font drawFont = new Font("Arial", 8);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            foreach (TreeBranch branch in n.Branches)
            {
                if (!branch.treeDrawn)
                {
                    PointF drawPoint = new PointF(
                        ((((nCount[cDepth + 1] * dMultiply) + nCount[cDepth + 1] * 45) + (NodesOnThisDepth * (dMultiply)) + NodesOnThisDepth * 20) / 2) + 20,
                        (((175 + (cDepth + 1) * dMultiply)) + (100 + cDepth * dMultiply)) / 2);

                    grap.DrawLine(new Pen(Color.SlateGray, 3), hPadding / 4 + (NodesOnThisDepth * dMultiply) + NodesOnThisDepth * 20, (100 + cDepth * dMultiply) + 75,
                        hPadding / 4 + (nCount[cDepth + 1] * dMultiply) + nCount[cDepth + 1] * 20, 100 + (cDepth + 1) * dMultiply);

                    grap.DrawString(n.lb + " =\n" + branch.lb, drawFont, drawBrush, drawPoint);

                    branch.treeDrawn = true;
                }
            }
        }

        public void GoDown()            //go downwards
        {
            cDepth++;
        }

        public void GoUp()              //go upwards
        {
            cDepth--;
        }

        public void Sign(string OutType, List<string> attribTypes)
        {
            Font f = new Font("Arial", 8);              //draw the font
            SolidBrush sb = new SolidBrush(Color.White); //draw brush
            PointF p = new PointF(50, 50);   // Create the point for upper-left corner of drawing.

            string conc = "";
            foreach (string s in attribTypes)       // Draw string to screen.
            {
                conc += s + ", ";
            }

            grap.DrawString("Plot for: " + OutType + "\nAttribute Types: " + conc, f, sb, p);
            isSigned = true;
        }
    }
}
