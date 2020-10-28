using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servico.Produto.Estruturas
{
    public class EstruturaProduto : EstruturaErro
    {
        public EstruturaProduto()
        {
            Produtos = new List<Models.Produto>();
        }

        public List<Models.Produto> Produtos { get; set; }
    }
}
