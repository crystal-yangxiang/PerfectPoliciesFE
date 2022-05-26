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

        [Display(Name = "Quiz Topic")]
        [Required]
        public string QuizTopic { get; set; }

        [Display(Name = "Quiz Creator Name")]
        [Required]
        public string QuizCreatorName { get; set; }

        [Display(Name = "Quiz Create Data")]
        [Required]
        public DateTime? QuizCreatedDate { get; set; }

        [Display(Name = "Percentage to Pass")]
        [Required]
        public int PassPercentage { get; set; }

    }
}
