using Mackowiak.GameCatalog.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mackowiak.GameCatalog.Web.Models
{
    public class DeveloperViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEdit { get; set; } = false;
    }
}
