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

            SubmitCommand = new RelayCommand(p => Finish(CheckAnswer()));

            // Setup 15-second timer
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => {
                TimerValue--;
                if (TimerValue <= 0) Finish(false);
            };
            _timer.Start();
        }

        private bool CheckAnswer() =>
            UserAnswer?.Trim().ToLower() == CorrectAnswer.ToLower();

        private void Finish(bool success)
        {
            _timer.Stop();
            _onResult?.Invoke(success);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}