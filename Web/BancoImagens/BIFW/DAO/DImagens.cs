using System;
using System.Collections.Generic;
using System.Text;
using FWBancoImagens.TO;
using FWBancoImagens.Common;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;

namespace FWBancoImagens.DAO
{
    internal class DImagens
    {
        private string conn;

        #region Singleton
        /// <summary>
        /// Construtor private
        /// </summary>
        private DImagens()
        {
            conn = ConnectionString.StringConn;
        }
        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static DImagens instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método que retorna uma única instância da classe
        /// Singleton
        /// </summary>
        /// <returns></returns>
        public static DImagens getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock){
                    if (instance == null)
                    {
                        instance = new DImagens();
                    }
                }
            }
            return instance;
        }
        #endregion

        /// <summary>
        /// Insere uma nova imagem no banco de dados
        /// </summary>
        /// <param name="dtoImagem">Objeto com propriedades da Imagem</param>
        /// <returns></returns>
        public Int64 InsereImagem(TImagens dtoImagem) {
            try
            {
                // Declara um array de parametros
                SqlParameter[] param = new SqlParameter[] 
                {
                    // Preenche o array de parametros
                    new SqlParameter("@LIC_ID", (Int32)dtoImagem.TipoLicenca.Id),
                    new SqlParameter("@IMG_CODIGO", dtoImagem.Codigo),
                    new SqlParameter("@IMG_TITULO", dtoImagem.Titulo),
                    new SqlParameter("@IMG_DETALHES", dtoImagem.Detalhes),
                    new SqlParameter("@IMG_ORIENTACAO", dtoImagem.Orientacao),
                    new SqlParameter("@IMG_AUI", dtoImagem.AUI),
                    new SqlParameter("@PAS_ID", (Int32)dtoImagem.Pasta.Id),
                    new SqlParameter("@FOR_ID", (Int32)dtoImagem.Fornecedor.Id)
                };
                
                // Execução da procedure
                Int64 idImagem = Convert.ToInt64(SqlHelper.ExecuteScalar(conn, "PRC_INS_IMAGENS", param));

                // Retorna o Id da Imagem
                return idImagem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Excluir(Int64 id)
        {
            string _sql;
            try
            {
                _sql = "DELETE FROM BI_IMG_X_PC WHERE IMG_ID = " + id;
                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);

                _sql = "DELETE FROM BI_IMG_X_SUBCAT WHERE IMG_ID = " + id;
                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);

                _sql = "DELETE FROM BI_IMG_X_DISP WHERE IMG_ID = " + id;
                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);

                _sql = "DELETE FROM BI_FAVORITOS WHERE IMG_ID = " + id;
                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);

                _sql = "DELETE FROM BI_CD_X_IMG WHERE IMG_ID = " + id;
                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);

                _sql = "DELETE FROM BI_IMAGENS WHERE IMG_ID = " + id;
                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Alterar(TImagens dto)
        {
            string _sql;

            try
            {
                _sql = "UPDATE BI_IMAGENS " +
                    " SET LIC_ID = " + dto.TipoLicenca.Id + "," + 
                    " IMG_CODIGO = '" + dto.Codigo +  "'," + 
                    " IMG_TITULO = '" + dto.Titulo + "'," + 
                    " IMG_DETALHES = '" + dto.Detalhes + "'," + 
                    " IMG_DT_CRIACAO = getdate(), " + 
                    " IMG_ORIENTACAO = '" + dto.Orientacao + "'," + 
                    " PAS_ID = " + dto.Pasta.Id + "," +
                    " IMG_AUI = '" + dto.AUI  + "'," +
                    " FOR_ID = " + dto.Fornecedor.Id + 
                    " WHERE IMG_ID = " + dto.Id;

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, _sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<TImagens> Pesquisar(string codigo)
        {
            string _sql;
            SqlDataReader dr;
            IList<TImagens> lst;
            TImagens dto;
            TPastas dtoPasta;
            //TDisponibilidades dtoDisponib;
            TTiposLicenca dtoTpLicenca;
            TFornecedores dtoFornecedor;
            //TSubCategorias dtoSubTema;

            try
            {
                _sql = "SELECT TOP 1 IMG_ID, LIC_ID, IMG_CODIGO, IMG_TITULO, IMG_DIMENSAO, IMG_DETALHES, IMG_DT_CRIACAO  " +
                        "IMG_COR, IMG_ORIENTACAO, PAS_ID, IMG_AUI, FOR_ID FROM BI_IMAGENS WHERE UPPER(IMG_CODIGO) = UPPER('" + codigo + "')";

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TImagens>();
                dto = new TImagens();

                if (dr.Read())
                {
                    dto = new TImagens();
                    dtoPasta = new TPastas();
                    //dtoDisponib = new TDisponibilidades();
                    dtoTpLicenca = new TTiposLicenca();
                    dtoFornecedor = new TFornecedores();
                    //dtoSubTema = new TSubCategorias();

                    dto.Id = dr.GetInt64(0);
                    dto.Codigo = dr.GetString(2);
                    dto.Titulo = dr.GetString(3);
                    dto.Detalhes = dr.GetString(5);
                    dtoPasta.Id = dr.GetInt32(8);
                    dto.Orientacao = dr.GetString(7);
                    //dtoDisponib.Id = objBODisp.Pesquisar(Convert.ToString(dr["FORMATO"]));
                    dtoTpLicenca.Id = dr.GetInt32(1);
                    dto.AUI = dr.GetString(9);
                    dtoFornecedor.Id = dr.GetInt32(10);
                    //dtoSubTema.Id = objBOSubCat.Pesquisar(Convert.ToString(dr["SUB_TEMA"]));

                    dto.Pasta = dtoPasta;
                    //dto.Disponibilidade = dtoDisponib;
                    dto.TipoLicenca = dtoTpLicenca;
                    dto.Fornecedor = dtoFornecedor;
                    //dto.PalavrasChave = Util.RetornaListaPalavras(Convert.ToString(dr["CHAVES"]));

                    lst.Add(dto);
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<TImagens> Pesquisar(Int64 id)
        {
            string _sql;
            SqlDataReader dr;
            IList<TImagens> lst;
            TImagens dto;
            TPastas dtoPasta;
            TDisponibilidades dtoDisponib;
            TTiposLicenca dtoTpLicenca;
            TFornecedores dtoFornecedor;
            TSubCategorias dtoSubTema;

            DPalavraChave objDAOPC;
            DDisponibilidades objDAODisp;
            DSubCategorias objDAOSubCat;
            DPastas objDAOPasta;

            try
            {
                _sql = "SELECT TOP 1 IMG_ID, LIC_ID, IMG_CODIGO, IMG_TITULO, IMG_DIMENSAO, IMG_DETALHES, IMG_DT_CRIACAO  " +
                        "IMG_COR, IMG_ORIENTACAO, PAS_ID, IMG_AUI, FOR_ID FROM BI_IMAGENS WHERE IMG_ID = " + id;

                dr = SqlHelper.ExecuteReader(conn, CommandType.Text, _sql);

                lst = new List<TImagens>();
                dto = new TImagens();

                if (dr.Read())
                {
                    dto = new TImagens();
                    dtoPasta = new TPastas();
                    dtoDisponib = new TDisponibilidades();
                    dtoTpLicenca = new TTiposLicenca();
                    dtoFornecedor = new TFornecedores();
                    dtoSubTema = new TSubCategorias();

                    dto.Id = dr.GetInt64(0);
                    dto.Codigo = dr.GetString(2);
                    dto.Titulo = dr.GetString(3);
                    dto.Detalhes = dr.GetString(5);

                    objDAOPasta = DPastas.getInstance();
                    dto.Pasta = objDAOPasta.Pesquisar(dr.GetInt32(8));
                    objDAOPasta = null;
                    
                    dto.Orientacao = dr.GetString(7);

                    objDAODisp = DDisponibilidades.getInstance();
                    dto.Disponibilidade = objDAODisp.Pesquisar(id);
                    objDAODisp = null;

                    dtoTpLicenca.Id = dr.GetInt32(1);
                    dto.AUI = dr.GetString(9);
                    dtoFornecedor.Id = dr.GetInt32(10);

                    objDAOSubCat = DSubCategorias.getInstance();
                    dto.Subtema = objDAOSubCat.Pesquisar(id);
                    objDAOSubCat = null;

                    dto.TipoLicenca = dtoTpLicenca;
                    dto.Fornecedor = dtoFornecedor;

                    objDAOPC = DPalavraChave.getInstance();
                    dto.PalavrasChave = objDAOPC.Listar(id);
                    objDAOPC = null;

                    lst.Add(dto);
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet Listar(string lstKeys, string licences)
        {
            DataSet _ds;
            try
            {
                // Declara um array de parametros
                SqlParameter[] param = new SqlParameter[] 
                {
                    // Preenche o array de parametros
                    new SqlParameter("@PC_PALAVRAS_CHAVE", lstKeys),
                    new SqlParameter("@LICENCAS", licences),
                    new SqlParameter("@ORIENTACAO", ""),
                    new SqlParameter("@TEMAS", ""),
                    new SqlParameter("@SUB_TEMAS", ""),
                    new SqlParameter("@FORMATOS", "")
                };

                // Execução da procedure
                //Int64 idImagem = Convert.ToInt64(SqlHelper.ExecuteScalar(conn, "PRC_SEL_IMAGES", param));
                _ds = SqlHelper.ExecuteDataset(conn, "PRC_SEL_IMAGES", param);

                // Retorna o Id da Imagem
                return _ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet Listar(string lstKeys, string licences, string orientation, string themes, string subThemes, string formats)
        {
            DataSet _ds;
            try
            {
                // Declara um array de parametros
                SqlParameter[] param = new SqlParameter[] 
                {
                    // Preenche o array de parametros
                    new SqlParameter("@PC_PALAVRAS_CHAVE", lstKeys),
                    new SqlParameter("@LICENCAS", licences),
                    new SqlParameter("@ORIENTACAO", orientation),
                    new SqlParameter("@TEMAS", themes),
                    new SqlParameter("@SUB_TEMAS", subThemes),
                    new SqlParameter("@FORMATOS", formats)
                };

                // Execução da procedure
                //Int64 idImagem = Convert.ToInt64(SqlHelper.ExecuteScalar(conn, "PRC_SEL_IMAGES", param));
                _ds = SqlHelper.ExecuteDataset(conn, "PRC_SEL_IMAGES", param);

                // Retorna o Id da Imagem
                return _ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet Listar(string codesImgs)
        {
            DataSet _ds;
            string _sql;
            try
            {
                _sql = "SELECT A.*, B.LIC_NOME FROM BI_IMAGENS A, BI_TIPO_LICENCA B " + 
                    "WHERE B.LIC_ID = A.LIC_ID AND A.IMG_CODIGO IN (" + codesImgs + ")";

                _ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, _sql);

                return _ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet Listar(string codesImgs, string themes, string subThemes)
        {
            DataSet _ds;
            string _sql;
            try
            {
                _sql = "SELECT * FROM BI_IMAGENS WHERE 1=1 " ;

                if (codesImgs != "")
                {
                    _sql += "AND IMG_CODIGO IN (" + codesImgs + ") ";
                }

                if (themes != "")
                {
                    _sql += "AND IMG_ID IN (SELECT A.IMG_ID " +
                                           "FROM BI_IMG_X_SUBCAT A, BI_SUB_CATEGORIAS B " +
                                          "WHERE B.SCT_ID = A.SCT_ID " +
                                            "AND B.CAT_ID IN(" + themes + ")) ";
                }

                if (subThemes != "")
                {
                    _sql += "AND IMG_ID IN (SELECT IMG_ID " + 
									       "FROM BI_IMG_X_SUBCAT " + 
									      "WHERE SCT_ID IN(" + subThemes + "))";
                }

                _ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, _sql);

                return _ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarFavoritos(Int32 clieId)
        {
            DataSet _ds;
            string _sql;
            try
            {
                _sql = "SELECT * FROM BI_IMAGENS WHERE IMG_ID IN (SELECT IMG_ID FROM BI_FAVORITOS WHERE CLI_ID = " + clieId + ")";

                _ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, _sql);

                return _ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }   
}
