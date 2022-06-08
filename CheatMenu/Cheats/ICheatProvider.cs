using CaptainOfIndustryMods.CheatMenu.Config;
using Mafi;
using Mafi.Collections;

namespace CaptainOfIndustryMods.CheatMenu.Cheats
{
    [MultiDependency]
    public interface ICheatProvider
    {
        Lyst<ICheatCommandBase> Cheats { get; }
    }
}