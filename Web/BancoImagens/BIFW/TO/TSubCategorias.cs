using System;
using System.Collections.Generic;
using System.Text;

namespace FWBancoImagens.TO
{
    public class TSubCategorias
    {
        private int _id;
        private string _nome;
        private DateTime _dtCriacao;
        private TCategorias _categoria;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public DateTime DataCriacao
        {
            get { return _dtCriacao; }
            set { _dtCriacao = value; }
        }

        public TCategorias  Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }
    }
}
