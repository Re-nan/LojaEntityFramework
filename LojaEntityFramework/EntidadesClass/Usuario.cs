using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEntityFramework.EntidadesClass
{
    /* Toda entidade do EntityFramework precisa ser uma classe pública 
     * Nossa Classe Usuario é abstrata para forçar que não se crie instancias de Usuario
     * pois queremos que todo usuario venda de PessoaFisica ou PessoaJuridica que herdam
     * de Usuario
     */
    public abstract class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}