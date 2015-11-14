using System;
using System.Collections.Generic;
using System.Text;

namespace FWBancoImagens.TO
{
    public class TPalavrasChave
    {
        private Int64 _id;
        private string _palavraChave;
        private string _fonetica;
        private DateTime _dtCriacao;
        private List<TImagens> _imagens;

        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Palavrachave
        {
            get { return _palavraChave; }
            set { _palavraChave = value; }
        }

        public string Fonetica
        {
            get { return _fonetica; }
            set { _fonetica = value; }
        }

        public DateTime DataCriacao
        {
            get { return _dtCriacao; }
            set { _dtCriacao = value; }
        }

        public List<TImagens> Imagens
        {
            get { return _imagens; }
            set { _imagens = value; }
        }
    }
}
