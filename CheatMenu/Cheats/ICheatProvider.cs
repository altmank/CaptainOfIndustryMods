using CaptainOfIndustryMods.CheatMenu.Data;
using Mafi;
using Mafi.Collections;

namespace CaptainOfIndustryMods.CheatMenu.Cheats
{
    [MultiDependency]
    public interface ICheatProvider
    {
        Lyst<CheatItem> Cheats { get; }
    }
}