using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenRoad.Web.Models.Galeria
{
    public class GaleriaCreateModel
    {
        public int Id { get; set; }
        public string PerfilNome { get; set; }
        public int PerfilId { get; set; }        
        public List<SelectListItem> Perfis = new List<SelectListItem>();
        public string FotoUrl { get; set; }
    }
}