using CaptainOfIndustryMods.CheatMenu.CheatProviders;
using Mafi;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Unity;
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
            var cheatMenuController = resolver.Resolve<CheatMenuController>();
            var unityInputManager = resolver.Resolve<IUnityInputMgr>();
            unityInputManager.RegisterGlobalShortcut(KeyCode.F8, cheatMenuController);
        }

        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
        }

        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool wasLoaded)
        {
            depBuilder.RegisterAllTypesImplementing<ICheatProvider>(typeof(CheatMenu).Assembly);
            depBuilder.RegisterDependency<CheatMenuController>().AsSelf().AsAllInterfaces();
        }
    }
}