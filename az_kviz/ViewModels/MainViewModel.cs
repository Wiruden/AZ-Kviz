using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace az_kviz.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
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
            // 1. Initialize all commands FIRST
            StartPvPCommand = new RelayCommand(p => CurrentView = new GameViewModel(false));
            StartAICommand = new RelayCommand(p => CurrentView = new GameViewModel(true));
            ShowAboutCommand = new RelayCommand(p => CurrentView = new AboutViewModel());
            ShowMenuCommand = new RelayCommand(p => CurrentView = this);
            QuitCommand = new RelayCommand(p => System.Windows.Application.Current.Shutdown());

            // 2. Set the initial view
            CurrentView = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}