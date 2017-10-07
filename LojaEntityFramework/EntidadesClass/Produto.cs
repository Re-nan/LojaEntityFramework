using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Do ponto de vista que todas essas classes dentro de EntidadeClass são entidades
 * e toda entidade vira uma Tabela, perceba que temos uma Entidade Categoria dentro
 * da Entidade produto, ambas vão virar tabela no banco, só que o entity vai tratar
 * essa Entidade dentro de outra como uma chave estrangeira relacionando as entidades
 * 
 * A tabela Produto salva os ID's da Categoria, ou seja Categoria é chave estrangeira
 * em Produto
 */
 
namespace LojaEntityFramework.EntidadesClass
{
    public class Produto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        /* Na tabela produtos o entity salva o ID da Categoria "CategoriaID", mas e se através
         * da entidade Produto eu quiser pegar o nome da Categoria? para isso criando a propriedade
         * Categoria.
         * 
         * Está propriedade é conhecida como navigation property
         * Através dessa propridade Categoria eu consigo navegar pelo meu relacionamento
         * com a entidade Produto. Toda navigation propertie tem que ser do tipo Virtual
         * porque o Entity Framework vai precisar reescrever ela
         * 
         * Produto deve ter um atributo do tipo Categoria e a classe Categoria deve ter uma lista de 
         * produtos, a principal diferença é que o entity exige além da composição um atributo que 
         * representa a chave estrangeira desse relacionamento que deve seguir a convenção NomeDaClasseID 
         */
        public virtual Categoria Categoria {get; set;}
        public int CategoriaID { get; set; }


        /* 1 Venda tem vários Produtos e 1 Produto pode está em várias Vendas
         * Temo um relacionamento many to many, ou seja N pra N
         * Com isso o entity precisa que você crie uma tabela/entidade/classe associativa que contenha o ID da
         * Venda e o ID do Produto, esta classe nomeamos de ProdutoVenda
         * 
         * E tanto a classe Produto como a venda possuem uma Lista de ProdutoVenda
         */
        public virtual IList<ProdutoVenda> ProdutoVenda { get; set; }

    }
}