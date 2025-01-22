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
    public class DevelopersController : Controller
    {
        private readonly DeveloperService _developerService;

        public DevelopersController()
        {
            _developerService = new DeveloperService();
        }

        // Akcja: Wyświetlanie listy produktów
        public IActionResult Index()
        {
            var developers = this._developerService.GetAllDevelopers().ToList();
            return View(developers);
        }

        // Akcja: Formularz dodawania produktu
        public IActionResult Create(int? id)
        {
            DeveloperViewModel viewModel;

            if (id.HasValue)
            {
                var developer = this._developerService.GetDeveloperById(id.Value);
                viewModel = new DeveloperViewModel
                {
                    Id = developer.Id,
                    Name = developer.Name,
                    Edit = true
                };
            }
            else
            {
                viewModel = new DeveloperViewModel();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeveloperViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                return View(viewModel);
            }

            if (!viewModel.Edit)
            {
                var developer = new Developer
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name
                };

                this._developerService.AddDeveloper(developer);
            }
            else
            {
                var developer = this._developerService.GetDeveloperById(viewModel.Id);
                developer.Name = viewModel.Name;

                this._developerService.UpdateDeveloper(developer);
            }

            return RedirectToAction(nameof(Index));
        }

        // Akcja: Usuwanie produktu
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = this._developerService.GetDeveloperById(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            this._developerService.RemoveDeveloper(game.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
