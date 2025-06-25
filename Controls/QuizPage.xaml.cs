using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CyberSecurityChatbotGUI.Models;
using CyberSecurityChatbotGUI.Services;

namespace CyberSecurityChatbotGUI.Controls
{
    public partial class QuizPage : UserControl
    {
        private List<QuizQuestion> questions = new List<QuizQuestion>();
        private int currentQuestionIndex = 0;
        private int score = 0;
        private string selectedAnswer = "";

        public QuizPage()
        {
            InitializeComponent();
            LoadQuestions();
            ShowQuestion();
        }

        private void LoadQuestions()
        {
            questions.Add(new QuizQuestion
            {
                QuestionText = "What should you do if you receive a suspicious email asking for your password?",
                Options = new[] { "Reply with your password", "Ignore it", "Report it as phishing", "Forward it to friends" },
                CorrectAnswer = "Report it as phishing"
            });

            questions.Add(new QuizQuestion
            {
                QuestionText = "True or False: Using the same password for every account is safe.",
                Options = new[] { "True", "False" },
                CorrectAnswer = "False"
            });

            questions.Add(new QuizQuestion
            {
                QuestionText = "Which of these is a strong password?",
                Options = new[] { "123456", "MyBirthday2020", "P@ssw0rd!", "password" },
                CorrectAnswer = "P@ssw0rd!"
            });

            // Add more questions (you need at least 10)
            questions.Add(new QuizQuestion
            {
                QuestionText = "What does 2FA stand for?",
                Options = new[] { "Two-Factor Authentication", "Two-Face Alert", "Two-Firewall Agreement", "Trusted Firewall Access" },
                CorrectAnswer = "Two-Factor Authentication"
            });

            questions.Add(new QuizQuestion
            {
                QuestionText = "True or False: It's okay to share your login info with friends.",
                Options = new[] { "True", "False" },
                CorrectAnswer = "False"
            });

            questions.Add(new QuizQuestion
            {
                QuestionText = "Which one helps protect your online privacy?",
                Options = new[] { "Incognito mode", "Public Wi-Fi", "Sharing location", "Clicking ads" },
                CorrectAnswer = "Incognito mode"
            });

            questions.Add(new QuizQuestion
            {
                QuestionText = "Phishing attempts often come through...",
                Options = new[] { "Emails", "Apps", "Games", "USB drives" },
                CorrectAnswer = "Emails"
            });

            questions.Add(new QuizQuestion
            {
                QuestionText = "Which of these is a sign of a scam website?",
                Options = new[] { "HTTPS in URL", "Spelling mistakes", "Verified badge", "Lock icon" },
                CorrectAnswer = "Spelling mistakes"
            });

            questions.Add(new QuizQuestion
            {
                QuestionText = "True or False: Antivirus software should be updated regularly.",
                Options = new[] { "True", "False" },
                CorrectAnswer = "True"
            });

            questions.Add(new QuizQuestion
            {
                QuestionText = "Which one should you avoid on public Wi-Fi?",
                Options = new[] { "Browsing news", "Streaming video", "Online banking", "Reading emails" },
                CorrectAnswer = "Online banking"
            });
        }

        private void ShowQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                QuestionTextBlock.Text = $"Quiz complete! You scored {score}/{questions.Count}.";
                AnswersPanel.Children.Clear();
                FeedbackTextBlock.Text = score >= 7 ? "Great job! You're a cybersecurity pro!" : "Keep learning to stay safe online!";
                ActivityLogger.LogAction($"Quiz completed. Score: {score}/{questions.Count}");
                return;
            }

            var q = questions[currentQuestionIndex];
            QuestionTextBlock.Text = q.QuestionText;
            AnswersPanel.Children.Clear();

            foreach (var option in q.Options)
            {
                RadioButton rb = new RadioButton
                {
                    Content = option,
                    GroupName = "Answers"
                };
                rb.Checked += (s, e) => selectedAnswer = option;
                AnswersPanel.Children.Add(rb);
            }

            FeedbackTextBlock.Text = "";
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedAnswer))
            {
                MessageBox.Show("Please select an answer before submitting.");
                return;
            }

            var q = questions[currentQuestionIndex];
            if (selectedAnswer == q.CorrectAnswer)
            {
                score++;
                FeedbackTextBlock.Text = "Correct! ✅";
            }
            else
            {
                FeedbackTextBlock.Text = $"Incorrect ❌ - The correct answer is: {q.CorrectAnswer}";
            }

            currentQuestionIndex++;
            selectedAnswer = "";

            // Delay before next question
            System.Threading.Tasks.Task.Delay(1200).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => ShowQuestion());
            });
        }
    }
}
