using Mafi.Collections;
using Mafi.Core.Input;
using Mafi.Core.Research;
using Mafi.Core.UnlockingTree;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class ResearchCheatProvider : ICheatProvider
    {
        private readonly IInputScheduler _inputScheduler;
        private readonly INodeUnlocker _nodeUnlocker;
        private readonly ResearchManager _researchManager;

        public ResearchCheatProvider(ResearchManager researchManager, IInputScheduler inputScheduler, INodeUnlocker nodeUnlocker)
        {
            _researchManager = researchManager;
            _inputScheduler = inputScheduler;
            _nodeUnlocker = nodeUnlocker;
        }

        public Lyst<CheatItem> Cheats => new Lyst<CheatItem>
        {
            new CheatItem("Finish Current Research", UnlockCurrentResearch)
        };


        private void UnlockCurrentResearch()
        {
            _inputScheduler.ScheduleInputCmd(new ResearchCheatFinishCmd());
        }
    }
}