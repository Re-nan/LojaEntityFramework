using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEntityFramework.EntidadesClass
{
    public class Venda
    {
        public int ID { get; set; }


        /* Definindo navigation property que serve para navegar entre as entidades, lembrando que toda
         * navigation property precisa ser do tipo virtual, lembrando que o tipo virtual permite que este
         * atributo ou metodo possa ser sobreescrevido por outra classe que herdar
         * 1 Cliente faz muitas Vendas, 1 pra N
         */ 
        public virtual Usuario Cliente { get; set; }
        public int ClienteID { get; set; }


        /* 1 Venda tem vários Produtos e 1 Produto pode está em várias Vendas
         * Temo um relacionamento many to many, ou seja N pra N
         * Com isso o entity precisa que você crie uma tabela/entidade/classe associativa que contenha o ID da
         * Venda e o ID do Produto, esta classe nomeamos de ProdutoVenda
         * 
         * E tanto a classe Produto como a venda possuem uma Lista de ProdutoVenda
         */
        public IList<ProdutoVenda> ProdutoVenda { get; set; }

    }
}
