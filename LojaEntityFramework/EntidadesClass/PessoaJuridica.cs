using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaEntityFramework.EntidadesClass
{
    public class PessoaJuridica : Usuario
    {
        public string CNPJ { get; set; }
    }
}
