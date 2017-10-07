using LojaEntityFramework.EntidadesClass;
using LojaEntityFramework.EntidadesEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace LojaEntityFramework.DAO
{
    class CategoriaDAO
    {
        private EntidadesContext contexto;

        public CategoriaDAO()
        {
            contexto = new EntidadesContext(); //Criando a conexão com o Entity
        }

        public void Adicionar(Categoria categoria)
        {
            //poderia adicionar algum código também de gravar log por aqui

            contexto.Categorias.Add(categoria); //Salvando no banco
            contexto.SaveChanges(); //fazendo o commit
            contexto.Dispose(); //Liberando a memória       
        }

        public void Atualizar()
        {
            /* para atualizar é só você pesquisar pelo id alguma Categoria, tendo 
             * o obj retornaro basta atribuir um novo nome para o atributo nome
             * do obj categoria que veio da pesquisa
             */
            contexto.SaveChanges();
        }




        public Categoria BuscarPorId(int id)
        {
            return contexto.Categorias.FirstOrDefault(c => c.ID == id);
        }

        public Categoria BuscarPorIdCategoriaIncluindoProduto(int id)
        {
            return contexto.Categorias.Include(c => c.Produtos).FirstOrDefault(c => c.ID == id);
        }




        public void Remover(Categoria categoria)
        {
            contexto.Categorias.Remove(categoria);
            contexto.SaveChanges();
            contexto.Dispose();
        }
    }
}
