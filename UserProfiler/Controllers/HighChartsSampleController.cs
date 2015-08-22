using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
                new HighChartsSampleModel() {Month = "Jan", Parameters = "Event"},
                new HighChartsSampleModel() {Month = "Feb", Parameters = "Weather"},
                new HighChartsSampleModel() {Month = "Mar", Parameters = "User Review"},
                new HighChartsSampleModel() {Month = "Apr", Parameters = "Tweets"},
                new HighChartsSampleModel() {Month = "May", Parameters = "Persona"}
            };

            var xDataMonth = highchartSample.Select(i => i.Month).ToArray();
            string[] yDataParameters = highchartSample.Select(i => i.Parameters).ToArray();

            var chart = new Highcharts("chart");
            chart.InitChart(new Chart{DefaultSeriesType = ChartTypes.Line});
            chart.SetTitle(new Title {Text = "Risk Score Profiling"});
            chart.SetSubtitle(new Subtitle {Text = "Sample Graph"});
            chart.SetXAxis(new XAxis { Categories = yDataParameters });
            chart.SetYAxis(new YAxis{ Title = new YAxisTitle{Text = "Social Media"}});
            chart.SetTooltip(new Tooltip
            {
                Enabled = true,
                Formatter = @"function(){return '<b>' + this.series.name +'</b><br/>' + this.x+':' + this.y;}"
            });
            chart.SetPlotOptions(new PlotOptions
            {
                Line = new PlotOptionsLine {DataLabels = new PlotOptionsLineDataLabels{Enabled = true}}
            });
            chart.SetSeries(new[] {new Series{Name = "Risk Score",Data = new Data(xDataMonth)}});
            return View(chart);
        }

    }
}
