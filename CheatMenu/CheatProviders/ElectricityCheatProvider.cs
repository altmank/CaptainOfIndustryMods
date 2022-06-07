using System;
using System.Reflection;
using CaptainOfIndustryMods.CheatMenu.Logging;
using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Factory.ElectricPower;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class ElectricityCheatProvider : ICheatProvider
    {
        private readonly IElectricityManager _electricityManager;
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        private FieldInfo _freeElectricityPerTickField;

        public ElectricityCheatProvider(IElectricityManager electricityManager)
        {
            _electricityManager = electricityManager;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private void SetAccessors()
        {
            if (!(_freeElectricityPerTickField is null)) return;
            var electricityManagerType = typeof(CoreMod).Assembly.GetType("Mafi.Core.Factory.ElectricPower.ElectricityManager");
            if (electricityManagerType is null)
            {
                CheatMenuLogger.Log.Error("Unable to fetch the ElectricityManager type.");
                throw new Exception("Unable to fetch the ElectricityManager type.");
            }

            _freeElectricityPerTickField = electricityManagerType.GetField("m_freeElectricityPerTick", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem("Add 100 KW Free Electricity", () => AddFreeElectricity(100)),

                new CheatItem("Remove 100 KW Free Electricity", () => RemoveFreeElectricity(100))
            };
        }

        private void RemoveFreeElectricity(int kw)
        {
            if (((Electricity)_freeElectricityPerTickField.GetValue(_electricityManager)).IsZero)
                //Don't think we should allow negative energy
                return;

            SetAccessors();
            _freeElectricityPerTickField.SetValue(_electricityManager, (Electricity)_freeElectricityPerTickField.GetValue(_electricityManager) - Electricity.FromKw(kw));
        }

        private void AddFreeElectricity(int kw)
        {
            SetAccessors();
            _freeElectricityPerTickField.SetValue(_electricityManager, (Electricity)_freeElectricityPerTickField.GetValue(_electricityManager) + Electricity.FromKw(kw));
        }
    }
}