using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BFavoritos
    {
        private DFavoritos objDAO;
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BFavoritos()
        {
            objDAO = DFavoritos.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BFavoritos instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BCds
        /// </summary>
        /// <returns></returns>
        public static BFavoritos getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BFavoritos();
                    }
                }
            }
            return instance;
        }

        public void Incluir(TFavoritos dto)
        {
            TFavoritos dtoRet;

            try
            {
                dtoRet = objDAO.Pesquisar(dto.Cliente.Id, dto.Imagem[0].Id);

                if (dtoRet.Imagem != null)
                {
                    if (dtoRet.Imagem.Count > 0)
                    {
                        if ((dtoRet.Imagem[0] as TImagens).Id == dto.Imagem[0].Id)
                        {
                            throw new Exception("Imagem já adicionada aos favoritos.");
                        }
                    }
                }

                objDAO.Incluir(dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Excluir(string imgIds)
        {
            try
            {
                objDAO.Excluir(imgIds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*public IList<TFavoritos> Listar()
        {
            try
            {
                return objDAO.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }*/
    }
}
