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
    public class ProductRepository : IRepository<Product>
    {
        private readonly GameCatalogDbContext _context;

        public ProductRepository(GameCatalogDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.Include(p => p.Producer).ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
