using Microsoft.AspNetCore.Mvc;
using TDE___Projeto_de_arquitetura.Models;
using TDE___Projeto_de_arquitetura.Services;

namespace TDE___Projeto_de_arquitetura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("ListaCliente")]
        public ActionResult<ResultadoClienteModel> ListaCliente()
        {
            try
            {
                var resultado = _clienteService.ListaClientes();

                if (resultado.Sucesso)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound(resultado);
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ResultadoClienteModel
                {
                    Sucesso = false,
                    Mensagem = "Ocorreu um erro ao processar a solicitação."
                });
            }
        }


        [HttpGet("ListaCliente/{id}")]
        public ActionResult<ResultadoClienteModel> ListaCliente(int id)
        {
            try
            {
                var resultado = _clienteService.ListaCliente(id);

                if (resultado.Sucesso)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound(resultado);
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ResultadoClienteModel
                {
                    Sucesso = false,
                    Mensagem = "Ocorreu um erro ao processar a solicitação."
                });
            }
        }



        [HttpPost("AdicionaCliente")]
        public ActionResult<ResultadoClienteModel> AdicionaCliente(ClienteModel cliente)
        {
            try
            {
                var resultado = _clienteService.AdicionaCliente(cliente);

                if (resultado.Sucesso)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultadoClienteModel
                {
                    Sucesso = false,
                    Mensagem = "Ocorreu um erro ao processar a solicitação."
                    //Mensagem = $"Erro interno ao adicionar o produto: {ex.Message}"
                });
            }
        }



        [HttpPut("EditaCliente/{id}")]
        public IActionResult EditaCliente(ClienteModel cliente, int id)
        {
            try
            {
                var resultado = _clienteService.EditaCliente(cliente, id);

                if (resultado.Sucesso)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultadoClienteModel
                {
                    Sucesso = false,
                    Mensagem = "Ocorreu um erro ao processar a solicitação."
                });
            }
        }

        [HttpDelete("DeletaCliente/{id}")]
        public ActionResult DeletaCliente(int id)
        {
            var existingCliente = _clienteService.ListaCliente(id);
            if (existingCliente == null)
            {
                return NotFound();
            }
            _clienteService.DeletaCliente(id);
            return NoContent();
        }
    }
}
