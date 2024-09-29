using TDE___Projeto_de_arquitetura.Models;

namespace TDE___Projeto_de_arquitetura.Services
{
    public interface IClienteService
    {
        public ResultadoClienteModel ListaClientes();
        public ResultadoClienteModel ListaCliente(int id);
        public ResultadoClienteModel AdicionaCliente(ClienteModel cliente);
        public ResultadoClienteModel EditaCliente(ClienteModel cliente, int id);
        public void DeletaCliente(int id);
    }
}
