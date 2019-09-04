using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenRoad.Web.Models.Galeria
{
    public class GaleriaBindingModel
    {
        public int Id { get; set; }        
        public string PerfilNome { get; set; }
        public string FotoUrl { get; set; }
        public int PerfilId { get; set; }
    }
}