using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Servico.Produto.Models;

namespace Servico.Produto.Estrutura
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