using System;
using System.Reflection;
using CaptainOfIndustryMods.CheatMenu.Logging;
using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Factory.ElectricPower;
using Mafi.Core.Population;

namespace CaptainOfIndustryMods.CheatMenu.Cheats.Generate
{
    public class UnityCheatProvider
    {
        private readonly UpointsManager _upointsManager;
        private FieldInfo _freeUnityPerMonthField;

        public UnityCheatProvider(UpointsManager upointsManager)
        {
            _upointsManager = upointsManager;
        }

        private void SetAccessors()
        {
            if (!(_freeUnityPerMonthField is null)) return;
            var managerType = typeof(CoreMod).Assembly.GetType("Mafi.Core.Population.UpointsManager");
            if (managerType is null)
            {
                CheatMenuLogger.Log.Error("Unable to fetch the UpointsManager type.");
                throw new Exception("Unable to fetch the UpointsManager type.");
            }

            _freeUnityPerMonthField = managerType.GetField("m_freeMonthlyUnity", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public void SetFreeUPoints(int uPoints)
        {
            SetAccessors();
            //Not yet working in UPointsManager code
            _freeUnityPerMonthField.SetValue(_upointsManager,  new Upoints(uPoints));
        }
    }
}