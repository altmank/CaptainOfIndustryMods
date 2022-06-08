using System;
using System.Reflection;
using CaptainOfIndustryMods.CheatMenu.Config;
using CaptainOfIndustryMods.CheatMenu.Logging;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Maintenance;

namespace CaptainOfIndustryMods.CheatMenu.Cheats.General
{
    public class MaintenanceCheatProvider : ICheatProvider
    {
        private readonly MaintenanceManager _maintenanceManager;
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        private FieldInfo _maintenanceDisabledField;
        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private void SetAccessors()
        {
            if (!(_maintenanceDisabledField is null)) return;
            var electricityManagerType = typeof(CoreMod).Assembly.GetType("Mafi.Core.Maintenance.MaintenanceManager");
            if (electricityManagerType is null)
            {
                CheatMenuLogger.Log.Error("Unable to fetch the MaintenanceManager type.");
                throw new Exception("Unable to fetch the MaintenanceManager type.");
            }

            _maintenanceDisabledField = electricityManagerType.GetField("m_maintenanceDisabled",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }


        public MaintenanceCheatProvider(MaintenanceManager maintenanceManager)
        {
            _maintenanceManager = maintenanceManager;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem(
                    "Toggle Maintenance",
                    () =>
                    {
                        SetAccessors();
                        var isMaintenanceEnabled = (bool)_maintenanceDisabledField.GetValue(_maintenanceManager);
                        _maintenanceDisabledField.SetValue(_maintenanceManager, !isMaintenanceEnabled);
                    }, true
                )
            };
        }
    }
}