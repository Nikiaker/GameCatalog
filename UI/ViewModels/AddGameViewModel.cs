using CommunityToolkit.Mvvm.Input;
using Core.DictionaryProvider;
using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.Core;
using Mackowiak.GameCatalog.DAO.Models;
using System.ComponentModel;
using System.Windows;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public partial class AddGameViewModel : INotifyPropertyChanged
    {
        private bool _isEdit = false;
        private GameService _gameService = new GameService();
        private DeveloperService _developerService = new DeveloperService();

        public event PropertyChangedEventHandler PropertyChanged;

        public Game Game { get; set; }
        public IDictionary<GameGenre, string> Genres { get; private set; }
        public IEnumerable<Developer> Developers { get; private set; }

        public RelayCommand<Window> SaveGameCommand { get; set; }

        public AddGameViewModel()
        {
            this.Game = new Game();
            this.Genres = DictionaryProvieder.GameGenreDictionary;
            this.Developers = this._developerService.GetAllDevelopers();

            this.SaveGameCommand = new RelayCommand<Window>(this.SaveGame);
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public AddGameViewModel(Game game)
            : this()
        {
            this.Game = game;
            this.OnPropertyChanged(nameof(this.Game));
            this._isEdit = true;
        }

        private void SaveGame(Window window)
        {
            if (!this._isEdit)
            {
                this._gameService.AddGame(this.Game);
            }
            else
            {
                this.Game.Developer = null;
                this._gameService.UpdateGame(this.Game);
            }
            window.Close();
        }
    }
}
