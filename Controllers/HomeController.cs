using ChartJSCore.Helpers;
using ChartJSCore.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfectPoliciesFE.Models;
using PerfectPoliciesFE.Models.ViewModels;
using PerfectPoliciesFE.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiRequest<QuestionsPerQuizViewModel> _reportService;

        public HomeController(ILogger<HomeController> logger, IApiRequest<QuestionsPerQuizViewModel> reportService)
        {
            _logger = logger;
            _reportService = reportService;

        }

        public IActionResult QuizList()
        {
            string apiURL = "https://localhost:44302/api/quiz";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage message = client.GetAsync(apiURL).Result;
                List<Quiz> quizzes = message.Content.ReadAsAsync<List<Quiz>>().Result;

                return View(quizzes);
            }
        }

        public IActionResult DisplayReport()
        {

            // Retrive the data from the API
            var reportData = _reportService.GetAll("Report", "QuestionPerQuizReport");

            // Define the empty chart

            Chart chart = new Chart();

            //Define the chart type
            chart.Type = Enums.ChartType.Bar;

            // Create an empty data object for the chart
            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();

            //set the labels for the X axis
            data.Labels = reportData.Select(c => c.QuizTitle).ToList();

            BarDataset dataset = new BarDataset()
            {
                Label = "Question Count",
                Data = reportData.Select(c => (double?)c.QuestionCount).ToList(),
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.CreateRandomChartColor(false),
                    ChartColor.CreateRandomChartColor(false),
                    ChartColor.CreateRandomChartColor(false),
                    ChartColor.CreateRandomChartColor(false)
                }};

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            ViewData["chart"] = chart;

            return View("DisplayReport");

        }

        public IActionResult ExportData()
        {
            //Get this data for this report (chanllege - find a way to improve this)
            var reportData = _reportService.GetAll("Report", "QuestionPerQuizReport");

            //Create a memory stream
            var stream = new MemoryStream();

            //Create a streamWriter to populate the memorystream
            using(var writeFile = new StreamWriter(stream, leaveOpen: true))
            {
                //generate the records to be written in the right format
                var csv = new CsvWriter(writeFile, CultureInfo.CurrentCulture,true);
                //write this csv data via the streamWriter to the memory stream
                csv.WriteRecords(reportData);
            }

            stream.Position = 0; // stream starts from 0, read the value out and reset to 0

            return File(stream, "applicaton/octect.stream", $"ReportData_{DateTime.Now.ToString("ddMM_HHmmss")}.csv");

            // "octect.stream" tells the brower do not try to open me
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Help()
        {
            return View("Help");
        }
    }
}
