using az_kviz.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace az_kviz.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private string _currentPlayerName;
        public string CurrentPlayerName
        {
            get => _currentPlayerName;
            set { _currentPlayerName = value; OnPropertyChanged(); }
        }

        private double _timerValue;
        public double TimerValue
        {
            get => _timerValue;
            set { _timerValue = value; OnPropertyChanged(); }
        }

        public int Player1Score { get; set; }
        public int Player2Score { get; set; }

        // 1. ADD THIS PROPERTY
        public ICommand SelectTileCommand { get; }

        public GameViewModel(bool isVsAI)
        {
            CurrentPlayerName = "David";
            TimerValue = 100;

            // 2. INITIALIZE THE COMMAND
            SelectTileCommand = new RelayCommand(param => ExecuteTileSelection(param));
        }

        // 3. ADD THIS METHOD
        private void ExecuteTileSelection(object parameter)
        {
            string letter = parameter?.ToString();

            // For now, let's just prove it works!
            MessageBox.Show($"Tile {letter} clicked! Opening QuestionDialog...");

            // Next step: Open QuestionDialog and start TimerService here
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}