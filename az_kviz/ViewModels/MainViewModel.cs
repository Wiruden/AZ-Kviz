using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace az_kviz.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // This logic is good! It shows the menu buttons only when CurrentView is the MainViewModel itself.
        public Visibility IsMenuVisible => CurrentView == this ? Visibility.Visible : Visibility.Collapsed;

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsMenuVisible));
            }
        }

        public ICommand StartPvPCommand { get; }
        public ICommand StartAICommand { get; }
        public ICommand ShowAboutCommand { get; }
        public ICommand QuitCommand { get; }
        public ICommand ShowMenuCommand { get; }

        public MainViewModel()
        {
            // Initialize commands

            // Starts a game with AI disabled (false)
            StartPvPCommand = new RelayCommand(p => {
                CurrentView = new GameViewModel(false);
            });

            // Starts a game with AI enabled (true)
            StartAICommand = new RelayCommand(p =>
            {
                var vm = new GameViewModel(true);
                this.CurrentView = vm;
            });

            StartAICommand = new RelayCommand(p => CurrentView = new GameViewModel(true));
            ShowAboutCommand = new RelayCommand(p => CurrentView = new AboutViewModel());
            ShowMenuCommand = new RelayCommand(p => CurrentView = this);
            QuitCommand = new RelayCommand(p => System.Windows.Application.Current.Shutdown());

            // Set the initial view to the menu
            CurrentView = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}