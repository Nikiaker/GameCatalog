using Mackowiak.GameCatalog.DAO.Models;
using Mackowiak.GameCatalog.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mackowiak.GameCatalog.DAO.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private readonly GameCatalogDbContext _context;

        public GameRepository()
        {
            this._context = new GameCatalogDbContext();
        }

        public GameRepository(GameCatalogDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Game> GetAll()
        {
            return this._context.Games.Include(p => p.Developer).ToList();
        }

        public Game GetById(int id)
        {
            return this._context.Games.Include(p => p.Developer).FirstOrDefault(p => p.Id == id);
        }

        public void Add(Game game)
        {
            this._context.Games.Add(game);
            this._context.SaveChanges();
        }

        public void Update(Game game)
        {
            this._context.Games.Update(game);
            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var game = this._context.Games.Find(id);
            if (game != null)
            {
                this._context.Games.Remove(game);
                this._context.SaveChanges();
            }
        }
    }
}
