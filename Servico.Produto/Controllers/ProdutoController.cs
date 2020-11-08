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

        List<Models.Produto> ListaAtual = new List<Models.Produto>();

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

                ListaAtual = estruturaProduto.Produtos;

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

                //Se validações ok, inserir produto na lista

                List<Models.Produto> listProdutos = new List<Models.Produto>();
                listProdutos = baseProdutos.PopularProdutos();

                ListaAtual = listProdutos;

                ListaAtual.Add(produto);

                estruturaProduto.Produtos = ListaAtual;

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

                if (ListaAtual.Count == 0)
                    listProdutos = baseProdutos.PopularProdutos();
                else
                    listProdutos = ListaAtual;

                var listaAuxiliar = listProdutos;

                foreach (var item in listProdutos)
                {
                    if (item.idProduct == produto.idProduct)
                    {
                        listaAuxiliar.Remove(item);
                        listaAuxiliar.Add(produto);
                    }
                    else
                        throw new Exception("O produto selecionado não existe");
                }

                ListaAtual = listaAuxiliar;

                estruturaProduto.Produtos = ListaAtual;

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

                if (ListaAtual.Count == 0)
                    listProdutos = baseProdutos.PopularProdutos();
                else
                    listProdutos = ListaAtual;

                var listaAuxiliar = listProdutos;

                foreach (var item in listProdutos)
                {
                    if (item.idProduct == idProduto)
                        listaAuxiliar.Remove(item);
                    else
                        throw new Exception("O produto selecionado não existe");
                }


                ListaAtual = listaAuxiliar;

                estruturaProduto.Produtos = ListaAtual;

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