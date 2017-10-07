using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaEntityFramework.EntidadesEntity;
using LojaEntityFramework.EntidadesClass;


/* DAO(Data Acces Object) é a camada responsável por acessar os dados do usuário
 * que está no banco, ele contém as minhas classes objetos que conseguem inserir,
 * deletar... dados do banco, como Usuarios, Produtos e etc...
 */

/* Todo processo que é sempre igual eu posso ISOLAR este processo em uma classe
 * e usar uma instancia dela sempre que necessário, isso deix ao código mais
 * organizado, mais enxuto, e mais fácil para dar manutenção ou adicionar uma
 * nova funcionalidade
 */ 

namespace LojaEntityFramework.DAO
{
    class UsuarioDAO
    {
        private EntidadesContext contexto;

        public UsuarioDAO()
        {
            contexto = new EntidadesContext(); //Criando a conexão com o Entity
        }

        public void Adicionar(Usuario usuario)
        {
            //poderia adicionar algum código também de gravar log por aqui

            contexto.Usuarios.Add(usuario); //Salvando no banco
            contexto.SaveChanges(); //fazendo o commit
            contexto.Dispose(); //Liberando a memória       
        }

        public void Atualizar()
        {
            /* para atualizar é só você pesquisar pelo id algum Usuario com o método
             * BuscarPorId, tendo o obj retornado basta atribuir um novo nome para
             * o atributo nome do obj usuario que veio da pesquisa
             * 
             * isso acontece porque no entity pq todas as entidades são entidades gerenciadas
             * Quando criado um usuario o status dessa entidade passa a ser Added e quandi
             * buscado uma instancia de usuarioDao ela está com status Unchanged
             * Ai você altera algum atributo e ela muda pra Modified e fica pronto pra receber o saveChanges()
             * Se eu tivesse dado um remove() mudaria o status para Deleted pronto para receber o saveChanges()
             * Estes são os 4 estados que o entity framework gerencia 
             */
            contexto.SaveChanges();
        }

        public Usuario BuscarPorId(int id)
        {
            /* Busque o primeiro usuario que você encontrar CUJO SEU ID seja igual
             * ao do parametro passado
             */
            return contexto.Usuarios.FirstOrDefault(u => u.ID == id);
        }

        public void Remover(Usuario usuario)
        {
            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
            contexto.Dispose();
        }

     
            

    }
}
