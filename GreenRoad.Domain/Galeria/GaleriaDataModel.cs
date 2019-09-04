using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenRoad.Domain.Galeria
{
    public class GaleriaDataModel
    {
        public int Id { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; }
        public string FotoUrl { get; set; }
    }
}
