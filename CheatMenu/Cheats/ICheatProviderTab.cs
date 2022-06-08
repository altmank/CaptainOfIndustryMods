using Mafi;

namespace CaptainOfIndustryMods.CheatMenu.Cheats
{
    [MultiDependency]
    public interface ICheatProviderTab
    {
        string Name { get; }
        string IconPath { get; }
    }
}