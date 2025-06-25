using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatbotGUI.Models
{
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public string[] Options { get; set; } // Options
        public string CorrectAnswer { get; set; }
    }
}

