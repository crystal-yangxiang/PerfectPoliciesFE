using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Models
{
    public class Question
    {
        [Display(Name = "Question Id")]
        public int QuestionId { get; set; }

        [Display(Name = "Question Topic")]
        public string QuestionTopic { get; set; }

        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        [Display(Name = "Question Image")]
        public string QuestionImageUrl { get; set; }

        //nevagation property
        public ICollection<Option> Options { get; set; }

        public Quiz Quiz { get; set; }

        //FK
        [Display(Name = "Quiz Topic")]
        public int QuizId { get; set; }
    }
}
