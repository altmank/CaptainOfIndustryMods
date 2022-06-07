using Mafi;
using Mafi.Collections;
using Mafi.Core.Input;
using Mafi.Core.World;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
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
                new CheatItem("Finish Exploration", () => _inputScheduler.ScheduleInputCmd(new ExploreFinishCheatCmd())),
                new CheatItem("Repair Fleet", () => _inputScheduler.ScheduleInputCmd(new FleetRepairCheatCmd()))
            };
        }
    }
}