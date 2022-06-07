using System;
using System.Reflection;
using CaptainOfIndustryMods.CheatMenu.Logging;
using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Buildings.Settlements;
using Mafi.Core.Population;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class PopulationCheatProvider : ICheatProvider
    {
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        private readonly SettlementsManager _settlementsManager;
        private readonly UpointsManager _upointsManager;


        public PopulationCheatProvider(SettlementsManager settlementsManager, UpointsManager upointsManager)
        {
            _settlementsManager = settlementsManager;
            _upointsManager = upointsManager;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        public Lyst<CheatItem> Cheats => _lazyCheats.Value;
        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem("Add 10 Pop", () => AddPopulation(10)),
                new CheatItem("Add 50 Pop", () => AddPopulation(50)),
                new CheatItem("Remove 10 Pop", () => RemovePopulation(10)),
                new CheatItem("Remove 50 Pop", () => RemovePopulation(50)),
                new CheatItem("Add 25 Unity", () => AddUnity(25)) {Tooltip = "Cannot exceed your max Unity"},
                
            };
        }

        private void AddUnity(int points)
        {
            _upointsManager.GenerateUnity(IdsCore.UpointsCategories.FreeUnity, new Upoints(points));
            
        }
        private void AddPopulation(int amount)
        {
            _settlementsManager.AddPops(amount, PopsAdditionReason.Other);
        }

        private void RemovePopulation(int amount)
        {
            _settlementsManager.RemovePopsAsMuchAs(amount);
        }
        
    }
}