using System;
using System.Reflection;
using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Buildings.Settlements;
using Mafi.Core.Factory.ElectricPower;
using Mafi.Core.Utils;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class ElectricityCheatProvider : ICheatProvider
    {
        private readonly IElectricityManager _electricityManager;
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        private FieldInfo _freeElectricityPerTickField;
        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private void SetAccessors()
        {
            if (!(_freeElectricityPerTickField is null)) return;
            var electricityManagerType = typeof(CoreMod).Assembly.GetType("Mafi.Core.Factory.ElectricPower.ElectricityManager");
            if (electricityManagerType is null)
            {
                Log.Info("*** CheatMenu ERROR *** Unable to fetch the ElectricityManager type.");
                throw new Exception("*** CheatMenu ERROR *** Unable to fetch the ElectricityManager type.");
            }

            _freeElectricityPerTickField = electricityManagerType.GetField("m_freeElectricityPerTick",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }


        public ElectricityCheatProvider(IElectricityManager electricityManager)
        {
            _electricityManager = electricityManager;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem
                {
                    Title = "Add 100 KW Free Electricity",
                    UsingReflection = true,
                    Action = () =>
                    {
                        SetAccessors();
                        _freeElectricityPerTickField.SetValue(_electricityManager, (Electricity)_freeElectricityPerTickField.GetValue(_electricityManager) + Electricity.FromKw(100));
                    }
                },
                new CheatItem
                {
                    Title = "Remove 100 KW Free Electricity",
                    UsingReflection = true,
                    Action = () =>
                    {
                        if (((Electricity)_freeElectricityPerTickField.GetValue(_electricityManager)).IsZero)
                        {
                            //Don't think we should allow negative energy
                            return;
                        }
                        SetAccessors();
                        _freeElectricityPerTickField.SetValue(_electricityManager, (Electricity)_freeElectricityPerTickField.GetValue(_electricityManager) - Electricity.FromKw(100));
                    }
                },
            };
        }
    }
}