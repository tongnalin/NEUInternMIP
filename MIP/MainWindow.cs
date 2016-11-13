using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml;

namespace MIP
{
    public partial class MainWindow : Form
    {

        List<CTBox> CTBoxList = new List<CTBox>();
        List<CTImage> imgList = new List<CTImage>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            List<string> patientInfo = new List<string>();
            List<string> scanInfo = new List<string>();
            CTBoxList.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"D:\";
            ofd.Filter = "图像描述文件（*.des)|*.des|生数据（*.raw)|*.raw";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.panel2.Controls.Clear();
                if (ofd.SafeFileName.EndsWith(".des"))
                {
                    //this.Controls.
                    XmlDocument xd = new XmlDocument();
                    xd.Load(ofd.FileName);
                    XmlNode imageNode = xd.SelectSingleNode("//Image");
                    XmlNode patientNode = xd.SelectSingleNode("//Patient");
                    XmlNode scanNode = xd.SelectSingleNode("//Scan");

                    foreach (XmlNode node in patientNode.ChildNodes)
                    {
                        patientInfo.Add(node.InnerText);
                    }

                    foreach (XmlNode node in scanNode.ChildNodes)
                    {
                        scanInfo.Add(node.InnerText);
                    }

                    for (int i = 0; i < imageNode.ChildNodes.Count; i++)
                    {
                        string path = ofd.FileName.Substring(0, ofd.FileName.Length - ofd.SafeFileName.Length) + imageNode.ChildNodes[i].InnerText;
                        CTBox cb = new CTBox(path, patientInfo, scanInfo, i);
                        cb.SeeNeighbour = GiveNeighbour;
                        CTBoxList.Add(cb);
                    }
                    
                    GenerateLayout();
                    //this.panel2.Refresh();
                }
            }
        }

        private CTImage GiveNeighbour(int o)
        {
            return CTBoxList[o % CTBoxList.Count].Getimage;
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            GenerateLayout();
        }

        private void GenerateLayout()
        {
            int topleftx = 0;
            int toplefty = 0;
            //this.panel2.Controls.Clear();
            foreach (CTBox item in CTBoxList)
            {
                if (topleftx + 400 > this.panel2.Size.Width)
                {
                    topleftx = 0;
                    toplefty += 250;
                }
                item.Location = new Point(topleftx, toplefty);
                this.panel2.Controls.Add(item);
                topleftx += 400;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow aw = new AboutWindow();
            aw.ShowDialog();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CTBoxList.Clear();
            this.panel2.Controls.Clear();
            GenerateLayout();
        }

        private void changeLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem t = sender as ToolStripMenuItem;
            if (t.Name == "looseToolStripMenuItem")
            {
                foreach (var item in CTBoxList)
                {
                    
                }
            }
            else if (t.Name == "compactToolStripMenuItem")
            {

            }
        }
    }
}

