using System;
using System.Reflection;
using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Buildings.Settlements;
using Mafi.Core.Factory.ElectricPower;
using Mafi.Core.Population;
using Mafi.Core.Utils;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class WorkerCheatProvider : ICheatProvider
    {
        private readonly IWorkersManager _workersManager;
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        private FieldInfo _amountOfFreeWorkersField;
        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private void SetAccessors()
        {
            if (!(_amountOfFreeWorkersField is null)) return;
            var workersManagerType = typeof(CoreMod).Assembly.GetType("Mafi.Core.Population.WorkersManager");
            if (workersManagerType is null)
            {
                Log.Info("*** CheatMenu ERROR *** Unable to fetch the WorkersManager type.");
                throw new Exception("*** CheatMenu ERROR *** Unable to fetch the WorkersManager type.");
            }

            _amountOfFreeWorkersField = workersManagerType.GetField("m_amountOfFreeWorkers",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }


        public WorkerCheatProvider(IWorkersManager workersManager)
        {
            _workersManager = workersManager;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem
                {
                    Title = "Add 10 workers",
                    UsingReflection = true,
                    Action = () =>
                    {
                        SetAccessors();
                        _amountOfFreeWorkersField.SetValue(_workersManager, (int)_amountOfFreeWorkersField.GetValue(_workersManager) + 10);
                    }
                }
            };
        }
    }
}