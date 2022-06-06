using System;
using System.Reflection;
using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Buildings.Settlements;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Factory.ElectricPower;
using Mafi.Core.Input;
using Mafi.Core.Utils;
using Mafi.Core.World;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class FleetCheatProvider : ICheatProvider
    {
        private readonly IInputScheduler _inputScheduler;
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        public Lyst<CheatItem> Cheats => _lazyCheats.Value;
        
        public FleetCheatProvider(IInputScheduler inputScheduler)
        {
            _inputScheduler = inputScheduler;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem
                {
                    Title = "Finish Exploration",
                    UsingReflection = true,
                    Action = () =>
                    {
                        _inputScheduler.ScheduleInputCmd<ExploreFinishCheatCmd>(new ExploreFinishCheatCmd());
                    }
                },
                new CheatItem
                {
                    Title = "Repair Fleet",
                    UsingReflection = true,
                    Action = () =>
                    {
                        _inputScheduler.ScheduleInputCmd<FleetRepairCheatCmd>(new FleetRepairCheatCmd());
                    }
                }
            };
        }
    }
}