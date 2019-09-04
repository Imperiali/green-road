﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GreenRoad.Web.Models.Horta
{
    public class HortaBindingModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public string PerfilNome { get; set; }
        public int PerfilId { get; set; }
        public List<SelectListItem> Perfis = new List<SelectListItem>();
    }
}
