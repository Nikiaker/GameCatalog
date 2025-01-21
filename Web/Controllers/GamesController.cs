using Core.DictionaryProvider;
using Mackowiak.GameCatalog.BL;
using Mackowiak.GameCatalog.Core;
using Mackowiak.GameCatalog.DAO;
using Mackowiak.GameCatalog.DAO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mackowiak.GameCatalog.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameService _gameService;

        public GamesController()
        {
            _gameService = new GameService();
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
            ViewBag.Genres = DictionaryProvieder.GameGenreDictionary.Select(g => new SelectListItem
            {
                Value = g.Key.ToString(),
                Text = g.Value
            });

            return View();
        }

        // Akcja: Obsługa zapisu nowego produktu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                _gameService.AddGame(game);
                return RedirectToAction(nameof(Index));
            }
            return View(game);
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
