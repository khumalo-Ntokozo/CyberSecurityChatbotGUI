using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityChatbotGUI.Services
{
    public static class ActivityLogger
    {
        private static List<string> log = new List<string>();

        public static void LogAction(string action)
        {
            log.Add($"[{DateTime.Now:HH:mm:ss}] {action}");
            if (log.Count > 10)
                log.RemoveAt(0); // Keep only latest 10 actions
        }

        public static List<string> GetRecentActions(int count = 5)
        {
            return log.Skip(Math.Max(0, log.Count - count)).ToList();
        }

        public static List<string> GetLog() => new List<string>(log);
    }
}
