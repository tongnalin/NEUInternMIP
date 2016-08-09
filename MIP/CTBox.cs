using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MIP
{
    public delegate CTImage seeNeighbour(int o);
    public partial class CTBox : UserControl
    {
        CTImage img;   
        int wPos = 400;
        int wWidth = 600;
        int order;
        //Bitmap imgshow;
        public seeNeighbour SeeNeighbour;
        public CTBox(string imgFullPath, List<string> patientInfo, List<string> scanInfo, int order)
        {
            InitializeComponent();
            this.order = order;
            this.img = new CTImage(imgFullPath, patientInfo, scanInfo);
            this.patientGroupBox.Text = img.PatientName;
            
            this.ctPictureBox.Image = img.InitPic as Image;
            //this.ctPictureBox.Refresh();
            WriteCTInfo();
        }

        public CTImage Getimage
        {
            get
            {
                return img;
            }
        }
        private void ctPictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureWindow pw = new PictureWindow(img, this.ctPictureBox.Image, wPos, wWidth);
            pw.Text = img.Path;
            pw.UpdateImg = updateCTImg;
            pw.ChangePicture = neighbourCtBox;
            pw.Show();
        }

        private void WriteCTInfo()
        {
            Label ageLable = new Label();
            ageLable.Text = "年龄：" + img.PatientAge.ToString();
            ageLable.Location = new Point(10, 20);
            this.patientGroupBox.Controls.Add(ageLable);

            Label sexLabel = new Label();
            sexLabel.Text = "性别：" + (img.PatientSex == "m"? "男" : "女");
            sexLabel.Location = new Point(10, 50);
            this.patientGroupBox.Controls.Add(sexLabel);

            TextBox hospitalTextBox = new TextBox();
            hospitalTextBox.Location = new Point(0, 80);
            hospitalTextBox.ReadOnly = true;
            hospitalTextBox.Multiline = true;
            hospitalTextBox.Width = this.patientGroupBox.Width;
            
            List<string> a = new List<string>();
            a.Add("图片信息：");
            foreach (string i in img.HospitalInfo)
            {
                a.Add(i);
                hospitalTextBox.Height += 12;
            }

            hospitalTextBox.Lines = a.ToArray();
            //hospitalLabel.Lines.
            this.patientGroupBox.Controls.Add(hospitalTextBox);
        }


        private void updateCTImg(Bitmap bp, int w, int c)
        {
            this.ctPictureBox.Image = bp;
            this.wPos = c;
            this.wWidth = w;
        }

        private CTImage neighbourCtBox(bool forward)
        {
            if (forward)
            {
                if ((--order) < 0)
                {
                    order++;
                }
                return SeeNeighbour(order);
            }
            else
            {
                order++;
                return SeeNeighbour(order);
            }
        }


    }
}
