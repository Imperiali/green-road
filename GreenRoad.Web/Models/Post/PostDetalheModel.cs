using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenRoad.Web.Models.Post
{
    public class PostDetalheModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; }
        public List<SelectListItem> Perfis = new List<SelectListItem>();
    }
}