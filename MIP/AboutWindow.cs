using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MIP
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();
            this.treeView1.ExpandAll();
            label1.Text = " ";
            label2.Text = " ";
        }

        

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "软件简介")
            {
                label1.Text = e.Node.Text;
                label2.Text = "  随着医学影像的数字化，医学影像在数量和大小上的增加越来越有必要使用计算机来存储处理及分析这些图像，对它进行必要的图像处理。计算机在医学图像处理中的应用除了成像方面的应用外，在对图像的管理上也有重要作用。\n" +
                    "  将计算机图像处理技术更好地应用医学成像已成为重要研究方向。当今影像医学新技术的发展特点，主要体现在数字化、多功能化、信息化、宽频带化等方面的综合应用。而数字化和信息化，因其可操作、可传输、直观、有效等特点，倍受因发现医务工作者的重视。\n" +
                    "  本软件就是基于这样的背景，从而设计出来的能对医学图像进行几何变换和测量的医学图像后处理系统。针对专业的医学图像，本软件可实现对原始数据的简单处理，并且加入了调节窗宽窗位、几何变换、标记等功能，从而辅助医生进行更细致的分析和更精确的诊断。";
            }
            if (e.Node.Text == "基本功能")
            {
                label1.Text = e.Node.Text;
                label2.Text = "  本系统主要包括五方面的功能：文件操作、图像显示、图像处理、图像测量、多视图显示。\n" +
                   "  文件操作：文件打开与退出。\n  图像显示：2D(图像平面显示)及修改后图像正常显示与保存。\n  图像处理：调窗、放缩、移动与旋转。\n  多视图显示：可单独显示单张图片并进行操作。\n  图像测量：直线、矩形、椭圆和文字。\n" +
                   "  其中，通过几何变换可以改善由于在图像采集过程中由于患者摆位、采集条件等原因带来的对诊断的影响，帮助医生更好地观察图像。图像的缩放功能可用于观察病变的局部细致的形态结构和整体形态。而旋转功能使医生根据自己的观察习惯而对图像进行不同角度的旋转。" +
                   "图像测量的主要目的是提取出对临床诊断有用的定量信息。这两类操作的实现，完成了辅助诊断的功能。";
            }
            if (e.Node.Text == "注意事项")
            {
                label1.Text = e.Node.Text;
                label2.Text = "注意！本程序Bug极多，请小心使用。";
            }
            if (e.Node.Text == "设计环境")
            {
                label1.Text = e.Node.Text;
                label2.Text = "Microsoft Windows 6.0 x64\n" +
                "Microsoft Visual Studio 2010\n" +
                "MSDN Library";
            }
            if (e.Node.Text == "参与人员")
            {
                label1.Text = e.Node.Text;
                label2.Text = "  郝世超\n  何远\n  李静晗\n  邢维洲";
            }
        }
    }
}
