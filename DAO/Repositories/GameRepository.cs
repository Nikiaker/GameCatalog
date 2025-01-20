using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using Mackowiak.GameCatalog.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mackowiak.GameCatalog.DAO.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private readonly GameCatalogDbContext _context;

        public GameRepository(GameCatalogDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games.Include(p => p.Developer).ToList();
        }

        public Game GetById(int id)
        {
            return _context.Games.Include(p => p.Developer).FirstOrDefault(p => p.Id == id);
        }

        public void Add(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public void Update(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var game = _context.Games.Find(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                _context.SaveChanges();
            }
        }
    }
}
