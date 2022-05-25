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

namespace PerfectPoliciesFE.Controllers
{
    public class OptionController : Controller
    {
        private readonly IApiRequest<Quiz> _quizService;
        private readonly IApiRequest<Question> _questionService;
        private readonly IApiRequest<Option> _optionService;

        public OptionController(IApiRequest<Quiz> quizService, IApiRequest<Question> questionService, IApiRequest<Option> optionService)
        {
            _optionService = optionService;
            _questionService = questionService;
            _quizService = quizService;
        }

        // GET: OptionController
        public ActionResult Index()
        {
            var options = _optionService.GetAll("Option");
            return View(options);
        }

        /// <summary>
        /// return a list of options only under sepcific QuestionId
        /// </summary>
        /// <param name="id">questionId</param>
        /// <returns></returns>
        public ActionResult OptionsForQuestionId(int id)
        {
            var options = _optionService.GetChildrenForParentID("Option", "GetForQuestionId", id);
            return View("Index", options);
        }

        // GET: OptionController/Details/5
        public ActionResult Details(int id)
        {
            // Check to see if there is a token, if not - redirect to the login page

            //FE login check
            if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }
            var option = _optionService.GetSingle("Option", id);
            return View(option);
        }

        // GET: OptionController/Create
        public ActionResult Create()
        {
            //get a list of all questions
            var questions = _questionService.GetAll("Question");

            //Create a list of selectListItems from the Question list

            var questionSelect = questions.Select(c => new SelectListItem
            {
                Value = c.QuestionId.ToString(),
                Text = c.QuestionText
            }).ToList();

            // Store this list in memory

            // ViewBag - Dynamic Object

            ViewBag.QuestionSelect = questionSelect;

            // ViewData - Key/Value(string/object) Dictionary

            ViewData.Add("QuestionSelect2", questionSelect);


            return View();
        }

        // POST: OptionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OptionCreate option)
        {
            try
            {
                Option newOption = new Option()
                {
                    OptionText = option.OptionText,
                    OptionLetter = option.OptionLetter,
                    OptionIsCorrect = option.OptionIsCorrect,
                    QuestionId = option.QuestionId
                };

                _optionService.Create("option", newOption);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OptionController/Edit/5
        public ActionResult Edit(int id)
        {
            if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(_optionService.GetSingle("Option", id));
        }

        // POST: OptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Option option)
        {
            try
            {
                if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }
                _optionService.Edit("Option", id, option);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OptionController/Delete/5
        public ActionResult Delete(int id)
        {
            if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(_optionService.GetSingle("Option", id));
        }

        // POST: OptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Option option)
        {
            try
            {
                if (Helpers.AuthHelper.IsNotLoggedIn(HttpContext))
                {
                    return RedirectToAction("Login", "Auth");
                }

                var test = HttpContext.Request;
                _optionService.Delete("Option", id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
