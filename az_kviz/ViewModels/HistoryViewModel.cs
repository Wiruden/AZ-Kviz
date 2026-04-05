// Jméno a příjmení: David Mihók
// Třída: 4.C
// Předmět: Programování a vývoj aplikací
// Program: AZ Kvíz
using az_kviz.Services.Storage;
using az_kviz.Services.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

namespace az_kviz.ViewModels
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Dictionary<string, string>> _historyEntries;
        public ObservableCollection<Dictionary<string, string>> HistoryEntries
        {
            get => _historyEntries;
            set { _historyEntries = value; OnPropertyChanged(); }
        }
        public ICommand BackToMenuCommand { get; }

        public HistoryViewModel()
        {
            var data = JsonStorageService.LoadHistory();
            HistoryEntries = new ObservableCollection<Dictionary<string, string>>(data);

            // Logika pro návrat do menu
            BackToMenuCommand = new RelayCommand(_ => {
                if (Application.Current.MainWindow.DataContext is MainViewModel mvm)
                {
                    mvm.CurrentView = null;
                    // To v tvém MainViewModelu automaticky nastaví IsMenuVisible na true
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}