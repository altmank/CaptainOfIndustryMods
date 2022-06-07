using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Economy;
using Mafi.Core.Input;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Core.World;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class ShipyardItemCheatProvider : ICheatProvider
    {
        private readonly IAssetTransactionManager _assetTransactionManager;
        private readonly ProtosDb _protosDb;
        private readonly Lazy<Lyst<CheatItem>> _lazyCheats;

        public ShipyardItemCheatProvider(IInputScheduler inputScheduler, IAssetTransactionManager assetTransactionManager, ProtosDb protosDb)
        {
            _assetTransactionManager = assetTransactionManager;
            _protosDb = protosDb;
            _lazyCheats = new Lazy<Lyst<CheatItem>>(GetCheats);
        }

        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem("Iron", () => AddItemToShipyard(Ids.Products.Iron)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Copper", () => AddItemToShipyard(Ids.Products.Copper)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Diesel", () => AddItemToShipyard(Ids.Products.Diesel)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Water", () => AddItemToShipyard(Ids.Products.Water)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Dirt", () => AddItemToShipyard(Ids.Products.Dirt)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Rock", () => AddItemToShipyard(Ids.Products.Rock)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Coal", () => AddItemToShipyard(Ids.Products.Coal)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Gold", () => AddItemToShipyard(Ids.Products.Gold)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Limestone", () => AddItemToShipyard(Ids.Products.Limestone)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Wood", () => AddItemToShipyard(Ids.Products.Wood)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Oil", () => AddItemToShipyard(Ids.Products.CrudeOil)){Tooltip = "Adds 100 of this product to the Shipyard storage"},
                new CheatItem("Meat", () => AddItemToShipyard(Ids.Products.Meat)){Tooltip = "Adds 100 of this product to the Shipyard storage"}
            };
        }

        private void AddItemToShipyard(ProductProto.ID product, int quantity = 100)
        {
            var ironProto = _protosDb.First<ProductProto>(p => p.Id == product);
            _assetTransactionManager.AddProduct(new ProductQuantity(ironProto.Value, new Quantity(quantity)), CreateReason.Cheated);
        }
    }
}