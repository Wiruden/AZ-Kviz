using az_kviz.Services.Game;
using az_kviz.Services.AI;
using az_kviz.Services.Validation;
using az_kviz.Services.Storage;
using az_kviz.Models.Quiz;
using az_kviz.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Controls.Primitives;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Media;

namespace az_kviz.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly TurnManager _turnManager = new TurnManager();
        private readonly AiPlayerService _aiService = new AiPlayerService();
        private readonly QuestionFileService _questionService = new QuestionFileService();
        private readonly WinChecker _winChecker = new WinChecker();

        private bool _isVsAI;

        // Commands
        public ICommand SelectTileCommand { get; }
        public ICommand OpenHelpCommand { get; }
        public ICommand OpenMenuCommand { get; }

        // Scores
        private int _player1Score;
        public int Player1Score { get => _player1Score; set { _player1Score = value; OnPropertyChanged(); } }

        private int _player2Score;
        public int Player2Score { get => _player2Score; set { _player2Score = value; OnPropertyChanged(); } }

        // Dynamic Labels
        public string Player2LabelText => _isVsAI ? "AI (Blue)" : "Player 2 (Blue)";

        public string CurrentPlayerName => _turnManager.CurrentPlayerId == 1
            ? "Player 1 (Red)"
            : (_isVsAI ? "AI (Blue)" : "Player 2 (Blue)");

        public GameViewModel(bool isVsAI)
        {
            _isVsAI = isVsAI;
            SelectTileCommand = new RelayCommand(param => ExecuteTileSelection(param));

            OpenHelpCommand = new RelayCommand(_ => {
                new HelpView().ShowDialog();
            });

            OpenMenuCommand = new RelayCommand(_ => {
                var result = MessageBox.Show("Opravdu menu?", "Menu", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes && Application.Current.MainWindow.DataContext is MainViewModel mvm)
                {
                    mvm.CurrentView = mvm;
                }
            });
        }

        private void ExecuteTileSelection(object parameter)
        {
            var button = parameter as ToggleButton;
            if (button == null || !button.IsEnabled) return;

            string letter = button.Content.ToString();
            var question = _questionService.GetRandomQuestionByLetter(letter);
            if (question == null) return;

            int activePlayer = _turnManager.CurrentPlayerId;

            var qvm = new QuestionViewModel(question.QuestionText, question.Answer, (success) =>
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    CloseQuestionDialog();

                    if (success)
                    {
                        button.Tag = activePlayer.ToString();
                        button.IsEnabled = false;
                        UpdateScore(activePlayer);
                        CheckForWinner(activePlayer);
                    }
                    else
                    {
                        button.IsChecked = false;
                        MessageBox.Show("Wrong!");
                    }

                    _turnManager.SwitchTurn(success);
                    RefreshUI();

                    // START AI SEQUENCE
                    if (_turnManager.CurrentPlayerId == 2 && _isVsAI)
                    {
                        await RunAiSequence();
                    }
                });
            });

            ShowDialog(qvm);
        }

        private async Task RunAiSequence()
        {
            await Task.Delay(1500); // AI "Thinking" delay
            HandleAiTurn();
        }

        private void HandleAiTurn()
        {
            // Find all hexagons specifically in the MainWindow
            var availableButtons = FindVisualChildren<ToggleButton>(Application.Current.MainWindow)
                .Where(b => b.IsEnabled && (b.Tag == null || b.Tag.ToString() == "0"))
                .ToList();

            if (availableButtons.Count == 0) return;

            var random = new Random();
            var selectedButton = availableButtons[random.Next(availableButtons.Count)];

            bool aiCorrect = _aiService.WillAnswerCorrectly();

            if (aiCorrect)
            {
                selectedButton.Tag = "2";
                selectedButton.IsEnabled = false;
                Player2Score += 10;
                CheckForWinner(2);
            }
            else
            {
                selectedButton.IsChecked = false;
            }

            _turnManager.SwitchTurn(aiCorrect);
            RefreshUI();
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }

        private void CheckForWinner(int playerId)
        {
            var owned = FindVisualChildren<ToggleButton>(Application.Current.MainWindow)
                .Where(b => b.Tag?.ToString() == playerId.ToString())
                .Select(b => b.Content.ToString())
                .ToList();

            if (_winChecker.CheckWin(owned))
            {
                string name = (playerId == 2 && _isVsAI) ? "AI" : $"Player {playerId}";
                MessageBox.Show($"{name} Wins!");
            }
        }

        private void UpdateScore(int playerId)
        {
            if (playerId == 1) Player1Score += 10;
            else Player2Score += 10;
        }

        private void RefreshUI()
        {
            OnPropertyChanged(nameof(CurrentPlayerName));
            OnPropertyChanged(nameof(Player2LabelText));
        }

        private void ShowDialog(QuestionViewModel qvm)
        {
            var dialog = new QuestionDialog { DataContext = qvm, Owner = Application.Current.MainWindow };
            dialog.ShowDialog();
        }

        private void CloseQuestionDialog()
        {
            foreach (Window win in Application.Current.Windows.OfType<QuestionDialog>().ToList())
                win.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T t) yield return t;
                foreach (var childOfChild in FindVisualChildren<T>(child)) yield return childOfChild;
            }
        }
    }
}