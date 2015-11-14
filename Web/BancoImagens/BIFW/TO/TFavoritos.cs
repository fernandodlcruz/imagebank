using System;
using System.Collections.Generic;
using System.Text;

namespace FWBancoImagens.TO
{
    public class TFavoritos
    {
        private TClientes _cliente;
        private IList<TImagens> _imagem;
        private DateTime _dtCriacao;

        public TClientes Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        public IList<TImagens> Imagem
        {
            get { return _imagem; }
            set { _imagem = value; }
        }

        public DateTime DataCriacao
        {
            get { return _dtCriacao; }
            set { _dtCriacao = value; }
        }
    }
}
