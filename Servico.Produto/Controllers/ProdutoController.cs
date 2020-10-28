using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Servico.Produto.BaseDados;
using Servico.Produto.Estruturas;

namespace Servico.Produto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public ProdutoController(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }

        /// <summary>
        /// Método que busca um ou mais produtos
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        [HttpGet]
        public EstruturaProduto Get(int? idProduto)
        {
            EstruturaProduto estruturaProduto = new EstruturaProduto();

            try
            {
                BaseProdutos baseProdutos = new BaseProdutos();

                List<Models.Produto> listProdutos = new List<Models.Produto>();
                listProdutos = baseProdutos.PopularProdutos();
                //Criar método para popular lista de produtos

                if (idProduto.HasValue)
                {
                    //Retornar produto que contenha o id especificado
                    estruturaProduto.Produtos = listProdutos.Where(x => x.idProduct == idProduto).ToList();

                    return estruturaProduto;
                }
                
                estruturaProduto.Produtos = listProdutos;

                //Retorna os produtos todos

                return estruturaProduto;
            }
            catch (Exception ex)
            {
                estruturaProduto.isError = true;
                estruturaProduto.descricaoErro = ex.Message;

                return estruturaProduto;
            }

        }

        /// <summary>
        /// Método para inserir produtos
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Models.Produto produto)
        {
            try
            {

                if (produto == null)
                {
                    //Erro
                    throw new Exception("o produto a ser inserido não pode ser nulo");
                }
                //Realizar outras validações

                //Se validações ok, inserir produto na lista

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        /// <summary>
        /// Métodos para alterar produtos
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] Models.Produto produto, int idProduto)
        {
            try
            {

                if (idProduto == 0)
                {
                    //Verificar e o produto escolhido existe

                    throw new Exception("produto selecionado não existe");
                }


                //Retorna os produtos todos

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        /// <summary>
        /// Método para deletar produtos
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int idProduto)
        {
            try
            {

                if (idProduto == 0)
                {
                    //Retornar produto que contenha o id especificado

                    throw new Exception("produto selecionado não existe");
                }


                //Retorna os produtos todos

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}