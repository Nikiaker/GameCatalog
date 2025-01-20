﻿using Mackowiak.GameCatalog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mackowiak.GameCatalog.DAO.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameGenre Genre { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
    }
}
