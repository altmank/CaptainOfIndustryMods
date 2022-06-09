using System;
using CaptainOfIndustryMods.CheatMenu.Cheats;
using CaptainOfIndustryMods.CheatMenu.Logging;
using Mafi;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;

namespace CaptainOfIndustryMods.CheatMenu
{
    public sealed class CheatMenu : IMod
    {
        public string Name => "CheatMenu";
        public int Version => 1;
        public bool IsUiOnly => false;

        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            CheatMenuLogger.Log.Info("Running version v9");
            CheatMenuLogger.Log.Info($"Built for game version 0.4.2a");
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