namespace CaptainOfIndustryMods.CheatMenu.Config
{
    public interface ICheatCommandBase
    {
        string Title { get; }
        string Tooltip { get; set; }
    }
}