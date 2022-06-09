using System;

namespace CaptainOfIndustryMods.CheatMenu.Config
{
    public class CheatButtonCommand : ICheatCommandBase
    {
        public CheatButtonCommand(string title, Action action)
        {
            Title = title;
            Action = action;
        }

        public Action Action { get; }
        public string Title { get; }
        public string Tooltip { get; set; }
    }
}