using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BFornecedores
    {
        private DFornecedores daoFornecedor;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BFornecedores()
        {
            daoFornecedor = DFornecedores.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BFornecedores instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BFornecedores
        /// </summary>
        /// <returns></returns>
        public static BFornecedores getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BFornecedores();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Inclusão de nova categoria
        /// </summary>
        /// <returns></returns>
        public void Incluir(TFornecedores dtoFornecedor)
        {
            IList<TFornecedores> lst;

            try
            {
                lst = daoFornecedor.Pesquisar(dtoFornecedor.Nome);

                if (lst.Count > 0)
                {
                    if ((lst[0] as TFornecedores).Nome.ToUpper() == dtoFornecedor.Nome.ToUpper())
                    {
                        throw new Exception("Fornecedor já existe cadastrado.");
                    }
                }

                daoFornecedor.Incluir(dtoFornecedor);
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
        public void Alterar(TFornecedores dtoFornecedor)
        {
            try
            {
                daoFornecedor.Alterar(dtoFornecedor);
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
                daoFornecedor.Excluir(id);
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
        public IList<TFornecedores> Listar()
        {
            try
            {
                return daoFornecedor.Listar();
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
        public TFornecedores Pesquisar(int id)
        {
            try
            {
                return daoFornecedor.Pesquisar(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Pesquisar(string nome)
        {
            IList<TFornecedores> lst;
            int _ret = 0;

            try
            {
                lst = daoFornecedor.Pesquisar(nome);

                if (lst.Count > 0)
                {
                    if ((lst[0] as TFornecedores).Nome.ToUpper() == nome.ToUpper())
                    {
                        _ret = (lst[0] as TFornecedores).Id;
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