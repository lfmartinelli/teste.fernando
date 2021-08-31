using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using goByte.smartSharp;

namespace goByte.smartIOPack
{
    #region smartIO ( Manipulação de arquivos )
    /// <summary>
    /// Bibliotecas para manipulação de arquivos e diretórios
    /// </summary>
    public class smartIO
    {
        #region Delete arquivos temporários

            /// <summary>
            /// Delete all temp files in directory
            /// </summary>
            /// <param name="dir">Directory of files</param>
            /// <param name="prefixo">Init string to identify especifics files to delete. Ex: prefixo="foto" del file like "foto01_temp.jpg, foto02_temp.jpg"</param>
            /// <param name="extencao">Extention of temp files to delete</param>
            /// <returns></returns>
            public static int delFilesTemp(string dir, string prefixo, string extencao)
            {
                dir = HttpRuntime.AppDomainAppPath + "\\" + dir;

                if (!Directory.Exists(dir))
                {
                    return (0);
                }

                int fileCount = 0;

                DirectoryInfo arquivos = new DirectoryInfo(dir);

                foreach (var file in arquivos.GetFiles($"{prefixo}*"))
                {
                    if (file.FullName.Contains("_temp." + extencao))
                    {
                        file.Delete();
                        fileCount += 1;
                    }
                }

                return (fileCount);
            }

        #endregion

        #region Altera resolução de uma arquivo de imagem
        /// <summary>
        /// Resize image file by new Width and Height
        /// </summary>
        /// <param name="orignalFile">Path of original file to resize</param>
        /// <param name="destinationFile">Path to new file resized</param>
        /// <param name="width">Width in pixels</param>
        /// <param name="height">height in pixels</param>
        /// <param name="deleteOriginalFile">True or False to delete original file</param>
        /// <returns>Error message ou "" to successful operation</returns>
        public string GerarMiniatura(string orignalFile, string destinationFile, int width, int height, bool deleteOriginalFile)
        {
            try
            {
                orignalFile.Replace("//", "/");
                destinationFile.Replace("//", "/");
                Image imgOriginal = Image.FromFile(orignalFile);
                Size DimensaoFinal = AlterarTamanho(imgOriginal.Size, new Size(width, height));
                ImageFormat Formato = imgOriginal.RawFormat;
                Bitmap imgFinal = new Bitmap(DimensaoFinal.Width, DimensaoFinal.Height);
                Graphics graf = Graphics.FromImage(imgFinal);
                graf.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graf.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graf.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graf.DrawImage(imgOriginal, new System.Drawing.Rectangle(0, 0, DimensaoFinal.Width, DimensaoFinal.Height));
                imgOriginal.Dispose();
                imgFinal.Save(destinationFile, Formato);
                imgFinal.Dispose();
            }
            catch (Exception e)
            {
                return(e.Message.ToString());
            }

            if(deleteOriginalFile)
            {
                if(File.Exists(orignalFile))
                {
                    File.Delete(orignalFile);
                }
            }

            return ("");
        }

        private Size AlterarTamanho(Size Original, Size Novo)
        {

            double ProporcaoOriginal = 0.00;
            ProporcaoOriginal = (((double)Original.Height) * 100) / ((double)Original.Width) / 100;
            Size novoTamanho = new Size();
            if (ProporcaoOriginal > 1)
            {
                ProporcaoOriginal = ((Original.Width * 100) / Original.Height) / 100;
                novoTamanho.Height = Novo.Height;
                novoTamanho.Width = int.Parse((double.Parse(Novo.Width.ToString()) * ProporcaoOriginal).ToString());
            }
            else
            {
                novoTamanho.Width = Novo.Width;
                novoTamanho.Height = Convert.ToInt32(((double)Novo.Width) * ProporcaoOriginal);
            }
            return (novoTamanho);
        }

        #endregion

        #region Convert CSV to DataTable
        /// <summary>
        /// Convert CSV document to a dataTable
        /// </summary>
        /// <param name="csv">CSV URI to convert</param>
        /// <returns>DataTable with CSV document data</returns>
        public static DataTable convertCSVToDatatable(string csv)
        {
            StreamReader documento = new StreamReader(csv, System.Text.UTF8Encoding.UTF8);

            string line = "";
            string old_line = "";
            DataTable tabCSV = new DataTable();
            int rowIndex = 0;

            while (line != null)
            {
                line = documento.ReadLine();

                if ((line != null) && (line != old_line))
                {
                    var cel = line.Replace("'\n'", "").Split(';');

                    DataRow row = tabCSV.NewRow();

                    for (int i = 0; i < cel.Count(); ++i)
                    {
                        if (rowIndex == 0)
                        {
                            #region Colunas

                            tabCSV.Columns.Add(smartString.clearLatinSymbols(cel[i].ToLower().Replace(" ", "_")));

                            #endregion
                        }
                        else
                        {
                            #region Rows

                            row[i] = cel[i];

                            #endregion
                        }
                    }

                    if (rowIndex > 0)
                    {
                        tabCSV.Rows.Add(row);
                    }

                    old_line = line;
                    rowIndex += 1;
                }
            }

            return (tabCSV);
        }

        public static bool isType(string valor, string tipo)
        {
            tipo = tipo.Replace(" ", "").ToLower();

            switch (tipo)
            {
                case "string":
                    float testeString = 0;
                    if (float.TryParse(valor, out testeString))
                    {
                        return (false);
                    }
                    break;
                case "float":
                    float testeFloat = 0;
                    if (float.TryParse(valor, out testeFloat))
                    {
                        return (true);
                    }
                    break;
                case "int":
                    int testeInt = 0;
                    if (int.TryParse(valor, out testeInt))
                    {
                        return (true);
                    }
                    break;
                case "decimal":
                    decimal testeDecimal = 0;
                    if (decimal.TryParse(valor, out testeDecimal))
                    {
                        return (true);
                    }
                    break;
                case "bool":
                    bool testeBool = false;
                    if (bool.TryParse(valor, out testeBool))
                    {
                        return (true);
                    }
                    break;
                case "datetime":
                    DateTime testeDate = DateTime.Today;
                    if (DateTime.TryParse(valor, out testeDate))
                    {
                        return (true);
                    }
                    break;
            }

            return (false);
        }

        #endregion

    }
    #endregion
}
