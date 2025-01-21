// BL: Logika biznesowa
namespace Mackowiak.GameCatalog.BL
{
    using Mackowiak.GameCatalog.Interfaces;
    using Mackowiak.GameCatalog.DAO;
    using Mackowiak.GameCatalog.DAO.Models;
    using Mackowiak.GameCatalog.DAO.Repositories;

    public class DeveloperService
    {
        private readonly IRepository<Developer> _repository;

        public DeveloperService()
        {
            _repository = new DeveloperRepository();
        }

        public DeveloperService(IRepository<Developer> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Developer> GetAllDevelopers()
        {
            return _repository.GetAll();
        }

        public IEnumerable<string> GetAllDeveloperNames()
        {
            return _repository.GetAll().Select(d => d.Name);
        }

        public void AddDeveloper(Developer developer)
        {
            if (string.IsNullOrEmpty(developer.Name))
                throw new ArgumentException("Nazwa produktu nie może być pusta.");

            _repository.Add(developer);
        }
    }
}