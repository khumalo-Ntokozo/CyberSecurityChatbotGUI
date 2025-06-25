using System.Collections.Generic;
using System.Linq;
using CyberSecurityChatbotGUI.Models;

namespace CyberSecurityChatbotGUI.Services
{
    public class TaskManager
    {
        public List<CyberTask> Tasks { get; private set; } = new List<CyberTask>();

        public void AddTask(CyberTask task)
        {
            Tasks.Add(task);
            ActivityLogger.LogAction($"Task added: '{task.Title}'" +
                (task.ReminderDate.HasValue ? $" (Reminder set for {task.ReminderDate.Value.ToShortDateString()})" : ""));
        }

        public void MarkCompleted(string title)
        {
            var task = Tasks.FirstOrDefault(t => t.Title == title);
            if (task != null) task.IsCompleted = true;
        }

        public void DeleteTask(string title)
        {
            var task = Tasks.FirstOrDefault(t => t.Title == title);
            if (task != null) Tasks.Remove(task);
        }
    }
}
