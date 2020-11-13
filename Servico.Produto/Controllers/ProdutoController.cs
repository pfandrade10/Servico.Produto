using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Servico.Produto.BaseDados;
using Servico.Produto.Estruturas;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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

                string ListaProdutosSession = HttpContext.Session.GetString("Produtos");

                if (string.IsNullOrEmpty(ListaProdutosSession))
                {
                    listProdutos = baseProdutos.PopularProdutos();
                }
                else
                    listProdutos = JsonConvert.DeserializeObject<List<Models.Produto>>(ListaProdutosSession);

                //Criar método para popular lista de produtos

                if (idProduto.HasValue)
                {
                    //Retornar produto que contenha o id especificado
                    estruturaProduto.Produtos = listProdutos.Where(x => x.idProduct == idProduto).ToList();

                    if(estruturaProduto.Produtos.Count == 0)
                    {
                        throw new Exception("Produto Selecionado não Existe");
                    }

                    return estruturaProduto;
                }
                
                estruturaProduto.Produtos = listProdutos;

                string listaProdutos = JsonConvert.SerializeObject(listProdutos);

                HttpContext.Session.SetString("Produtos", listaProdutos);

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
        public EstruturaProduto Post(Models.Produto produto)
        {
            EstruturaProduto estruturaProduto = new EstruturaProduto();

            try
            {
                BaseProdutos baseProdutos = new BaseProdutos();

                if (produto == null)
                {
                    //Erro
                    throw new Exception("o produto a ser inserido não pode ser nulo");
                }
                //Realizar outras validações

                List<Models.Produto> listProdutos = new List<Models.Produto>();

                string ListaProdutosSession = HttpContext.Session.GetString("Produtos");

                if (string.IsNullOrEmpty(ListaProdutosSession))
                    listProdutos = baseProdutos.PopularProdutos();
                else
                    listProdutos = JsonConvert.DeserializeObject<List<Models.Produto>>(ListaProdutosSession);

                if (listProdutos.Where(x => x.idProduct == produto.idProduct).SingleOrDefault() != null)
                    throw new Exception("Já existe um produto com esse Identificador registrado");

                listProdutos.Add(produto);

                estruturaProduto.Produtos = listProdutos;

                string listaProdutos = JsonConvert.SerializeObject(estruturaProduto.Produtos);

                HttpContext.Session.SetString("Produtos", listaProdutos);

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
        /// Métodos para alterar produtos
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        [HttpPut]
        public EstruturaProduto Put([FromBody] Models.Produto produto)
        {
            EstruturaProduto estruturaProduto = new EstruturaProduto();

            try
            {

                BaseProdutos baseProdutos = new BaseProdutos();

                if (produto == null)
                    throw new Exception("o produto a ser alterado não pode ser nulo");
                

                if (produto.idProduct == 0)
                    throw new Exception("Favor selecionar um produto!");

                List<Models.Produto> listProdutos = new List<Models.Produto>();

                string ListaProdutosSession = HttpContext.Session.GetString("Produtos");

                if (string.IsNullOrEmpty(ListaProdutosSession))
                    listProdutos = baseProdutos.PopularProdutos();
                else
                    listProdutos = JsonConvert.DeserializeObject<List<Models.Produto>>(ListaProdutosSession);

                Models.Produto produtoAlterado = listProdutos.Where(x => x.idProduct == produto.idProduct).SingleOrDefault();

                if (produtoAlterado == null)
                    throw new Exception("produto selecionado não existe");

                listProdutos.Remove(produtoAlterado);
                listProdutos.Add(produto);

                estruturaProduto.Produtos = listProdutos;

                string listaProdutos = JsonConvert.SerializeObject(estruturaProduto.Produtos);

                HttpContext.Session.SetString("Produtos", listaProdutos);

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
        /// Método para deletar produtos
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        [HttpDelete]
        public EstruturaProduto Delete(int idProduto)
        {

            EstruturaProduto estruturaProduto = new EstruturaProduto();
            try
            {
                BaseProdutos baseProdutos = new BaseProdutos();

                if (idProduto == 0)
                {
                    //Retornar produto que contenha o id especificado

                    throw new Exception("produto selecionado não existe");
                }

                List<Models.Produto> listProdutos = new List<Models.Produto>();

                string ListaProdutosSession = HttpContext.Session.GetString("Produtos");

                if (string.IsNullOrEmpty(ListaProdutosSession))
                    listProdutos = baseProdutos.PopularProdutos();
                else
                    listProdutos = JsonConvert.DeserializeObject<List<Models.Produto>>(ListaProdutosSession);

                Models.Produto produtoRemovido = listProdutos.Where(x => x.idProduct == idProduto).SingleOrDefault();

                if(produtoRemovido == null)
                    throw new Exception("produto selecionado não existe");

                listProdutos.Remove(produtoRemovido);

                estruturaProduto.Produtos = listProdutos;

                string listaProdutos = JsonConvert.SerializeObject(estruturaProduto.Produtos);

                HttpContext.Session.SetString("Produtos", listaProdutos);

                return estruturaProduto;
            }
            catch (Exception ex)
            {
                estruturaProduto.isError = true;
                estruturaProduto.descricaoErro = ex.Message;

                return estruturaProduto;
            }

        }
    }
}