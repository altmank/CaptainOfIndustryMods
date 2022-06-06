using System;
using System.Reflection;
using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Buildings.Settlements;
using Mafi.Core.Utils;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class InstantBuildCheatProvider : ICheatProvider
    {
        private readonly IInstaBuildManager _instaBuildManager;
        private FieldInfo _instantBuildProperty;
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private void SetInstantBuildAccessors()
        {
            if (!(_instantBuildProperty is null))
            {
                return;
            }

            var instantBuildManager = typeof(CoreMod).Assembly.GetType("Mafi.Core.Utils.InstaBuildManager");
            if (instantBuildManager is null)
            {
                Log.Info("*** CheatMenu ERROR *** Unable to fetch the InstaBuildManager type.");
                throw new Exception("*** CheatMenu ERROR *** Unable to fetch the InstaBuildManager type.");
            }

            _instantBuildProperty = instantBuildManager.GetField("<IsInstaBuildEnabled>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }


        public InstantBuildCheatProvider(IInstaBuildManager instaBuildManager)
        {
            _instaBuildManager = instaBuildManager;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem
                {
                    Title = "Toggle Instant Build",
                    UsingReflection = false,
                    Action = () =>
                    {
                        SetInstantBuildAccessors();
                        _instantBuildProperty.SetValue(_instaBuildManager,
                            !(bool)_instantBuildProperty.GetValue(_instaBuildManager));
                    }
                }
            };
        }
    }
}