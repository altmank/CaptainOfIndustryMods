using CaptainOfIndustryMods.CheatMenu.Config;
using Mafi;
using Mafi.Collections;
using Mafi.Core.Input;
using Mafi.Core.World;

namespace CaptainOfIndustryMods.CheatMenu.Cheats.General
{
    public class FleetCheatProvider : ICheatProvider
    {
        private readonly IInputScheduler _inputScheduler;
        private readonly Lazy<Lyst<CheatItem>> _lazyCheats;

        public FleetCheatProvider(IInputScheduler inputScheduler)
        {
            _inputScheduler = inputScheduler;
            _lazyCheats = new Lazy<Lyst<CheatItem>>(GetCheats);
        }

        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem("Finish Exploration", () => _inputScheduler.ScheduleInputCmd(new ExploreFinishCheatCmd()))
                    { Tooltip = "Set your ship to do an action and then press this button and they will complete it immediately" },
                new CheatItem("Repair Fleet", () => _inputScheduler.ScheduleInputCmd(new FleetRepairCheatCmd()))
            };
        }
    }
}