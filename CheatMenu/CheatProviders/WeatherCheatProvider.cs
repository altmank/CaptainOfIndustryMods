using System;
using System.Reflection;
using CaptainOfIndustryMods.CheatMenu.Data;
using CaptainOfIndustryMods.CheatMenu.Logging;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Environment;
using Mafi.Core.Prototypes;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class WeatherCheatProvider : ICheatProvider
    {
        private readonly Mafi.Lazy<Lyst<CheatItem>> _lazyCheats;
        private readonly ProtosDb _protosDb;
        private readonly IWeatherManager _weatherManager;
        private PropertyInfo _currentWeatherProperty;
        private FieldInfo _overrideDurationField;

        public WeatherCheatProvider(IWeatherManager weatherManager, ProtosDb protosDb)
        {
            _weatherManager = weatherManager;
            _protosDb = protosDb;
            _lazyCheats = new Mafi.Lazy<Lyst<CheatItem>>(GetCheats);
        }

        public Lyst<CheatItem> Cheats => _lazyCheats.Value;

        private void SetWeatherAccessors()
        {
            if (!(_currentWeatherProperty is null) && !(_overrideDurationField is null)) return;
            var weatherManagerType = typeof(CoreMod).Assembly.GetType("Mafi.Core.Environment.WeatherManager");
            if (weatherManagerType is null)
            {
                CheatMenuLogger.Log.Error("Unable to fetch the WeatherManager type.");
                throw new Exception("Unable to fetch the WeatherManager type.");
            }

            _currentWeatherProperty = weatherManagerType.GetProperty("CurrentWeather");
            _overrideDurationField = weatherManagerType.GetField("m_weatherOverrideDuration",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private Lyst<CheatItem> GetCheats()
        {
            return new Lyst<CheatItem>
            {
                new CheatItem(
                    "Reset weather",
                    () =>
                    {
                        SetWeatherAccessors();
                        SetWeather(Ids.Weather.Sunny, true);
                    }),
                new CheatItem(
                    "Sunny weather",
                    () =>
                    {
                        SetWeatherAccessors();
                        SetWeather(Ids.Weather.Sunny);
                    }),
                new CheatItem(
                    "Cloudy weather",
                    () =>
                    {
                        SetWeatherAccessors();
                        SetWeather(Ids.Weather.Cloudy);
                    }),

                new CheatItem(
                    "Rainy weather",
                    () =>
                    {
                        SetWeatherAccessors();
                        SetWeather(Ids.Weather.Rainy);
                    }),
                new CheatItem(
                    "Heavy rain weather",
                    () =>
                    {
                        SetWeatherAccessors();
                        SetWeather(Ids.Weather.HeavyRain);
                    })
            };
        }

        private void SetWeather(Proto.ID weatherTypeId, bool reset = false)
        {
            _currentWeatherProperty.SetValue(_weatherManager, _protosDb.First<WeatherProto>(x => x.Id == weatherTypeId).Value);
            _overrideDurationField.SetValue(_weatherManager, reset ? 0 : int.MaxValue);
        }
    }
}