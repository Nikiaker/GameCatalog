﻿// BL: Logika biznesowa
namespace Mackowiak.GameCatalog.BL
{
    using Mackowiak.GameCatalog.Interfaces;
    using Mackowiak.GameCatalog.DAO;
    using Mackowiak.GameCatalog.DAO.Models;

    public class GameService
    {
        private readonly IRepository<Game> _repository;

        public GameService(IRepository<Game> repository)
        {
            _repository = repository;
        }

        public void AddProduct(Game game)
        {
            if (string.IsNullOrEmpty(game.Name))
                throw new ArgumentException("Nazwa produktu nie może być pusta.");

            _repository.Add(game);
        }
    }
}