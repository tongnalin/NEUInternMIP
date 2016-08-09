using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace MIP
{
    public class CTImage
    {
        short[] imgData;
        string path;

        string patientName;
        int patientAge;
        string patientSex;

        List<string> hospitalInfo;
        List<string> patientInfo;
        int pictureLateral;
        //Bitmap picture;
        public string PatientName 
        {
            get
            {
                return patientName;
            }
        }
        public int PatientAge 
        {
            get
            {
                return patientAge;
            }
        }
        public string PatientSex
        {
            get
            {
                return patientSex;
            }
        }
        public List<string> HospitalInfo
        {
            get
            {
                return hospitalInfo;
            }
        }
        public List<string> PatientInfo
        {
            get
            {
                return patientInfo;
            }
        }
        public string Path 
        { 
            get
            {
                return path;
            }
        }
        public CTImage(string pathToCT, List<string> patientInfo, List<string> scanInfo)
        {
            path = pathToCT;
            FileStream fs = new FileStream(pathToCT, FileMode.Open);
            BinaryReader r = new BinaryReader(fs);
            long pictureSize = fs.Length / 2;
            imgData = new short[pictureSize];
            pictureLateral = (int)Math.Sqrt(pictureSize);
            
            patientName = patientInfo[0];
            patientSex = patientInfo[1];
            patientAge = Convert.ToInt32(patientInfo[2]);

            hospitalInfo = scanInfo;
            this.patientInfo = patientInfo;
            for (int i = 0; i < pictureSize; i++)
            {
                imgData[i] = r.ReadInt16();
            }
            fs.Close();
        }

        public Bitmap InitPic
        {
            get
            {
                return imgProcess(400, 600);
            }
        }

        public Bitmap imgProcess(int center, int width)
        {
            Bitmap picture = new Bitmap(pictureLateral, pictureLateral);
            Byte[] lookUpTable = new Byte[4096]; // Initialized as Zeros
            int offSet = 1024;
            //byte[] grayBmp = new byte[pictureLateral * pictureLateral];

            int low = center - width / 2 + offSet;
            int high = center + width / 2 + offSet;

            for (int i = low; i <= high; i++)
            {
                lookUpTable[i] = (Byte)((i - low) / (double)width * 255);
            }

            for (int i = high + 1; i < 4096; i++)
            {
                lookUpTable[i] = 255;
            }

            for (int i = 0; i < imgData.Length; i++)
            {
                //dataToShow[i] = lookUpTable[imgData[i]];
                Byte tmpPixelValue = lookUpTable[imgData[i]];
                picture.SetPixel(i % 512, i / 512, Color.FromArgb(tmpPixelValue, tmpPixelValue, tmpPixelValue));
            }
            return picture;
        }

        public Bitmap fasterProcess(int center, int width,int angle)
        {
            Bitmap picture = new Bitmap(pictureLateral, pictureLateral);
            Byte[] lookUpTable = new Byte[4096]; // Initialized as Zeros
            int offSet = 1024;
            byte[] grayBmp = new byte[pictureLateral * pictureLateral];

            int low = center - width / 2 + offSet;
            int high = center + width / 2 + offSet;

            for (int i = low; i <= high; i++)
            {
                lookUpTable[i] = (Byte)((i - low) / (double)width * 255);
            }

            for (int i = high + 1; i < 4096; i++)
            {
                lookUpTable[i] = 255;
            }            

            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    short data = (short)(imgData[i * 512 + j]);
                    switch (angle)
                    {
                        case 0:
                            grayBmp[i * 512 + j] = lookUpTable[data];
                            break;
                        case 1:
                            grayBmp[j * 512 + 511 - i] = lookUpTable[data];
                            break;
                        case 2:
                            grayBmp[(511 - i) * 512 + 511 - j] = lookUpTable[data];
                            break;
                        case 3:
                            grayBmp[(511 -j) * 512 + i] = lookUpTable[data];
                            break;
                        default:
                            break;
                    }
                    //bmpImage.SetPixel(j, i, pixel);   速度很慢，使用下面的方法把数据一次性传过去
                }
            }

            //Byte[] grayBmp1 = new Byte[512 * 512];
            //if (angle > 0)
            //{
                
            //    for (; angle > 0; angle--)
            //    {
            //        for (int i = 0; i < 512; i++)
            //        {
            //            for (int j = 0; j < 512; j++)
            //                grayBmp1[j * 512 + 511 - i] = grayBmp[i * 512 + j];
            //        }
                    //for (int i = 0; i < 512; i++)
                    //{
                    //    for (int j = 0; j < 512; j++)
                    //        grayBmp[i * 512 + j] = grayBmp1[i * 512 + j];
                    //}
            //    }
            //}

            unsafe
            {
                //fixed使得使用grayAddr指针时,grayBmp的存储位置不会被垃圾收集器移动
                fixed (byte* grayAddr = grayBmp)
                {
                    IntPtr ptr = (IntPtr)(byte*)grayAddr;
                    picture = new Bitmap(512, 512, 512, PixelFormat.Format8bppIndexed, ptr);
                    ColorPalette pal = picture.Palette;    //得到的是bmpImage.Palette的副本？
                    for (int i = 0; i < 256; i++)
                    {
                        pal.Entries[i] = Color.FromArgb(i, i, i);
                    }

                    picture.Palette = pal; //必须重新赋值，才可以生效
                }
            }
            return picture;
        }
    }
}
