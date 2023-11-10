// In LocalizationManager.cs
using System;
using System.Globalization;
using System.Resources;
using UNO_Spielprojekt.Resources;

namespace UNO_Spielprojekt.Localization
{
    public static class LocalizationManager
    {
        private static ResourceManager _resourceManager;

        static LocalizationManager()
        {
            SetLanguage(new CultureInfo("en-US"));
        }

        public static void SetLanguage(CultureInfo culture)
        {
            _resourceManager = new ResourceManager("UNO_Spielprojekt.Resources.Resource", typeof(Resource).Assembly);

            var localizedResourceSet = _resourceManager.GetResourceSet(culture, true, true);

            if (localizedResourceSet != null)
            {
                Console.WriteLine($"Resources loaded for culture: {culture.Name}");
            }
            else
            {
                Console.WriteLine($"No resources found for culture: {culture.Name}");
            }
        }

        public static string GetLocalizedString(string key)
        {
            return _resourceManager.GetString(key);
        }
    }
}