using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Mackowiak.GameCatalog.UI.View;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private GameService gameService = new GameService();

        public ICommand AddGameCommand { get; set; }
        public ICommand ManageDeveloperCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditGameCommand { get; set; }
        public ICommand RemoveGameCommand { get; set; }

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

        public MainViewModel()
        {
            // Pobranie produktów z bazy danych
            LoadGames();

            InitializeCommands();
        }

        private void LoadGames()
        {
            // Pobranie produktów z bazy danych
            var games = gameService.GetAllGames();
            this.Games = new ObservableCollection<Game>(games);
            OnPropertyChanged(nameof(Games));
            OnPropertyChanged(nameof(FilteredGames));
        }

        private void InitializeCommands()
        {
            AddGameCommand = new RelayCommand(AddGame);
            ManageDeveloperCommand = new RelayCommand(ManageDeveloper);
            SearchCommand = new RelayCommand(Search);
            EditGameCommand = new RelayCommand(EditGame);
            RemoveGameCommand = new RelayCommand(RemoveGame);
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
            this.gameService = new GameService();
            this.LoadGames();
        }

        private void Search()
        {
            // Wyszukiwanie gry
            OnPropertyChanged(nameof(FilteredGames));
        }

        private void EditGame()
        {
            if (this.SelectedGame != null)
            {
                var window = new AddGameWindow(this.SelectedGame);
                window.ShowDialog();
                this.gameService = new GameService();
                LoadGames();
            }
        }

        private void RemoveGame()
        {
            if (this.SelectedGame != null)
            {
                gameService.RemoveGame(this.SelectedGame.Id);
                LoadGames();
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}