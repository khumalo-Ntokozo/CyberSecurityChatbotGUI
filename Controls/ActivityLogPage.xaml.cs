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
using CyberSecurityChatbotGUI.Services;

namespace CyberSecurityChatbotGUI.Controls
{
    public partial class ActivityLogPage : UserControl
    {
        public ActivityLogPage()
        {
            InitializeComponent();
            LoadLog();
        }

        public void LoadLog()
        {
            List<string> recentLogs = ActivityLogger.GetLog();

            LogItemsControl.Items.Clear();
            foreach (var logEntry in recentLogs)
            {
                LogItemsControl.Items.Add(new TextBlock { Text = logEntry, TextWrapping = System.Windows.TextWrapping.Wrap });
            }
        }
    }
}