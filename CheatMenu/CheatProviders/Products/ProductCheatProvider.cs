using Mafi;
using Mafi.Core;
using Mafi.Core.Economy;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders.Products
{
    public class ProductCheatProvider
    {
        private readonly IAssetTransactionManager _assetTransactionManager;
        private readonly ProtosDb _protosDb;

        public ProductCheatProvider(IAssetTransactionManager assetTransactionManager, ProtosDb protosDb)
        {
            _assetTransactionManager = assetTransactionManager;
            _protosDb = protosDb;
        }

        public void AddItemToShipyard(ProductProto.ID product, int quantity = 1000)
        {
            var ironProto = _protosDb.First<ProductProto>(p => p.Id == product);
            _assetTransactionManager.AddProduct(new ProductQuantity(ironProto.Value, new Quantity(quantity)), CreateReason.Cheated);
        }
    }
}