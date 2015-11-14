using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;
using System.Data;
using System.Data.SqlClient;

namespace FWBancoImagens.BO
{
    public class BCds
    {
        #region Singleton
        private DCds daoCds;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BCds()
        {
            daoCds = DCds.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BCds instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BCds
        /// </summary>
        /// <returns></returns>
        public static BCds getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BCds();
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
        public void Incluir(TCds dto)
        {
            IList<TCds> lst;

            try
            {
                lst = daoCds.Pesquisar(dto.Nome);

                if (lst.Count > 0)
                {
                    if ((lst[0] as TCds).Nome.ToUpper() == dto.Nome.ToUpper())
                    {
                        throw new Exception("CD já existe cadastrado.");
                    }
                }

                Int32 idCD = daoCds.Incluir(dto);

                if (idCD != 0)
                {
                    daoCds.ExcluirAssociacao(idCD);
                    foreach (TImagens dtoImg in dto.Imagens)
                    {
                        daoCds.AssociarImagens(idCD, dtoImg.Id);
                    }                    
                }
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
        public void Alterar(TCds dto)
        {
            try
            {
                daoCds.Alterar(dto);

                if (dto.Id != 0)
                {
                    daoCds.ExcluirAssociacao(dto.Id);
                    foreach (TImagens dtoImg in dto.Imagens)
                    {
                        daoCds.AssociarImagens(dto.Id, dtoImg.Id);
                    }
                }
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
                daoCds.ExcluirAssociacao(id);
                daoCds.Excluir(id);
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
        public IList<TCds> Listar()
        {
            try
            {
                return daoCds.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet Listar(Int32 cdId)
        {
            try
            {
                return daoCds.Listar(cdId);
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
        public TCds Pesquisar(int id)
        {
            try
            {
                return daoCds.Pesquisar(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}