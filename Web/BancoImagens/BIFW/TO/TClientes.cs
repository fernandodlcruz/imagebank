using System;
using System.Collections.Generic;
using System.Text;

namespace FWBancoImagens.TO
{
    public class TClientes
    {
        private int _id;
        private string _razaoSoc;
        private string _email;
        private string _cpfCnpj;
        private string _telComercial;
        private string _fax;
        private string _contato;
        private string _telContato;
        private string _endereco;
        private string _numero;
        private string _complemento;
        private string _estado;
        private string _cidade;
        private string _cep;
        private DateTime _dtCriacao;
        private string _userName;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string RazaoSocial
        {
            get { return _razaoSoc; }
            set { _razaoSoc = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string CpfCnpj
        {
            get { return _cpfCnpj; }
            set { _cpfCnpj = value; }
        }

        public string TelefoneComercial
        {
            get { return _telComercial; }
            set { _telComercial = value; }
        }

        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        public string Contato
        {
            get { return _contato; }
            set { _contato = value; }
        }

        public string TelefoneContato
        {
            get { return _telContato; }
            set { _telContato = value; }
        }

        public string Endereco
        {
            get { return _endereco; }
            set { _endereco = value; }
        }

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }

        public string CEP
        {
            get { return _cep; }
            set { _cep = value; }
        }

        public DateTime DataCriacao
        {
            get { return _dtCriacao; }
            set { _dtCriacao = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
    }
}
