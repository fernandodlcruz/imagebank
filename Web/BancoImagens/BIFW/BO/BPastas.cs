using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BPastas
    {
        private DPastas daoPastas;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BPastas()
        {
            daoPastas = DPastas.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BPastas instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BCategorias
        /// </summary>
        /// <returns></returns>
        public static BPastas getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BPastas();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Inclusão de nova categoria
        /// </summary>
        /// <returns></returns>
        public void Incluir(TPastas dtoPasta)
        {
            IList<TPastas> lstPas;

            try
            {
                lstPas = daoPastas.Pesquisar(dtoPasta.Descricao);

                if (lstPas.Count > 0)
                {
                    if ((lstPas[0] as TPastas).Descricao.ToUpper() == dtoPasta.Descricao.ToUpper())
                    {
                        throw new Exception("Pasta já existe cadastrada.");
                    }
                }

                daoPastas.Incluir(dtoPasta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Alteração de pasta
        /// </summary>
        /// <returns></returns>
        public void Alterar(TPastas dtoPasta)
        {
            try
            {
                daoPastas.Alterar(dtoPasta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Exclusão de pasta
        /// </summary>
        /// <returns></returns>
        public void Excluir(int id)
        {
            try
            {
                daoPastas.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista de todas pastas
        /// </summary>
        /// <returns></returns>
        public IList<TPastas> Listar()
        {
            try
            {
                return daoPastas.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista uma pasta especifica
        /// </summary>
        /// <returns></returns>
        public TPastas Pesquisar(int id)
        {
            try
            {
                return daoPastas.Pesquisar(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Pesquisar(string descricao)
        {
            IList<TPastas> lst;
            int _ret = 0;

            try
            {
                lst = daoPastas.Pesquisar(descricao);

                if (lst.Count > 0)
                {
                    if ((lst[0] as TPastas).Descricao.ToUpper() == descricao.ToUpper())
                    {
                        _ret = (lst[0] as TPastas).Id;
                    }
                }

                return _ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<TPastas> Listar(int gavId)
        {
            try
            {
                return daoPastas.Listar(gavId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
