namespace Mackowiak.GameCatalog.BL
{
    using Mackowiak.GameCatalog.Interfaces;
    using Mackowiak.GameCatalog.DAO.Models;
    using Mackowiak.GameCatalog.DAO.Repositories;

    public class DeveloperService
    {
        private readonly IRepository<Developer> _repository;

        public DeveloperService()
        {
            this._repository = new DeveloperRepository();
        }

        public DeveloperService(IRepository<Developer> repository)
        {
            this._repository = repository;
        }

        public IEnumerable<Developer> GetAllDevelopers()
        {
            return this._repository.GetAll();
        }

        public IEnumerable<string> GetAllDeveloperNames()
        {
            return this._repository.GetAll().Select(d => d.Name);
        }

        public Developer GetDeveloperById(int id)
        {
            return this._repository.GetById(id);
        }

        public void AddDeveloper(Developer developer)
        {
            if (string.IsNullOrEmpty(developer.Name))
                throw new ArgumentException("Nazwa produktu nie może być pusta.");

            this._repository.Add(developer);
        }

        public void UpdateDeveloper(Developer developer)
        {
            this._repository.Update(developer);
        }

        public void RemoveDeveloper(int id)
        {
            this._repository.Delete(id);
        }
    }
}