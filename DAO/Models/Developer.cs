using Mackowiak.GameCatalog.Interfaces;

namespace Mackowiak.GameCatalog.DAO.Models
{
    public class Developer : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
