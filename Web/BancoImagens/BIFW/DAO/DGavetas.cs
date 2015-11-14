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
    internal class DGavetas
    {
        private string conn;

        /// <summary>
        /// Construtor private
        /// </summary>
        private DGavetas()
        {
             conn = ConnectionString.StringConn;
        }
        
        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DGavetas instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DGavetas getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock){
                    if (instance == null)
                    {
                        instance = new DGavetas();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Insere uma nova gaveta no banco de dados
        /// </summary>
        /// <param name="dtoGaveta"></param>
        /// <returns></returns>
        public void Incluir(TGavetas dtoGaveta)
        {
            string _sql;
            try
            {
                _sql = "INSERT INTO BI_GAVETA(GAV_DESCRICAO) VALUES('" +
                    dtoGaveta.Descricao + "')";

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Altera uma gaveta no banco de dados
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public void Alterar(TGavetas dtoGaveta)
        {
            string _sql;
            try
            {
                _sql = "UPDATE BI_GAVETA SET GAV_DESCRICAO = '" + dtoGaveta.Descricao + "' WHERE GAV_ID = " + dtoGaveta.Id;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma gaveta no banco de dados
        /// </summary>
        /// <param name="dtoGaveta"></param>
        /// <returns></returns>
        public void Excluir(int id)
        {
            string _sql;
            try
            {
                _sql = "DELETE BI_GAVETA WHERE GAV_ID = " + id;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista todas as gavetas do banco de dados
        /// </summary>
        /// <param name="dtoGaveta"></param>
        /// <returns></returns>
        public IList<TGavetas> Listar()
        {
            string _sql;
            SqlDataReader drGav;
            IList<TGavetas> lstGav;
            TGavetas dtoGav;

            try
            {
                _sql = "SELECT GAV_ID, GAV_DESCRICAO FROM BI_GAVETA ORDER BY GAV_DESCRICAO";

                drGav = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstGav = new List<TGavetas>();

                while (drGav.Read())
                {
                    dtoGav = new TGavetas();

                    dtoGav.Id = drGav.GetInt32(0);
                    dtoGav.Descricao = drGav.GetString(1);

                    lstGav.Add(dtoGav);
                }

                return lstGav;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista uma gaveta do banco de dados pelo nome
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public IList<TGavetas> Pesquisar(string descricao)
        {
            string _sql;
            SqlDataReader drGav;
            IList<TGavetas> lstGav;
            TGavetas dtoGav;

            try
            {
                _sql = "SELECT TOP 1 GAV_ID, GAV_DESCRICAO FROM BI_GAVETA WHERE UPPER(GAV_DESCRICAO) = UPPER('" + descricao + "')";

                drGav = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstGav = new List<TGavetas>();
                dtoGav = new TGavetas();

                if (drGav.Read())
                {
                    dtoGav.Id = drGav.GetInt32(0);
                    dtoGav.Descricao = drGav.GetString(1);

                    lstGav.Add(dtoGav);
                }

                return lstGav;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista um tema do banco de dados pelo id
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public TGavetas Pesquisar(int id)
        {
            string _sql;
            SqlDataReader drGav;
            TGavetas dtoGav;

            try
            {
                _sql = "SELECT TOP 1 GAV_ID, GAV_DESCRICAO FROM BI_GAVETA WHERE GAV_ID = " + id;

                drGav = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dtoGav = new TGavetas();
                if (drGav.Read())
                {
                    dtoGav.Id = drGav.GetInt32(0);
                    dtoGav.Descricao = drGav.GetString(1);
                }

                return dtoGav;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
