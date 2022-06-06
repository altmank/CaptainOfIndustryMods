using Mafi.Collections;
using Mafi.Core.Vehicles;
using Mafi.Core.Prototypes;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class VehiclesCheatProvider : ICheatProvider
    {
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        private readonly ProtosDb _protosDb;
        private readonly IVehiclesManager _vehiclesManager;


        public VehiclesCheatProvider(VehiclesManager VehiclesManager, ProtosDb protosDb)
        {
            _vehiclesManager = VehiclesManager;
            _protosDb = protosDb;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }
        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem
                {
                    Title = "Vehicle Limit Add 100",
                UsingReflection = false,
                // Sometimes the devs make it easier than others!
                Action = () => _vehiclesManager.IncreaseVehicleLimit(100),
                },
                 new CheatItem
                {
                    Title = "Vehicle Limit Remove 100",
                UsingReflection = false,
                // Sometimes the devs make it easier than others!
                Action = () => _vehiclesManager.IncreaseVehicleLimit(-100),
                },
                  new CheatItem
                {
                    Title = "Vehicle Limit Add 10",
                UsingReflection = false,
                // Sometimes the devs make it easier than others!
                Action = () => _vehiclesManager.IncreaseVehicleLimit(10),
                },
                 new CheatItem
                {
                    Title = "Vehicle Limit Remove 10",
                UsingReflection = false,
                // Sometimes the devs make it easier than others!
                Action = () => _vehiclesManager.IncreaseVehicleLimit(-10),
                },
            };

        }
    }
}