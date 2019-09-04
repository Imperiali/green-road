using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenRoad.Domain.Horta
{
    public class HortaDataModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public string PerfilNome { get; set; }
        public int PerfilId { get; set; }
    }
}
