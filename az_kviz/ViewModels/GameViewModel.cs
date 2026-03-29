using az_kviz.ViewModels;
using az_kviz.Models.Quiz;
using az_kviz.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using az_kviz.Models;
using az_kviz.Services.Storage;

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
        public ICommand SelectTileCommand { get; }

        public GameViewModel(bool isVsAI)
        {
            CurrentPlayerName = "David";
            TimerValue = 100;

            SelectTileCommand = new RelayCommand(param => ExecuteTileSelection(param));
        }

        private readonly QuestionFileService _questionService = new QuestionFileService();

        private void ExecuteTileSelection(object parameter)
        {
            string letter = parameter?.ToString();
            if (string.IsNullOrEmpty(letter)) return;

            // 1. Get the question
            Question questionData = _questionService.GetRandomQuestionByLetter(letter);

            if (questionData == null)
            {
                MessageBox.Show($"No questions found for the letter: {letter}", "Missing Data");
                return;
            }

            // 2. Setup the QuestionViewModel with the data from JSON
            var qvm = new QuestionViewModel(questionData.QuestionText, questionData.Answer, (success) =>
            {
                // This code runs when the user clicks "SUBMIT" or time runs out
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    // Close the popup
                    foreach (Window win in System.Windows.Application.Current.Windows)
                    {
                        if (win is QuestionDialog) win.Close();
                    }

                    if (success)
                    {
                        MessageBox.Show("Correct! The tile is yours.");
                        // TODO: Add logic here to change the button color to Blue or Orange
                    }
                    else
                    {
                        MessageBox.Show("Incorrect answer or time ran out.");
                    }
                });
            });

            // 3. Show the Dialog
            var dialog = new QuestionDialog
            {
                DataContext = qvm,
                Owner = System.Windows.Application.Current.MainWindow
            };
            dialog.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }


}