using Mackowiak.GameCatalog.Core;
using Mackowiak.GameCatalog.Interfaces;

namespace Mackowiak.GameCatalog.DAO.Models
{
    public class Game : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameGenre Genre { get; set; }
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }
    }
}
