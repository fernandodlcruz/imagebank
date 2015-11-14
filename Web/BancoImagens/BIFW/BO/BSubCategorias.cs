using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BSubCategorias
    {
        #region Singleton
        private DSubCategorias daoSubCategoria;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BSubCategorias()
        {
            daoSubCategoria = DSubCategorias.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BSubCategorias instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BSubCategorias
        /// </summary>
        /// <returns></returns>
        public static BSubCategorias getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BSubCategorias();
                    }
                }
            }
            return instance;
        }
        #endregion

        /// <summary>
        /// Inclusão de nova categoria
        /// </summary>
        /// <returns></returns>
        public void Incluir(TSubCategorias dtoSubCategoria)
        {
            IList<TSubCategorias> lstSubCat;

            try
            {
                lstSubCat = daoSubCategoria.Pesquisar(dtoSubCategoria.Categoria.Id, dtoSubCategoria.Nome);

                if (lstSubCat.Count > 0)
                {
                    if ((lstSubCat[0] as TSubCategorias).Nome.ToUpper() == dtoSubCategoria.Nome.ToUpper())
                    {
                        throw new Exception("Sub-categoria já existe cadastrada.");
                    }
                }

                daoSubCategoria.Incluir(dtoSubCategoria);
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
        public void Alterar(TSubCategorias dtoSubCategoria)
        {
            try
            {
                daoSubCategoria.Alterar(dtoSubCategoria);
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
                daoSubCategoria.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista de todas categorias
        /// </summary>
        /// <returns></returns>
        public IList<TSubCategorias> Listar()
        {
            try
            {
                return daoSubCategoria.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<TSubCategorias> Listar(string themesId)
        {
            try
            {
                return daoSubCategoria.Listar(themesId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista um sub-tema especifico
        /// </summary>
        /// <returns></returns>
        public TSubCategorias Pesquisar(int id)
        {
            try
            {
                return daoSubCategoria.Pesquisar(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Pesquisar(string nome)
        {
            IList<TSubCategorias> lstSubCat;
            int _ret = 0;

            try
            {
                lstSubCat = daoSubCategoria.Pesquisar("", nome);

                if (lstSubCat.Count > 0)
                {
                    if ((lstSubCat[0] as TSubCategorias).Nome.ToUpper() == nome.ToUpper())
                    {
                        _ret = (lstSubCat[0] as TSubCategorias).Id;
                    }
                }

                return _ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AssociarImagens(long imgId, int sctId)
        {
            try
            {
                daoSubCategoria.AssociarImagens(imgId, sctId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DesassociarImagens(long imgId)
        {
            try
            {
                daoSubCategoria.DesassociarImagens(imgId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}