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
    internal class DClientes
    {
        private string conn;

        #region Singleton
        /// <summary>
        /// Construtor private
        /// </summary>
        private DClientes()
        {
            conn = ConnectionString.StringConn;
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DClientes instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DClientes getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new DClientes();
                    }
                }
            }
            return instance;
        }
        #endregion

        /// <summary>
        /// Pesquisa um cliente do banco de dados
        /// </summary>
        /// <param name="dtoCategoria"></param>
        /// <returns></returns>
        public TClientes Pesquisar(string userName)
        {
            string _sql;
            SqlDataReader dr;
            TClientes dto;

            try
            {
                _sql = "SELECT TOP 1 CLI_ID,CLI_NOME_RAZAO_SOCIAL,mem.Email,CLI_CPF_CNPJ,isnull(CLI_TELEFONE_COM,'') CLI_TELEFONE_COM, " +
                    "isnull(CLI_FAX,'') CLI_FAX, isnull(CLI_CONTATO,'') CLI_CONTATO,isnull(CLI_TELEFONE_CONTATO,'') CLI_TELEFONE_CONTATO, " + 
                    "isnull(CLI_ENDERECO,'') CLI_ENDERECO,isnull(CLI_NUMERO,'') CLI_NUMERO,isnull(CLI_COMPLEMENTO,'') CLI_COMPLEMENTO, " +
                    "isnull(CLI_ESTADO,'') CLI_ESTADO,isnull(CLI_CIDADE,'') CLI_CIDADE,isnull(CLI_CEP,'') CLI_CEP, " +
                    "isnull(CLI_DT_ULT_ATLZ,'') CLI_DT_ULT_ATLZ,CLI.UserName " +
                    "FROM BI_CLIENTE CLI " + 
                    "inner join aspnet_users users on users.username = CLI.username " + 
                    "inner join aspnet_membership mem on mem.userId = users.userId " + 
                    "WHERE UPPER(CLI.UserName) = UPPER('" + userName + "')";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                dto = new TClientes();

                if (dr.Read())
                {
                    dto.Id = dr.GetInt32(0);
                    dto.RazaoSocial = dr.GetString(1);
                    dto.Email = dr.GetString(2);
                    dto.CpfCnpj = dr.GetString(3);
                    dto.TelefoneComercial = (dr.GetString(4)==null?"":dr.GetString(4));
                    dto.Fax = (dr.GetString(5) == null ? "" : dr.GetString(5));
                    dto.Contato = (dr.GetString(6) == null ? "" : dr.GetString(6));
                    dto.TelefoneContato = (dr.GetString(7) == null ? "" : dr.GetString(7));
                    dto.Endereco = (dr.GetString(8) == null ? "" : dr.GetString(8));
                    dto.Numero = (dr.GetString(9) == null ? "" : dr.GetString(9));
                    dto.Complemento = (dr.GetString(10) == null ? "" : dr.GetString(10));
                    dto.Estado = dr.GetString(11);
                    dto.Cidade = dr.GetString(12);
                    dto.CEP = dr.GetString(13);
                    dto.DataCriacao = dr.GetDateTime(14);
                    dto.UserName = dr.GetString(15);
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
