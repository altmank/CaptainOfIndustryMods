using CaptainOfIndustryMods.CheatMenu.Data;
using Mafi;
using Mafi.Collections;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    [MultiDependency]
    public interface ICheatProvider
    {
        Lyst<CheatItem> Cheats { get; }
    }
}