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
    internal class DParametros
    {
        private string conn;

        #region Singleton
        /// <summary>
        /// Construtor private
        /// </summary>
        private DParametros()
        {
            conn = ConnectionString.StringConn;
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DParametros instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DParametros getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new DParametros();
                    }
                }
            }
            return instance;
        }
        #endregion

        /// <summary>
        /// Insere uma nova sub-categoria no banco de dados
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void Incluir(TParametros dto)
        {
            string _sql;
            try
            {
                _sql = "INSERT INTO BI_PARAMETROS(PAR_CODIGO, PAR_VALOR) VALUES('" +
                    dto.Parametro + "', '" + dto.Valor + "')";

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Altera uma sub-categoria no banco de dados
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void Alterar(TParametros dto)
        {
            string _sql;
            try
            {
                _sql = "UPDATE BI_PARAMETROS SET PAR_CODIGO = '" + dto.Parametro +
                    "', PAR_VALOR = '" + dto.Valor + "' WHERE PAR_ID = " + dto.Id;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma categoria no banco de dados
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void Excluir(int id)
        {
            string _sql;
            try
            {
                _sql = "DELETE BI_PARAMETROS WHERE PAR_ID = " + id;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista todas as categoria do banco de dados
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public IList<TParametros> Listar()
        {
            string _sql;
            SqlDataReader dr;
            IList<TParametros> lst;
            TParametros dto;

            try
            {
                _sql = "SELECT PAR_ID, PAR_CODIGO, PAR_VALOR FROM BI_PARAMETROS ORDER BY PAR_CODIGO";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TParametros>();

                while (dr.Read())
                {
                    dto = new TParametros();

                    dto.Id = dr.GetInt32(0);
                    dto.Parametro = dr.GetString(1);
                    dto.Valor = dr.GetString(2);

                    lst.Add(dto);
                }

                dr.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Pesquisa uma sub-categoria do banco de dados
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public IList<TParametros> Pesquisar(string codigo)
        {
            string _sql;
            SqlDataReader dr;
            IList<TParametros> lst;
            TParametros dto;

            try
            {
                _sql = "SELECT TOP 1 PAR_ID, PAR_CODIGO, PAR_VALOR FROM BI_PARAMETROS WHERE UPPER(PAR_CODIGO) = UPPER('" +
                    codigo + "')";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TParametros>();

                if (dr.Read())
                {
                    dto = new TParametros();

                    dto.Id = dr.GetInt32(0);
                    dto.Parametro = dr.GetString(1);
                    dto.Valor = dr.GetString(2);

                    lst.Add(dto);
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Pesquisa uma sub-categoria do banco de dados por ID
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public TParametros Pesquisar(int id)
        {
            string _sql;
            SqlDataReader dr;
            TParametros dto;

            try
            {
                _sql = "SELECT TOP 1 PAR_ID, PAR_CODIGO, PAR_VALOR FROM BI_PARAMETROS WHERE PAR_ID = " + id;

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dto = new TParametros();

                if (dr.Read())
                {
                    dto.Id = dr.GetInt32(0);
                    dto.Parametro = dr.GetString(1);
                    dto.Valor = dr.GetString(2);
                }

                dr.Dispose();

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Int32 ReturnClientId(string userName)
        {
            string _sql;
            SqlDataReader dr;
            Int32 _ret = 0;

            try
            {
                _sql = "SELECT CLI_ID FROM BI_CLIENTE WHERE USERNAME = '" + userName + "'";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                if (dr.Read())
                {
                    _ret = dr.GetInt32(0);
                }

                dr.Dispose();

                return _ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ReturnClientEmail(string userName)
        {
            string _sql;
            SqlDataReader dr;
            string _ret = "";

            try
            {
                _sql = "SELECT EMAIL FROM ASPNET_MEMBERSHIP WHERE USERID IN(" +
                    "SELECT USERID FROM ASPNET_USERS WHERE USERNAME = '" + userName + "')";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                if (dr.Read())
                {
                    _ret = dr.GetString(0);
                }

                dr.Dispose();

                return _ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Int32 ReturnQuantityImages(Int32 clieId)
        {
            string _sql;
            SqlDataReader dr;
            Int32 _ret = 0;

            try
            {
                _sql = "SELECT COUNT(*) FROM BI_FAVORITOS WHERE CLI_ID = '" + clieId + "'";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                if (dr.Read())
                {
                    _ret = dr.GetInt32(0);
                }

                dr.Dispose();

                return _ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}