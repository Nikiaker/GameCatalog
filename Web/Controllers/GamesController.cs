using Core.DictionaryProvider;
using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.DAO.Models;
using Mackowiak.GameCatalog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mackowiak.GameCatalog.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameService _gameService;
        private readonly DeveloperService _developerService;

        public GamesController()
        {
            this._gameService = new GameService();
            this._developerService = new DeveloperService();
        }

        // Akcja: Wyświetlanie listy produktów
        public IActionResult Index()
        {
            var games = this._gameService.GetAllGames().ToList();
            return this.View(games);
        }

        // Akcja: Formularz dodawania produktu
        public IActionResult Create(int? id)
        {
            GameViewModel viewModel;

            if (id.HasValue)
            {
                var game = this._gameService.GetGameById(id.Value);
                viewModel = new GameViewModel
                {
                    Id = game.Id,
                    Name = game.Name,
                    Genre = game.Genre,
                    DeveloperId = game.DeveloperId,
                    IsEdit = true
                };
            }
            else
            {
                viewModel = new GameViewModel();
            }

            viewModel.AvailableGenres = DictionaryProvieder.GameGenreDictionary
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.Value
                });
            viewModel.AvailableDevelopers = this._developerService.GetAllDevelopers()
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                });

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                viewModel.AvailableGenres = DictionaryProvieder.GameGenreDictionary
                    .Select(g => new SelectListItem
                    {
                        Value = g.Key.ToString(),
                        Text = g.Value
                    });
                viewModel.AvailableDevelopers = this._developerService.GetAllDevelopers()
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.Name
                    });

                return this.View(viewModel);
            }

            if (!viewModel.IsEdit)
            {
                var game = new Game
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Genre = viewModel.Genre,
                    DeveloperId = viewModel.DeveloperId
                };

                this._gameService.AddGame(game);
            }
            else
            {
                var game = this._gameService.GetGameById(viewModel.Id);
                game.Name = viewModel.Name;
                game.Genre = viewModel.Genre;
                game.DeveloperId = viewModel.DeveloperId;

                this._gameService.UpdateGame(game);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        // Akcja: Usuwanie produktu
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var game = _gameService.GetGameById(id.Value);
            if (game == null)
            {
                return this.NotFound();
            }

            _gameService.RemoveGame(game.Id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
