using System;
using CyberSecurityChatbotGUI.Models;

namespace CyberSecurityChatbotGUI.Services
{
    public class NLPInterpreter
    {
        public enum IntentType
        {
            AddTask,
            SetReminder,
            StartQuiz,
            ShowLog,
            CybersecurityQuestion,  // new intent
            Unknown
        }

        public IntentType DetectIntent(string input)
        {
            input = input.ToLower();

            if (input.Contains("task") || input.Contains("remind me"))
                return IntentType.AddTask;

            if (input.Contains("reminder"))
                return IntentType.SetReminder;

            if (input.Contains("quiz"))
                return IntentType.StartQuiz;

            if (input.Contains("log"))
                return IntentType.ShowLog;

            // Detect cybersecurity questions
            if (input.Contains("phishing") || input.Contains("strong passwords") || input.Contains("privacy"))
                return IntentType.CybersecurityQuestion;

            return IntentType.Unknown;
        }



        public TaskModel ExtractTaskFromInput(string input)
        {
            var task = new TaskModel();
            input = input.ToLower();

            // Title detection
            if (input.Contains("remind me to"))
            {
                string[] parts = input.Split(new[] { "remind me to" }, StringSplitOptions.None);
                if (parts.Length > 1)
                    task.Title = parts[1].Split(new[] { " in ", " tomorrow", " description" }, StringSplitOptions.None)[0].Trim();
            }
            else if (input.Contains("task to"))
            {
                string[] parts = input.Split(new[] { "task to" }, StringSplitOptions.None);
                if (parts.Length > 1)
                    task.Title = parts[1].Split(new[] { " in ", " tomorrow", " description" }, StringSplitOptions.None)[0].Trim();
            }
            else
            {
                task.Title = "New Task";
            }

            // Reminder detection
            if (input.Contains("tomorrow"))
            {
                task.ReminderDate = DateTime.Now.AddDays(1);
            }
            else if (input.Contains("in 3 days"))
            {
                task.ReminderDate = DateTime.Now.AddDays(3);
            }

            // Description detection
            if (input.Contains("description"))
            {
                string[] parts = input.Split(new[] { "description" }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    task.Description = parts[1].Trim(' ', '.', ':', '"', '\'');
                }
            }
            else
            {
                task.Description = "No description provided.";
            }

            return task;
        }
    }
}
