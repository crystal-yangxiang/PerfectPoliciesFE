using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Models.DTO
{
    public class QuizCreate
    {
        [Display(Name = "Quiz Title")]
        [Required]
        [StringLength(100)]
        public string QuizTitle { get; set; }

        public string QuizTopic { get; set; }
        public string QuizCreatorName { get; set; }
        public DateTime? QuizCreatedDate { get; set; }
        public int PassPercentage { get; set; }

    }
}
