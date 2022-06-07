using System;

namespace CaptainOfIndustryMods.CheatMenu
{
    public class CheatItem
    {
        public CheatItem(string title, Action action, bool isToggle = false)
        {
            Title = title;
            Action = action;
            IsToggle = isToggle;
        }

        public string Title { get; }
        public Action Action { get; }
        public bool IsToggle { get; }
        public string Tooltip { get; set; }
        
    }
}