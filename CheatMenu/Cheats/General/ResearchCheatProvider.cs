using CaptainOfIndustryMods.CheatMenu.Data;
using Mafi.Collections;
using Mafi.Core.Input;
using Mafi.Core.Research;
using Mafi.Core.UnlockingTree;

namespace CaptainOfIndustryMods.CheatMenu.Cheats.General
{
    public class ResearchCheatProvider : ICheatProvider
    {
        private readonly IInputScheduler _inputScheduler;

        public ResearchCheatProvider( IInputScheduler inputScheduler)
        {
            _inputScheduler = inputScheduler;
        }

        public Lyst<CheatItem> Cheats => new Lyst<CheatItem>
        {
            new CheatItem("Finish Current Research", UnlockCurrentResearch)
                { Tooltip = "Start research, and then use this command to instantly complete it. You can also use Instant Mode to complete started research immediately." }
        };


        private void UnlockCurrentResearch()
        {
            _inputScheduler.ScheduleInputCmd(new ResearchCheatFinishCmd());
        }
    }
}