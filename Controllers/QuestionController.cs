using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerfectPoliciesFE.Models;
using PerfectPoliciesFE.Models.DTO;
using PerfectPoliciesFE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace PerfectPoliciesFE.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IApiRequest<Quiz> _quizService;
        private readonly IApiRequest<Question> _questionService;


        public QuestionController(IApiRequest<Quiz> quizService, IApiRequest<Question> questionService, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _questionService = questionService;
            _quizService = quizService;

        }

        // GET: QuestionController
        public ActionResult Index()
        {
            var questions = _questionService.GetAll("Question");
            return View(questions);
        }

        // GET: QuestionController/
        public ActionResult QuestionsForQuizId(int id)
        {
            var questions = _questionService.GetChildrenForParentID("Question", "GetForQuizId", id);
            return View("Index", questions);
        }

        // GET: QuestionController/Details/5
        public ActionResult Details(int id)
        {
            // Check to see if there is a token, if not - redirect to the login page

            //FE login check
            //if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
            //{
            //    return RedirectToAction("Login", "Auth");
            //}

            var question = _questionService.GetSingle("Question", id);
            return View(question);
        }

        // GET: QuestionController/Create
        public ActionResult Create()
        {
            // Get a list of all quizzes
            var quizzes = _quizService.GetAll("Quiz"); //get all quizes

            // Create a List of selectlistitems from the quiz list

            var quizSelect = quizzes.Select(c => new SelectListItem
            {
                Value = c.QuizId.ToString(),
                Text = c.QuizTitle
            }).ToList();

            // Store this list in memory

            // ViewBag - Dynamic Object

            ViewBag.QuizSelect = quizSelect;

            // ViewData - Key/Value(string/object) Dictionary

            ViewData.Add("QuizSelect2", quizSelect);

            return View();
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionCreate question)
        {
            try
            {
                Question newQuestion = new Question()
                {
                    QuestionTopic = question.QuestionTopic,
                    QuestionText = question.QuestionText,
                    QuestionImageUrl = question.QuestionImageUrl,
                    QuizId = question.QuizId
                };

                _questionService.Create("Question", newQuestion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Edit/5
        public ActionResult Edit(int id)
        {
            if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(_questionService.GetSingle("Question", id));
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Question question)
        {
            try
            {
                if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }
                _questionService.Edit("Question", id, question);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionController/Delete/5
        public ActionResult Delete(int id)
        {
            if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(_questionService.GetSingle("Question", id));
        }

        // POST: QuestionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Question question)
        {
            try
            {
                if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }
                var test = HttpContext.Request;
                _questionService.Delete("Question", id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            string folderRoot = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\Uploads");
            string filePath = Path.Combine(folderRoot, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { success = true, message = "File Uploaded" });
        }
    }
}
