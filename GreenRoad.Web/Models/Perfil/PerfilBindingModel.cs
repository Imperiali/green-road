using GreenRoad.Domain.Galeria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenRoad.Web.Models.Perfil
{
    public class PerfilBindingModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public int Pontos { get; set; }        
        public List<GaleriaDataModel> Fotos { get; set; } = new List<GaleriaDataModel>();
    }
}
