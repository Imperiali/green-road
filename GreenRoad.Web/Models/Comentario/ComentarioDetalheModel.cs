using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenRoad.Web.Models.Comentario
{
    public class ComentarioDetalheModel
    {
        public int Id { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; }
        public string Texto { get; set; }
        public int PostId { get; set; }
        public string PostNome { get; set; }
    }
}