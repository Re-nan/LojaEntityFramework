using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaEntityFramework.EntidadesEntity;
using LojaEntityFramework.EntidadesClass;
using Microsoft.Data.Entity;

namespace LojaEntityFramework.DAO
{
    class ProdutoDAO
    {
        private EntidadesContext contexto;

        public ProdutoDAO()
        {
            contexto = new EntidadesContext(); //Criando a conexão com o Entity
        }

        public void Adicionar(Produto produto)
        {
            //poderia adicionar algum código também de gravar log por aqui

            contexto.Produtos.Add(produto); //Salvando no banco
            contexto.SaveChanges(); //fazendo o commit
            contexto.Dispose(); //Liberando a memória       
        }

        public void Atualizar()
        {
            /* para atualizar é só você pesquisar pelo id algum Produto, tendo 
             * o obj retornaro basta atribuir um novo nome para o atributo nome
             * do obj produto que veio da pesquisa
             */
            contexto.SaveChanges();
        }

        public Produto BuscarPorId(int id)
        {
            return contexto.Produtos.FirstOrDefault(p => p.ID == id);
        }

        public Produto BuscarPorIdProdutoIncluindoCategoria(int id)
        {
            return contexto.Produtos.Include(produto => produto.Categoria).FirstOrDefault(p => p.ID == id);
        }

        public void Remover(Produto produto)
        {
            contexto.Produtos.Remove(produto);
            contexto.SaveChanges();
            contexto.Dispose();
        }

        /* Filtra um Produto pelo nome, preço, nome da categoria e todos esses parametros tem que serem opcionais
         * então se eu passar só o nome ele tem que filtrar só usando nome, se eu passar só preço ele filtra só
         * usando preço, se eu passar só a categeoria ele frilta só usando o nome da categoria MAS se eu passar
         * o nome do produto e o preço, ele tem que aplicar os dois filtros
         */

        //Vai retornar uma lista de Produtos, pra cada if o entityFramework mescla as queries e só depois eles manda o return
        public IList<Produto> BuscarPorNomePrecoCategoria(string nome, decimal preco, string nomeCategoria)
        {
            // a var busca recebe uma lista com TODOS os produtos
            var busca = from p in contexto.Produtos
                        select p;

            /*
            if (!string.IsNullOrEmpty(nome))
            {
                busca = from p in busca
                        where p.Nome == nome
                        select p;
            }

            if (preco > 0.0m)
            {
                busca = from p in busca
                        where p.Preco == preco
                        select p;
            }

            if (!string.IsNullOrEmpty(nomeCategoria))
            {
                busca = from p in busca
                        where p.Categoria.Nome == nomeCategoria
                        select p;
            }
            OU
            Podemos diminuir este código para ficar um pouco mais legível utiliando Lync
            No C# podemos escrever LINQ de duas formas, a primeira é por meio de um conjunto de palavras reservadas.
            Além disso, toda IQuery possui uma série de métodos para personalizá-la - podemos substituir a palavra 
            reservada where pela chamada do método Where()
            */

            if (!string.IsNullOrEmpty(nome))
            {
                busca = busca.Where(p => p.Nome == nome);
            }

            if (preco > 0.0m)
            {
                busca = busca.Where(p => p.Preco == preco);
            }

            if (!string.IsNullOrEmpty(nomeCategoria))
            {
                busca = busca.Where(p => p.Categoria.Nome == nomeCategoria);
            }



            return busca.ToList();
        }


         
    }
}
