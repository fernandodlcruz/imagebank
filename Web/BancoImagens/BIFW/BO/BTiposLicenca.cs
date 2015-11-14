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
        /// Vari�veis st�ticas 
        /// </summary>
        private static BTiposLicenca instance = null;
        private static object objLock = new object();

        /// <summary>
        /// M�todo Singleton que retorna uma �nica inst�ncia da classe
        /// BTiposLicenca
        /// </summary>
        /// <returns></returns>
        public static BTiposLicenca getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma int�ncia ser� criada
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
        /// Inclus�o de novo tipo licen�a
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
                        throw new Exception("Tipo de licen�a j� existe cadastrada.");
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
        /// Altera��o de tipo de licen�a
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
        /// Exclus�o de tipo de licen�a
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
        /// Lista de todos tipos de licen�a
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
        /// Lista um tipo de licen�a espec�fico
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
