using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BCategorias
    {
        private DCategorias daoCategoria;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BCategorias()
        {
            daoCategoria = DCategorias.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BCategorias instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BCategorias
        /// </summary>
        /// <returns></returns>
        public static BCategorias getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BCategorias();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Inclusão de nova categoria
        /// </summary>
        /// <returns></returns>
        public void Incluir(TCategorias dtoCategoria)
        {
            IList<TCategorias> lstCat;

            try
            {
                lstCat = daoCategoria.Pesquisar(dtoCategoria.Nome);

                if (lstCat.Count > 0)
                {
                    if ((lstCat[0] as TCategorias).Nome.ToUpper() == dtoCategoria.Nome.ToUpper())
                    {
                        throw new Exception("Tema já existe cadastrado.");
                    }
                }

                daoCategoria.Incluir(dtoCategoria);
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
        public void Alterar(TCategorias dtoCategoria)
        {
            try
            {
                daoCategoria.Alterar(dtoCategoria);
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
        public void Excluir(string id)
        {
            try
            {
                daoCategoria.Excluir(id);
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
        public IList<TCategorias> Listar()
        {
            try
            {
                return daoCategoria.Listar();
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
        public TCategorias Pesquisar(string id)
        {
            try
            {
                return daoCategoria.Pesquisar(id, "");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
