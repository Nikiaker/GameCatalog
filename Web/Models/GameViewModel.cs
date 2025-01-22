using Mackowiak.GameCatalog.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mackowiak.GameCatalog.Web.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameGenre Genre { get; set; }
        public int DeveloperId { get; set; }
        [BindNever]
        public IEnumerable<SelectListItem> AvailableGenres { get; set; }
        [BindNever]
        public IEnumerable<SelectListItem> AvailableDevelopers { get; set; }
        public bool Edit { get; set; } = false;

        public GameViewModel()
        {
            AvailableGenres = Enumerable.Empty<SelectListItem>();
            AvailableDevelopers = Enumerable.Empty<SelectListItem>();
        }
    }
}
