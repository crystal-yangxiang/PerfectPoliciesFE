using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Models
{
    public class Quiz
    {
        [Display(Name = "Quiz Id")]
        public int QuizId { get; set; }

        [Display(Name = "Quiz Title")]
        public string QuizTitle { get; set; }

        [Display(Name = "Quiz Topic")]
        public string QuizTopic { get; set; }

        [Display(Name = "Quiz Creator Name")]
        public string QuizCreatorName { get; set; }

        [Display(Name = "Quiz Create Date")]
        public DateTime? QuizCreatedDate { get; set; }

        [Display(Name = "Pass Percentage")]
        public int PassPercentage { get; set; }
    }
}
