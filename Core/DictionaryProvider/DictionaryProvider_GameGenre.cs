using Mackowiak.GameCatalog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string GetGameGenre(GameGenre gameGenre)
        {
            if (GameGenreDictionary.ContainsKey(gameGenre))
            {
                return GameGenreDictionary[gameGenre];
            }

            return string.Empty;
        }

        public static GameGenre GetGameGenre(string gameGenre)
        {
            foreach (var item in GameGenreDictionary)
            {
                if (item.Value == gameGenre)
                {
                    return item.Key;
                }
            }

            return GameGenre.Unknown;
        }
    }
}
