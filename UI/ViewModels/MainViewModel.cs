using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mackowiak.GameCatalog.UI.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Game> Games { get; set; }

        public MainViewModel()
        {
            using (var context = new GameCatalogDbContext())
            {
                // Pobranie produktów z bazy danych
                var games = context.Games.ToList();
                this.Games = new ObservableCollection<Game>(games);
            }
        }
    }
}