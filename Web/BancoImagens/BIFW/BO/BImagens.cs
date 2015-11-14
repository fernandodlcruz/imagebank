using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
// Imports FrameworkBancoImagens
using FWBancoImagens.DAO;
using FWBancoImagens.TO;
using FWBancoImagens.Common;
// Imports da marca d'agua
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace FWBancoImagens.BO
{
    public class BImagens
    {
        private DImagens objDAO;

        # region Singleton Metodos
        /// <summary>
        /// Construtor Private
        /// </summary>
        private BImagens()
        {
            objDAO = DImagens.getInstance();
        }

        /// <summary>
        /// Variáveis státicas 
        /// </summary>
        private static BImagens instance = null;
        private static object objLock = new object();

        /// <summary>
        /// Método Singleton que retorna uma única instância da classe
        /// BIMAGENS
        /// </summary>
        /// <returns></returns>
        public static BImagens getInstance()
        {
            if (instance == null)
            {
                // Loca o objeto garantindo que somente uma intância será criada
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new BImagens();
                    }
                }
            }
            return instance;
        }
        #endregion

        #region Demais metodos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoImag"></param>
        /// <returns></returns>
        public Int64 InsereImagem(TImagens dtoImag) {
            try
            {
                return (Int64)objDAO.InsereImagem(dtoImag);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Excluir(Int64 id)
        {
            try
            {
                objDAO.Excluir(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Alterar(TImagens dto)
        {
            try
            {
                objDAO.Alterar(dto);

                BPalavraChave objBOPC = BPalavraChave.getInstance();

                objBOPC.DesassociarPalavrasChave(dto.Id);
                objBOPC.InserePalavraChave(dto.Id, dto.PalavrasChave);

                objBOPC = null;

                //SUB TEMAS
                BSubCategorias objBOSubTema = BSubCategorias.getInstance();

                objBOSubTema.DesassociarImagens(dto.Id);
                objBOSubTema.AssociarImagens(dto.Id, dto.Subtema.Id);

                objBOSubTema = null;

                //FORMATOS
                BDisponibilidades objBODisp = BDisponibilidades.getInstance();

                objBODisp.DesassociarImagens(dto.Id);
                objBODisp.AssociarImagens(dto.Id, dto.Disponibilidade.Id);

                objBODisp = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void LoadImages(string caminhoExcel, string caminhoBnk)
        {
            DataSet ds = new DataSet();
            TImagens dto;
            TPastas dtoPasta;
            TDisponibilidades dtoDisponib;
            TTiposLicenca dtoTpLicenca;
            TFornecedores dtoFornecedor;
            TSubCategorias dtoSubTema;
            IList<TImagens> lst;

            BDisponibilidades objBODisp = BDisponibilidades.getInstance();
            BTiposLicenca objBOTpLicenca = BTiposLicenca.getInstance();
            BFornecedores objBOForn = BFornecedores.getInstance();
            BSubCategorias objBOSubCat = BSubCategorias.getInstance();
            BPalavraChave objBOPChave = BPalavraChave.getInstance();
            BPastas objPasta = BPastas.getInstance();

            try
            {
                ds = Util.AbreExcel(caminhoExcel, Util.GetParameterValue("NOME_ARQUIVO_EXCEL"), "SELECT * FROM [Imagens$]");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst = objDAO.Pesquisar(Convert.ToString(dr["NUMERO_IMG"]));

                    if (lst.Count <= 0)
                    {
                        if (Convert.ToString(dr["NUMERO_IMG"]) == "")
                        {
                            break;
                        }

                        dto = new TImagens();
                        dtoPasta = new TPastas();
                        dtoDisponib = new TDisponibilidades();
                        dtoTpLicenca = new TTiposLicenca();
                        dtoFornecedor = new TFornecedores();
                        dtoSubTema = new TSubCategorias();

                        dto.Codigo = Convert.ToString(dr["NUMERO_IMG"]);
                        dto.Titulo = Convert.ToString(dr["TITULO"]);
                        dto.Detalhes = Convert.ToString(dr["DETALHES"]);
                        dtoPasta.Id = objPasta.Pesquisar(Convert.ToString(dr["PASTA"]));
                        dto.Orientacao = Convert.ToString(dr["ORIENTACAO"]);
                        dtoDisponib.Id = objBODisp.Pesquisar(Convert.ToString(dr["FORMATO"]));
                        dtoTpLicenca.Id = objBOTpLicenca.Pesquisar(Convert.ToString(dr["DIREITO"]));
                        dto.AUI = Convert.ToString(dr["AUI"]);
                        dtoFornecedor.Id = objBOForn.Pesquisar(Convert.ToString(dr["FTG_N"]));
                        dtoSubTema.Id = objBOSubCat.Pesquisar(Convert.ToString(dr["SUB_TEMA"]));

                        dto.Pasta = dtoPasta;
                        dto.Disponibilidade = dtoDisponib;
                        dto.TipoLicenca = dtoTpLicenca;
                        dto.Fornecedor = dtoFornecedor;
                        dto.PalavrasChave = Util.RetornaListaPalavras(Convert.ToString(dr["CHAVES"]));

                        Int64 _ret = InsereImagem(dto);

                        objBOPChave.InserePalavraChave(_ret, dto.PalavrasChave);

                        objBOSubCat.AssociarImagens(_ret, dtoSubTema.Id);
                        objBODisp.AssociarImagens(_ret, dtoDisponib.Id);

                        CriaMarcaDagua(dto.Codigo + ".jpg", caminhoBnk, dto.Codigo + ".jpg");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ds.Dispose();
            }
        }

        public void DeleteImages(string caminhoExcel, string caminhoBnk)
        {
            string _arquivo;
            DataSet ds = new DataSet();

            try
            {
                ds = Util.AbreExcel(caminhoExcel, Util.GetParameterValue("NOME_ARQUIVO_EXCEL"), "SELECT * FROM [Imagens$]");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (Convert.ToString(dr["NUMERO_IMG"]) == "")
                    {
                        break;
                    }

                    _arquivo = caminhoBnk + Convert.ToString(dr["NUMERO_IMG"]) + ".jpg";

                    if (File.Exists(_arquivo))
                    {
                        File.Delete(_arquivo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ds.Dispose();
            }
        }

        public TImagens Pesquisar(string codigo)
        {
            IList<TImagens> lst;
            TImagens _ret = null;

            try
            {
                lst = objDAO.Pesquisar(codigo);

                if (lst.Count > 0)
                {
                    _ret = lst[0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _ret;
        }

        public TImagens Pesquisar(Int64 id)
        {
            IList<TImagens> lst;
            TImagens _ret = null;

            try
            {
                lst = objDAO.Pesquisar(id);

                if (lst.Count > 0)
                {
                    _ret = lst[0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _ret;
        }

        public DataSet Listar(string lstKeys, string licences)
        {
            try
            {
                return objDAO.Listar(lstKeys, licences);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet Listar(string lstKeys, string licences, string orientation, string themes, string subThemes, string formats)
        {
            try
            {
                return objDAO.Listar(lstKeys, licences, orientation, themes, subThemes, formats);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet Listar(string codesImgs)
        {
            try
            {
                return objDAO.Listar(codesImgs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet Listar(string codesImgs, string themes, string subThemes)
        {
            try
            {
                return objDAO.Listar(codesImgs, themes, subThemes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ListarFavoritos(Int32 clieId)
        {
            try
            {
                return objDAO.ListarFavoritos(clieId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Cria a marca d'água nas imagens que serão exibidas
        /// </summary>
        /// <param name="imgNome"></param>
        /// <param name="dirImagem"></param>
        /// <param name="nomeSaida"></param>
        public void CriaMarcaDagua(string imgNome, string dirImagem, string nomeSaida)
        {
            //define a string com a mensagem de Copyright
            string Copyright = "Copyright © " + Convert.ToString(DateTime.Now.Year) + " - Fototeca Internacional";

            //Cria um objeto imagem contendo a foto para add a marca d'agua
            Image imgPhoto = Image.FromFile(dirImagem + imgNome);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //Cria um novo bitmap do tamanho original da foto
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //Carrega o bitmap para o objeto Graphics 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //Cria a imagem contendo a marca d'agua
            Image imgWatermark = new Bitmap(dirImagem + "LogoFototeca(AM).gif");
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;

            //------------------------------------------------------------
            // Inserindo a mensagem de corpyright
            //------------------------------------------------------------

            // Reenderiza a qualidade do grafico
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
                imgPhoto,                               // Photo Image object
                new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
                0,                                      // x-coordinate of the portion of the source image to draw. 
                0,                                      // y-coordinate of the portion of the source image to draw. 
                phWidth,                                // Width of the portion of the source image to draw. 
                phHeight,                               // Height of the portion of the source image to draw. 
                GraphicsUnit.Pixel);                    // Units of measure 

            //-------------------------------------------------------
            //to maximize the size of the Copyright message we will 
            //test multiple Font sizes to determine the largest posible 
            //font we can use for the width of the Photograph
            //define an array of point sizes you would like to consider as possiblities
            //-------------------------------------------------------
            //int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            int[] sizes = new int[] { 12, 10, 8, 6, 4 };

            Font crFont = null;
            SizeF crSize = new SizeF();

            //Loop through the defined sizes checking the length of the Copyright string
            //If its length in pixles is less then the image width choose this Font size.
            for (int i = 0; i < 7; i++)
            {
                //set a Font object to Arial (i)pt, Bold
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                //Measure the Copyright string in this Font
                crSize = grPhoto.MeasureString(Copyright, crFont);

                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            //Since all photographs will have varying heights, determine a 
            //position 5% from the bottom of the image
            int yPixlesFromBottom = (int)(phHeight * .05);

            //Now that we have a point size use the Copyrights string height 
            //to determine a y-coordinate to draw the string of the photograph
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));

            //Determine its x-coordinate by calculating the center of the width of the image
            float xCenterOfImg = (phWidth / 2);

            //Define the text layout by setting the text alignment to centered
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //define a Brush which is semi trasparent black (Alpha set to 153)
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //Draw the Copyright string
            grPhoto.DrawString(Copyright,                 //string of text
                crFont,                                   //font
                semiTransBrush2,                           //Brush
                new PointF(xCenterOfImg + 1, yPosFromBottom + 1),  //Position
                StrFormat);

            //define a Brush which is semi trasparent white (Alpha set to 153)
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //Draw the Copyright string a second time to create a shadow effect
            //Make sure to move this text 1 pixel to the right and down 1 pixel
            grPhoto.DrawString(Copyright,                 //string of text
                crFont,                                   //font
                semiTransBrush,                           //Brush
                new PointF(xCenterOfImg, yPosFromBottom),  //Position
                StrFormat);                               //Text alignment
            
            //------------------------------------------------------------
            //Step #2 - Insert Watermark image
            //------------------------------------------------------------

            //Create a Bitmap based on the previously modified photograph Bitmap
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            //Load this Bitmap into a new Graphic Object
            Graphics grWatermark = Graphics.FromImage(bmWatermark);

            //To achieve a transulcent watermark we will apply (2) color 
            //manipulations by defineing a ImageAttributes object and 
            //seting (2) of its properties.
            ImageAttributes imageAttributes = new ImageAttributes();

            //The first step in manipulating the watermark image is to replace 
            //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
            //to do this we will use a Colormap and use this to define a RemapTable
            ColorMap colorMap = new ColorMap();

            //My watermark was defined with a background of 100% Green this will
            //be the color we search for and replace with transparency
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            //The second color manipulation is used to change the opacity of the 
            //watermark.  This is done by applying a 5x5 matrix that contains the 
            //coordinates for the RGBA space.  By setting the 3rd row and 3rd column 
            //to 0.3f we achive a level of opacity
            //1.0 old
            float[][] colorMatrixElements = { 
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},        
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            //For this example we will place the watermark in the upper right
            //hand corner of the photograph. offset down 10 pixels and to the 
            //left 10 pixles

            //int xPosOfWm = ((phWidth - wmWidth) - 10);
            //int yPosOfWm = 10;

            int xPosOfWm = ((phWidth - wmWidth) / 2);
            int yPosOfWm = ((phHeight - wmHeight) / 2);

            grWatermark.DrawImage(imgWatermark,
            new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight),  //Set the detination Position
                0,                  // x-coordinate of the portion of the source image to draw. 
                0,                  // y-coordinate of the portion of the source image to draw. 
                wmWidth,            // Watermark Width
                wmHeight,		    // Watermark Height
                GraphicsUnit.Pixel, // Unit of measurment
                imageAttributes);   //ImageAttributes Object

            //    new Rectangle(xPosOfWm, yPosOfWm, phWidth, phHeight),  //Set the detination Position
            //    0,                  // x-coordinate of the portion of the source image to draw. 
            //    0,                  // y-coordinate of the portion of the source image to draw. 
            //    phWidth,            // Watermark Width
            //    phHeight,		    // Watermark Height
            //    GraphicsUnit.Pixel, // Unit of measurment
            //    imageAttributes);   //ImageAttributes Object
                
            //Replace the original photgraphs bitmap with the new Bitmap
            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();

            //save new image to file system.
            imgPhoto.Save(dirImagem + "BI_" + nomeSaida, ImageFormat.Jpeg);
            imgPhoto.Dispose();
            imgWatermark.Dispose();
        }

        #endregion
    }
}
