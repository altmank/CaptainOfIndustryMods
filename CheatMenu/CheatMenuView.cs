using Mafi.Collections;
using Mafi.Localization;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UserInterface;

namespace CaptainOfIndustryMods.CheatMenu
{
    public class CheatMenuView : WindowView
    {
        private readonly Dict<string, Lyst<CheatItem>> _cheatItems;

        public CheatMenuView(Dict<string, Lyst<CheatItem>> cheatItems) : base("CheatMenu", noHeader: true)
        {
            _cheatItems = cheatItems;
        }

        protected override void BuildWindowContent()
        {
            var buttonsContainer = Builder
                .NewStackContainer("Buttons container")
                .SetStackingDirection(StackContainer.Direction.TopToBottom)
                .SetSizeMode(StackContainer.SizeMode.StaticDirectionAligned)
                .SetItemSpacing(25f)
                .SetInnerPadding(Offset.Top(20f) + Offset.Bottom(10f))
                //TODO: PutTo require a reference to UnityEngine.CoreModule, probably because of it's IUiElement parameter containing a GameObject property
                .PutTo(GetContentPanel());

            Builder.NewTitle("Title")
                .SetText("Cheat menu")
                .SetPreferredSize()
                .AppendTo(buttonsContainer, offset: Offset.LeftRight(20));

            var largest = 0f;

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
                        //TODO: May be a bug, the OnClick method has a parameter of type UnityEngine.AudioSource which require a reference to the unity library
                        .OnClick(cheatItem.Action);
                    btn.AppendTo(buttonGroupContainer, btn.GetOptimalSize(), ContainerPosition.MiddleOrCenter);
                }

                var width = buttonGroupContainer.GetDynamicWidth();
                if (width > largest) largest = width;
            }

            SetContentSize(largest, buttonsContainer.GetDynamicHeight());
            PositionSelfToCenter();
        }
    }
}