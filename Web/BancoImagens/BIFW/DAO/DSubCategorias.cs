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
    internal class DSubCategorias
    {
        private string conn;

        /// <summary>
        /// Construtor private
        /// </summary>
        private DSubCategorias()
        {
            conn = ConnectionString.StringConn;
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DSubCategorias instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DSubCategorias getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new DSubCategorias();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Insere uma nova sub-categoria no banco de dados
        /// </summary>
        /// <param name="dtoSubCategoria"></param>
        /// <returns></returns>
        public void Incluir(TSubCategorias dtoSubCategoria)
        {
            string _sql;
            try
            {
                _sql = "INSERT INTO BI_SUB_CATEGORIAS(SCT_NOME, SCT_DT_CRIACAO, CAT_ID) VALUES('" +
                    dtoSubCategoria.Nome + "', getdate(), '" + dtoSubCategoria.Categoria.Id + "')";

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
        /// <param name="dtoSubCategoria"></param>
        /// <returns></returns>
        public void Alterar(TSubCategorias dtoSubCategoria)
        {
            string _sql;
            try
            {
                _sql = "UPDATE BI_SUB_CATEGORIAS SET SCT_NOME = '" + dtoSubCategoria.Nome +
                    "', SCT_DT_CRIACAO = getdate(), CAT_ID = '" + dtoSubCategoria.Categoria.Id + "' WHERE SCT_ID = " + dtoSubCategoria.Id;

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
        /// <param name="dtoSubCategoria"></param>
        /// <returns></returns>
        public void Excluir(int id)
        {
            string _sql;
            try
            {
                _sql = "DELETE BI_SUB_CATEGORIAS WHERE SCT_ID = " + id;

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
        public IList<TSubCategorias> Listar()
        {
            string _sql;
            SqlDataReader drSubCat;
            IList<TSubCategorias> lstSubCat;
            TSubCategorias dtoSubCat;
            TCategorias dtoCat;
            
            try
            {
                _sql = "SELECT A.SCT_ID, A.SCT_NOME, A.SCT_DT_CRIACAO, A.CAT_ID, B.CAT_NOME FROM BI_SUB_CATEGORIAS A, BI_CATEGORIAS B WHERE B.CAT_ID = A.CAT_ID ORDER BY B.CAT_NOME, SCT_NOME";
                /*_sql = "SELECT        BI_CATEGORIAS.*, BI_SUB_CATEGORIAS.* " +
                          "FROM            BI_CATEGORIAS INNER JOIN " +
                         "BI_SUB_CATEGORIAS ON BI_CATEGORIAS.CAT_ID = BI_SUB_CATEGORIAS.CAT_ID";*/

                drSubCat = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstSubCat = new List<TSubCategorias>();

                while (drSubCat.Read())
                {
                    dtoSubCat = new TSubCategorias();
                    dtoCat = new TCategorias();

                    dtoSubCat.Id = drSubCat.GetInt32(0);
                    dtoSubCat.Nome = drSubCat.GetString(1);
                    dtoSubCat.DataCriacao = drSubCat.GetDateTime(2);
                    dtoCat.Id = drSubCat.GetString(3);
                    dtoCat.Nome = drSubCat.GetString(4);
                    dtoSubCat.Categoria = dtoCat;

                    lstSubCat.Add(dtoSubCat);
                }

                return lstSubCat;
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
        public IList<TSubCategorias> Pesquisar(string catId, string nome)
        {
            string _sql;
            SqlDataReader drSubCat;
            IList<TSubCategorias> lstSubCat;
            TSubCategorias dtoSubCat;
            TCategorias dtoCat;

            try
            {
                _sql = "SELECT TOP 1 SCT_ID, SCT_NOME, SCT_DT_CRIACAO, CAT_ID FROM BI_SUB_CATEGORIAS ";
                
                if (nome.Trim() != "")
                {
                    _sql += " WHERE UPPER(SCT_NOME) = UPPER('" + nome + "')";
                }

                if (catId.Trim() != "")
                {
                    if (nome.Trim() != "")
                    {
                        _sql += " AND ";
                    }
                    else
                    {
                        _sql += " WHERE ";
                    }

                    _sql +=  " CAT_ID = '" + catId + "'";
                }                

                drSubCat = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstSubCat = new List<TSubCategorias>();

                if (drSubCat.Read())
                {
                    dtoSubCat = new TSubCategorias();
                    dtoCat = new TCategorias();

                    dtoSubCat.Id = drSubCat.GetInt32(0);
                    dtoSubCat.Nome = drSubCat.GetString(1);
                    dtoSubCat.DataCriacao = drSubCat.GetDateTime(2);
                    dtoCat.Id = drSubCat.GetString(3);
                    dtoSubCat.Categoria = dtoCat;

                    lstSubCat.Add(dtoSubCat);
                }

                return lstSubCat;
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
        public TSubCategorias Pesquisar(int id)
        {
            string _sql;
            SqlDataReader drSubCat;
            TSubCategorias dtoSubCat;
            TCategorias dtoCat;

            try
            {
                _sql = "SELECT TOP 1 SCT_ID, SCT_NOME, SCT_DT_CRIACAO, CAT_ID FROM BI_SUB_CATEGORIAS WHERE SCT_ID = " + id;

                drSubCat = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dtoSubCat = new TSubCategorias();
                dtoCat = new TCategorias();

                if (drSubCat.Read())
                {
                    dtoSubCat.Id = drSubCat.GetInt32(0);
                    dtoSubCat.Nome = drSubCat.GetString(1);
                    dtoSubCat.DataCriacao = drSubCat.GetDateTime(2);
                    dtoCat.Id = drSubCat.GetString(3);
                    dtoSubCat.Categoria = dtoCat;
                }

                return dtoSubCat;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<TSubCategorias> Listar(string themesId)
        {
            string _sql;
            SqlDataReader drSubCat;
            IList<TSubCategorias> lstSubCat;
            TSubCategorias dtoSubCat;
            TCategorias dtoCat;

            try
            {
                _sql = "SELECT A.SCT_ID, A.SCT_NOME, A.SCT_DT_CRIACAO, A.CAT_ID, B.CAT_NOME FROM BI_SUB_CATEGORIAS A, BI_CATEGORIAS B " + 
                    "WHERE B.CAT_ID = A.CAT_ID AND A.CAT_ID IN (" + themesId + ") ORDER BY B.CAT_NOME, SCT_NOME";

                drSubCat = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstSubCat = new List<TSubCategorias>();

                while (drSubCat.Read())
                {
                    dtoSubCat = new TSubCategorias();
                    dtoCat = new TCategorias();

                    dtoSubCat.Id = drSubCat.GetInt32(0);
                    dtoSubCat.Nome = drSubCat.GetString(1);
                    dtoSubCat.DataCriacao = drSubCat.GetDateTime(2);
                    dtoCat.Id = drSubCat.GetString(3);
                    dtoCat.Nome = drSubCat.GetString(4);
                    dtoSubCat.Categoria = dtoCat;

                    lstSubCat.Add(dtoSubCat);
                }

                return lstSubCat;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AssociarImagens(long imgId, int sctId)
        {
            string _sql;

            try
            {
                _sql = "INSERT INTO BI_IMG_X_SUBCAT(IMG_ID, SCT_ID) VALUES(" +
                    imgId + ", " + sctId + ")";

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DesassociarImagens(long imgId)
        {
            string _sql;

            try
            {
                _sql = "DELETE BI_IMG_X_SUBCAT WHERE IMG_ID = " + imgId;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TSubCategorias Pesquisar(Int64 imgId)
        {
            string _sql;
            SqlDataReader drSubCat;
            TSubCategorias dtoSubCat;
            TCategorias dtoCat;

            try
            {
                _sql = "SELECT TOP 1 A.SCT_ID, A.SCT_NOME, A.SCT_DT_CRIACAO, A.CAT_ID FROM BI_SUB_CATEGORIAS A, BI_IMG_X_SUBCAT B " +
                    "WHERE A.SCT_ID = B.SCT_ID AND B.IMG_ID = " + imgId;

                drSubCat = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dtoSubCat = new TSubCategorias();
                dtoCat = new TCategorias();

                if (drSubCat.Read())
                {
                    dtoSubCat.Id = drSubCat.GetInt32(0);
                    dtoSubCat.Nome = drSubCat.GetString(1);
                    dtoSubCat.DataCriacao = drSubCat.GetDateTime(2);
                    dtoCat.Id = drSubCat.GetString(3);
                    dtoSubCat.Categoria = dtoCat;
                }

                return dtoSubCat;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}