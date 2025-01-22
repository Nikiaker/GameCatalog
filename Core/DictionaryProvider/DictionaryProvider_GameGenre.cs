using Mackowiak.GameCatalog.Core;

namespace Core.DictionaryProvider
{
    public static class DictionaryProvieder
    {
        public static IDictionary<GameGenre, string> GameGenreDictionary = new Dictionary<GameGenre, string>
        {
            { GameGenre.RPG, "RPG" },
            { GameGenre.FPS, "FPS" },
            { GameGenre.RTS, "RTS" },
            { GameGenre.Adventure, "Adventure" }
        };
    }
}
