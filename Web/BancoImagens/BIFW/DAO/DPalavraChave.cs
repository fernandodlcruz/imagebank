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
    internal class DPalavraChave
    {
        private string conn;

        #region Singleton
        /// <summary>
        /// Construtor private
        /// </summary>
        private DPalavraChave()
        {
            conn = ConnectionString.StringConn;
        }
        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DPalavraChave instance = null;
        private static object objLock = new object();
        //private string strConnection =  ;
        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DPalavraChave getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock){
                    if (instance == null)
                    {
                        instance = new DPalavraChave();
                    }
                }
            }
            return instance;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavra"></param>
        /// <returns></returns>
        public Int64 InserePalavraChave(string palavra) 
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] 
                {
                    // Preenche o array de parametros
                    new SqlParameter("@PC_PALAVRA_CHAVE", palavra)
                };

                Int64 idPalavraChave = Convert.ToInt64(SqlHelper.ExecuteScalar(conn, "prc_ins_palavra_chave", param));
                // Retorna o Id da palavra
                return idPalavraChave;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idImagem"></param>
        /// <param name="palavraChave"></param>
        public void InserePalavraChave(Int64 idImagem, string palavraChave)
        {
            string _sql;

            try
            {
                //_sql = "DELETE BI_IMG_X_PC WHERE IMG_ID = " + idImagem;
                //SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);

                //_sql = "DELETE BI_PALAVRA_CHAVE WHERE PC_ID = " + idImagem;
                //SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
                //TODO: Verificar se precisa deletar as palavras de uma imagem antes de inserir

                Int64 idPalavraChave = InserePalavraChave(palavraChave);
                this.InserePalavraXImagem(idImagem, idPalavraChave);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void DesassociarPalavrasChave(Int64 idImagem)
        {
            string _sql;
            
            try
            {
                _sql = "DELETE BI_IMG_X_PC WHERE IMG_ID = " + idImagem;
                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idImagem"></param>
        /// <param name="idPalavraChave"></param>
        private void InserePalavraXImagem(Int64 idImagem, Int64 idPalavraChave)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] 
                {
                    // Preenche o array de parametros
                    new SqlParameter("@IMG_ID", idImagem),
                    new SqlParameter("@PC_ID", idPalavraChave)
                };

                SqlHelper.ExecuteNonQuery(conn, "PRC_INS_IMG_X_PC", param);
                // Retorna true
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<TPalavrasChave> Listar(Int64 imgId)
        {
            string _sql;
            SqlDataReader dr;
            IList<TPalavrasChave> lst;
            TPalavrasChave dto;

            try
            {
                _sql = "SELECT A.PC_ID, A.PC_PALAVRA_CHAVE, A.PC_FONETICA, A.PC_DT_CRIACAO " +
                        "FROM BI_PALAVRA_CHAVE A, BI_IMG_X_PC B WHERE A.PC_ID = B.PC_ID AND B.IMG_ID = " + imgId;

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TPalavrasChave>();
                dto = new TPalavrasChave();

                while (dr.Read())
                {
                    dto = new TPalavrasChave();

                    dto.Id = dr.GetInt64(0);
                    dto.Palavrachave = dr.GetString(1);
                    dto.Fonetica = dr.GetString(2);
                    dto.DataCriacao = dr.GetDateTime(3);

                    lst.Add(dto);
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
