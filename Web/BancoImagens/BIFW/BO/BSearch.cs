using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FWBancoImagens.DAO;
using FWBancoImagens.TO;
using FWBancoImagens.Common;

namespace FWBancoImagens.BO
{
    public class BSearch
    {
        #region Singleton
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BSearch()
        {
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BSearch instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BCds
        /// </summary>
        /// <returns></returns>
        public static BSearch getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BSearch();
                    }
                }
            }
            return instance;
        }
        #endregion

        public DataSet SimpleSearch(string keys, string licences)
        {
            BImagens objBOImg = BImagens.getInstance();

            return objBOImg.Listar(Util.GetListKeysForSearch(keys), licences);
        }

        public DataSet AdvancedSearch(string lstKeys, string licences, string orientation, string themes, string subThemes, string formats)
        {
            BImagens objBOImg = BImagens.getInstance();
            string _orientation = "";
            string _keys = "";

            if (lstKeys != "")
            {
                _keys = Util.GetListKeysForSearch(lstKeys);
            }

            if (orientation != "")
            {
                _orientation = Util.GetListKeysForSearch(orientation);
            }

            return objBOImg.Listar(_keys, licences, _orientation, themes, subThemes, formats);
        }

        public DataSet CodeSearch(string codesImgs)
        {
            BImagens objBOImg = BImagens.getInstance();

            return objBOImg.Listar(Util.GetListKeysForSearch(codesImgs));
        }

        public DataSet SearchForEdit(string codesImgs, string themes, string subThemes)
        {
            BImagens objBOImg = BImagens.getInstance();
            string _codes = "";

            if (codesImgs != "")
            {
                _codes = Util.GetListKeysForSearch(codesImgs);
            }

            return objBOImg.Listar(_codes, themes, subThemes);
        }
    }
}
