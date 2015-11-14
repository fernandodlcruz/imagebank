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
    internal class DTiposLicenca
    {
        private string conn;

        /// <summary>
        /// Construtor private
        /// </summary>
        private DTiposLicenca()
        {
            conn = ConnectionString.StringConn;
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DTiposLicenca instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DTiposLicenca getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new DTiposLicenca();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Insere uma novo tipo licença no banco de dados
        /// </summary>
        /// <param name="dtoTpLicenca"></param>
        /// <returns></returns>
        public void Incluir(TTiposLicenca dtoTpLicenca)
        {
            string _sql;
            try
            {
                _sql = "INSERT INTO BI_TIPO_LICENCA(LIC_NOME, LIC_DESCRICAO, LIC_DT_CRIACAO) VALUES('" +
                    dtoTpLicenca.Nome + "', '" + dtoTpLicenca.Descricao + "', getdate())";

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Altera um tipo licença no banco de dados
        /// </summary>
        /// <param name="dtoTpLicenca"></param>
        /// <returns></returns>
        public void Alterar(TTiposLicenca dtoTpLicenca)
        {
            string _sql;
            try
            {
                _sql = "UPDATE BI_TIPO_LICENCA SET LIC_NOME = '" + dtoTpLicenca.Nome + "', LIC_DESCRICAO = '" + dtoTpLicenca.Descricao +
                    "', LIC_DT_CRIACAO = getdate() WHERE LIC_ID = " + dtoTpLicenca.Id;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um tipo licenca no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Excluir(int id)
        {
            string _sql;
            try
            {
                _sql = "DELETE BI_TIPO_LICENCA WHERE LIC_ID = " + id;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os tipos licença do banco de dados
        /// </summary>
        /// <param name="dtoTpLicenca"></param>
        /// <returns></returns>
        public IList<TTiposLicenca> Listar()
        {
            string _sql;
            SqlDataReader drTpLic;
            IList<TTiposLicenca> lstTpLic;
            TTiposLicenca dtoTpLic;

            try
            {
                _sql = "SELECT LIC_ID, LIC_NOME, LIC_DESCRICAO, LIC_DT_CRIACAO FROM BI_TIPO_LICENCA ORDER BY LIC_NOME";

                drTpLic = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstTpLic = new List<TTiposLicenca>();                

                while (drTpLic.Read())
                {
                    dtoTpLic = new TTiposLicenca();

                    dtoTpLic.Id = drTpLic.GetInt32(0);
                    dtoTpLic.Nome = drTpLic.GetString(1);
                    dtoTpLic.Descricao = drTpLic.GetString(2);
                    dtoTpLic.DataCriacao = drTpLic.GetDateTime(3);

                    lstTpLic.Add(dtoTpLic);
                }

                return lstTpLic;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista uma categoria do banco de dados
        /// </summary>
        /// <param name="dtoTpLicenca"></param>
        /// <returns></returns>
        public IList<TTiposLicenca> Pesquisar(string nome)
        {
            string _sql;
            SqlDataReader drTpLic;
            IList<TTiposLicenca> lstTpLic;
            TTiposLicenca dtoTpLic;

            try
            {
                _sql = "SELECT TOP 1 LIC_ID, LIC_NOME, LIC_DESCRICAO, LIC_DT_CRIACAO FROM BI_TIPO_LICENCA WHERE UPPER(LIC_NOME) = UPPER('" + nome + "')";

                drTpLic = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstTpLic = new List<TTiposLicenca>();
                dtoTpLic = new TTiposLicenca();

                if (drTpLic.Read())
                {
                    dtoTpLic.Id = drTpLic.GetInt32(0);
                    dtoTpLic.Nome = drTpLic.GetString(1);
                    dtoTpLic.Descricao = drTpLic.GetString(2);
                    dtoTpLic.DataCriacao = drTpLic.GetDateTime(3);

                    lstTpLic.Add(dtoTpLic);
                }

                return lstTpLic;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista uma categoria do banco de dados por ID
        /// </summary>
        /// <param name="dtoTpLicenca"></param>
        /// <returns></returns>
        public TTiposLicenca Pesquisar(int id)
        {
            string _sql;
            SqlDataReader drTpLic;
            TTiposLicenca dtoTpLic;

            try
            {
                _sql = "SELECT TOP 1 LIC_ID, LIC_NOME, LIC_DESCRICAO, LIC_DT_CRIACAO FROM BI_TIPO_LICENCA WHERE LIC_ID = " + id;

                drTpLic = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dtoTpLic = new TTiposLicenca();

                if (drTpLic.Read())
                {
                    dtoTpLic.Id = drTpLic.GetInt32(0);
                    dtoTpLic.Nome = drTpLic.GetString(1);
                    dtoTpLic.Descricao = drTpLic.GetString(2);
                    dtoTpLic.DataCriacao = drTpLic.GetDateTime(3);
                }

                return dtoTpLic;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}