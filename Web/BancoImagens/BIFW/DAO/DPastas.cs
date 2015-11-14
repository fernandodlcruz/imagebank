using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.TO;
using FWBancoImagens.Common;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace FWBancoImagens.DAO
{
    internal class DPastas
    {
        private string conn;

        /// <summary>
        /// Construtor private
        /// </summary>
        private DPastas()
        {
             conn = ConnectionString.StringConn;
        }
        
        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DPastas instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DPastas getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock){
                    if (instance == null)
                    {
                        instance = new DPastas();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Insere uma nova pasta no banco de dados
        /// </summary>
        /// <param name="dtoPasta"></param>
        /// <returns></returns>
        public void Incluir(TPastas dtoPasta)
        {
            string _sql;
            try
            {
                _sql = "INSERT INTO BI_PASTA(PAS_DESCRICAO, GAV_ID) VALUES('" +
                    dtoPasta.Descricao + "'," + dtoPasta.Gaveta.Id + ")";

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Altera uma pasta no banco de dados
        /// </summary>
        /// <param name="dtoPasta"></param>
        /// <returns></returns>
        public void Alterar(TPastas dtoPasta)
        {
            string _sql;
            try
            {
                _sql = "UPDATE BI_PASTA SET PAS_DESCRICAO = '" + dtoPasta.Descricao + "', GAV_ID = " + dtoPasta.Gaveta.Id + " WHERE PAS_ID = " + dtoPasta.Id;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma pasta no banco de dados
        /// </summary>
        /// <param name="dtoPasta"></param>
        /// <returns></returns>
        public void Excluir(int id)
        {
            string _sql;
            try
            {
                _sql = "DELETE BI_PASTA WHERE PAS_ID = " + id;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista todas as pastas do banco de dados
        /// </summary>
        /// <param name="dtoPasta"></param>
        /// <returns></returns>
        public IList<TPastas> Listar()
        {
            string _sql;
            SqlDataReader drPas;
            IList<TPastas> lstPas;
            TPastas dtoPasta;
            TGavetas dtoGaveta;

            try
            {
                _sql = "SELECT PAS_ID, PAS_DESCRICAO, GAV_ID FROM BI_PASTA ORDER BY PAS_DESCRICAO";

                drPas = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstPas = new List<TPastas>();

                while (drPas.Read())
                {
                    dtoPasta = new TPastas();
                    dtoGaveta = new TGavetas();

                    dtoPasta.Id = drPas.GetInt32(0);
                    dtoPasta.Descricao = drPas.GetString(1);
                    dtoGaveta.Id = drPas.GetInt32(2);
                    dtoPasta.Gaveta = dtoGaveta;

                    lstPas.Add(dtoPasta);
                }

                return lstPas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista uma pasta do banco de dados pelo nome
        /// </summary>
        /// <param name="dtoPasta"></param>
        /// <returns></returns>
        public IList<TPastas> Pesquisar(string descricao)
        {
            string _sql;
            SqlDataReader drPas;
            IList<TPastas> lstPas;
            TPastas dtoPasta;
            TGavetas dtoGav;

            try
            {
                _sql = "SELECT TOP 1 PAS_ID, PAS_DESCRICAO, GAV_ID FROM BI_PASTA WHERE UPPER(PAS_DESCRICAO) = UPPER('" + descricao + "')";

                drPas = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstPas = new List<TPastas>();
                dtoPasta = new TPastas();
                dtoGav = new TGavetas();

                if (drPas.Read())
                {
                    dtoPasta.Id = drPas.GetInt32(0);
                    dtoPasta.Descricao = drPas.GetString(1);

                    dtoGav.Id = drPas.GetInt32(2);
                    dtoPasta.Gaveta = dtoGav;

                    lstPas.Add(dtoPasta);
                }

                return lstPas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista uma pasta do banco de dados pelo id
        /// </summary>
        /// <param name="dtoPasta"></param>
        /// <returns></returns>
        public TPastas Pesquisar(int id)
        {
            string _sql;
            SqlDataReader drPas;
            TPastas dtoPasta;
            TGavetas dtoGaveta;

            try
            {
                _sql = "SELECT TOP 1 PAS_ID, PAS_DESCRICAO, GAV_ID FROM BI_PASTA WHERE PAS_ID = " + id;

                drPas = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dtoPasta = new TPastas();
                if (drPas.Read())
                {                    
                    dtoGaveta = new TGavetas();

                    dtoPasta.Id = drPas.GetInt32(0);
                    dtoPasta.Descricao = drPas.GetString(1);
                    dtoGaveta.Id = drPas.GetInt32(2);
                    dtoPasta.Gaveta = dtoGaveta;
                }

                return dtoPasta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<TPastas> Listar(int gavId)
        {
            string _sql;
            SqlDataReader drPas;
            IList<TPastas> lstPas;
            TPastas dtoPasta;
            TGavetas dtoGaveta;

            try
            {
                _sql = "SELECT PAS_ID, PAS_DESCRICAO, GAV_ID FROM BI_PASTA WHERE GAV_ID = " + gavId + " ORDER BY PAS_DESCRICAO";

                drPas = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstPas = new List<TPastas>();

                while (drPas.Read())
                {
                    dtoPasta = new TPastas();
                    dtoGaveta = new TGavetas();

                    dtoPasta.Id = drPas.GetInt32(0);
                    dtoPasta.Descricao = drPas.GetString(1);
                    dtoGaveta.Id = drPas.GetInt32(2);
                    dtoPasta.Gaveta = dtoGaveta;

                    lstPas.Add(dtoPasta);
                }

                return lstPas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
