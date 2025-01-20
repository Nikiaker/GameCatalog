using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public class MainViewModel
    {
        private GameService gameService = new GameService();

        public ObservableCollection<Game> Games { get; set; }

        public MainViewModel()
        {
            // Pobranie produktów z bazy danych
            var games = gameService.GetAllGames();
            this.Games = new ObservableCollection<Game>(games);
        }
    }
}