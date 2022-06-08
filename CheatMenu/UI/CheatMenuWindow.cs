using CaptainOfIndustryMods.CheatMenu.Cheats;
using CaptainOfIndustryMods.CheatMenu.Logging;
using Mafi;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core;
using Mafi.Localization;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UiFramework.Components.Tabs;
using Mafi.Unity.UserInterface;
using UnityEngine;

namespace CaptainOfIndustryMods.CheatMenu.UI
{
    [GlobalDependency(RegistrationMode.AsSelf)]
    public class CheatMenuWindow : WindowView
    {
        private readonly ImmutableArray<ICheatProviderTab> _cheatTabs;
        private TabsContainer _tabsContainer;

        public CheatMenuWindow(AllImplementationsOf<ICheatProviderTab> cheatTabs) : base("CheatMenu")
        {
            _cheatTabs = cheatTabs.Implementations;
            CheatMenuLogger.Log.Info($"Found {_cheatTabs.Length} cheat tabs");
        }

        public override void RenderUpdate(GameTime gameTime)
        {
            //Apparently this shit makes tab changing work?
            _tabsContainer.SyncUpdate(gameTime);
            base.RenderUpdate(gameTime);
        }

        public override void SyncUpdate(GameTime gameTime)
        {
            //Apparently this shit makes tab changing work?
            _tabsContainer.RenderUpdate(gameTime);
            base.SyncUpdate(gameTime);
        }

        protected override void BuildWindowContent()
        {
            CheatMenuLogger.Log.Info("Started building cheat menu");
            SetTitle(new LocStrFormatted("Cheat Menu"));
            var size = new Vector2(680f, 400f);
            SetContentSize(size);
            PositionSelfToCenter();
            MakeMovable();

            _tabsContainer = Builder.NewTabsContainer(size.x.RoundToInt(), size.y.RoundToInt());
            foreach (var tab in _cheatTabs)
            {
                CheatMenuLogger.Log.Info($"Adding {tab.Name} tab to cheat menu");
                _tabsContainer.AddTab(tab.Name, new IconStyle(tab.IconPath, Builder.Style.Global.DefaultPanelTextColor), (Tab)tab);
            }

            _tabsContainer.PutTo(GetContentPanel());

            CheatMenuLogger.Log.Info("Finished building cheat menu");
        }
    }
}