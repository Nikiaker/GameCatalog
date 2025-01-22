namespace Mackowiak.GameCatalog.BL
{
    using Mackowiak.GameCatalog.Interfaces;
    using Mackowiak.GameCatalog.DAO.Models;
    using Mackowiak.GameCatalog.DAO.Repositories;

    public class GameService
    {
        private readonly IRepository<Game> _repository;

        public GameService()
        {
            this._repository = new GameRepository();
        }

        public GameService(IRepository<Game> repository)
        {
            this._repository = repository;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return this._repository.GetAll();
        }

        public Game GetGameById(int id)
        {
            return this._repository.GetById(id);
        }

        public void AddGame(Game game)
        {
            if (string.IsNullOrEmpty(game.Name))
                throw new ArgumentException("Nazwa produktu nie może być pusta.");

            this._repository.Add(game);
        }

        public void UpdateGame(Game game)
        {
            this._repository.Update(game);
        }

        public void RemoveGame(int id)
        {
            this._repository.Delete(id);
        }
    }
}