using Microsoft.CodeAnalysis.Options;
using PerfectPoliciesFE.Models;
using PerfectPoliciesFE.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Services
{
    public class OptionService
    {
        public static HttpClient _client;

        public OptionService()
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

        // Get All Options
        public List<Option> GetOptions()
        {
            var response = _client.GetAsync("Option").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var options = response.Content.ReadAsAsync<List<Option>>().Result;

            return options;
        }

        public List<Option> GetOptionsForQuestionId(int id)
        {
            var response = _client.GetAsync($"Option/GetForQuestionId/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var options = response.Content.ReadAsAsync<List<Option>>().Result;

            return options;
        }


        // Get a single Option
        public Option GetSingleOption(int id)
        {
            var response = _client.GetAsync($"Option/{id}").Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
            var option = response.Content.ReadAsAsync<Option>().Result;

            return option;
        }


        // Create a new option

        public void CreateOption(OptionCreate option)
        {
            var response = _client.PostAsJsonAsync("Option", option).Result;
#if DEBUG
            response.EnsureSuccessStatusCode();
#endif
        }

        // Update a option

        // Delete a option


    }
}
