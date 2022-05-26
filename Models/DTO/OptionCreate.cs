using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Models.DTO
{
    public class OptionCreate
    {
        [Display(Name = "Option Text")]
        [Required]
        public string OptionText { get; set; }

        [Display(Name = "Question Letter")]
        [Required]
        public string OptionLetter { get; set; }

        [Display(Name = "Tick Correct Option")]
        [Required]
        public bool OptionIsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
