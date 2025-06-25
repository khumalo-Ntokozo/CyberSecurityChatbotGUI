# CyberSecurityChatbotGUI

This project is a Cybersecurity Awareness Chatbot developed using C# and WPF (Windows Presentation Foundation). The chatbot simulates intelligent user interaction by responding to natural language inputs and guiding users through various cybersecurity tasks and learning activities. It combines core programming concepts like GUI development, basic NLP (Natural Language Processing), task management, and user activity logging.

✅ Key Features
Natural Language Interaction:
Users can type in phrases like "Remind me to update my password tomorrow" or "Create a task to review privacy settings," and the chatbot intelligently interprets the intent and acts on it (e.g., adding tasks with reminders and descriptions).

Task Assistant Page:
A GUI where users can add, complete, and delete cybersecurity-related tasks. Completed tasks are styled differently (e.g., faded or grayed out) to indicate progress.

Cybersecurity Quiz Game:
Users can test their cybersecurity knowledge through a built-in multiple-choice quiz. The bot can start the quiz using natural commands like “Start quiz.”

Activity Log Viewer:
Keeps track of recent user actions such as task creation, task completion, and quiz activity. Users can type "Show log" or "List my recent actions" to view their latest interactions.

Cybersecurity Knowledge Base:
The chatbot responds to cybersecurity-related questions like:

“Tell me about phishing”

“Tips for strong passwords”

“What should I know about privacy?”

GUI Navigation:
Users can navigate between Tasks, Quiz, and Log using dedicated buttons. Each page is modular and cleanly separated for clarity and maintainability.

💡 Technical Overview
Framework: WPF (.NET)

Language: C#

Architecture: Modular with separate components for GUI controls (TasksPage, QuizPage), services (NLPInterpreter, ActivityLogger), and models (TaskModel).

NLP System: A basic keyword-based interpreter to identify user intents like adding a task, setting a reminder, starting a quiz, or asking a cybersecurity question.
