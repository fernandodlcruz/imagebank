using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
// Framework imports
using FWBancoImagens.TO;
using FWBancoImagens.BO;

namespace FWBancoImagens.Common
{
    public static class Util
    {
        private static string _connStringExcel = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|CAMINHO||NOME_ARQUIVO|;Extended Properties='Excel 8.0;HDR=YES;Connect Timeout=0'";

        public static IList<TPalavrasChave> RetornaListaPalavras(string palavras)
        {
            IList<TPalavrasChave> iPalavraChave = new List<TPalavrasChave>();
            if (!string.IsNullOrEmpty(palavras))
            {
                string[] arr = palavras.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    TPalavrasChave dtoPalavraChave = new TPalavrasChave();
                    dtoPalavraChave.Palavrachave = arr[i].Trim();
                    iPalavraChave.Add(dtoPalavraChave);
                }
            }
            
            return iPalavraChave;
        }

        public static string GetListKeysForSearch(string palavras)
        {
            string _ret = "";

            if (!string.IsNullOrEmpty(palavras))
            {
                string[] arr = palavras.Split(',');

                for (int i = 0; i < arr.Length; i++)
                {
                    _ret += "'" + arr[i].Trim() + "',";
                }
            }

            return _ret.Substring(0, _ret.Length - 1);
        }

        public static string GetParameterValue(string codigo)
        {
            BParametros objBO = BParametros.getInstance();

            return objBO.RetornaValorParametro(codigo);
        }

        public static Int32 ReturnUserId(string userName)
        {
            BParametros objBO = BParametros.getInstance();

            return objBO.ReturnUserId(userName);
        }

        public static string ReturnClientEmail(string userName)
        {
            BParametros objBO = BParametros.getInstance();

            return objBO.ReturnClientEmail(userName);
        }

        public static Int32 ReturnQuantityImages(Int32 clieId)
        {
            BParametros objBO = BParametros.getInstance();

            return objBO.ReturnQuantityImages(clieId);
        }

        public static string Decimal2DB(decimal valor)
        {
            return valor.ToString().Replace(',', '.');
        }

        public static DataSet AbreExcel(string caminho, string arquivo, string query)
        {
            _connStringExcel = _connStringExcel.Replace("|CAMINHO|", caminho).Replace("|NOME_ARQUIVO|", arquivo);

            using (OleDbConnection _cn = new OleDbConnection(_connStringExcel))
            {
                _cn.Open();
                OleDbDataAdapter _adapter = new OleDbDataAdapter(query, _cn);
                DataSet ds = new DataSet("EXCEL");
                _adapter.Fill(ds);
                return ds;
            }
        }
    }
}