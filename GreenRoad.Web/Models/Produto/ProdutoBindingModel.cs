using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GreenRoad.Web.Models.Produto
{
    public class ProdutoBindingModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Foto { get; set; }
        public int HortaId { get; set; }
        public string HortaTitulo { get; set; }
        public List<SelectListItem> Hortas = new List<SelectListItem>(); 
    }
}
