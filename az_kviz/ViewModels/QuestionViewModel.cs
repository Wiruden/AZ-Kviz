using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace az_kviz.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }

        private string _userAnswer;
        public string UserAnswer
        {
            get => _userAnswer;
            set { _userAnswer = value; OnPropertyChanged(); }
        }

        private int _timerValue = 15;
        public int TimerValue
        {
            get => _timerValue;
            set { _timerValue = value; OnPropertyChanged(); }
        }

        public ICommand SubmitCommand { get; }
        private DispatcherTimer _timer;
        private Action<bool> _onResult;

        public QuestionViewModel(string question, string answer, Action<bool> onResult)
        {
            QuestionText = question;
            CorrectAnswer = answer;
            _onResult = onResult;

            // Manual submit: isTimeout = false
            SubmitCommand = new RelayCommand(p => Finish(CheckAnswer(), false));

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => {
                TimerValue--;
                if (TimerValue <= 0)
                {
                    // Automatic timeout: isTimeout = true, success = false
                    Finish(false, true);
                }
            };
            _timer.Start();
        }

        private bool CheckAnswer() =>
            UserAnswer?.Trim().ToLower() == CorrectAnswer.ToLower();

        private void Finish(bool success, bool isTimeout = false)
        {
            // If it's a manual submit (not a timeout), check for empty input
            if (!isTimeout && string.IsNullOrWhiteSpace(UserAnswer))
            {
                MessageBox.Show("Please enter an answer before submitting!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _timer.Stop();
            if (isTimeout)
            {
                MessageBox.Show("Time is up! The turn passes to your opponent.", "Timeout", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _onResult?.Invoke(success);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}