using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPoliciesFE.Models
{
    public class Option
    {

        [Display(Name = "Option Id" )]
        public int OptionId { get; set; }

        [Display(Name = "Option Text")]
        public string OptionText { get; set; }

        [Display(Name = "Option Letter")]
        public string OptionLetter { get; set; }

        [Display(Name = "Option Is Correct")]
        public bool OptionIsCorrect { get; set; }

        //navigation property
        public Question Question { get; set; }

        //FK
        [Display(Name = "Question Id")]
        public int QuestionId { get; set; }


    }
}
