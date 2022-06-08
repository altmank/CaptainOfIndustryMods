using System.Collections.Generic;
using CaptainOfIndustryMods.CheatMenu.Data;
using Mafi;
using Mafi.Collections;
using Mafi.Core.Syncers;
using Mafi.Localization;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UiFramework.Components.Tabs;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    [GlobalDependency(RegistrationMode.AsEverything)]
    public class GeneralTab : Tab, ICheatProviderTab
    {
        private readonly Dict<string, Lyst<CheatItem>> _cheatItems;

        public GeneralTab(AllImplementationsOf<ICheatProvider> cheatProviders) : base(nameof(GeneralTab), SyncFrequency.OncePerSec)
        {
            _cheatItems = cheatProviders.Implementations
                .Select(x => new KeyValuePair<string, Lyst<CheatItem>>(x.GetType().Name, x.Cheats))
                .ToDict();
        }

        public string Name => "General";
        public string IconPath => "Assets/Unity/UserInterface/Toolbar/Settlement.svg";

        protected override void BuildUi()
        {
            var buttonsContainer = Builder
                .NewStackContainer("Buttons container")
                .SetStackingDirection(StackContainer.Direction.TopToBottom)
                .SetSizeMode(StackContainer.SizeMode.StaticDirectionAligned)
                .SetItemSpacing(25f)
                .SetInnerPadding(Offset.Top(20f) + Offset.Bottom(10f))
                .PutToTopOf(this, 680f);

            foreach (var outer in _cheatItems)
            {
                var buttonGroupContainer = Builder
                    .NewStackContainer("Buttons container")
                    .SetStackingDirection(StackContainer.Direction.LeftToRight)
                    .SetSizeMode(StackContainer.SizeMode.StaticDirectionAligned)
                    .SetItemSpacing(10f)
                    .SetInnerPadding(Offset.All(10f));

                buttonGroupContainer.AppendTo(buttonsContainer, buttonGroupContainer.GetDynamicHeight());

                foreach (var cheatItem in outer.Value)
                {
                    var btn = Builder.NewBtn("button")
                        .SetButtonStyle(Style.Global.GeneralBtn)
                        .SetText(new LocStrFormatted(cheatItem.Title))
                        .AddToolTip(cheatItem.Tooltip)
                        .OnClick(cheatItem.Action);
                    btn.AppendTo(buttonGroupContainer, btn.GetOptimalSize(), ContainerPosition.MiddleOrCenter);
                }
            }
        }
    }
}