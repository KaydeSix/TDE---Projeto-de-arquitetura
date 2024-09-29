using MySql.Data.MySqlClient;
using System.Data;
using TDE___Projeto_de_arquitetura.Models;

namespace TDE___Projeto_de_arquitetura.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        #region Conexão
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentException("Connection string is null or empty");
            }
        }

        private IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
        #endregion

        #region Métodos
        public List<ClienteModel> ListaClientes()
        {
            List<ClienteModel> Clientes = new List<ClienteModel>();

            using (var connection = GetConnection())
            {
                string query = @"SELECT 
                            C.ID_CLIENTE,
                            C.NOME_CLIENTE,
                            C.EMAIL_CLIENTE,
                            C.TELEFONE_CLIENTE,
                            C.CNPJ,
                            C.IND_ATIVO,
                            C.ID_CATEGORIA,
                            C.INSCRICAO_ESTADUAL,
                            C.ESTADO,
                            C.CIDADE,
                            C.BAIRRO,
                            C.LOGRADOURO,
                            C.NUMERO,
                            C.COMPLEMENTO,
                            C.CEP
                         FROM CLIENTE C
                         WHERE IND_ATIVO = 1";

                using (var reader = connection.ExecuteReader(query))
                {
                    while (reader.Read())
                    {
                        ClienteModel cliente = new ClienteModel
                        {
                            IdCliente = reader.GetInt32(reader.GetOrdinal("ID_CLIENTE")),
                            NomeCliente = reader["NOME_CLIENTE"].ToString(),
                            EmailCliente = reader["EMAIL_CLIENTE"].ToString(),
                            TelefoneCliente = reader["TELEFONE_CLIENTE"].ToString(),
                            CNPJ = reader["CNPJ"].ToString(),
                            IndAtivo = reader.GetBoolean(reader.GetOrdinal("IND_ATIVO")),
                            IdCategoria = reader.GetInt32(reader.GetOrdinal("ID_CATEGORIA")),
                            InscricaoEstadual = reader["INSCRICAO_ESTADUAL"].ToString(),
                            Estado = reader["ESTADO"].ToString(),
                            Cidade = reader["CIDADE"].ToString(),
                            Bairro = reader["BAIRRO"].ToString(),
                            Logradouro = reader["LOGRADOURO"].ToString(),
                            Numero = reader["NUMERO"].ToInt(),
                            Complemento = reader["COMPLEMENTO"].ToString(),
                            CEP = reader["CEP"].ToString()
                        };
                        Clientes.Add(cliente);
                    }
                }

                return Clientes;
            }
        }


        public ClienteModel ListaCliente(int id)
        {
            ClienteModel cliente = null;

            using (var connection = GetConnection())
            {
                string query = @"SELECT 
                            ID_CLIENTE,
                            NOME_CLIENTE,
                            EMAIL_CLIENTE,
                            TELEFONE_CLIENTE,
                            CNPJ,
                            IND_ATIVO,
                            ID_CATEGORIA,
                            INSCRICAO_ESTADUAL,
                            ESTADO,
                            CIDADE,
                            BAIRRO,
                            LOGRADOURO,
                            NUMERO,
                            COMPLEMENTO,
                            CEP
                         FROM CLIENTE
                         WHERE ID_CLIENTE = @Id";

                using (var reader = connection.ExecuteReader(query, new { Id = id }))
                {
                    while (reader.Read())
                    {
                        cliente = new ClienteModel
                        {
                            IdCliente = reader.GetInt32(reader.GetOrdinal("ID_CLIENTE")),
                            NomeCliente = reader["NOME_CLIENTE"].ToString(),
                            EmailCliente = reader["EMAIL_CLIENTE"].ToString(),
                            TelefoneCliente = reader["TELEFONE_CLIENTE"].ToString(),
                            CNPJ = reader["CNPJ"].ToString(),
                            IndAtivo = reader["IND_ATIVO"].ToBool(),
                            IdCategoria = reader.GetInt32(reader.GetOrdinal("ID_CATEGORIA")),
                            InscricaoEstadual = reader["INSCRICAO_ESTADUAL"].ToString(),
                            Estado = reader["ESTADO"].ToString(),
                            Cidade = reader["CIDADE"].ToString(),
                            Bairro = reader["BAIRRO"].ToString(),
                            Logradouro = reader["LOGRADOURO"].ToString(),
                            Numero = reader["NUMERO"].ToInt(),
                            Complemento = reader["COMPLEMENTO"].ToString(),
                            CEP = reader["CEP"].ToString()
                        };
                    }
                }
            }

            return cliente;
        }


        public void AdicionaCliente(ClienteModel cliente)
        {
            using (var connection = GetConnection())
            {
                string query = @"INSERT INTO CLIENTE (
                            NOME_CLIENTE,
                            EMAIL_CLIENTE,
                            TELEFONE_CLIENTE,
                            CNPJ,
                            IND_ATIVO,
                            ID_CATEGORIA,
                            INSCRICAO_ESTADUAL,
                            ESTADO,
                            CIDADE,
                            BAIRRO,
                            LOGRADOURO,
                            NUMERO,
                            COMPLEMENTO,
                            CEP
                         ) VALUES (
                            @NomeCliente,
                            @EmailCliente,
                            @TelefoneCliente,
                            @Cnpj,
                            @IndAtivo,
                            @IdCategoria,
                            @InscricaoEstadual,
                            @Estado,
                            @Cidade,
                            @Bairro,
                            @Logradouro,
                            @Numero,
                            @Complemento,
                            @Cep
                         )";

                connection.Execute(query, new
                {
                    NomeCliente = cliente.NomeCliente,
                    EmailCliente = cliente.EmailCliente,
                    TelefoneCliente = cliente.TelefoneCliente,
                    Cnpj = cliente.CNPJ,
                    IndAtivo = cliente.IndAtivo,
                    IdCategoria = cliente.IdCategoria,
                    InscricaoEstadual = cliente.InscricaoEstadual,
                    Estado = cliente.Estado,
                    Cidade = cliente.Cidade,
                    Bairro = cliente.Bairro,
                    Logradouro = cliente.Logradouro,
                    Numero = cliente.Numero,
                    Complemento = cliente.Complemento,
                    Cep = cliente.CEP
                });
            }
        }


        public void EditaCliente(ClienteModel cliente, int id)
        {
            using (var connection = GetConnection())
            {
                string query = @"
                            UPDATE CLIENTE
                            SET NOME_CLIENTE = @NomeCliente,
                            EMAIL_CLIENTE = @EmailCliente,
                            TELEFONE_CLIENTE = @TelefoneCliente,
                            CNPJ = @Cnpj,
                            IND_ATIVO = @IndAtivo,
                            ID_CATEGORIA = @IdCategoria,
                            INSCRICAO_ESTADUAL = @InscricaoEstadual,
                            ESTADO = @Estado,
                            CIDADE = @Cidade,
                            BAIRRO = @Bairro,
                            LOGRADOURO = @Logradouro,
                            NUMERO = @Numero,
                            COMPLEMENTO = @Complemento,
                            CEP = @Cep
                         WHERE ID_CLIENTE = @IdCliente";

                connection.Execute(query, new
                {
                    NomeCliente = cliente.NomeCliente,
                    EmailCliente = cliente.EmailCliente,
                    TelefoneCliente = cliente.TelefoneCliente,
                    Cnpj = cliente.CNPJ,
                    IndAtivo = cliente.IndAtivo,
                    IdCategoria = cliente.IdCategoria,
                    InscricaoEstadual = cliente.InscricaoEstadual,
                    Estado = cliente.Estado,
                    Cidade = cliente.Cidade,
                    Bairro = cliente.Bairro,
                    Logradouro = cliente.Logradouro,
                    Numero = cliente.Numero,
                    Complemento = cliente.Complemento,
                    Cep = cliente.CEP,
                    IdCliente = id
                });
            }
        }

        public void DeletaCliente(int id)
        {
            using (var connection = GetConnection())
            {
                string query = "UPDATE CLIENTE SET IND_ATIVO = 0 WHERE ID_CLIENTE = @Id";

                connection.Execute(query, new { Id = id });
            }
        }
        #endregion
    }
}
