using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEntityFramework.EntidadesClass
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nome { get; set; }


        /* Um produto tem uma categoria e uma categoria tem uma lista de produtos
         * Definindo que a minha categoria vai ter vários produtos */
        public IList<Produto> Produtos { get; set; }
    }
}
