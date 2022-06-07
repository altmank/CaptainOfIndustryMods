using Mafi;
using Mafi.Collections;
using Mafi.Core.Vehicles;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class VehiclesCheatProvider : ICheatProvider
    {
        private readonly Lazy<Lyst<CheatItem>> _lazyCheats;
        private readonly IVehiclesManager _vehiclesManager;


        public VehiclesCheatProvider(IVehiclesManager vehiclesManager)
        {
            _vehiclesManager = vehiclesManager;
            _lazyCheats = new Lazy<Lyst<CheatItem>>(GetCheats);
        }

        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem(
                    "Vehicle Limit Add 100",
                    () => _vehiclesManager.IncreaseVehicleLimit(100)),
                new CheatItem(
                    "Vehicle Limit Remove 100",
                    () => _vehiclesManager.IncreaseVehicleLimit(-100)),
                new CheatItem(
                    "Vehicle Limit Add 10",
                    () => _vehiclesManager.IncreaseVehicleLimit(10)),
                new CheatItem(
                    "Vehicle Limit Remove 10",
                    () => _vehiclesManager.IncreaseVehicleLimit(-10))
            };
        }
    }
}