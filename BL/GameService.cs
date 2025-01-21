// BL: Logika biznesowa
namespace Mackowiak.GameCatalog.BL
{
    using Mackowiak.GameCatalog.Interfaces;
    using Mackowiak.GameCatalog.DAO;
    using Mackowiak.GameCatalog.DAO.Models;
    using Mackowiak.GameCatalog.DAO.Repositories;

    public class GameService
    {
        private readonly IRepository<Game> _repository;

        public GameService()
        {
            _repository = new GameRepository();
        }

        public GameService(IRepository<Game> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _repository.GetAll();
        }

        public Game GetGameById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddGame(Game game)
        {
            if (string.IsNullOrEmpty(game.Name))
                throw new ArgumentException("Nazwa produktu nie może być pusta.");

            _repository.Add(game);
        }

        public void UpdateGame(Game game)
        {
            _repository.Update(game);
        }

        public void RemoveGame(int id)
        {
            _repository.Delete(id);
        }
    }
}