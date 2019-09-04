using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenRoad.Domain.Comentario
{
    public class ComentarioDataModel
    {
        public int Id { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; }
        public string Texto { get; set; }
        public int PostId { get; set; }
        public string PostNome { get; set; }
    }
}
