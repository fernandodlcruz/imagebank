using System;
using System.Collections.Generic;
using System.Text;

namespace FWBancoImagens.TO
{
    public class TGavetas
    {
        private int _id;
        private string _descricao;

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
    }
}
