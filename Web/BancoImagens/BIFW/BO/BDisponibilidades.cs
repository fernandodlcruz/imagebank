using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BDisponibilidades
    {
        private DDisponibilidades daoDisponibilidade;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BDisponibilidades()
        {
            daoDisponibilidade = DDisponibilidades.getInstance();
        }

        /// <summary>
        /// Vari�veis st�ticas 
        /// </summary>
        private static BDisponibilidades instance = null;
        private static object objLock = new object();

        /// <summary>
        /// M�todo Singleton que retorna uma �nica inst�ncia da classe
        /// BCategorias
        /// </summary>
        /// <returns></returns>
        public static BDisponibilidades getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma int�ncia ser� criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BDisponibilidades();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Inclus�o de nova categoria
        /// </summary>
        /// <returns></returns>
        public void Incluir(TDisponibilidades dto)
        {
            IList<TDisponibilidades> lst;

            try
            {
                lst = daoDisponibilidade.Pesquisar(dto.Descricao);

                if (lst.Count > 0)
                {
                    if ((lst[0] as TDisponibilidades).Descricao.ToUpper() == dto.Descricao.ToUpper())
                    {
                        throw new Exception("Disponibilidade j� existe cadastrada.");
                    }
                }

                daoDisponibilidade.Incluir(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Altera��o de categoria
        /// </summary>
        /// <returns></returns>
        public void Alterar(TDisponibilidades dto)
        {
            try
            {
                daoDisponibilidade.Alterar(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Exclus�o de categoria
        /// </summary>
        /// <returns></returns>
        public void Excluir(int id)
        {
            try
            {
                daoDisponibilidade.Excluir(id);
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
        public IList<TDisponibilidades> Listar()
        {
            try
            {
                return daoDisponibilidade.Listar();
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
        public TDisponibilidades Pesquisar(int id)
        {
            try
            {
                return daoDisponibilidade.Pesquisar(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Pesquisar(string descricao)
        {
            IList<TDisponibilidades> lst;
            int _ret = 0;

            try
            {
                lst = daoDisponibilidade.Pesquisar(descricao);

                if (lst.Count > 0)
                {
                    if ((lst[0] as TDisponibilidades).Descricao.ToUpper() == descricao.ToUpper())
                    {
                        _ret = (lst[0] as TDisponibilidades).Id;
                    }
                }

                return _ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AssociarImagens(long imgId, int disId)
        {
            try
            {
                daoDisponibilidade.AssociarImagens(imgId, disId);
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
                daoDisponibilidade.DesassociarImagens(imgId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
