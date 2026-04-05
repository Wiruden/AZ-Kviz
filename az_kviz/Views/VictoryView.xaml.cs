// Jméno a příjmení: David Mihók
// Třída: 4.C
// Předmět: Programování a vývoj aplikací
// Program: AZ Kvíz
using System.Windows;

namespace az_kviz.Views
{
    public partial class VictoryView : Window
    {
        public bool RequestNewGame { get; private set; }

        public VictoryView(string winnerName)
        {
            InitializeComponent();
            WinnerText.Text = $"Winner: {winnerName}";
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            RequestNewGame = true;
            this.Close();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            RequestNewGame = false;
            this.Close();
        }
    }
}