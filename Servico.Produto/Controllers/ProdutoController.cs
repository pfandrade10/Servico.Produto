using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Servico.Produto.Models;

namespace Servico.Produto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment _enviornment;

        public ProdutoController(IOptions<AppSettings> appSettings, IWebHostEnvironment environment)
        {
            this._appSettings = appSettings.Value;
            _enviornment = environment;
        }

        /// <summary>
        /// Método que busca um ou mais produtos
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(int? idProduto)
        {
            try
            {

                if (idProduto.HasValue)
                {
                    //Retornar produto que contenha o id especificado

                    return Ok();
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
        public IActionResult Put([FromBody]Models.Produto produto, int idProduto)
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