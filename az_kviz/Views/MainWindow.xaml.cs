// Jméno a příjmení: David Mihók
// Třída: 4.C
// Předmět: Programování a vývoj aplikací
// Program: AZ Kvíz
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace az_kviz.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new az_kviz.ViewModels.MainViewModel();
        }
    }
}