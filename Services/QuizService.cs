using PerfectPoliciesFE.Models;
using PerfectPoliciesFE.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Services
{
    public class QuizService
    {
        public static HttpClient _client;

        public QuizService()
        {
            // if the client has not been instiatated
            if (_client == null)
            {
                // Instantiate a new client
                _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:44302/api/");
                // Dictates to the API which specific data to return to the front end. If no json, API will send nothing.
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        // Get All Quizzes
        public List<Quiz> GetQuizzes()
        {
            // /api/Quiz
            var response = _client.GetAsync("Quiz").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var quizzes = response.Content.ReadAsAsync<List<Quiz>>().Result;

            return quizzes;
        }

        // Get a single Quiz
        public Quiz GetSingleQuiz(int id)
        {
            // /api/Quiz/1
            var response = _client.GetAsync($"Quiz/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var quiz = response.Content.ReadAsAsync<Quiz>().Result;

            return quiz;
        }


        // Create a new quiz

        public void CreateQuiz(QuizCreate quiz)
        {
            var response = _client.PostAsJsonAsync("Quiz", quiz).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
        }

        // Update a quiz

        // Delete a quiz

    }
}

