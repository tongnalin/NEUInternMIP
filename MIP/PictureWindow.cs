using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;

namespace MIP
{
    public delegate void transferImgBack(Bitmap bm, int w, int c);
    public delegate void drawCandidate(Point a, Point b, ref PaintEventArgs e);
    public delegate CTImage changePicture(bool forward);

    //public void DrawEllipse(Pen pen, int x, int y, int width, int height);
    //public void DrawRectangle(Pen pen, int x, int y, int width, int height);
    //public void DrawLine(Pen pen, int x1, int y1, int x2, int y2);

    //public delegate Image changeimage

    enum WorkingMode
    {
        drawing,
        drawingPolygon,
        transforming,
        zooming,
        texting,
        rotating,
        nothing
    }

    enum DrawOperation
    {
        line,
        rects,
        ellipse,
        polygon,
        text
    }
    //public delegate void storePointCandidate(Point a, Point b);
    
    public partial class PictureWindow : Form
    {
        CTImage img;
        int wPos;
        int wWidth;
        //int order;
        WorkingMode workingMode = WorkingMode.nothing;

        Bitmap bmp = null;// = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
        private Point ptStart = new Point(0, 0);
        private Point ptEnd = new Point(0, 0);
        private Point elasticPtEnd = new Point(0, 0);

        private List<Point> lines = new List<Point>();
        private List<Point> ellipses = new List<Point>();
        private List<Point> rects = new List<Point>();
        private List<String> textbox = new List<String>();
        private List<PointF> text_point = new List<PointF>();
        private List<DrawOperation> editList = new List<DrawOperation>(); 
        private List<Point> ShapeListPointer;
        DrawOperation curDrawOps;

        //text文本所用字体和颜色
        Font drawfont = new Font("Consolas", 9);
        SolidBrush drawbrush = new SolidBrush(Color.BlueViolet);

        //private System.Collections.ArrayList Shapes;
        private bool bMouseDown = false;
        Point mouseDownPoint;

        public transferImgBack UpdateImg;
        public changePicture ChangePicture;
        private drawCandidate DrawCandidate;
        

        public PictureWindow( CTImage img, Image i, int c, int w)
        {
            InitializeComponent();
            this.pictureBox1.Image = i;
            this.img = img;
            //this.order = order;
            wPos = c;
            wWidth = w;
            this.pictureBox1.Size = new Size(512, 512);
            this.pictureBoxPanel.Size = new Size(550, 512);
            this.windowStatusLabel.Text = wPos + ", " + wWidth;
            this.windowCenterTrackBar.Value = wPos;
            this.windowWidthTrackBar.Value = wWidth;
        }

        private void PictureWindow_Paint(object sender, PaintEventArgs e)
        {
            int x = 0;
            int y = 0;
            Font f = new Font("Consolas", 9);

            foreach (string item in img.HospitalInfo)
	        {
                if (item != "600" && item != "400")
                {
                    e.Graphics.DrawString(item, f, Brushes.Green, x, y);
                    y += 12;
                }
	        }
            y = 0;

            foreach (string item in img.PatientInfo)
            {
                if (x < item.Length)
                    x = item.Length;
            }


            foreach (string item in img.PatientInfo)
            {
                e.Graphics.DrawString(item, f, Brushes.Green, this.Width - x*9, y);
                y += 12;
            }
            y = 0;
            //this.Refresh();
            //e.Graphics.DrawString(wWidth.ToString(), f, Brushes.Red, 0, this.Height -70);

        }
        
