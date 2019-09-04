﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenRoad.Web.Models.Horta
{
    public class HortaDetalheModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public string PerfilNome { get; set; }
        public int PerfilId { get; set; }
    }
}