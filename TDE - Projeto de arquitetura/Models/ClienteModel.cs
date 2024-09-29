namespace TDE___Projeto_de_arquitetura.Models
{
    public class ClienteModel
    {
        public long IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string CNPJ { get; set; }
        public bool IndAtivo { get; set; }
        public long IdCategoria { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
    }
}
