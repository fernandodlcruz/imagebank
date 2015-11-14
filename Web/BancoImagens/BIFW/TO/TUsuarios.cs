using System;
using System.Collections.Generic;
using System.Text;

namespace FWBancoImagens.TO
{
    class TUsuarios
    {
        private int _id;
        private string _login;
        private string _senha;
        private TClientes _cliente;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        public TClientes Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
    }
}
