using CaptainOfIndustryMods.CheatMenu.Cheats;
using CaptainOfIndustryMods.CheatMenu.Logging;
using CaptainOfIndustryMods.CheatMenu.UI;
using Mafi;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Unity;
using Mafi.Unity.InputControl.Toolbar;
using UnityEngine;

namespace CaptainOfIndustryMods.CheatMenu
{
    // The IMod implementation must be sealed!
    public sealed class CheatMenu : IMod
    {
        // Name must be alphanumeric
        public string Name => "CheatMenu";
        public int Version => 1;
        public bool IsUiOnly => false;

        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            CheatMenuLogger.Log.Info("Running version v7");
            CheatMenuLogger.Log.Info("Built for game version v.0.4.2");
        }

        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
        }

        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool wasLoaded)
        {
            depBuilder.RegisterAllTypesImplementing<ICheatProvider>(typeof(CheatMenu).Assembly);
        }
    }
}