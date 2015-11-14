using System;
using System.Collections.Generic;
using System.Text;

namespace FWBancoImagens.TO
{
    public class TPastas
    {
        private int _id;
        private string _descricao;
        private TGavetas _gaveta;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        
        public TGavetas Gaveta
		        {
		            get { return _gaveta; }
		            set { _gaveta = value; }
        }
    }
}
