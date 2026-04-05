using System.Windows;

namespace az_kviz.Services.UI
{
    /// <summary>
    /// Service for handling runtime theme switching between Light and Dark modes.
    /// </summary>
    public static class ThemeService
    {
        public static void ChangeTheme(bool isDark)
        {
            string themeName = isDark ? "DarkTheme.xaml" : "LightTheme.xaml";

            var dict = new ResourceDictionary
            {
                Source = new Uri($"/az_kviz;component/Resources/Themes/{themeName}", UriKind.RelativeOrAbsolute)
            };

            var appDicts = Application.Current.Resources.MergedDictionaries;

            var oldTheme = appDicts.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme.xaml"));

            if (oldTheme != null)
            {
                appDicts.Remove(oldTheme);
            }
            appDicts.Add(dict);
        }
    }
}