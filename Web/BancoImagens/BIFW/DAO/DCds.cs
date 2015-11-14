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
    internal class DCds
    {
        private string conn;

        #region Singleton
        /// <summary>
        /// Construtor private
        /// </summary>
        private DCds()
        {
            conn = ConnectionString.StringConn;
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DCds instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DCds getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new DCds();
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
        public Int32 Incluir(TCds dto)
        {
            string _sql;
            try
            {
                // Declara um array de parametros
                SqlParameter[] param = new SqlParameter[] 
                {
                    // Preenche o array de parametros
                    new SqlParameter("@CD_NOME", dto.Nome),
                    new SqlParameter("@CD_VALOR", Convert.ToDouble(dto.Valor)),
                    new SqlParameter("@CD_CAPA", dto.Capa),
                    new SqlParameter("@CD_NRO_IMAGENS", Convert.ToInt32(dto.NumeroImagens)),
                    new SqlParameter("@CD_RESOLUCAO", dto.Resolucao)
                };

                // Execução da procedure
                Int32 idCD = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, "PRC_INS_CDS", param));

                //_sql = "INSERT INTO BI_CDS(CD_NOME, CD_VALOR, CD_CAPA) VALUES('" +
                //    dto.Nome + "', " + Util.Decimal2DB(dto.Valor) + ", '" + dto.Capa + "')";

                //SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
                return idCD;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AssociarImagens(int cdId, long imgId)
        {
            string _sql;

            try
            {
                _sql = "INSERT INTO BI_CD_X_IMG(CD_ID, IMG_ID) VALUES(" +
                    cdId + ", " + imgId + ")";

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void ExcluirAssociacao(int cdId)
        {
            string _sql;

            try
            {
                _sql = "DELETE BI_CD_X_IMG WHERE CD_ID = " + cdId;

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
        public void Alterar(TCds dto)
        {
            string _sql;
            try
            {
                _sql = "UPDATE BI_CDS SET CD_NOME = '" + dto.Nome +
                    "', CD_VALOR = " + Util.Decimal2DB(dto.Valor) + ", CD_CAPA = '" + dto.Capa + 
                    "', CD_NRO_IMAGENS = " + dto.NumeroImagens + ", CD_RESOLUCAO = '" + dto.Resolucao + "' WHERE CD_ID = " + dto.Id;

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
                _sql = "DELETE BI_CDS WHERE CD_ID = " + id;

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
        public IList<TCds> Listar()
        {
            string _sql;
            SqlDataReader dr;
            IList<TCds> lst;
            TCds dto;
            
            try
            {
                _sql = "SELECT CD_ID, CD_NOME, CD_VALOR, CD_CAPA, CD_NRO_IMAGENS, CD_RESOLUCAO FROM BI_CDS ORDER BY CD_NOME";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TCds>();

                while (dr.Read())
                {
                    dto = new TCds();

                    dto.Id = dr.GetInt32(0);
                    dto.Nome = dr.GetString(1);
                    dto.Valor = dr.GetDecimal(2);
                    dto.Capa = dr.GetString(3);
                    dto.NumeroImagens = dr.GetInt32(4);
                    dto.Resolucao = dr.GetString(5);

                    lst.Add(dto);
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet Listar(Int32 cdId)
        {
            DataSet _ds;
            string _sql;
            try
            {
                _sql = "SELECT * FROM BI_IMAGENS WHERE IMG_ID IN (SELECT IMG_ID FROM BI_CD_X_IMG WHERE CD_ID = " + cdId + ")";

                _ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, _sql);

                return _ds;
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
        public IList<TCds> Pesquisar(string nome)
        {
            string _sql;
            SqlDataReader dr;
            IList<TCds> lst;
            TCds dto;

            try
            {
                _sql = "SELECT TOP 1 CD_ID, CD_NOME, CD_VALOR, CD_CAPA, CD_NRO_IMAGENS, CD_RESOLUCAO FROM BI_CDS WHERE UPPER(CD_NOME) = UPPER('" +
                    nome + "')";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TCds>();

                if (dr.Read())
                {
                    dto = new TCds();

                    dto.Id = dr.GetInt32(0);
                    dto.Nome = dr.GetString(1);
                    dto.Valor = dr.GetDecimal(2);
                    dto.Capa = dr.GetString(3);
                    dto.NumeroImagens = dr.GetInt32(4);
                    dto.Resolucao = dr.GetString(5);

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
        public TCds Pesquisar(int id)
        {
            string _sql;
            SqlDataReader dr;
            TCds dto;
            TImagens dtoImg;
            DImagens daoImg = DImagens.getInstance();
            IList<TImagens> lstImg;
            IList<TImagens> lstImgInc = new List<TImagens>();

            try
            {
                _sql = "SELECT TOP 1 CD_ID, CD_NOME, CD_VALOR, CD_CAPA, CD_NRO_IMAGENS, CD_RESOLUCAO FROM BI_CDS WHERE CD_ID = " + id;

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dto = new TCds();

                if (dr.Read())
                {
                    dto.Id = dr.GetInt32(0);
                    dto.Nome = dr.GetString(1);
                    dto.Valor = dr.GetDecimal(2);
                    dto.Capa = dr.GetString(3);
                    dto.NumeroImagens = dr.GetInt32(4);
                    dto.Resolucao = dr.GetString(5);
                }

                _sql = "SELECT CD_ID, IMG_ID FROM BI_CD_X_IMG WHERE CD_ID = " + id;

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                while (dr.Read())
                {
                    lstImg = daoImg.Pesquisar(dr.GetInt64(1));

                    if (lstImg.Count > 0)
                    {
                        lstImgInc.Add(lstImg[0]);
                    }
                }

                dto.Imagens = lstImgInc;

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}