using TDE___Projeto_de_arquitetura.Models;

namespace TDE___Projeto_de_arquitetura.Repositories
{
    public interface IClienteRepository
    {
        public List<ClienteModel> ListaClientes();
        public ClienteModel ListaCliente(int id);
        public void AdicionaCliente(ClienteModel cliente);
        public void EditaCliente(ClienteModel cliente, int id);
        public void DeletaCliente(int id);
    }
}
