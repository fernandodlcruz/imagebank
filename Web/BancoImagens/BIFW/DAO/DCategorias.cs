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
    internal class DCategorias
    {
        private string conn;

        /// <summary>
        /// Construtor private
        /// </summary>
        private DCategorias()
        {
             conn = ConnectionString.StringConn;
        }
        
        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DCategorias instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DCategorias getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock){
                    if (instance == null)
                    {
                        instance = new DCategorias();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Insere uma nova categoria no banco de dados
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public void Incluir(TCategorias dtoCategoria)
        {
            string _sql;
            try
            {
                _sql = "INSERT INTO BI_CATEGORIAS(CAT_ID, CAT_NOME, CAT_DT_CRIACAO) VALUES('" +
                    dtoCategoria.Id.Trim().ToUpper() +"', '" + dtoCategoria.Nome + "', getdate())";

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Altera uma categoria no banco de dados
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public void Alterar(TCategorias dtoCategoria)
        {
            string _sql;
            try
            {
                _sql = "UPDATE BI_CATEGORIAS SET CAT_NOME = '" + dtoCategoria.Nome + "', CAT_DT_CRIACAO = getdate() WHERE CAT_ID = '" + dtoCategoria.Id + "'";

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
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public void Excluir(string id)
        {
            string _sql;
            try
            {
                _sql = "DELETE BI_CATEGORIAS WHERE CAT_ID = '" + id + "'";

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
        public IList<TCategorias> Listar()
        {
            string _sql;
            SqlDataReader drCat;
            IList<TCategorias> lstCat;
            TCategorias dtoCat;

            try
            {
                _sql = "SELECT CAT_ID, CAT_NOME, CAT_DT_CRIACAO FROM BI_CATEGORIAS ORDER BY CAT_ID, CAT_NOME";

                drCat = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstCat = new List<TCategorias>();
                
                while (drCat.Read())
                {
                    dtoCat = new TCategorias();

                    dtoCat.Id = drCat.GetString(0);
                    dtoCat.Nome = drCat.GetString(1);
                    dtoCat.DataCriacao = drCat.GetDateTime(2);

                    lstCat.Add(dtoCat);
                }

                return lstCat;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista um tema do banco de dados pelo nome
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public IList<TCategorias> Pesquisar(string nome)
        {
            string _sql;
            SqlDataReader drCat;
            IList<TCategorias> lstCat;
            TCategorias dtoCat;

            try
            {
                _sql = "SELECT TOP 1 CAT_ID, CAT_NOME, CAT_DT_CRIACAO FROM BI_CATEGORIAS WHERE UPPER(CAT_NOME) = UPPER('" + nome + "')";

                drCat = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lstCat = new List<TCategorias>();
                dtoCat = new TCategorias();

                if (drCat.Read())
                {
                    dtoCat.Id = drCat.GetString(0);
                    dtoCat.Nome = drCat.GetString(1);
                    dtoCat.DataCriacao = drCat.GetDateTime(2);

                    lstCat.Add(dtoCat);
                }

                return lstCat;
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
        public TCategorias Pesquisar(string id, string nome)
        {
            string _sql;
            SqlDataReader drCat;
            TCategorias dtoCat;

            try
            {
                _sql = "SELECT TOP 1 CAT_ID, CAT_NOME, CAT_DT_CRIACAO FROM BI_CATEGORIAS WHERE UPPER(CAT_ID) = UPPER('" + id + "')";

                if (nome.Trim() != "")
                {
                    _sql += " AND UPPER(CAT_NOME) = UPPER('" + nome + "')";
                }

                drCat = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dtoCat = new TCategorias();
                if (drCat.Read())
                {
                    dtoCat.Id = drCat.GetString(0);
                    dtoCat.Nome = drCat.GetString(1);
                    dtoCat.DataCriacao = drCat.GetDateTime(2);
                }

                return dtoCat;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
