using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.DAO.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Mackowiak.GameCatalog.UI.View;
using System.ComponentModel;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private GameService _gameService = new GameService();

        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Filter { get; set; }
        public ObservableCollection<Game> Games { get; set; }
        public ObservableCollection<Game> FilteredGames
        {
            get
            {
                if (!string.IsNullOrEmpty(Filter))
                {
                    return new ObservableCollection<Game>(
                        Games.Where(g => g.Name.ToLower().Contains(Filter.ToLower()) ||
                                    g.Genre.ToString().ToLower().Contains(Filter.ToLower()) ||
                                    g.Developer.Name.ToLower().Contains(Filter.ToLower())));
                }

                return Games;
            }
        }
        public Game SelectedGame { get; set; }

        public ICommand AddGameCommand { get; set; }
        public ICommand ManageDeveloperCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditGameCommand { get; set; }
        public ICommand RemoveGameCommand { get; set; }

        public MainViewModel()
        {
            this.LoadGames();
            this.InitializeCommands();
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void LoadGames()
        {
            // Pobranie produktów z bazy danych
            this._gameService = new GameService();
            var games = _gameService.GetAllGames();
            this.Games = new ObservableCollection<Game>(games);
            this.OnPropertyChanged(nameof(this.Games));
            this.OnPropertyChanged(nameof(this.FilteredGames));
        }

        private void InitializeCommands()
        {
            this.AddGameCommand = new RelayCommand(this.AddGame);
            this.ManageDeveloperCommand = new RelayCommand(this.ManageDeveloper);
            this.SearchCommand = new RelayCommand(this.Search);
            this.EditGameCommand = new RelayCommand(this.EditGame);
            this.RemoveGameCommand = new RelayCommand(this.RemoveGame);
        }

        private void AddGame()
        {
            // Otwórz okno dodawania produktu
            var window = new AddGameWindow();
            window.ShowDialog();
            this.LoadGames();
        }

        private void ManageDeveloper()
        {
            // Dodanie nowego producenta
            var window = new ManageDevelopersWindow();
            window.ShowDialog();
            this.LoadGames();
        }

        private void Search()
        {
            // Wyszukiwanie gry
            this.OnPropertyChanged(nameof(this.FilteredGames));
        }

        private void EditGame()
        {
            if (this.SelectedGame != null)
            {
                var window = new AddGameWindow(this.SelectedGame);
                window.ShowDialog();
                this.LoadGames();
            }
        }

        private void RemoveGame()
        {
            if (this.SelectedGame != null)
            {
                this._gameService.RemoveGame(this.SelectedGame.Id);
                this.LoadGames();
            }
        }
    }
}