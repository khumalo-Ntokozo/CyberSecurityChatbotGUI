using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CyberSecurityChatbotGUI.Controls;
using CyberSecurityChatbotGUI.Services;



namespace CyberSecurityChatbotGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NLPInterpreter nlp = new NLPInterpreter();
        private TasksPage taskPage = new TasksPage();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInputTextBox.Text;
            var intent = nlp.DetectIntent(userInput);

            switch (intent)
            {
                case NLPInterpreter.IntentType.AddTask:
                    var task = nlp.ExtractTaskFromInput(userInput);
                    taskPage.AddTask(task); // ✅ Use the TaskModel
                    MainContentControl.Content = taskPage;
                    break;

                case NLPInterpreter.IntentType.SetReminder:
                    MessageBox.Show("Detected intent to set a reminder.");
                    ActivityLogger.LogAction("NLP: Detected Set Reminder intent");
                    break;

                case NLPInterpreter.IntentType.StartQuiz:
                    MainContentControl.Content = new Controls.QuizPage();
                    ActivityLogger.LogAction("NLP: Detected Start Quiz intent");
                    break;

                case NLPInterpreter.IntentType.ShowLog:
                    MessageBox.Show(string.Join("\n", ActivityLogger.GetRecentActions()));
                    ActivityLogger.LogAction("NLP: Detected Show Log intent");
                    break;

                case NLPInterpreter.IntentType.CybersecurityQuestion:
                    if (userInput.ToLower().Contains("phishing"))
                    {
                        MessageBox.Show("Phishing is a cyberattack that tries to trick you into giving sensitive info. Always check email sources and links!");
                    }
                    else if (userInput.ToLower().Contains("strong passwords"))
                    {
                        MessageBox.Show("Strong passwords use a mix of letters, numbers, and symbols. Avoid common words and update regularly.");
                    }
                    else if (userInput.ToLower().Contains("privacy"))
                    {
                        MessageBox.Show("Protect your privacy by managing app permissions, using secure connections, and being cautious with personal info online.");
                    }
                    else
                    {
                        MessageBox.Show("Sorry, I don't have info on that topic yet.");
                    }
                    ActivityLogger.LogAction($"NLP: Provided cybersecurity info on topic.");
                    break;


                default:
                    MessageBox.Show("I'm not sure what you mean. Can you rephrase?");
                    break;
            }

            UserInputTextBox.Text = "";
        }

        // Handles "Quiz" button click
        private void OpenQuiz_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new Controls.QuizPage();
            ActivityLogger.LogAction("User opened the Quiz Page.");
        }

        // Handles "Tasks" button click
        private void OpenTasks_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new Controls.TasksPage(); // Make sure TaskPage exists
            ActivityLogger.LogAction("User opened the Task Page.");
        }

        // Handles "Log" button click
        private Controls.ActivityLogPage logPage = new Controls.ActivityLogPage();
        private void OpenLog_Click(object sender, RoutedEventArgs e)
        {
            logPage.LoadLog();  // Refresh logs every time the page opens
            MainContentControl.Content = logPage;
            ActivityLogger.LogAction("User opened the activity log page.");
        }

        private void ClearUserInputTextBox(object sender, RoutedEventArgs e)
        {
            if (UserInputTextBox.Text == "Type your message...")
            {
                UserInputTextBox.Text = "";
            }
        }

        private void RestoreUserInputTextBox(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserInputTextBox.Text))
            {
                UserInputTextBox.Text = "Type your message...";
            }
        }

        private void UserInputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserInputTextBox.Text == "Type your message...")
            {
                UserInputTextBox.Text = "";
                UserInputTextBox.Foreground = Brushes.Black;
            }
        }

        private void UserInputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserInputTextBox.Text))
            {
                UserInputTextBox.Text = "Type your message...";
                UserInputTextBox.Foreground = Brushes.Gray;
            }
        }


    }
}
