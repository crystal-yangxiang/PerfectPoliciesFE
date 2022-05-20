using PerfectPoliciesFE.Models;
using PerfectPoliciesFE.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Services
{
    public class QuestionService
    {
        public static HttpClient _client;

        public QuestionService()
        {
            // if the client has not been instiatated
            if (_client == null)
            {
                // Instantiate a new client
                _client = new HttpClient();
                _client.BaseAddress = new Uri("https://localhost:44302/api/");

                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        // Get All Questions
        public List<Question> GetQuestions()
        {
            var response = _client.GetAsync("Question").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var questions = response.Content.ReadAsAsync<List<Question>>().Result;

            return questions;
        }

        public List<Question> GetQuestionsForQuizId(int id) // create services
        {
            var response = _client.GetAsync($"Question/GetForQuizId/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var questions = response.Content.ReadAsAsync<List<Question>>().Result;

            return questions;

        }


        // Get a single Question
        public Question GetSingleQuestion(int id)
        {
            var response = _client.GetAsync($"Question/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var question = response.Content.ReadAsAsync<Question>().Result;

            return question;
        }


        // Create a new Question

        public void CreateQuestion(QuestionCreate question)
        {
            var response = _client.PostAsJsonAsync("Question", question).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
        }

        // Update a question

        // Delete a question
    }
}
