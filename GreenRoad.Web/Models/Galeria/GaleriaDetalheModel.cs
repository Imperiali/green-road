using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenRoad.Web.Models.Galeria
{
    public class GaleriaDetalheModel
    {
        public int Id { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; }
        public string FotoUrl { get; set; }
    }
}