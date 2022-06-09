using System;
using CaptainOfIndustryMods.CheatMenu.Config;
using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Syncers;
using Mafi.Localization;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UiFramework.Components.Tabs;
using UnityEngine;

namespace CaptainOfIndustryMods.CheatMenu.Cheats.Weather
{
    [GlobalDependency(RegistrationMode.AsEverything)]
    public class WeatherTab : Tab, ICheatProviderTab
    {
        private readonly WeatherCheatProvider _weatherCheatProvider;
        private readonly Dict<SwitchBtn, Func<bool>> _switchBtns = new Dict<SwitchBtn, Func<bool>>();

        public WeatherTab(NewInstanceOf<WeatherCheatProvider> weatherCheatProvider) : base(nameof(WeatherTab), SyncFrequency.OncePerSec)
        {
            _weatherCheatProvider = weatherCheatProvider.Instance;
        }

        public string Name => "Weather";
        public string IconPath => "Assets/Unity/UserInterface/Toolbar/WorldMap.svg";

        public override void RenderUpdate(GameTime gameTime)
        {
            RefreshValues();
            base.RenderUpdate(gameTime);
        }

        public override void SyncUpdate(GameTime gameTime)
        {
            RefreshValues();
            base.SyncUpdate(gameTime);
        }

        private void RefreshValues()
        {
            foreach (var (switchBtn, enable) in _switchBtns) switchBtn.SetState(enable());
        }

        protected override void BuildUi()
        {
            var buttonsContainer = Builder
                .NewStackContainer("Buttons container")
                .SetStackingDirection(StackContainer.Direction.TopToBottom)
                .SetSizeMode(StackContainer.SizeMode.StaticDirectionAligned)
                .SetItemSpacing(25f)
                .SetInnerPadding(Offset.Top(20f) + Offset.Bottom(10f))
                .PutToTopOf(this, 680f);

            var buttonGroupContainer = Builder
                .NewStackContainer("Buttons container")
                .SetStackingDirection(StackContainer.Direction.LeftToRight)
                .SetSizeMode(StackContainer.SizeMode.StaticDirectionAligned)
                .SetItemSpacing(10f)
                .SetInnerPadding(Offset.All(10f));

            buttonGroupContainer.AppendTo(buttonsContainer, buttonGroupContainer.GetDynamicHeight());

            foreach (var cheatItem in _weatherCheatProvider.Cheats)
                switch (cheatItem)
                {
                    case CheatToggleCommand toggleCommand:
                        CreateCheatToggleSwitch(cheatItem, toggleCommand, buttonGroupContainer);
                        break;
                    case CheatButtonCommand cheatCommand:
                    {
                        CreateCheatButton(cheatItem, cheatCommand, buttonGroupContainer);
                        break;
                    }
                }


            RefreshValues();
        }

        private void CreateCheatButton(ICheatCommandBase cheatItem, CheatButtonCommand cheatButtonCommand, StackContainer buttonGroupContainer)
        {
            var btn = Builder.NewBtn("button")
                .SetButtonStyle(Style.Global.GeneralBtn)
                .SetText(new LocStrFormatted(cheatItem.Title))
                .AddToolTip(cheatItem.Tooltip)
                .OnClick(cheatButtonCommand.Action);
            btn.AppendTo(buttonGroupContainer, btn.GetOptimalSize(), ContainerPosition.MiddleOrCenter);
        }

        private void CreateCheatToggleSwitch(ICheatCommandBase cheatItem, CheatToggleCommand toggleCommand, StackContainer buttonGroupContainer)
        {
            var toggleBtn = Builder.NewSwitchBtn()
                .SetText(cheatItem.Title)
                .AddTooltip(new LocStrFormatted(cheatItem.Tooltip))
                .SetOnToggleAction(toggleCommand.Action);

            toggleBtn.AppendTo(buttonGroupContainer, new Vector2(toggleBtn.GetWidth(), 25), ContainerPosition.MiddleOrCenter);
            _switchBtns.Add(toggleBtn, toggleCommand.IsToggleEnabled);
        }
    }
}