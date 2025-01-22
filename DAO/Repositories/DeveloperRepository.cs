using Mackowiak.GameCatalog.DAO.Models;
using Mackowiak.GameCatalog.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mackowiak.GameCatalog.DAO.Repositories
{
    public class DeveloperRepository : IRepository<Developer>
    {
        private readonly GameCatalogDbContext _context;

        public DeveloperRepository()
        {
            this._context = new GameCatalogDbContext();
        }

        public DeveloperRepository(GameCatalogDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Developer> GetAll()
        {
            return this._context.Developers.Include(d => d.Games).ToList();
        }

        public Developer GetById(int id)
        {
            return this._context.Developers.Include(d => d.Games).FirstOrDefault(p => p.Id == id);
        }

        public void Add(Developer developer)
        {
            this._context.Developers.Add(developer);
            this._context.SaveChanges();
        }

        public void Update(Developer developer)
        {
            this._context.Developers.Update(developer);
            this._context.SaveChanges();
        }

        public void Delete(int id)
        {
            var developer = this._context.Developers.Find(id);
            if (developer != null)
            {
                this._context.Developers.Remove(developer);
                this._context.SaveChanges();
            }
        }
    }
}
