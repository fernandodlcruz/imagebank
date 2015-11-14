using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;
using System.Data;
using System.Data.SqlClient;

namespace FWBancoImagens.BO
{
    public class BClientes
    {
        #region Singleton
        private DClientes objDAO;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BClientes()
        {
            objDAO = DClientes.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BClientes instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BCds
        /// </summary>
        /// <returns></returns>
        public static BClientes getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BClientes();
                    }
                }
            }
            return instance;
        }
        #endregion

        /// <summary>
        /// Lista um cliente especifico
        /// </summary>
        /// <returns></returns>
        public TClientes Pesquisar(string userName)
        {
            try
            {
                return objDAO.Pesquisar(userName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
