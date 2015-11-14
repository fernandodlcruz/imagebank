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
    internal class DFavoritos
    {
        private string conn;

        /// <summary>
        /// Construtor private
        /// </summary>
        private DFavoritos()
        {
            conn = ConnectionString.StringConn;
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DFavoritos instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DFavoritos getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new DFavoritos();
                    }
                }
            }
            return instance;
        }

        public void Incluir(TFavoritos dto)
        {
            string _sql;

            try
            {
                _sql = "INSERT INTO BI_FAVORITOS(CLI_ID, IMG_ID, FAV_DT_CRIACAO) VALUES(" +
                    dto.Cliente.Id + ", " + dto.Imagem[0].Id + ", GETDATE())";

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Excluir(string imgIds)
        {
            string _sql;

            try
            {
                _sql = "DELETE BI_FAVORITOS WHERE IMG_ID IN (" + imgIds + ")";

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<TFavoritos> Listar(int cliId)
        {
            string _sql;
            SqlDataReader dr;
            IList<TFavoritos> lst;
            IList<TImagens> lstImg;
            TFavoritos dto;
            TClientes dtoCli;
            TImagens dtoImg;

            try
            {
                _sql = "SELECT CLI_ID, IMG_ID, FAV_DT_CRIACAO FROM BI_FAVORITOS WHERE CLI_ID = " + cliId + " ORDER BY IMG_ID";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TFavoritos>();
                lstImg = new List<TImagens>();

                dr.Read();

                dto = new TFavoritos();
                dtoCli = new TClientes();
                dtoImg = new TImagens();

                dtoCli.Id = dr.GetInt32(0);
                dtoImg.Id = dr.GetInt64(1);

                dto.DataCriacao = dr.GetDateTime(2);
                dto.Cliente = dtoCli;
                lstImg.Add(dtoImg);

                while (dr.Read())
                {
                    dtoImg = new TImagens();

                    dtoImg.Id = dr.GetInt64(1);
                    
                    lstImg.Add(dtoImg);
                }

                dto.Imagem = lstImg;
                lst.Add(dto);

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TFavoritos Pesquisar(int idClie, Int64 idImg)
        {
            string _sql;
            SqlDataReader dr;
            TFavoritos dto;
            TImagens dtoImg;
            TClientes dtoClie;
            IList<TImagens> lstImg;

            try
            {
                _sql = "SELECT TOP 1 CLI_ID, IMG_ID, FAV_DT_CRIACAO FROM BI_FAVORITOS WHERE CLI_ID = " + idClie + " AND IMG_ID = " + idImg;

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dto = new TFavoritos();
                dtoImg = new TImagens();
                dtoClie = new TClientes();
                lstImg = new List<TImagens>();

                if (dr.Read())
                {

                    dtoClie.Id = dr.GetInt32(0);
                    dtoImg.Id = dr.GetInt64(1);

                    dto.Cliente = dtoClie;
                    lstImg.Add(dtoImg);
                    dto.Imagem = lstImg;
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
