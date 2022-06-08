using Mafi;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    [MultiDependency]
    public interface ICheatProviderTab
    {
        string Name { get; }
        string IconPath { get; }
    }
}