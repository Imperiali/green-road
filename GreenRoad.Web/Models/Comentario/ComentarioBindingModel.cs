using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GreenRoad.Web.Models.Comentario
{
    public class ComentarioBindingModel
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int PostId { get; set; }
        public string PostNome { get; set; }
        public List<SelectListItem> Posts = new List<SelectListItem>();
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; }
        public List<SelectListItem> Perfis = new List<SelectListItem>();
    }
}
