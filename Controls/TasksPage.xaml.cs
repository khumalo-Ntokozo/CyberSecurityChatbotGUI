using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CyberSecurityChatbotGUI.Models;
using CyberSecurityChatbotGUI.Services;

namespace CyberSecurityChatbotGUI.Controls
{
    public partial class TasksPage : UserControl
    {
        private static List<TaskModel> tasks = new List<TaskModel>();

        public TasksPage()
        {
            InitializeComponent();
            LoadTasks();
        }

        public void LoadTasks()
        {
            TaskListBox.ItemsSource = null;
            TaskListBox.ItemsSource = tasks;
        }

        // ✅ NLP: Add full task model
        public void AddTask(TaskModel task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                task.Title = "Untitled Task";

            task.IsCompleted = false;
            tasks.Add(task);
            ActivityLogger.LogAction($"Task added via NLP: {task.Title}");
            LoadTasks();
        }

        // 🔄 Still keep for string-only title input
        public void AddTask(string title)
        {
            var newTask = new TaskModel
            {
                Title = title,
                Description = "",
                ReminderDate = null,
                IsCompleted = false
            };

            tasks.Add(newTask);
            ActivityLogger.LogAction($"Task added via NLP: {title}");
            LoadTasks();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleTextBox.Text.Trim();
            string description = TaskDescriptionTextBox.Text.Trim();
            DateTime? reminderDate = ReminderDatePicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            var newTask = new TaskModel
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate,
                IsCompleted = false
            };

            tasks.Add(newTask);
            ActivityLogger.LogAction($"Task added manually: {title}");
            LoadTasks();
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is TaskModel task)
            {
                task.IsCompleted = true;
                ActivityLogger.LogAction($"Task marked completed: {task.Title}");
                LoadTasks();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is TaskModel task)
            {
                tasks.Remove(task);
                ActivityLogger.LogAction($"Task deleted: {task.Title}");
                LoadTasks();
            }
        }

        private void ClearTextBoxOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && (tb.Text == "Task Title" || tb.Text == "Description"))
            {
                tb.Text = "";
            }
        }

        private void RestoreTextBoxOnFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "TaskTitleTextBox")
                    tb.Text = "Task Title";
                else if (tb.Name == "TaskDescriptionTextBox")
                    tb.Text = "Description";
            }
        }
    }
}