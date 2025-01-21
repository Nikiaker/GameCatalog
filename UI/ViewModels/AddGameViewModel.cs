using CommunityToolkit.Mvvm.Input;
using Core.DictionaryProvider;
using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.Core;
using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public partial class AddGameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool edit = false;

        public Game Game { get; set; }
        public IDictionary<GameGenre, string> Genres { get; private set; }

        public IEnumerable<Developer> Developers { get; private set; }

        private GameService gameService = new GameService();
        private DeveloperService developerService = new DeveloperService();

        public RelayCommand<Window> SaveGameCommand { get; set; }


        public AddGameViewModel()
        {
            Game = new Game();
            Genres = DictionaryProvieder.GameGenreDictionary;
            Developers = developerService.GetAllDevelopers();

            SaveGameCommand = new RelayCommand<Window>(SaveGame);
        }

        public AddGameViewModel(Game game)
            : this()
        {
            this.Game = game;
            this.OnPropertyChanged(nameof(Game));
            edit = true;
        }

        private void SaveGame(Window window)
        {
            if (!edit)
            {
                gameService.AddGame(this.Game);
            }
            else
            {
                this.Game.Developer = null;
                gameService.UpdateGame(Game);
            }
            window.Close();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
