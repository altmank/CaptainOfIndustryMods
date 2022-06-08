using System;
using System.Reflection;
using CaptainOfIndustryMods.CheatMenu.Data;
using CaptainOfIndustryMods.CheatMenu.Logging;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Utils;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class InstantBuildCheatProvider : ICheatProvider
    {
        private readonly IInstaBuildManager _instaBuildManager;
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        private FieldInfo _instantBuildProperty;


        public InstantBuildCheatProvider(IInstaBuildManager instaBuildManager)
        {
            _instaBuildManager = instaBuildManager;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        public Lyst<CheatItem> Cheats => _lazyCheats.Value;


        private void SetInstantBuildAccessors()
        {
            if (!(_instantBuildProperty is null)) return;

            var instantBuildManager = typeof(CoreMod).Assembly.GetType("Mafi.Core.Utils.InstaBuildManager");
            if (instantBuildManager is null)
            {
                CheatMenuLogger.Log.Error("Unable to fetch the InstaBuildManager type.");
                throw new Exception("Unable to fetch the InstaBuildManager type.");
            }

            _instantBuildProperty = instantBuildManager.GetField("<IsInstaBuildEnabled>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem(
                    "Toggle Instant Mode",
                    ToggleInstantMode,
                    true
                )
                {
                    Tooltip = "Enables instant build, instant research, instant upgrades (shipyards, buildings, settlements, mines), instant vehicle construction, and instant repair."
                }
            };
        }

        private void ToggleInstantMode()
        {
            SetInstantBuildAccessors();
            _instantBuildProperty.SetValue(_instaBuildManager, !(bool)_instantBuildProperty.GetValue(_instaBuildManager));
        }
    }
}