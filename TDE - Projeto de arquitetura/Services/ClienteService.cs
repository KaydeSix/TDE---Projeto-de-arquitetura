using TDE___Projeto_de_arquitetura.Models;
using TDE___Projeto_de_arquitetura.Repositories;

namespace TDE___Projeto_de_arquitetura.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public ResultadoClienteModel ListaClientes()
        {
            var cliente = _clienteRepository.ListaClientes();

            if (cliente.Count != 0)
            {
                return new ResultadoClienteModel
                {
                    Sucesso = true,
                    Mensagem = "Clientes listados com sucesso.",
                    Cliente = cliente
                };
            }
            else
            {
                return new ResultadoClienteModel
                {
                    Sucesso = false,
                    Mensagem = "Nenhum cliente cadastrado.",
                    Cliente = new List<ClienteModel>()
                };
            }
        }

        public ResultadoClienteModel ListaCliente(int id)
        {
            var cliente = _clienteRepository.ListaCliente(id);

            if (cliente != null)
            {
                return new ResultadoClienteModel
                {
                    Sucesso = true,
                    Mensagem = "Cliente listado com sucesso.",
                    Cliente = cliente
                };
            }
            else
            {
                return new ResultadoClienteModel
                {
                    Sucesso = false,
                    Mensagem = "Nenhum cliente encontrado."
                };
            };
        }

        public ResultadoClienteModel AdicionaCliente(ClienteModel cliente)
        {

            //usa o  FirstOrDefault para encontrar se o nome passaado existe na lista, se n achar define null q é padrao do comando
            var clienteExistente = _clienteRepository.ListaClientes()
                .FirstOrDefault(p => p.NomeCliente.Equals(cliente.NomeCliente, StringComparison.OrdinalIgnoreCase));

            if (clienteExistente == null)
            {
                _clienteRepository.AdicionaCliente(cliente);

                return new ResultadoClienteModel
                {
                    Sucesso = true,
                    Mensagem = "Cliente adicionado com sucesso."
                };
            }

            else
            {
                return new ResultadoClienteModel
                {
                    Sucesso = false,
                    Mensagem = "Já existe um cliente cadastrado com este nome."
                };
            }
        }

        public ResultadoClienteModel EditaCliente(ClienteModel cliente, int id)
        {
            //caso o cleinte tente enviar a requisição com o mesmo nome q esta n de erro
            var nomeCliente = cliente.NomeCliente;

            //usa o  FirstOrDefault para encontrar se o nome passaado existe na lista, se n achar define null q é padrao do comando
            var clienteExistente = _clienteRepository.ListaClientes()
                .FirstOrDefault(p => p.NomeCliente.Equals(cliente.NomeCliente, StringComparison.OrdinalIgnoreCase));

            if (clienteExistente == null || clienteExistente.IdCliente == id)
            {
                _clienteRepository.EditaCliente(cliente, id);

                return new ResultadoClienteModel
                {
                    Sucesso = true,
                    Mensagem = "Cliente editado com sucesso."
                };
            }

            else
            {
                return new ResultadoClienteModel
                {
                    Sucesso = false,
                    Mensagem = "Já existe um cliente cadastrado com este nome."
                };
            }
        }

        public void DeletaCliente(int id)
        {
            _clienteRepository.DeletaCliente(id);
        }
    }
}
