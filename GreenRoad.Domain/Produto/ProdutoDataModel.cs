using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenRoad.Domain.Produto
{
    public class ProdutoDataModel
    {
        public int Id { get; set; }    
        public int HortaId { get; set; }    
        public string HortaTitulo { get; set; }    
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Foto { get; set; }
    }
}
