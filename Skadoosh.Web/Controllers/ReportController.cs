using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.Windows.Forms;

namespace Skadoosh.Web.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        Bitmap bm = null;
        bool isRunning = false;
        public ActionResult Index()
        {
            return View();
        }

        public FileContentResult ImageReport()
        {
            System.Web.UI.DataVisualization.Charting.pi
            Chart c = new Chart();
            c.AntiAliasing = AntiAliasingStyles.All;
            c.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            c.Width = 640; //SET HEIGHT
            c.Height = 480; //SET WIDTH

            ChartArea ca = new ChartArea();
            ca.BackColor = Color.FromArgb(248, 248, 248);
            ca.BackSecondaryColor = Color.FromArgb(255, 255, 255);
            ca.BackGradientStyle = GradientStyle.TopBottom;

            ca.AxisY.IsMarksNextToAxis = true;
            ca.AxisY.Title = "Gigabytes Used";
            ca.AxisY.LineColor = Color.FromArgb(157, 157, 157);
            ca.AxisY.MajorTickMark.Enabled = true;
            ca.AxisY.MinorTickMark.Enabled = true;
            ca.AxisY.MajorTickMark.LineColor = Color.FromArgb(157, 157, 157);
            ca.AxisY.MinorTickMark.LineColor = Color.FromArgb(200, 200, 200);
            ca.AxisY.LabelStyle.ForeColor = Color.FromArgb(89, 89, 89);
            ca.AxisY.LabelStyle.Format = "{0:0.0}";
            ca.AxisY.LabelStyle.IsEndLabelVisible = false;
            ca.AxisY.LabelStyle.Font = new Font("Calibri", 4, FontStyle.Regular);
            ca.AxisY.MajorGrid.LineColor = Color.FromArgb(234, 234, 234);

            ca.AxisX.IsMarksNextToAxis = true;
            ca.AxisX.LabelStyle.Enabled = false;
            ca.AxisX.LineColor = Color.FromArgb(157, 157, 157);
            ca.AxisX.MajorGrid.LineWidth = 0;
            ca.AxisX.MajorTickMark.Enabled = true;
            ca.AxisX.MinorTickMark.Enabled = true;
            ca.AxisX.MajorTickMark.LineColor = Color.FromArgb(157, 157, 157);
            ca.AxisX.MinorTickMark.LineColor = Color.FromArgb(200, 200, 200);

            c.ChartAreas.Add(ca);

            Series s = new Series();
            s.Font = new Font("Lucida Sans Unicode", 6f);
            s.Color = Color.FromArgb(215, 47, 6);
            s.BorderColor = Color.FromArgb(159, 27, 13);
            s.BackSecondaryColor = Color.FromArgb(173, 32, 11);
            s.BackGradientStyle = GradientStyle.LeftRight;

            
        
            for (int x = 0; x < 6; x++ )
            {
                DataPoint p = new DataPoint();
                p.XValue = x;
                p.YValues = new double[] { x + 3 };
                p.Label = "t" + x;
                p.LegendText = "t" + x;
        
                s.Points.Add(p);
                var l = new Legend("x" + x.ToString());
                l.Title = "x" + x;
                c.Legends.Add(l);

            }

            c.Series.Add(s);
            var ms = new MemoryStream();
            c.SaveImage(ms, ChartImageFormat.Png);
            //var m_thread = new Thread(DoWork);
            //m_thread.SetApartmentState(ApartmentState.STA);
            //m_thread.Start();
            //m_thread.Join();
            //isRunning = true;
            //while (isRunning)
            //{
            //    int x = 10;
            //}

            //var request = HttpWebRequest.Create("http://localhost:1704/report/Index");
            //Bitmap bitmap;
            //using (Stream stream = request.GetResponse().GetResponseStream())
            //{
            //    bitmap = new Bitmap(stream);
            //}
            //var ms = new MemoryStream();
            //bm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
           

            var fs = new FileContentResult(ms.ToArray(), "image/png");
            return fs;
        }
        private void DoWork()
        {
            Bitmap bm = null;
            WebBrowser wb = new WebBrowser() { Width = 800, Height = 600 };
           
            wb.Navigate("http://localhost:1704/report/Index");
            wb.DocumentCompleted += (e, a) =>
            {
                using (var g = wb.CreateGraphics())
                {
                    bm = new Bitmap(wb.Size.Width, wb.Size.Height, g);

                    wb.DrawToBitmap(bm, wb.ClientRectangle);
                    isRunning = false;
                }
            };
            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }

            wb.Dispose();

        }
        public byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

    }




}
