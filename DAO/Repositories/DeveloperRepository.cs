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
    public class DeveloperRepository : IRepository<Developer>
    {
        private readonly GameCatalogDbContext _context;

        public DeveloperRepository()
        {
            _context = new GameCatalogDbContext();
        }

        public DeveloperRepository(GameCatalogDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Developer> GetAll()
        {
            return _context.Developers.Include(d => d.Games).ToList();
        }

        public Developer GetById(int id)
        {
            return _context.Developers.Include(d => d.Games).FirstOrDefault(p => p.Id == id);
        }

        public void Add(Developer developer)
        {
            _context.Developers.Add(developer);
            _context.SaveChanges();
        }

        public void Update(Developer developer)
        {
            _context.Developers.Update(developer);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var developer = _context.Developers.Find(id);
            if (developer != null)
            {
                _context.Developers.Remove(developer);
                _context.SaveChanges();
            }
        }
    }
}
