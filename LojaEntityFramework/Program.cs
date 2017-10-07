using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using LojaEntityFramework.EntidadesClass;
using LojaEntityFramework.DAO;
using LojaEntityFramework.EntidadesEntity;
using Microsoft.Data.Entity;



/* Entity Framework
 * É responsável por mapear coisas do mundo dos obj para coisas do mundo relacional
 * 
 * Comandos para instalar o EntityFramework no console do nuget
 * 
 * Para começar a trabalhar com o Entity Framework precisamos fazer a instalação do Entity no nosso projeto, 
 * para isso vamos usar os seguintes comandos no Nuget: 
 * -Pre é pra pegar até as versões que estão em pré release
 * Install-Package EntityFramework.MicrosoftSqlServer -Version 7.0.0-rc1-final -Pre
 * 
 * Precisaremos dos pacotes que contêm os comandos de migrações do Entity Framework. Para isto, instalaremos 
 * a implementação do Entity Framework que trabalha com o Sql Server e com migrations, executando o seguinte
 * comando:
 * Install-Package EntityFramework.Commands -Version 7.0.0-rc1-final -pre
 */

/* Migrations
 * O Migrations é responsável por fazer a transformação entre as classe para a entidade em si
 * ele é como um versionamento, ele pode criar tabela como pode desfazer a criação voltando pro estado anterior
 * 
 * Comando para instalar o Migrations
 *     Add-Migration nomeDaMigracao
 * Ex. Add-Migration CriaTbUsuario
 * feito isso ele cria uma pasta Migrations no projeto
 * 
 * Comando para aplicar a migração CriaTbUsuario
 * update-database
 * 
 * RESUMO - PASSOS
 * Criar suas Entity class
 * instalar EntityFramework
 * Criar DataSet
 * Criar EntidadesContext
 * Executar Migration + update+database
 */

/* O Entity Framework gerencia o ciclo de vida das entidades
 * Durante o tempo de vida de uma entidade, cada entidade possui um estado baseado na operação que foi realizada 
 * sobre a entidade via contexto (DBContext)
 * 
 * 1 - Added - A entidade é marcada como adicionada;
 * 2 - Deleted - A entidade é marcada como deletada;
 * 3 - Modified - A entidade foi modificada;
 * 4 - Unchanged - A entidade não foi modificada;
 * 5 - Detached - A entidade não esta sendo tratada no contexto;   
 * 
 * Tenha em mente que o estado de uma entidade reflete o seu relacionamento com o contexto e não com o banco de
 * dados, portando você não precisa dar um add para dar update em um dado, basta você buscar e alterar o dado de
 * uma entidade via context e dar o saveChanges() que ele vai saber que a entidade foi alterada e vai fazer sozinho
 * o update
 */

