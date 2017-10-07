using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEntityFramework.EntidadesClass
{
    public class ProdutoVenda
    {
        /* Esta classe representa um relacionamento N pra N entre a classe Produto e a classe Venda */

        public int VendaID { get; set; } //Criando atributo da entidade
        public virtual Venda Venda { get; set; } //Criando atributo associativo que vem da classe Venda

        public int ProdutoID { get; set; } //Criando atributo da entidade
        public virtual Produto Produto { get; set; } //Criando atributo associativo que vem da classe Produto
    }
}
