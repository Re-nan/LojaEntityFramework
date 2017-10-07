using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using LojaEntityFramework.EntidadesClass;
using System.Configuration;

/* Foi instalado o entity e criado um DataSet na aplicação
 * Está classe é responsável por ser a configuração entre o EntityFramework e as minhas classes Entidades
 * Toda classe que irá se tornar uma Entidade deve ser mapeado aqui para elas se tornem entidades de fato
 */

/* O entity framework consegue encontrar nossa entidade através dos atributos do tipo DbSet da classe
 * que herda de DbContext se não criarmos os atributos no nosso contexto o entity não conseguirá salvar
 * os nossos dados.
 */

namespace LojaEntityFramework.EntidadesEntity
{
    class EntidadesContext : DbContext
    {
        /* Recebe um conjunto de objs Usuario
         */
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ProdutoVenda> ProdutoVenda { get; set; }
        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }



        /* Reescrevendo o metodo OnConfigure para definir o provider da conexão do EntityFramework
         * Provider é o cara que fala com qual banco a gente ta trabalhando
         */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* Pegando string de conexao que fica no arquivo App.Config do projeto
             * Para isso eu adiciono a referencia System.Configuration no projeto e seu namespace na classe que vou usar
             * e agora posso usar a classe ConfigurationManager para ler o valor do elemento "connectionStrings" do App.Config
             */
            string stringConexao = ConfigurationManager.ConnectionStrings["lojaConnectionString"].ConnectionString; 
            optionsBuilder.UseSqlServer(stringConexao); //Dizendo que vou usar o Sql Sever passando a string de conexao
            base.OnConfiguring(optionsBuilder);
        }


        /* Quando temos entidades N pra N o entity precisa criar uma chave única com a junção dessas chaves estrangeiras que 
         * que já estão na entidade, chamamos isso de chave primária composta(2 ou mais chaves distintas para compor uma chave
         * primária.
         * E pra isso precisamos falar pro entity framework que quando ele tiver criando as entidades eu quero defindir essa chave
         * composta no ProdutoVenda, e para isso eu tenho que sobreescrever o método OnModelCreating()
         */
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* modelBuilder é o cara responsável por criar os modelos, precisamos dizer que minha classe ProdutoVenda tem uma
             * chave primária composta por outras duas chaves primárias que são estrangeiras na class ProdutoVenda */
            modelBuilder.Entity<ProdutoVenda>().HasKey(pv => new { pv.VendaID, pv.ProdutoID }); //passando um objeto anonimo
            base.OnModelCreating(modelBuilder);
        }
         


    }
}
