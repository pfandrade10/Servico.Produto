using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servico.Produto.BaseDados
{
    public class BaseProdutos
    {

        public List<Models.Produto> PopularProdutos()
        {
            try
            {
                int qtdeProdutos = 15;

                List<Models.Produto> produtos = new List<Models.Produto>();

                for (int i = 0; i < qtdeProdutos; i++)
                {
                    Models.Produto produto = new Models.Produto
                    {
                        idProduct = i + 1,
                        productName = "Produto " + i,
                        description = "Descricao " + i,
                        cathegory = $"{i}",
                        price = 100*(i),
                    };

                    produtos.Add(produto);
                }

                return produtos;
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao popular dados: " + ex.Message);
            }
        }
    }
}
