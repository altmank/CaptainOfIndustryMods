using System;

namespace CaptainOfIndustryMods.CheatMenu.Config
{
    public class CheatCommand: ICheatCommandBase
    {
        public CheatCommand(string title, Action action)
        {
            Title = title;
            Action = action;
        }

        public string Title { get; }
        public Action Action { get; }
        public string Tooltip { get; set; }
    }
}