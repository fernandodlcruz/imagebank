using System;
using System.Collections.Generic;
using System.Text;

namespace FWBancoImagens.TO
{
    public class TParametros
    {
        private int _id;
        private string _codigo;
        private string _valor;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Parametro
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
    }
}
