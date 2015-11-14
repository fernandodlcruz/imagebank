using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BTiposLicenca
    {
        private DTiposLicenca daoTpLicenca;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BTiposLicenca()
        {
            daoTpLicenca = DTiposLicenca.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BTiposLicenca instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BTiposLicenca
        /// </summary>
        /// <returns></returns>
        public static BTiposLicenca getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BTiposLicenca();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Inclusão de novo tipo licença
        /// </summary>
        /// <returns></returns>
        public void Incluir(TTiposLicenca dtoTpLicenca)
        {
            IList<TTiposLicenca> lstTpLic;

            try
            {
                lstTpLic = daoTpLicenca.Pesquisar(dtoTpLicenca.Nome);

                if (lstTpLic.Count > 0)
                {
                    if ((lstTpLic[0] as TTiposLicenca).Nome.ToUpper() == dtoTpLicenca.Nome.ToUpper())
                    {
                        throw new Exception("Tipo de licença já existe cadastrada.");
                    }
                }

                daoTpLicenca.Incluir(dtoTpLicenca);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Alteração de tipo de licença
        /// </summary>
        /// <returns></returns>
        public void Alterar(TTiposLicenca dtoTpLicenca)
        {
            try
            {
                daoTpLicenca.Alterar(dtoTpLicenca);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Exclusão de tipo de licença
        /// </summary>
        /// <returns></returns>
        public void Excluir(int id)
        {
            try
            {
                daoTpLicenca.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista de todos tipos de licença
        /// </summary>
        /// <returns></returns>
        public IList<TTiposLicenca> Listar()
        {
            try
            {
                return daoTpLicenca.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista um tipo de licença específico
        /// </summary>
        /// <returns></returns>
        public TTiposLicenca Pesquisar(int id)
        {
            try
            {
                return daoTpLicenca.Pesquisar(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Pesquisar(string nome)
        {
            IList<TTiposLicenca> lstTpLic;
            int _ret = 0;

            try
            {
                lstTpLic = daoTpLicenca.Pesquisar(nome);

                if (lstTpLic.Count > 0)
                {
                    if ((lstTpLic[0] as TTiposLicenca).Nome.ToUpper() == nome.ToUpper())
                    {
                        _ret = (lstTpLic[0] as TTiposLicenca).Id;
                    }
                }

                return _ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
