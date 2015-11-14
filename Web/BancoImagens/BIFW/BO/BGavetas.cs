using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BGavetas
    {
        private DGavetas daoGaveta;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BGavetas()
        {
            daoGaveta = DGavetas.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BGavetas instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BCategorias
        /// </summary>
        /// <returns></returns>
        public static BGavetas getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BGavetas();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Inclusão de nova categoria
        /// </summary>
        /// <returns></returns>
        public void Incluir(TGavetas dtoGaveta)
        {
            IList<TGavetas> lstGav;

            try
            {
                lstGav = daoGaveta.Pesquisar(dtoGaveta.Descricao);

                if (lstGav.Count > 0)
                {
                    if ((lstGav[0] as TGavetas).Descricao.ToUpper() == dtoGaveta.Descricao.ToUpper())
                    {
                        throw new Exception("Gaveta já existe cadastrado.");
                    }
                }

                daoGaveta.Incluir(dtoGaveta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Alteração de categoria
        /// </summary>
        /// <returns></returns>
        public void Alterar(TGavetas dtoGaveta)
        {
            try
            {
                daoGaveta.Alterar(dtoGaveta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Exclusão de categoria
        /// </summary>
        /// <returns></returns>
        public void Excluir(int id)
        {
            try
            {
                daoGaveta.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista de todos temas
        /// </summary>
        /// <returns></returns>
        public IList<TGavetas> Listar()
        {
            try
            {
                return daoGaveta.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista um tema especifico
        /// </summary>
        /// <returns></returns>
        public TGavetas Pesquisar(int id)
        {
            try
            {
                return daoGaveta.Pesquisar(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
