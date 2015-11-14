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
    internal class DFornecedores
    {
        private string conn;

        /// <summary>
        /// Construtor private
        /// </summary>
        private DFornecedores()
        {
            conn = ConnectionString.StringConn;
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DFornecedores instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DFornecedores getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new DFornecedores();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Insere uma nova sub-categoria no banco de dados
        /// </summary>
        /// <param name="dtoFornecedor"></param>
        /// <returns></returns>
        public void Incluir(TFornecedores dtoFornecedor)
        {
            string _sql;
            try
            {
                _sql = "INSERT INTO BI_FORNECEDORES(FOR_NOME, FOR_DT_CRIACAO) VALUES('" +
                    dtoFornecedor.Nome + "', getdate())";

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
        /// <param name="dtoFornecedor"></param>
        /// <returns></returns>
        public void Alterar(TFornecedores dtoFornecedor)
        {
            string _sql;
            try
            {
                _sql = "UPDATE BI_FORNECEDORES SET FOR_NOME = '" + dtoFornecedor.Nome +
                    "', FOR_DT_CRIACAO = getdate() WHERE FOR_ID = " + dtoFornecedor.Id;

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
        /// <param name="dtoFornecedor"></param>
        /// <returns></returns>
        public void Excluir(int id)
        {
            string _sql;
            try
            {
                _sql = "DELETE BI_FORNECEDORES WHERE FOR_ID = " + id;

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
        /// <param name="dtoFornecedor"></param>
        /// <returns></returns>
        public IList<TFornecedores> Listar()
        {
            string _sql;
            SqlDataReader dr;
            IList<TFornecedores> lst;
            TFornecedores dtoFornecedor;
            
            try
            {
                _sql = "SELECT FOR_ID, FOR_NOME, FOR_DT_CRIACAO FROM BI_FORNECEDORES ORDER BY FOR_NOME";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TFornecedores>();

                while (dr.Read())
                {
                    dtoFornecedor = new TFornecedores();

                    dtoFornecedor.Id = dr.GetInt32(0);
                    dtoFornecedor.Nome = dr.GetString(1);
                    dtoFornecedor.DataCriacao = dr.GetDateTime(2);

                    lst.Add(dtoFornecedor);
                }

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
        /// <param name="dtoFornecedor"></param>
        /// <returns></returns>
        public IList<TFornecedores> Pesquisar(string nome)
        {
            string _sql;
            SqlDataReader dr;
            IList<TFornecedores> lst;
            TFornecedores dtoFornecedor;

            try
            {
                _sql = "SELECT TOP 1 FOR_ID, FOR_NOME, FOR_DT_CRIACAO FROM BI_FORNECEDORES WHERE UPPER(FOR_NOME) = UPPER('" +
                    nome + "')";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TFornecedores>();

                if (dr.Read())
                {
                    dtoFornecedor = new TFornecedores();

                    dtoFornecedor.Id = dr.GetInt32(0);
                    dtoFornecedor.Nome = dr.GetString(1);
                    dtoFornecedor.DataCriacao = dr.GetDateTime(2);

                    lst.Add(dtoFornecedor);
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
        public TFornecedores Pesquisar(int id)
        {
            string _sql;
            SqlDataReader dr;
            TFornecedores dto;

            try
            {
                _sql = "SELECT TOP 1 FOR_ID, FOR_NOME, FOR_DT_CRIACAO FROM BI_FORNECEDORES WHERE FOR_ID = " + id;

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dto = new TFornecedores();

                if (dr.Read())
                {
                    dto.Id = dr.GetInt32(0);
                    dto.Nome = dr.GetString(1);
                    dto.DataCriacao = dr.GetDateTime(2);
                }

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}