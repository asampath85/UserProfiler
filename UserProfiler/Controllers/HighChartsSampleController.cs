using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using UserProfiler.Models;

namespace UserProfiler.Controllers
{
    public class HighChartsSampleController : Controller
    {
        //
        // GET: /HighChartsSampleModel/

        public ActionResult Index()
        {
            var highchartSample = new List<HighChartsSampleModel>
            {
                new HighChartsSampleModel() {Parameters = "Event", GoodScore = 23.45D, AverageScore = 15.32D,BadScore = 9.4D},
                new HighChartsSampleModel() {Parameters = "Weather",GoodScore=45.67D,AverageScore = 33.24D,BadScore = 12.23D},
                new HighChartsSampleModel() {Parameters = "User Review",GoodScore=67.23D,AverageScore = 31.23D,BadScore = 10.11D},
                new HighChartsSampleModel() {Parameters = "Tweets",GoodScore = 89.67D,AverageScore = 12.33D,BadScore = 3.43D},
                new HighChartsSampleModel() {Parameters = "Persona",GoodScore=38.34D,AverageScore = 25.34D,BadScore = 16.43D}
            };

            var xDataParameters = highchartSample.Select(i => i.Parameters).ToArray();
            var goodScore = highchartSample.Select(i => new object[] { i.GoodScore }).ToArray();
            var badScore = highchartSample.Select(i => new object[] { i.BadScore }).ToArray();
            var averageScore = highchartSample.Select(i => new object[] { i.AverageScore }).ToArray();

            var chart = new Highcharts("chart");
            chart.InitChart(new Chart{DefaultSeriesType = ChartTypes.Bar});
            chart.SetTitle(new Title {Text = "Risk Score Profiling"});
            chart.SetSubtitle(new Subtitle {Text = "Risk predicting using social media"});
            chart.SetXAxis(new XAxis { Categories = xDataParameters });
            chart.SetYAxis(new YAxis{ Title = new YAxisTitle{Text = "Months"}});
            chart.SetLegend(new Legend{ Enabled = true,});
            chart.SetTooltip(new Tooltip
            {
                Enabled = true,
                Formatter = @"function(){return '<b>' + this.series.name +'</b><br/>' + this.x+':' + this.y;}"
            });
            chart.SetPlotOptions(new PlotOptions
            {
                Bar = new PlotOptionsBar
                {
                    DataLabels = new PlotOptionsBarDataLabels{Enabled = true},
                    PointWidth = 12,
                    GroupPadding = 0.1,
                    Shadow = true,
                    BorderWidth = 2
                }
            });
            chart.SetSeries(new[]
            {
                new Series{Name = "Good Score",Data = new Data(goodScore), Color = Color.LightGreen},
                new Series{Name ="Average Score",Data = new Data(averageScore),Color=Color.Orange}, 
                new Series{Name ="Bad Score",Data = new Data(badScore),Color = Color.Red}, 
            });
            
            return View(chart);
        }

    }
}
