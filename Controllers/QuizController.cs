using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerfectPoliciesFE.Models;
using PerfectPoliciesFE.Models.DTO;
using PerfectPoliciesFE.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace PerfectPoliciesFE.Controllers
{
    public class QuizController : Controller
    {
        // Pull service from Startup Class
       
        private readonly IApiRequest<Quiz> _quizService;

        private string controllerName = "Quiz";

        public QuizController(IApiRequest<Quiz> quizService)
        {
            
            _quizService = quizService;
        }

        [HttpPost]
        public IActionResult Filter(IFormCollection collection)
        {
            var result = collection["teacherDDL"].ToString();
            return RedirectToAction("Index", new { filter = result });
        }


        // GET: QuizController
        public ActionResult Index(string filter = "")
        {
            var quizzes = _quizService.GetAll(controllerName);

            var quizDDL = quizzes.Select(c => new SelectListItem
            {
                Value = c.QuizCreatorName,
                Text = c.QuizCreatorName
            });

            ViewBag.QuizDDL = quizDDL;

            if (!String.IsNullOrEmpty(filter))
            {
                var quizFilteredList = quizzes.Where(c => c.QuizCreatorName == filter);
                return View(quizFilteredList);
            }

            return View(quizzes);
        }

        // GET: QuizController/Details/5
        public ActionResult Details(int id)
        {

            // Check to see if there is a token, if not - redirect to the login page

            //FE login check
            if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

            var quiz = _quizService.GetSingle(controllerName, id);
            
           
            return View(quiz);
        }

        // GET: QuizController/Create
        public IActionResult Create(IFormCollection collection)
        {
            QuizCreate quiz = new QuizCreate();
            string filterText = collection["organisationName"].ToString();
            quiz.QuizCreatorName = HttpContext.Session.GetString("organisationName");
            return View(quiz);
        }

        // POST: QuizController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuizCreate quiz)
        {
            try
            {
                Quiz newQuiz = new Quiz()
                {
                    QuizTitle = quiz.QuizTitle,
                    QuizTopic = quiz.QuizTopic,
                    QuizCreatorName = quiz.QuizCreatorName,
                    QuizCreatedDate = quiz.QuizCreatedDate,
                    PassPercentage = quiz.PassPercentage
                };

                _quizService.Create(controllerName, newQuiz);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizController/Edit/5
        public ActionResult Edit(int id)
        {
            if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(_quizService.GetSingle(controllerName, id));
        }

        // POST: QuizController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Quiz quiz)
        {
            try
            {
                if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }
                _quizService.Edit(controllerName, id, quiz);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizController/Delete/5
        public ActionResult Delete(int id)
        {
            if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(_quizService.GetSingle(controllerName, id));
        }

        // POST: QuizController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Quiz quiz)
        {
            try
            {
                if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }
                var test = HttpContext.Request;
                _quizService.Delete(controllerName, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Return a filtered list of quizzes which contain and match input string
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult FilterQuiz(IFormCollection collection)
        {
            string filterText = collection["organisationName"].ToString();

            // save filterText
            HttpContext.Session.SetString("organisationName", filterText);

            //retrieve a list of all quizzes
            var quizzes = _quizService.GetAll(controllerName);

            //filter that list, return the results to a new list
            var filteredList = quizzes.Where(c => c.QuizTitle.ToLower().Contains(filterText.ToLower())).ToList();

            // return this list to the index page
            return View("Index", filteredList);
        }
    }
}
