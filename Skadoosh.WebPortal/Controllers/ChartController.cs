using Skadoosh.Common.DomainModels;
using Skadoosh.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace Skadoosh.WebPortal.Controllers
{
    public class ChartController : Controller
    {
        public Color[] ColorCollection
        {
            get
            {
                var collection = new Color[]{
                    Color.FromArgb(225, 118, 21),
                    Color.FromArgb(31, 167, 11),
                    Color.FromArgb(11, 16, 167),
                    Color.FromArgb(235, 92, 53),
                    Color.FromArgb(10, 77, 56),
                    Color.FromArgb(32, 75, 51),
                    Color.FromArgb(141, 17, 127)
                };
                return collection;
            }
        }

        public ActionResult Index()
        {
            return View();
        }
        public async Task<FileContentResult> BarChart(int id, int? w, int? h)
        {
            PresenterVM vm = new PresenterVM();
            var responses = await vm.GetResponsesAndLoadDataByQuestionId(id);


            Chart c = new Chart();

            c.AntiAliasing = AntiAliasingStyles.All;
            c.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            c.Width = w.HasValue ? w.Value : 640; //SET HEIGHT
            c.Height = h.HasValue ? h.Value : 480; //SET WIDTH

            ChartArea ca = new ChartArea();

            ca.BackColor = Color.FromArgb(248, 248, 248);
            ca.BackSecondaryColor = Color.FromArgb(255, 255, 255);
            ca.BackGradientStyle = GradientStyle.TopBottom;

            ca.AxisY.IsMarksNextToAxis = true;
            ca.AxisY.Title = "Total Responses";
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

            var cnt = 0;
            foreach (var r in responses.ToLookup(x => x.OptionId))
            {
                var count = r.Count();
                DataPoint p = new DataPoint();
                p.Color = ColorCollection[cnt];
                p.BackSecondaryColor = ConvertToDarker(p.Color);
                p.BackGradientStyle = GradientStyle.LeftRight;
                p.XValue = r.Key;
                p.YValues = new double[] { count };
                cnt++;
                s.Points.Add(p);
            }


            c.Series.Add(s);
            var ms = new MemoryStream();
            c.SaveImage(ms, ChartImageFormat.Png);

            var fs = new FileContentResult(ms.ToArray(), "image/png");
            return fs;
        }
        public async Task<FileContentResult> PieChart(int id, int? w, int? h)
        {

            PresenterVM vm = new PresenterVM();
            var responses = await vm.GetResponsesAndLoadDataByQuestionId(id);
            
            Chart c = new Chart();
            c.Titles.Add(vm.CurrentQuestion.QuestionText);
            c.AntiAliasing = AntiAliasingStyles.All;
            c.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            c.Width =w.HasValue? w.Value: 640; //SET HEIGHT
            c.Height = h.HasValue?h.Value: 480; //SET WIDTH

            ChartArea ca = new ChartArea();
          
            ca.AxisY.IsMarksNextToAxis = true;
            ca.AxisY.Title = "Total Responses";
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
            s.ChartType = SeriesChartType.Pie;
            s.Font = new Font("Lucida Sans Unicode", 12f, FontStyle.Bold);

            Legend l = new Legend("test");
            l.Docking = Docking.Bottom;
            l.BackColor = Color.White;
            l.Alignment = StringAlignment.Center;

            c.Legends.Add(l);
            var cnt = 0;
            foreach (var r in responses.ToLookup(x=>x.OptionId))
            {
                var count = r.Count();
                DataPoint p = new DataPoint();
                p.Color = ColorCollection[cnt];
                p.BackSecondaryColor = ConvertToDarker(p.Color);
                p.BackGradientStyle = GradientStyle.LeftRight;
                p.XValue = cnt;
                p.Label = count.ToString();
             
              
                p.LegendText = vm.CurrentQuestion.Options.First(x => x.Id == r.Key).OptionText;
                p.YValues = new double[] {count};

                cnt++;
                s.Points.Add(p);
            }


            c.Series.Add(s);
            var ms = new MemoryStream();
            c.SaveImage(ms, ChartImageFormat.Png);

            var fs = new FileContentResult(ms.ToArray(), "image/png");
            return fs;
        }

        private Color ConvertToDarker(Color c)
        {
           var f=0.5;
           var Rnew = (int)Math.Round((1 - f) * c.R + f * 255);
           var Gnew = (int)Math.Round((1 - f) * c.G + f * 255);
           var Bnew = (int)Math.Round((1 - f) * c.B + f * 255);
           return Color.FromArgb(Rnew, Gnew, Bnew);
        }
    }
}
