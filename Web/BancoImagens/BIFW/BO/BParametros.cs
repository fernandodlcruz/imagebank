using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BParametros
    {
        private DParametros daoParametros;

        #region Singleton
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BParametros()
        {
            daoParametros = DParametros.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BParametros instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BParametros
        /// </summary>
        /// <returns></returns>
        public static BParametros getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BParametros();
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
        public void Incluir(TParametros dto)
        {
            IList<TParametros> lst;

            try
            {
                lst = daoParametros.Pesquisar(dto.Parametro);

                if (lst.Count > 0)
                {
                    if ((lst[0] as TParametros).Parametro.ToUpper() == dto.Parametro.ToUpper())
                    {
                        throw new Exception("Parametro já existe cadastrado.");
                    }
                }

                daoParametros.Incluir(dto);
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
        public void Alterar(TParametros dto)
        {
            try
            {
                daoParametros.Alterar(dto);
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
                daoParametros.Excluir(id);
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
        public IList<TParametros> Listar()
        {
            try
            {
                return daoParametros.Listar();
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
        public TParametros Pesquisar(int id)
        {
            try
            {
                return daoParametros.Pesquisar(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Lista um parametro especifico
        /// </summary>
        /// <returns></returns>
        public string RetornaValorParametro(string codigo)
        {
            IList<TParametros> lst;
            string _ret = "";

            try
            {
                lst = daoParametros.Pesquisar(codigo);

                if (lst.Count > 0)
                {
                    if ((lst[0] as TParametros).Parametro.ToUpper() == codigo.ToUpper())
                    {
                        _ret = (lst[0] as TParametros).Valor;
                    }
                }
                else
                {
                    throw new Exception("Parametro não cadastrado.");
                }

                return _ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Int32 ReturnUserId(string userName)
        {
            return daoParametros.ReturnClientId(userName);
        }

        public string ReturnClientEmail(string userName)
        {
            return daoParametros.ReturnClientEmail(userName);
        }

        public Int32 ReturnQuantityImages(Int32 clieId)
        {
            return daoParametros.ReturnQuantityImages(clieId);
        }
    }
}