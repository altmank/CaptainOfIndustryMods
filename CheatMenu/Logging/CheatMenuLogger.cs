using System;

namespace CaptainOfIndustryMods.CheatMenu.Logging
{
    public static class CheatMenuLogger
    {
        private const string CheatMenuLogPrefix = "CheatMenu: ";

        public static class Log
        {
            public static void Info(string message)
            {
                Mafi.Log.Info($"{CheatMenuLogPrefix}{message}");
            }

            public static void Error(string message)
            {
                Mafi.Log.Error($"{CheatMenuLogPrefix}{message}", true);
            }

            public static void Exception(Exception e, string message)
            {
                Mafi.Log.Exception(e, $"{CheatMenuLogPrefix}{message}");
            }
        }
    }
}