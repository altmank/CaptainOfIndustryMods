using System;
using Mafi;
using Mafi.Core.GameLoop;
using Mafi.Unity;
using Mafi.Unity.InputControl;
using Mafi.Unity.InputControl.Toolbar;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UserInterface;
using UnityEngine;

namespace CaptainOfIndustryMods.CheatMenu.UI
{
    [GlobalDependency(RegistrationMode.AsEverything)]
    public class CheatMenuController : BaseWindowController<CheatMenuWindow>, IToolbarItemInputController
    {
        private readonly ToolbarController _toolbarController;

        public CheatMenuController(IUnityInputMgr inputManager, IGameLoopEvents gameLoop, CheatMenuWindow cheatMenuWindow, ToolbarController toolbarController)
            : base(inputManager, gameLoop, cheatMenuWindow)
        {
            _toolbarController = toolbarController;
        }


        public override void RegisterUi(UiBuilder builder)
        {
            _toolbarController.AddMainMenuButton("Cheat Menu", this, "Assets/Unity/UserInterface/Toolbar/Power.svg", 1337f, manager => KeyBindings.FromKey(KbCategory.Tools, KeyCode.F8));
            base.RegisterUi(builder);
        }

        public bool IsVisible { get; }
        public event Action<IToolbarItemInputController> VisibilityChanged;
    }
}