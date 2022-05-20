using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Models.DTO
{
    public class QuestionCreate
    {
        public string QuestionTopic { get; set; }
        public string QuestionText { get; set; }
        public string QuestionImageUrl { get; set; }
        public int QuizId { get; set; }
    }
}
