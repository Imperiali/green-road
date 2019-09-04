using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenRoad.Web.Models.Produto
{
    public class ProdutoDetalheModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Foto { get; set; }
        public int HortaId { get; set; }
        public string HortaTitulo { get; set; }        
    }
}