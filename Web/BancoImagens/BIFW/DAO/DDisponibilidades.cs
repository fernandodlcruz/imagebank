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
    internal class DDisponibilidades
    {
        private string conn;

        /// <summary>
        /// Construtor private
        /// </summary>
        private DDisponibilidades()
        {
             conn = ConnectionString.StringConn;
        }
        
        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DDisponibilidades instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DDisponibilidades getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock){
                    if (instance == null)
                    {
                        instance = new DDisponibilidades();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Insere uma nova gaveta no banco de dados
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void Incluir(TDisponibilidades dto)
        {
            string _sql;
            try
            {
                _sql = "INSERT INTO BI_DISPONIBILIDADES(DIS_DESCRICAO) VALUES('" +
                    dto.Descricao + "')";

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
        public void Alterar(TDisponibilidades dto)
        {
            string _sql;
            try
            {
                _sql = "UPDATE BI_DISPONIBILIDADES SET DIS_DESCRICAO = '" + dto.Descricao + "' WHERE DIS_ID = " + dto.Id;

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
                _sql = "DELETE BI_DISPONIBILIDADES WHERE DIS_ID = " + id;

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
        public IList<TDisponibilidades> Listar()
        {
            string _sql;
            SqlDataReader dr;
            IList<TDisponibilidades> lst;
            TDisponibilidades dto;

            try
            {
                _sql = "SELECT DIS_ID, DIS_DESCRICAO FROM BI_DISPONIBILIDADES ORDER BY DIS_DESCRICAO";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TDisponibilidades>();

                while (dr.Read())
                {
                    dto = new TDisponibilidades();

                    dto.Id = dr.GetInt32(0);
                    dto.Descricao = dr.GetString(1);

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
        /// Lista uma gaveta do banco de dados pelo nome
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public IList<TDisponibilidades> Pesquisar(string descricao)
        {
            string _sql;
            SqlDataReader dr;
            IList<TDisponibilidades> lst;
            TDisponibilidades dto;

            try
            {
                _sql = "SELECT TOP 1 DIS_ID, DIS_DESCRICAO FROM BI_DISPONIBILIDADES WHERE UPPER(DIS_DESCRICAO) = UPPER('" + descricao + "')";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TDisponibilidades>();
                dto = new TDisponibilidades();

                if(dr.Read())
                {
                    dto.Id = dr.GetInt32(0);
                    dto.Descricao = dr.GetString(1);

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
        /// Lista um tema do banco de dados pelo id
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public TDisponibilidades Pesquisar(int id)
        {
            string _sql;
            SqlDataReader dr;
            TDisponibilidades dto;

            try
            {
                _sql = "SELECT TOP 1 DIS_ID, DIS_DESCRICAO FROM BI_DISPONIBILIDADES WHERE DIS_ID = " + id;

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dto = new TDisponibilidades();
                if (dr.Read())
                {
                    dto.Id = dr.GetInt32(0);
                    dto.Descricao = dr.GetString(1);
                }

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TDisponibilidades Pesquisar(Int64 imgId)
        {
            string _sql;
            SqlDataReader dr;
            TDisponibilidades dto;

            try
            {
                _sql = "SELECT TOP 1 A.DIS_ID, A.DIS_DESCRICAO FROM BI_DISPONIBILIDADES A, BI_IMG_X_DISP B WHERE A.DIS_ID = B.DIS_ID AND " +
                    "B.IMG_ID = " + imgId;

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dto = new TDisponibilidades();
                if (dr.Read())
                {
                    dto.Id = dr.GetInt32(0);
                    dto.Descricao = dr.GetString(1);
                }

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AssociarImagens(long imgId, int disId)
        {
            string _sql;

            try
            {
                _sql = "INSERT INTO BI_IMG_X_DISP(IMG_ID, DIS_ID) VALUES(" +
                    imgId + ", " + disId + ")";

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
                _sql = "DELETE BI_IMG_X_DISP WHERE IMG_ID = " + imgId;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}