﻿using CaptainOfIndustryMods.CheatMenu.Data;
using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Buildings.Settlements;
using Mafi.Core.Population;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class PopulationCheatProvider : ICheatProvider
    {
        private readonly Lazy<Lyst<CheatItem>> _lazyCheats;
        private readonly SettlementsManager _settlementsManager;
        private readonly UpointsManager _upointsManager;


        public PopulationCheatProvider(SettlementsManager settlementsManager, UpointsManager upointsManager)
        {
            _settlementsManager = settlementsManager;
            _upointsManager = upointsManager;
            _lazyCheats = new Lazy<Lyst<CheatItem>>(GetCheats);
        }

        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem("Add 10 Pop", () => AddPopulation(10)) { Tooltip = "Adds 10 people to your population" },
                new CheatItem("Add 50 Pop", () => AddPopulation(50)) { Tooltip = "Adds 50 people to your population" },
                new CheatItem("Remove 10 Pop", () => RemovePopulation(10)) { Tooltip = "Removes 10 people to your population, great for purging homeless, you heartless monster" },
                new CheatItem("Remove 50 Pop", () => RemovePopulation(50)) { Tooltip = "Removes 50 people to your population, great for purging lots of homeless, you heartless monster" },
                new CheatItem("Add 25 Unity", () => AddUnity(25)) { Tooltip = "Add Unity to your current supply, it will not exceed your max Unity" }
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