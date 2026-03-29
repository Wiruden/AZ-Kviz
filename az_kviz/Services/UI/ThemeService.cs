using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace az_kviz.Services.UI
{
    public class ThemeService
    {
        public void SetDarkMode(bool isDark)
        {
            var bg = isDark ? System.Windows.Media.Brushes.DimGray : System.Windows.Media.Brushes.White;
            var fg = isDark ? System.Windows.Media.Brushes.White : System.Windows.Media.Brushes.Black;

            Application.Current.MainWindow.Background = bg;
        }
    }
}