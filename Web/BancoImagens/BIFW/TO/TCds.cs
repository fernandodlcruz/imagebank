using System;
using System.Collections.Generic;
using System.Text;

namespace FWBancoImagens.TO
{
    public class TCds
    {
        private int _id;
        private string _nome;
        private decimal _valor;
        private string _capa;
        private IList<TImagens> _imagens;
        private int _nroImagens;
        private string _resolucao;

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

        public decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public string Capa
        {
            get { return _capa; }
            set { _capa = value; }
        }

        public IList<TImagens> Imagens
        {
            get { return _imagens; }
            set { _imagens = value; }
        }

        public int NumeroImagens
        {
            get { return _nroImagens; }
            set { _nroImagens = value; }
        }

        public string Resolucao
        {
            get { return _resolucao; }
            set { _resolucao = value; }
        }
    }
}
