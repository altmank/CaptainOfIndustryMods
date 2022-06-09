using System.Linq;
using CaptainOfIndustryMods.CheatMenu.Config;
using CaptainOfIndustryMods.CheatMenu.Logging;
using Mafi.Collections;
using Mafi.Core.Input;
using Mafi.Core.MessageNotifications;
using Mafi.Core.Prototypes;
using Mafi.Core.Research;

namespace CaptainOfIndustryMods.CheatMenu.Cheats.General
{
    public class ResearchCheatProvider : ICheatProvider
    {
        private readonly IInputScheduler _inputScheduler;
        private readonly ResearchManager _researchManager;
        private readonly UnlockedProtosDb _unlockedProtosDb;
        private readonly IMessageNotificationsManager _messageNotificationsManager;

        public ResearchCheatProvider(IInputScheduler inputScheduler, ResearchManager researchManager, UnlockedProtosDb unlockedProtosDb, IMessageNotificationsManager messageNotificationsManager)
        {
            _inputScheduler = inputScheduler;
            _researchManager = researchManager;
            _unlockedProtosDb = unlockedProtosDb;
            _messageNotificationsManager = messageNotificationsManager;
        }

        public Lyst<ICheatCommandBase> Cheats => new Lyst<ICheatCommandBase>
        {
            new CheatCommand("Finish Current Research", UnlockCurrentResearch) { Tooltip = "Start research, and then use this command to instantly complete it. You can also use Instant Mode to complete started research immediately." },
            new CheatCommand("Unlock All Research", UnlockAllResearch) { Tooltip = "Unlocks all research including research that requires discoveries to research." }
        };

        private void UnlockCurrentResearch()
        {
            _inputScheduler.ScheduleInputCmd(new ResearchCheatFinishCmd());
        }

        private void UnlockAllResearch()
        {
            var researchUnlockProtos = _researchManager.AllNodes.SelectMany(x => x.Proto.RequiredUnlockedProtos.ToLyst());

            CheatMenuLogger.Log.Info("Unlocking TechnologyProtos that are required by research...");
            foreach (var tech in researchUnlockProtos)
            {
                CheatMenuLogger.Log.Info($"Unlocking TechnologyProto {tech}");
                _unlockedProtosDb.Unlock(tech);
            }

            do
            {
                var count = _researchManager.AllNodes.Count(x => x.State == ResearchNodeState.Available);
                CheatMenuLogger.Log.Info($"Researchable Node Count: {count}");

                foreach (var researchNodeProto in _researchManager.AllNodes.Filter(x => x.State == ResearchNodeState.Available))
                {
                    var success = _researchManager.TryStartResearch(researchNodeProto.Proto, out var errorMessage);
                    CheatMenuLogger.Log.Info($"Starting {researchNodeProto.Proto.Id.Value} research, success: {success} {errorMessage}");
                    CheatMenuLogger.Log.Info($"Cheating {researchNodeProto.Proto.Id.Value} research finish");
                    _researchManager.Cheat_FinishCurrent();
                }
            } while (_researchManager.AllNodes.Any(x => x.State == ResearchNodeState.Available));
            
            _messageNotificationsManager.DismissAllNotifications();
        }
    }
}