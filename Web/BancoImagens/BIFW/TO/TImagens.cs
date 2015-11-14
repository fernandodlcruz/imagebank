using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.Common;

namespace FWBancoImagens.TO
{
    public class TImagens
    {
        private Int64 _id;
        private string _codImg;
        private string _titulo;
        private string _dimensao;
        private string _detalhes;
        private DateTime _dtCriacao;
        private Enumeradores.TipoImagem _cor;
        //private Enumeradores.OrientacaoImagem _orientacao;
        private string _orientacao;
        private string _aui;

        private TTiposLicenca _tpLicenca;
        private TPastas _pasta;
        private TFornecedores _fornecedor;
        private TDisponibilidades _disponibilidades;
        private IList<TPalavrasChave> _pChaves;
        private TSubCategorias _subTema;

        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public TTiposLicenca TipoLicenca
        {
            get { return _tpLicenca; }
            set { _tpLicenca = value; }
        }

        public string Codigo
        {
            get { return _codImg; }
            set { _codImg = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string Dimensao
        {
            get { return _dimensao; }
            set { _dimensao = value; }
        }

        public string Detalhes
        {
            get { return _detalhes; }
            set { _detalhes = value; }
        }

        public DateTime DataCriacao
        {
            get { return _dtCriacao; }
            set { _dtCriacao = value; }
        }

        public TDisponibilidades Disponibilidade
        {
            get { return _disponibilidades; }
            set { _disponibilidades = value; }
        }

        public Enumeradores.TipoImagem Cor
        {
            get { return _cor; }
            set { _cor = value; }
        }

        public string Orientacao
        {
            get { return _orientacao; }
            set { _orientacao = value; }
        }

        public string AUI
        {
            get { return _aui; }
            set { _aui = value; }
        }

        public TPastas Pasta
        {
            get { return _pasta; }
            set { _pasta = value; }
        }

        public TFornecedores Fornecedor
        {
            get { return _fornecedor; }
            set { _fornecedor = value; }
        }

        public IList<TPalavrasChave> PalavrasChave
        {
            get { return _pChaves; }
            set { _pChaves = value; }
        }

        public TSubCategorias Subtema
        {
            get { return _subTema; }
            set { _subTema = value; }
        }
    }
}