        //四个画图用的事件
        //1.
        private void pictureBox1_Draw_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (!bMouseDown)
            {
                ptStart = new Point(e.X, e.Y);
                ptEnd = new Point(e.X, e.Y);
            }
            bMouseDown = !bMouseDown;
        }
        //2.
        private void pictureBox1_Draw_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            elasticPtEnd = new Point(e.X, e.Y);
            ptEnd = elasticPtEnd;
            //lines.Add(ptStart);
            //lines.Add(ptEnd);
            //if(StorePointCandidate != null)
            //    StorePointCandidate(ptStart, ptEnd);
            if (ShapeListPointer != null)
            {
                ShapeListPointer.Add(rotatePointCounterClockwise(ptStart));
                ShapeListPointer.Add(rotatePointCounterClockwise(ptEnd));
                editList.Add(curDrawOps);
            }
            bMouseDown = !bMouseDown;

            var tmp = DrawCandidate;
            DrawCandidate = null;
            this.pictureBox1.Refresh();
            DrawCandidate = tmp;
        }
        //3.
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < lines.Count; i += 2)
            {
                e.Graphics.DrawLine(System.Drawing.Pens.Red, rotatePointClockwise(lines[i])
                    , rotatePointClockwise(lines[i + 1]));
            }

            for (int i = 0; i < ellipses.Count; i+=2)
            {
                e.Graphics.DrawEllipse(System.Drawing.Pens.Aqua
                    , rectFromTwoPoints(rotatePointClockwise(ellipses[i]), rotatePointClockwise(ellipses[i + 1])));
            }

            for (int i = 0; i < rects.Count; i+=2)
            {
                e.Graphics.DrawRectangle(System.Drawing.Pens.Olive
                    , rectFromTwoPoints(rotatePointClockwise(rects[i]), rotatePointClockwise(rects[i + 1])));
            }
            for (int i = 0; i < text_point.Count; i++)
            {
                e.Graphics.DrawString(textbox[i], drawfont, drawbrush
                    , rotatePointFClockwise(new PointF(text_point[i].X, text_point[i].Y)));
            }

            foreach (List<Point> polygon in polygonList)
            {
                for (int i = 0; i < polygon.Count; i++)
                {
                    if (i == 0)
                    {
                        e.Graphics.DrawLine(Pens.Yellow, rotatePointClockwise(polygon[0])
                            , rotatePointClockwise(polygon[polygon.Count - 1]));
                    }
                    else
                    {
                        e.Graphics.DrawLine(Pens.Yellow, rotatePointClockwise(polygon[i])
                            , rotatePointClockwise(polygon[i - 1]));
                    }
                }
            }

            if (drawingPolygon && polygonPointsList.Count > 1)
            {
                for (int i = 1; i < polygonPointsList.Count; i++)
                {
                    e.Graphics.DrawLine(Pens.Yellow, rotatePointClockwise(polygonPointsList[i])
                        , rotatePointClockwise(polygonPointsList[i - 1]));
                }
            }

            if ((workingMode == WorkingMode.drawing || (workingMode == WorkingMode.drawingPolygon && drawingPolygon)) 
                && DrawCandidate != null)
                DrawCandidate(ptStart, elasticPtEnd, ref e);
        }

        private Rectangle rectFromTwoPoints(Point a, Point b)
        {
            int tlX = a.X > b.X ? b.X : a.X;
            int tlY = a.Y > b.Y ? b.Y : a.Y;
            int brX = a.X < b.X ? b.X : a.X;
            int brY = a.Y < b.Y ? b.Y : a.Y;
            int width = brX - tlX;
            int height = brY - tlY;
            //Rectangle i = new Rectangle(
            return new Rectangle(tlX, tlY, width, height);            
        }
        //4.
        private void pictureBox1_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            elasticPtEnd = new Point(e.X, e.Y);
            this.pictureBox1.Refresh();
        }
        //文字标记
        private void pictureBox1_Text_Click(object sender, EventArgs e)
        {
            Point wordpoint = new Point(0, 0);
            TextBox t = new TextBox();
            t.Text = "Mark";
            ptEnd = MousePosition;
            wordpoint.X = this.PointToClient(ptEnd).X;
            wordpoint.Y = this.PointToClient(ptEnd).Y;
            t.SetBounds(wordpoint.X, wordpoint.Y, 60, 20);
            this.Controls.Add(t);
            t.Enabled = true;
            t.BringToFront();
            t.KeyPress += new KeyPressEventHandler(t_KeyPress);
        }

        void t_KeyPress(object sender, KeyPressEventArgs k)
        {
            if (k.KeyChar == (char)13)
            {
                TextBox t = sender as TextBox;
                Graphics g = pictureBox1.CreateGraphics();

                PointF drawPoint = new PointF(t.Location.X - this.pictureBoxPanel.Left - this.pictureBox1.Left, t.Location.Y - this.pictureBoxPanel.Top - this.pictureBox1.Top);
                string s = t.Text;
                textbox.Add(s);
                text_point.Add(rotatePointFCounterClockwise(new PointF(drawPoint.X , drawPoint.Y)));
                editList.Add(curDrawOps);
                this.Controls.Remove(t);
                this.pictureBox1.Refresh();
                g.DrawString(s, drawfont, drawbrush, drawPoint);
            }
        }

        private void ShapeButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (workingMode != WorkingMode.drawing)
            {
                eventUnregister();
                this.pictureBox1.MouseDown += this.pictureBox1_Draw_MouseDown;
                this.pictureBox1.MouseMove += this.pictureBox1_Draw_MouseMove;
                this.pictureBox1.MouseUp += this.pictureBox1_Draw_MouseUp;
                workingMode = WorkingMode.drawing;
            }
            
            if (btn.Name == "drawLineButton")
            {
                DrawCandidate = drawLineCandidate;
                ShapeListPointer = lines;
                curDrawOps = DrawOperation.line;
            }
            else if (btn.Name == "EllipseButton")
            {
                DrawCandidate = drawEllipseCandidate;
                ShapeListPointer = ellipses;
                curDrawOps = DrawOperation.ellipse;

            }
            else if (btn.Name == "RectangleButton")
            {
                DrawCandidate = drawRectangleCandidate;
                ShapeListPointer = rects;
                curDrawOps = DrawOperation.rects;
            }
        }
        
        private void textButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DrawCandidate = null;
            ShapeListPointer = null;
            if (workingMode != WorkingMode.texting)
            {
                //this.pictureBox1.MouseUp -= this.pictureBox1_Draw_MouseUp;
                //this.pictureBox1.MouseDown -= this.pictureBox1_Draw_MouseDown;
                //this.pictureBox1.MouseMove -= this.pictureBox1_Draw_MouseMove;
                eventUnregister();
                this.pictureBox1.MouseClick += this.pictureBox1_Text_Click;
                workingMode = WorkingMode.texting;
                curDrawOps = DrawOperation.text;
            }
        }

        private void drawLineCandidate(Point a, Point b, ref PaintEventArgs e)
        {
            if (workingMode == WorkingMode.drawing)
                e.Graphics.DrawLine(System.Drawing.Pens.Red, a, b);
            else if (workingMode == WorkingMode.drawingPolygon)
                e.Graphics.DrawLine(Pens.Yellow, a, b);

        }

        private void drawEllipseCandidate(Point a, Point b, ref PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(System.Drawing.Pens.Aqua, rectFromTwoPoints(a, b));
        }

        private void drawRectangleCandidate(Point a, Point b, ref PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(System.Drawing.Pens.Olive, rectFromTwoPoints(a, b));
        }

        private void undoButton_Click(object sender, EventArgs e)
        {

            if (editList.Count > 0)
            {
                DrawOperation ops = editList[editList.Count - 1];
                switch (ops)
                {
                    case DrawOperation.line:
                        lines.RemoveAt(lines.Count - 1);
                        lines.RemoveAt(lines.Count - 1);
                        break;
                    case DrawOperation.rects:
                        rects.RemoveAt(rects.Count - 1);
                        rects.RemoveAt(rects.Count - 1);
                        break;
                    case DrawOperation.ellipse:
                        ellipses.RemoveAt(ellipses.Count - 1);
                        ellipses.RemoveAt(ellipses.Count - 1);
                        break;
                    case DrawOperation.text:
                        textbox.RemoveAt(textbox.Count - 1);
                        text_point.RemoveAt(text_point.Count - 1);
                        break;
                    case DrawOperation.polygon:
                        polygonList.RemoveAt(polygonList.Count - 1);
                        break;
                    default:
                        break;
                }
                editList.RemoveAt(editList.Count - 1);
            }
            
            var tmp = DrawCandidate;
            DrawCandidate = null;
            this.pictureBox1.Refresh();
            DrawCandidate = tmp;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            lines.Clear();
            rects.Clear();
            ellipses.Clear();
            textbox.Clear();
            text_point.Clear();
            editList.Clear();
            polygonList.Clear();
            var tmp = DrawCandidate;
            DrawCandidate = null;
            this.pictureBox1.Refresh();
            DrawCandidate = tmp;
        }
        //调窗
        
        private void pictureBox1_setWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (!bMouseDown)
            {
                ptStart = new Point(e.X, e.Y);
                //ptEnd = new Point(e.X, e.Y);
            }
            bMouseDown = !bMouseDown;
        }

        private void pictureBox1_setWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            elasticPtEnd = new Point(e.X, e.Y);
            //this.pictureBox1.Refresh();
            int dX = elasticPtEnd.X - ptStart.X;
            int dY = elasticPtEnd.Y - ptStart.Y;
            if (wWidth+(dX / 50) > 0 && wPos+(dY / 50) > 0)
            {
                wWidth += (dX / 50);
                wPos += (dY / 50);
                if (wPos + wWidth / 2 < 4095 && wPos - wWidth / 2 > 0)
                {
                    this.pictureBox1.Image = this.img.fasterProcess(wPos, wWidth, angle/90);
                    this.windowCenterTrackBar.Value = wPos;
                    this.windowWidthTrackBar.Value = wWidth;
                    this.windowStatusLabel.Text = wPos + ", " + wWidth;

                }
            }
            
        }

        private void pictureBox1_setWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            int dX = elasticPtEnd.X - ptStart.X;
            int dY = elasticPtEnd.Y - ptStart.Y;
            if (wWidth + (dX / 50) > 0 && wPos + (dY / 50) > 0)
            {
                wWidth += (dX / 50);
                wPos += (dY / 50);
                if (wPos + wWidth / 2 < 4095 && wPos - wWidth / 2 > 0)
                {
                    this.pictureBox1.Image = this.img.fasterProcess(wPos, wWidth, angle / 90);
                    this.windowCenterTrackBar.Value = wPos;
                    this.windowWidthTrackBar.Value = wWidth;
                    this.windowStatusLabel.Text = wPos + ", " + wWidth;

                }
            }

            bMouseDown = !bMouseDown;
        }

        private void windowTrackbar_Scroll(object sender, EventArgs e)
        {
            if (workingMode != WorkingMode.transforming)
            {
                ShapeListPointer = null;
                DrawCandidate = null;
                eventUnregister();
                this.pictureBox1.MouseDown += this.pictureBox1_setWindow_MouseDown;
                this.pictureBox1.MouseMove += this.pictureBox1_setWindow_MouseMove;
                this.pictureBox1.MouseUp += this.pictureBox1_setWindow_MouseUp;
                workingMode = WorkingMode.transforming;
            }
            TrackBar t = sender as TrackBar;
            pictureBox1.Image = img.fasterProcess(this.windowCenterTrackBar.Value, this.windowWidthTrackBar.Value, angle/90);
            this.windowStatusLabel.Text = this.windowCenterTrackBar.Value + "," + this.windowWidthTrackBar.Value;
            if (t.Name == "windowWidthTrackBar")
            {
                this.wWidth = this.windowWidthTrackBar.Value;
            }
            else if (t.Name == "windowCenterTrackBar")
            {
                this.wPos = this.windowCenterTrackBar.Value;
            }
        }

        private void setWindowButton_Click(object sender, EventArgs e)
        {
            if (workingMode != WorkingMode.transforming)
            {
                ShapeListPointer = null;
                DrawCandidate = null;
                //this.pictureBox1.MouseDown -= this.pictureBox1_Draw_MouseDown;
                //this.pictureBox1.MouseMove -= this.pictureBox1_Draw_MouseMove;
                //this.pictureBox1.MouseUp -= this.pictureBox1_Draw_MouseUp;
                //this.pictureBox1.MouseClick -= this.pictureBox1_Text_Click;

                //this.pictureBox1.MouseUp -= this.pictureBox1_MouseUp_Zoom;
                //this.pictureBox1.MouseDown -= this.pictureBox1_MouseDown_Zoom;
                //this.pictureBox1.MouseMove -= this.pictureBox1_MouseMove_Zoom;
                //this.pictureBox1.MouseWheel -= this.PictureBox1_MouseWheel;
                eventUnregister();
                this.pictureBox1.MouseDown += this.pictureBox1_setWindow_MouseDown;
                this.pictureBox1.MouseMove += this.pictureBox1_setWindow_MouseMove;
                this.pictureBox1.MouseUp += this.pictureBox1_setWindow_MouseUp;
                workingMode = WorkingMode.transforming;
            }
        }

        private void eventUnregister()
        {
            switch (workingMode)
            {
                case WorkingMode.drawing:
                    this.pictureBox1.MouseDown -= this.pictureBox1_Draw_MouseDown;
                    this.pictureBox1.MouseMove -= this.pictureBox1_Draw_MouseMove;
                    this.pictureBox1.MouseUp -= this.pictureBox1_Draw_MouseUp;
                    break;
                case WorkingMode.transforming:
                    this.pictureBox1.MouseDown -= this.pictureBox1_setWindow_MouseDown;
                    this.pictureBox1.MouseMove -= this.pictureBox1_setWindow_MouseMove;
                    this.pictureBox1.MouseUp -= this.pictureBox1_setWindow_MouseUp;
                    break;
                case WorkingMode.zooming:
                    this.pictureBox1.MouseUp -= this.pictureBox1_MouseUp_Zoom;
                    this.pictureBox1.MouseDown -= this.pictureBox1_MouseDown_Zoom;
                    this.pictureBox1.MouseMove -= this.pictureBox1_MouseMove_Zoom;
                    this.pictureBox1.MouseWheel -= this.PictureBox1_MouseWheel;
                    this.pictureBox1.MouseEnter -= this.pictureBox1_MouseEnter;
                    break;
                case WorkingMode.texting:
                    this.pictureBox1.MouseClick -= this.pictureBox1_Text_Click;
                    break;
                case WorkingMode.drawingPolygon:
                    this.pictureBox1.MouseUp -= new MouseEventHandler(pictureBox1_DrawPolygon_MouseUp);
                    this.pictureBox1.MouseDown -= new MouseEventHandler(pictureBox1_DrawPolygon_MouseDown);
                    this.pictureBox1.MouseMove -= new MouseEventHandler(pictureBox1_DrawPolygon_MouseMove);
                    break;
                case WorkingMode.rotating:
                case WorkingMode.nothing:
                    break;
                default:
                    break;
            }
        }

        //旋转
        int angle = 0;
        private void rotateButton_1(object sender, EventArgs e)
        {
            angle += 90;
            angle = angle % 360;
            this.pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

            var tmp = DrawCandidate;
            DrawCandidate = null;
            this.pictureBox1.Refresh();
            DrawCandidate = tmp;
        }
        private Bitmap rotateImage(Bitmap b)
        {
            Bitmap newB = new Bitmap(b.Height, b.Width);

            for (int j = 0; j < b.Height; j++)
            {
                for (int i = 0; i < b.Width; i++)
                {
                    Point tempP = new Point(this.pictureBox1.Height - 1 - j, i);
                    newB.SetPixel(tempP.X, tempP.Y, b.GetPixel(i, j));
                }
            }
            return newB;
        }
        private Point rotatePointClockwise(Point p)
        {
            Point newP;
            switch (angle)
            {
                
                case 0:
                    newP = new Point((int)(p.X * scale), (int)(p.Y *scale));
                    break;
                case 90:
                    newP =  new Point((int)((this.pictureBox1.Image.Height - 1 - p.Y)*scale), (int)(p.X * scale));
                    break;
                case 180:
                    newP = new Point((int)((this.pictureBox1.Image.Width - 1 - p.X)*scale), (int)((this.pictureBox1.Image.Height - 1 - p.Y)*scale));
                    break;
                case 270:
                    newP = new Point((int)(p.Y * scale), (int)((this.pictureBox1.Image.Width - 1 - p.X) * scale));
                    break;
                default:
                    newP = p;
                    break;
            }
            return newP;
        }
        private Point rotatePointCounterClockwise(Point p)
        {
            Point newP;
            switch (angle)
            {

                case 0:
                    newP = new Point((int)(p.X / scale), (int)(p.Y / scale));
                    break;
                case 90:
                    newP = new Point((int)(p.Y / scale), (int)((this.pictureBox1.Width -1 -p.X) / scale));
                    break;
                case 180:
                    newP = new Point((int)((this.pictureBox1.Width - 1 - p.X) / scale), (int)((this.pictureBox1.Height - 1 - p.Y) / scale));
                    break;
                case 270:
                    newP = new Point((int)((this.pictureBox1.Height - 1 - p.Y) / scale), (int)(p.X / scale));
                    break;
                default:
                    newP = p;
                    break;
            }
            return newP;
        }
        private PointF rotatePointFClockwise(PointF p)
        {
            PointF newP;
            switch (angle)
            {

                case 0:
                    newP = new PointF(p.X * scale, p.Y * scale);
                    break;
                case 90:
                    newP = new PointF((this.pictureBox1.Image.Height - 1 - p.Y) * scale, p.X * scale);
                    break;
                case 180:
                    newP = new PointF((this.pictureBox1.Image.Width - 1 - p.X) * scale, (this.pictureBox1.Image.Height - 1 - p.Y) * scale);
                    break;
                case 270:
                    newP = new PointF(p.Y * scale, (this.pictureBox1.Image.Width - 1 - p.X) * scale);
                    break;
                default:
                    newP = p;
                    break;
            }
            return newP;
        }
        private PointF rotatePointFCounterClockwise(PointF p)
        {
            PointF newP;
            switch (angle)
            {

                case 0:
                    newP = new PointF(p.X / scale, p.Y / scale);
                    break;
                case 90:
                    newP = new PointF(p.Y / scale, (this.pictureBox1.Width - 1 - p.X) / scale);
                    break;
                case 180:
                    newP = new PointF((this.pictureBox1.Width - 1 - p.X) / scale, (this.pictureBox1.Height - 1 - p.Y) / scale);
                    break;
                case 270:
                    newP = new PointF((this.pictureBox1.Height - 1 - p.Y) / scale, p.X / scale);
                    break;
                default:
                    newP = p;
                    break;
            }
            return newP;
        }

        //Zoom Button
        private void zoomButton_Click(object sender, EventArgs e)
        {
            if (workingMode != WorkingMode.zooming)
            {
                //this.pictureBox1.MouseClick -= this.pictureBox1_Text_Click;
                //this.pictureBox1.MouseUp -= this.pictureBox1_Draw_MouseUp;
                //this.pictureBox1.MouseDown -= this.pictureBox1_Draw_MouseDown;
                //this.pictureBox1.MouseMove -= this.pictureBox1_Draw_MouseMove;
                eventUnregister();
                this.pictureBox1.MouseUp += this.pictureBox1_MouseUp_Zoom;
                this.pictureBox1.MouseDown += this.pictureBox1_MouseDown_Zoom;
                this.pictureBox1.MouseMove += this.pictureBox1_MouseMove_Zoom;
                this.pictureBox1.MouseWheel += this.PictureBox1_MouseWheel;
                this.pictureBox1.MouseEnter += this.pictureBox1_MouseEnter;
                
                workingMode = WorkingMode.zooming;
            }
            
        }

        //鼠标滑动事件
        float scale = 1.0F;
        //double scale = 1.0;
        bool isSelected;
        void PictureBox1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int numberOfTextLinesToMove = 0;
            numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            if (numberOfTextLinesToMove > 0)
            {
                this.pictureBox1.Height += Convert.ToInt32(this.pictureBox1.Height * 0.1);
                this.pictureBox1.Width += Convert.ToInt32(this.pictureBox1.Width * 0.1);
                scale = (this.pictureBox1.Height * 1.0F) / (this.pictureBox1.Image.Height * 1.0F);
            }
            else if (numberOfTextLinesToMove < 0)
            {
                this.pictureBox1.Height -= Convert.ToInt32(this.pictureBox1.Height * 0.1);
                this.pictureBox1.Width -= Convert.ToInt32(this.pictureBox1.Width * 0.1);
                scale = (this.pictureBox1.Height * 1.0F) / (this.pictureBox1.Image.Height * 1.0F);
            }
            //var tmp = DrawCandidate;
            //DrawCandidate = null;
            this.pictureBox1.Refresh();
            //DrawCandidate = tmp;
        }
        //获取焦点 ，焦点存在才可以使用滚轮
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }
        private void pictureBox1_MouseDown_Zoom(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !(isSelected == true))
            {
                Point p1 = MousePosition;
                mouseDownPoint.X = this.PointToClient(p1).X;
                mouseDownPoint.Y = this.PointToClient(p1).Y;
                isSelected = true;
            }
        }
        private void pictureBox1_MouseMove_Zoom(object sender, MouseEventArgs e)
        {
            Point p1 = MousePosition;
            if (isSelected && IsMouseInPanel())
            {
                this.pictureBox1.Left = this.pictureBox1.Left + (this.PointToClient(p1).X - mouseDownPoint.X);
                this.pictureBox1.Top = this.pictureBox1.Top + (this.PointToClient(p1).Y - mouseDownPoint.Y);

                mouseDownPoint.X = this.PointToClient(p1).X;
                mouseDownPoint.Y = this.PointToClient(p1).Y;
            }
        }
        private void pictureBox1_MouseUp_Zoom(object sender, MouseEventArgs e)
        {
            isSelected = false;
        }

        private bool IsMouseInPanel()
        {
            Point p1 = MousePosition;
            if (this.pictureBox1.Left < PointToClient(p1).X
            && PointToClient(p1).X < this.pictureBox1.Left + this.pictureBox1.Width
            && this.pictureBox1.Top < PointToClient(p1).Y
            && PointToClient(p1).Y < this.pictureBox1.Top + this.pictureBox1.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void navigatePicButton_Click(object sender, EventArgs e)
        {
            if (ChangePicture == null)
                return;
            Button btn = sender as Button;
            if (btn.Name == "nextPicButton")
            {
                img = ChangePicture(false);
            }
            else
            {
                img = ChangePicture(true);
            }
            this.pictureBox1.Image = img.fasterProcess(wPos, wWidth, angle/90);
        }

        private void saveToDiskButton_Click(object sender, EventArgs e)
        {
            saveButton_Click(sender, e);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG (*.png)|*.png|JPEG(*.jpg)|*.jpg|All Files(*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (bmp != null)
            {                
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    int dotPos = sfd.FileName.Length;
                    for (int i = 0; i < sfd.FileName.Length; i++)
                    {
                        if (sfd.FileName[i] == '.')
                        {
                            dotPos = i;
                        }
                    }
                    string saveFormat = sfd.FileName.Substring(dotPos);
                    if (saveFormat == ".jpg" || saveFormat == ".jpeg")
                    {
                        bmp.Save(sfd.FileName, ImageFormat.Jpeg);
                    }
                    else if (saveFormat == ".png")
                    {
                        bmp.Save(sfd.FileName, ImageFormat.Bmp);
                    }
                    else
                    {
                        MessageBox.Show("格式不支持，请选择JPEG或者PNG图片格式。");
                    }
                }
                bmp = null;
            }
            else
            {
                MessageBox.Show("请先保存！");
            }
           
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Graphics draw = Graphics.FromImage(bmp);
            draw.DrawImage(pictureBox1.Image, 0, 0);

            //scale是PictureBox和PictureBox中的图片的倍数 用的时候改一下表达式
            //int scale = pictureBox1.Image.Height / pictureBox1.Height;

            for (int i = 0; i < lines.Count; i += 2)
            {
                draw.DrawLine(System.Drawing.Pens.Red,
                    lines[i].X, lines[i].Y, lines[i + 1].X, lines[i + 1].Y);
            }

            for (int i = 0; i < ellipses.Count; i += 2)
            {
                draw.DrawEllipse(System.Drawing.Pens.Aqua, rectFromTwoPoints(ellipses[i], ellipses[i + 1]));
            }

            for (int i = 0; i < rects.Count; i += 2)
            {
                draw.DrawRectangle(System.Drawing.Pens.Olive, rectFromTwoPoints(rects[i], rects[i + 1]));
            }

            for (int i = 0; i < textbox.Count; i++)
            {
                draw.DrawString(textbox[i], drawfont, drawbrush, text_point[i]);
            }

            if (UpdateImg != null)
                if (bmp != null)
                    UpdateImg(bmp, wWidth, wPos);

            draw.Dispose();
        }

        private void PictureWindow_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        //draw polygon
        List<List<Point>> polygonList = new List<List<Point>>();
        List<Point> polygonPointsList = new List<Point>();
        bool drawingPolygon = false;
        private void drawPolygonButton_Click(object sender, EventArgs e)
        {
            if (workingMode != WorkingMode.drawingPolygon)
            {
                DrawCandidate = drawLineCandidate;
                eventUnregister();
                this.pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_DrawPolygon_MouseUp);
                this.pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_DrawPolygon_MouseDown);
                this.pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_DrawPolygon_MouseMove);
                workingMode = WorkingMode.drawingPolygon;
            }
        }

        private void pictureBox1_DrawPolygon_MouseDown(object sender, MouseEventArgs e)
        {
            if (!drawingPolygon)
            {
                polygonPointsList.Clear();
                drawingPolygon = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                //add one point
                ptStart = new Point(e.X, e.Y);
                polygonPointsList.Add(rotatePointCounterClockwise(ptStart));
            }
            else if (e.Button == MouseButtons.Right)
            {
                //add one polygen rotatePointCounterClockwise
                polygonPointsList.Add(rotatePointCounterClockwise(new Point(e.X, e.Y)));

                List<Point> p = new List<Point>(polygonPointsList);
                editList.Add(DrawOperation.polygon);
                polygonList.Add(p);
                drawingPolygon = false;
                this.pictureBox1.Refresh();
            }
        }

        private void pictureBox1_DrawPolygon_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_DrawPolygon_MouseMove(object sender, MouseEventArgs e)
        {
            if(drawingPolygon)
            {
                elasticPtEnd = new Point(e.X, e.Y);
                this.pictureBox1.Refresh();
            }            
        }



    }
}
