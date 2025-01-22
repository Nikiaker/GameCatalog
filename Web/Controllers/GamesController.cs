using Core.DictionaryProvider;
using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.Core;
using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using Mackowiak.GameCatalog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mackowiak.GameCatalog.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameService _gameService;
        private readonly DeveloperService _developerService;

        public GamesController()
        {
            _gameService = new GameService();
            _developerService = new DeveloperService();
        }

        // Akcja: Wyświetlanie listy produktów
        public IActionResult Index()
        {
            var games = this._gameService.GetAllGames().ToList();
            return View(games);
        }

        // Akcja: Formularz dodawania produktu
        public IActionResult Create()
        {
            var viewModel = new GameViewModel
            {
                AvailableGenres = DictionaryProvieder.GameGenreDictionary
                    .Select(g => new SelectListItem
                    {
                        Value = g.Key.ToString(),
                        Text = g.Value
                    }),
                AvailableDevelopers = this._developerService.GetAllDevelopers()
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.Name
                    })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var game = new Game
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Genre = viewModel.Genre,
                DeveloperId = viewModel.DeveloperId
            };

            this._gameService.AddGame(game);

            return RedirectToAction(nameof(Index));
        }

        // Akcja: Formularz edycji produktu
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.GetGameById(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // Akcja: Obsługa zapisu edytowanego produktu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _gameService.UpdateGame(game);
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // Akcja: Usuwanie produktu
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.GetGameById(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            //_gameService.RemoveGame(game.Id);

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var produkt = _gameService.GetGameById(id);
            _gameService.RemoveGame(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
