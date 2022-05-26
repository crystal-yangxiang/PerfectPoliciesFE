using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Models.DTO
{
    public class QuestionCreate
    {
        [Display(Name = "Question Topic")]
        [Required]
        public string QuestionTopic { get; set; }

        [Display(Name = "Question Text")]
        [Required]
        public string QuestionText { get; set; }

        [Display(Name = "Question Image")]
        public string QuestionImageUrl { get; set; }

        public int QuizId { get; set; }
    }
}