namespace LojaEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Todo código que sempre se repete eu posso isolar ele
             * 
             * IDbConnection é reponsável por fazer conexão com banco de dados
             * SqlConnection porque é uma conexão com o SQL Server
             * IDbCommand recebe um comando junto uma conexao sql
             * CreateCommand() permite realizar comandos sql
             * CommandText uma var string de IDbCommand que recebe um comando em string
             */

            //IDbConnection conexao = new SqlConnection();
            //IDbCommand comando = conexao.CreateCommand();
            //comando.CommandText = "SELECT * FROM tbl_Usuario";
            //IDataReader leitor = comando.ExecuteReader();

            //        while(leitor.Read())
            //        {
            //         int id = Convert.ToInt32(leitor["id"]); //lendo a coluna id da tabela
            //         string nome = Convert.ToString(leitor["nome"]);
            //        }



            /* --- Criando CriaUsuario(do EntityClass) pelo modo inicialização rápida --- */
            UsuarioDAO contextUsuarioDAO = new UsuarioDAO(); //Contexto para acesso ao dados de Usuario no DAO
            //Usuario usuario = new Usuario()
            //{
            //    Nome = "Roger",
            //    Senha = "123"
            //};
            //contextUsuarioDAO.Adicionar(usuario);
            //Console.WriteLine("Salvou o usuário: " + usuario.Nome);

            /* --- Pesquisando usuario por Id e alterando o nome --- */
            //Usuario recebeUsuario = contextUsuarioDAO.BuscarPorId(1);
            //recebeUsuario.Nome = "RENAN";
            //contextUsuarioDAO.Atualizar(); //Só chama o save changes
            //Console.WriteLine(recebeUsuario.Nome);





            /* --- Criando categoria --- */
            CategoriaDAO contextCategoriaDAO = new CategoriaDAO();
            Categoria categoria = new Categoria()
            {
                Nome = "Roupa"
            };
            //contextCategoriaDAO.Adicionar(categoria);
            //Console.WriteLine("Categoria " + categoria.Nome + " criada!");


            /* --- Criando produto --- */
            ProdutoDAO contextProdutoDAO = new ProdutoDAO();
            Produto p = new Produto()
            {
                Nome = "Moletom",
                Preco = 20,
                //Categoria = categoria //Passando obj Categoria
                //OU 
                CategoriaID = 2 //que ele também saberia(quando vc já sabe o id da categoria q vc quer atribuir)
            };
            //contextProdutoDAO.Adicionar(p); 

            /* --- Buscando produto --- */
            p = contextProdutoDAO.BuscarPorId(1);
            //Console.WriteLine("Produto: " + p.Nome);
            //Console.WriteLine("Categoria: " + p.Categoria.Nome);

            /* --- Buscando categoria do Produto --- */
            /* Isso não funciona mais no Entity Framework 7 porque quando ele trás a entidade
             * produto ele trás somente o que é de Produto, e o que é estrangeira ele não trás
             * para isso é necessário dizer que você quer trazer de Produto e o que for de
             * estrangeira.
             * Ele faz isso porque quando a gente trabalha com Produto, nem sempre a gente quer
             * trabalhar com os relacionamentos dele e por motivo de performance o Entity trás
             * por padrão só o que for de produto, para trazer com seus relacionamentos é necessário
             * especificar com a clausula Include() 
             * contexto.Produtos.FirstOrDefault(p => p.ID == id); //Trazendo só produto
             * contexto.Produtos.Include(produto => produto.Categoria).FirstOrDefault(p => p.ID == id); //Trazendo Produto mais o relacionamento com Categoria
             */
            p = contextProdutoDAO.BuscarPorIdProdutoIncluindoCategoria(1);
            //Console.WriteLine("Categoria: " + p.Categoria.Nome);



            /* Agora o inverso, de um categoria quero listar todos os produtos dela */
            //Console.WriteLine("\nLista de Produtos em uma Categoria");
            contextCategoriaDAO = new CategoriaDAO();
            Categoria c = contextCategoriaDAO.BuscarPorIdCategoriaIncluindoProduto(1);

            //foreach(Produto prod in c.Produtos)
            //{
            //    Console.WriteLine(prod.Nome);
            //}

            /* Usar LINQ facilita a escrita de consultas personalizadas, afinal é uma tecnologia
             * que desenvolvedores C# estão familiarizados, além de ser possível de referenciar
             * as nossas entidades como classes e não como tabelas do banco, o que evita diversos
             * Joins e torna o LINQ mais legível do que o SQL. Além disso com o LINQ não precisamos
             * nos preocupar com detalhes do SQL, que mudam de banco para banco
             */

            /* Usando Lync para selecionar Todos os objs produtos que já foram criados
             * acesso o contexto do Entity e não do DAO. EntidadesContext é a ligação
             * direta com as Entidades, por isso uso lync para fazer o acesso aos dados
             * prd é uma instancia de Produto
             * 
             * from prd
             * recebe a instancia de context.Produtos
             * 
             * context.Produtos select prd
             * traga uma instancia de produto
             */
            EntidadesContext context = new EntidadesContext();
            var busca = from prd in context.Produtos select prd;

            Console.WriteLine("Listando Produtos inseridos na tabela Produtos");
            /* Por produtos e as outras entidades serem uma coleção(um DbSet) dentro de
             * EntidadesContext eu faço um for para trazer todos os obj Produtos que foram
             * criados
             */
            foreach (var item in busca)
            {
                Console.WriteLine(item.Nome);
            }

            //Ou
            /*IList<Produto> resultado = busca.ToList();
            foreach (var item in resultado)
            {
                Console.WriteLine(item.Nome);
            }*/

            Console.WriteLine("\nProdutos ordenados por nome");
            /* Usando o lync para fazer uma consulta dos Produtos com order by */
            busca = from prd in context.Produtos orderby prd.Nome select prd;
            foreach (var item in busca)
            {
                Console.WriteLine(item.Nome); //já tras ordenado crescente
            }

            Console.WriteLine("\nProdutos ordenados por preço");
            /* Usando o lync para fazer uma consulta dos Produtos ordenando por preço */
            busca = from prd in context.Produtos orderby prd.Preco select prd;
            foreach (var item in busca)
            {
                Console.WriteLine(item.Nome);
            }

            Console.WriteLine("\nProdutos ordenados por preço cujo preço > 30");
            /* Usando o lync para fazer uma consulta dos Produtos quando preço > 30 */
            busca = from prd in context.Produtos where prd.Preco > 30 orderby prd.Preco select prd;
            foreach (var item in busca)
            {
                Console.WriteLine(item.Nome);
            }

            Console.WriteLine("\nProdutos quando a categoria for igual a Roupas orderando por preço");
            /* Usando o lync para fazer uma consulta envolvendo as entidades Produto e Categoria
             * sem precisar fazer inner join. Estou usando a navigation property prd.Categoria.Nome
             * Trazendo Roupas quando a categeroria de Produto for igual a "Roupas" ordenando por preço */
            busca = from prd in context.Produtos where prd.Categoria.Nome == "Roupas" orderby prd.Preco select prd;
            foreach (var item in busca)
            {
                Console.WriteLine(item.Nome);
            }

            Console.WriteLine("\nProdutos com preço acima de 40, quando a categoria for igual a Roupas orderando por preço");
            /* Usando o lync para fazer uma consulta envolvendo as entidades Produto e categoria
             * sem precisar fazer inner join. Estou usando a navigation property prd.Categoria.Nome
             * Trazendo Roupas quando a categeroria de Produto for igual a "Roupas" e preço maior que 40 ordenando por preço */
            busca = from prd in context.Produtos where prd.Categoria.Nome == "Roupas" && prd.Preco > 40 orderby prd.Preco select prd;
            foreach (var item in busca)
            {
                Console.WriteLine(item.Nome);
            }

            /* Usar um obj de EntidadesContext não é o recomendado, devemos escrever métodos nas nossas
             * classes DAO para que elas façam estes selects
             */


            Console.WriteLine();


            /* Todas vez que criamos uma consulta usando LINQ ela só é executada quando chamamos o método ToList<>(); ou quando
             * executamos um foreach na busca, isso nos permite editar a consulta diversas vezes antes de percorrer.
             * 
             * Todas as consultas que executamos até agora poderiam ser filtradas adicionando um if dentro do nosso foreach,
             * qual é o problema dessa abordagem?
             * Quando adicionamos um if dentro do foreach as verificações são executadas depois da query trazer todos
             * os registros do banco o que causaria uma perda de performance, quando adicionamos o where no LINQ a consulta
             * já traz os resultados filtrados direto do banco.

            /* Buscando todos os produtos quando o preco for 30 */
            IList<Produto> resultado;
            resultado = contextProdutoDAO.BuscarPorNomePrecoCategoria(null, 30, null);
            foreach (var prd in resultado)
            {
                Console.WriteLine("Produtos com preço 30: " + prd.Nome);
            }

            /* Buscando todos produtos quando o nome for Mouse */
            resultado = contextProdutoDAO.BuscarPorNomePrecoCategoria("Mouse", 0, null);
            foreach (var prd in resultado)
            {
                Console.WriteLine("\nProdutos com nome Mouse: " + prd.Nome);
            }

            /* Buscando todos produtos quando o nome for Mouse e o preço for 10, note que aqui o entity consegue combinar
             * os dois parametros, porém se fosse 30 não traria, pois não existe mouse com preço 30*/
            resultado = contextProdutoDAO.BuscarPorNomePrecoCategoria("Mouse", 10, null);
            foreach (var prd in resultado)
            {
                Console.WriteLine("\nProdutos com nome Mouse e preço 10: " + prd.Nome + "\n");
            }

            /* Buscando todos produtos quando a Categoria for Informática*/
            resultado = contextProdutoDAO.BuscarPorNomePrecoCategoria(null, 0, "Informatica");
            foreach (var prd in resultado)
            {
                Console.WriteLine("Produtos com a categoria Informatica: " + prd.Nome);
            }


            /* ----- Criando VENDA ----- */

            // Buscando usuario
            Usuario user = contextUsuarioDAO.BuscarPorId(1); //Busca o usuario renan da entidade Usuario

            //Criando Venda
            //Venda v = new Venda()
            //{
            //    //ID = é definido sozinho pelo entity
            //    //ProdutoVenda = é definido sozinho pelo entity
            //    Cliente = user //estou passando o usuario/Cliente desta venda
            //};

            //Adicionando 2 Produtos a minha Venda (mouse e teclado)
            //Produto p1 = context.Produtos.FirstOrDefault(prod => prod.ID == 1);
            //Produto p2 = context.Produtos.FirstOrDefault(prod => prod.ID == 2);

            //Ação da venda
            //ProdutoVenda pv1 = new ProdutoVenda()
            //{
            //    Venda = v,
            //    Produto = p1
            //};

            //Ação da venda
            //ProdutoVenda pv2 = new ProdutoVenda()
            //{
            //    Venda = v, //Note que a venda ainda é a mesma
            //    Produto = p2
            //};

            //context.Vendas.Add(v);
            //context.ProdutoVenda.Add(pv1);
            //context.ProdutoVenda.Add(pv2);
            //context.SaveChanges();
            //context.Dispose();
            //Console.WriteLine("\nVenda Criada");


            /*----- Recuperando dados da Venda ----- */

            //Recupera as Venda incluindo a lista de ProdutoVenda
            Venda vendaRealizada = context.Vendas.Include(v => v.ProdutoVenda).ThenInclude(pv => pv.Produto).FirstOrDefault(v => v.ID == 1);

            Console.WriteLine("\nProdutos Vendidos");
            foreach (var pv in vendaRealizada.ProdutoVenda)
            {
                Console.WriteLine(pv.Produto.Nome);
            }


            /* ----- Cadastrando novo usuário agora com CPF e CPJ ----- */
            /* Na entidade Usuario ele cria a coluna Discriminator pra referenciar de qual entidade filha veio o dado */

            PessoaFisica pf = new PessoaFisica()
            {
                Nome = "Bernardo",
                CPF = "12345",
                Senha = "123"
            };

            //context.PessoasFisicas.Add(pf);

            PessoaJuridica pj = new PessoaJuridica()
            {
                Nome = "Nick",
                CNPJ = "678910",
                Senha = "123"
            };

            //context.PessoasJuridicas.Add(pj);
            context.SaveChanges();


            Console.ReadKey();
        }
    }
}
