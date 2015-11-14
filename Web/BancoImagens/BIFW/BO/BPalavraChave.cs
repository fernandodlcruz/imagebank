using System;
using System.Collections.Generic;
using System.Text;
// Imports FrameworkBancoImagens
using FWBancoImagens.DAO;
using FWBancoImagens.TO;

namespace FWBancoImagens.BO
{
    public class BPalavraChave
    {
        #region Singleton Metodos
        /// <summary>
        /// Construtor private
        /// </summary>
        private BPalavraChave() {}
        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BPalavraChave instance = null;
        private static object objLock = new object();
        //private string strConnection =  ;
        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static BPalavraChave getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock){
                    if (instance == null)
                    {
                        instance = new BPalavraChave();
                    }
                }
            }
            return instance;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="palavraChave"></param>
        /// <returns></returns>
        public Int64 InserePalavraChave(string palavraChave) 
        {
            try
            {
                DPalavraChave daoPalavraChave = DPalavraChave.getInstance();
                return (Int64)daoPalavraChave.InserePalavraChave(palavraChave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idImagem"></param>
        /// <param name="palavraChave"></param>
        public void InserePalavraChave(Int64 idImagem, IList<TPalavrasChave> lstPChaves)
        {
            try
            {
                DPalavraChave daoPalavraChave = DPalavraChave.getInstance();

                for (int i = 0; i < lstPChaves.Count; i++)
                {
                    daoPalavraChave.InserePalavraChave(idImagem, (lstPChaves[i] as TPalavrasChave).Palavrachave);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DesassociarPalavrasChave(Int64 idImg)
        {
            try
            {
                DPalavraChave daoPalavraChave = DPalavraChave.getInstance();
                daoPalavraChave.DesassociarPalavrasChave(idImg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
