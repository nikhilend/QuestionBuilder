using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBuilder.DTO
{
    public class Question
    {
        public string? Desc { get; set; }
        public Options? Options {  get; set; }   
        public string? CorrectAnswer { get; set; }
    }
}
