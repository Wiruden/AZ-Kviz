using az_kviz.ViewModels;
using az_kviz.Models.Quiz;
using az_kviz.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using az_kviz.Models;
using az_kviz.Services.Storage;
using System;
using System.Linq;

namespace az_kviz.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private int _currentPlayer = 1;
        public string CurrentPlayerName => _currentPlayer == 1 ? "Player 1 (Red)" : "Player 2 (Blue)";

        private double _timerValue;
        public double TimerValue
        {
            get => _timerValue;
            set { _timerValue = value; OnPropertyChanged(); }
        }

        private int _player1Score;
        public int Player1Score
        {
            get => _player1Score;
            set { _player1Score = value; OnPropertyChanged(); }
        }

        private int _player2Score;
        public int Player2Score
        {
            get => _player2Score;
            set { _player2Score = value; OnPropertyChanged(); }
        }

        public ICommand SelectTileCommand { get; }
        private readonly QuestionFileService _questionService = new QuestionFileService();

        public GameViewModel(bool isVsAI)
        {
            TimerValue = 100;
            SelectTileCommand = new RelayCommand(param => ExecuteTileSelection(param));
        }

        private void ExecuteTileSelection(object parameter)
        {
            var button = parameter as System.Windows.Controls.Primitives.ToggleButton;
            if (button == null) return;

            string letter = button.Content.ToString();
            Question questionData = _questionService.GetRandomQuestionByLetter(letter);

            if (questionData == null)
            {
                MessageBox.Show($"No questions found for: {letter}", "Missing Data");
                button.IsChecked = false;
                return;
            }

            int activePlayer = _currentPlayer;

            var qvm = new QuestionViewModel(questionData.QuestionText, questionData.Answer, (success) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (Window win in System.Windows.Application.Current.Windows)
                    {
                        if (win is QuestionDialog) win.Close();
                    }

                    if (success)
                    {
                        button.Tag = activePlayer.ToString();
                        button.IsEnabled = false;

                        if (activePlayer == 1) Player1Score += 10; else Player2Score += 10;
                    }
                    else
                    {
                        button.IsChecked = false;
                        MessageBox.Show("Wrong answer or time's up!");
                    }

                    _currentPlayer = (_currentPlayer == 1) ? 2 : 1;
                    OnPropertyChanged(nameof(CurrentPlayerName));
                });
            });

            var dialog = new QuestionDialog
            {
                DataContext = qvm,
                Owner = System.Windows.Application.Current.MainWindow
            };
            dialog.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}