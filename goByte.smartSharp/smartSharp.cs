//SmartSahrp Library v0.8.1 | (c) 2019 by goByte Solutions - Fernando Martinelli - Brazil
//Methods collection to manipulate data, strings, date and others
//Tools to integrate JavaScript and C#, promoting more integration between front-end and back-end

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.Web.UI;

namespace goByte.smartSharp
{

    #region smartStrings

    #region smartList
    /// <summary>
    /// Pacote de instruções Regex resumidas mais parecidas com o padrão do JavaScript
    /// </summary>
    public static class smartList
    {

        #region Push
        /// <summary>
        /// Find in text, all results by regex matches and add that in exiting list
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="pattern">Pattern regex</param>
        /// <param name="list">List(string)Existing list to add finded parts</param>
        /// <returns>Updated List with all parts add by regex match results'.</returns>
        public static List<string> push(string text, string pattern, List<string> listagem_de_trechos)
        {
            Regex regexPattern = new Regex(pattern);

            foreach (Match result in regexPattern.Matches(text))
            {
                listagem_de_trechos.Add(result.Value);
            }

            return (listagem_de_trechos);
        }

        #endregion

        #region listIndexOf
        /// <summary>
        /// Search parts in text by regex pattern and return a List with index of start each matches
        /// </summary>
        /// <param name="text">String with text</param>
        /// <param name="pattern">Regex pattern</param>
        /// <returns>List(int) return a List with index of start each matches.</returns>
        public static List<int> listIndexOf(string text, string pattern)
        {
            //var list = new List<string>() { "bird", "frog", "ball", "leaf" };

            //// Invoke FindAll info reach.
            //// ... FindAll is only invoked once for the entire loop.
            //foreach (string value in list.FindAll(element => element.StartsWith("b")))
            //{
            //    Console.WriteLine("FINDALL, STARTSWITH B: {0}", value);
            //}

            List<int> listagem_de_index_trechos = new List<int>();
            Regex regexPattern = new Regex(pattern);

            foreach (Match result in regexPattern.Matches(text))
            {
                int pos = text.IndexOf(result.Value);
                listagem_de_index_trechos.Add(pos);
                text = text.Substring(0, pos - 1) + text.Substring(pos + result.Value.Length);
            }

            return (listagem_de_index_trechos);
        }

        #endregion
    }

    #endregion

    #region smartString (Charcters sequencies)
    public static class smartString
    {

        #region Format text to Title case
        /// <summary>
        /// Formar text to title case. Format in capital of each first letter of sentence. Not format Pronouns.
        /// </summary>
        /// <param name="text">Characters sequence to format</param>
        /// <returns>String formated</returns>
        public static string toTitleCase(string text)
        {
            if (text == null) { return (""); }

            if (text.Length < 3) { return (text); }

            text = text.ToLower();
            text = text.Substring(0, 1).ToUpper() + text.Substring(1);
            text = text.Replace(" a", " A")
                       .Replace(" b", " B")
                       .Replace(" c", " C")
                       .Replace(" d", " D")
                       .Replace(" e", " E")
                       .Replace(" f", " F")
                       .Replace(" g", " G")
                       .Replace(" h", " H")
                       .Replace(" i", " I")
                       .Replace(" j", " J")
                       .Replace(" k", " K")
                       .Replace(" l", " L")
                       .Replace(" m", " M")
                       .Replace(" n", " N")
                       .Replace(" o", " O")
                       .Replace(" p", " P")
                       .Replace(" q", " Q")
                       .Replace(" r", " R")
                       .Replace(" s", " S")
                       .Replace(" t", " T")
                       .Replace(" u", " U")
                       .Replace(" v", " V")
                       .Replace(" x", " X")
                       .Replace(" y", " Y")
                       .Replace(" w", " W")
                       .Replace(" z", " Z");
            text = text.Replace(" De ", " de ")
                       .Replace(" Do ", " do ")
                       .Replace(" Da ", " da ")
                       .Replace(" Das ", " das ")
                       .Replace(" Dos ", " dos ")
                       .Replace(" E ", " e ")
                       .Replace(" A ", " a ")
                       .Replace(" O ", " o ")
                       .Replace(" Para ", " para ");
            return (text);
        }

        #endregion

        #region Remove text from string

        /// <summary>
        /// Remove text sequence from a string using starr and end coordinates.
        /// </summary>
        /// <param name="text">Old string</param>
        /// <param name="value">Text sequence to remove in string. The value is case sensetive</param>
        /// <param name="start">Start position in string to work. If start pos is greater than the text lenght, return ""</param>
        /// <param name="end">End position in string to work. Only search value at this position. If end pos is greater than the rest of text lenght, change to lenght of text.</param>
        /// <returns>New string without value.</returns>
        public static string removeText(string text, string value, int start, int end)
        {
            if (end == 0 || end >= text.Length) { end = text.Length - 1; }
            if (start >= text.Length) { return (""); }

            string trechoOriginal = text.Substring(start, end);
            string trechoNovo = "";
            trechoNovo = trechoOriginal.Replace(value, "");
            //int textPos = trechoOriginal.ToLower().IndexOf(value.ToLower());
            //trechoOriginal = trechoOriginal.Substring(textPos, value.Length-1);

            text = text.Replace(trechoOriginal, trechoNovo);

            return (text);
        }

        #endregion

        #region Clear latin simbols
        /// <summary>
        /// Convert latin letter with accents to same letter without accents
        /// </summary>
        /// <param name="text">Sequence caharacters to format</param>
        /// <returns></returns>
        public static string clearLatinSymbols(string text)
        {
            //texto = Regex.Replace(texto, @"[^a-zA-z0-9!@#]+", "");
            text = text.Replace("á", "a").Replace("à", "a").Replace("ã", "a").Replace("â", "a").Replace("é", "e").Replace("è", "e").Replace("ê", "e").Replace("ó", "o");
            text = text.Replace("ò", "o").Replace("õ", "o").Replace("ô", "o").Replace("í", "i").Replace("ì", "").Replace("ú", "u").Replace("ù", "u").Replace("ü", "u");
            text = text.Replace("Á", "A").Replace("À", "A").Replace("Ã", "A").Replace("Â", "A").Replace("É", "E").Replace("È", "E").Replace("Ê", "E").Replace("Ó", "O");
            text = text.Replace("Ò", "O").Replace("Õ", "O").Replace("Ô", "O").Replace("Í", "I").Replace("Ì", "I").Replace("Ú", "U").Replace("Ù", "U").Replace("Ü", "U");
            text = text.Replace("Ç", "C").Replace("ç", "c");
            text = text.Replace("'", "");
            text = text.Replace("\"", "");

            return (text);
        }

        #endregion

        #region Converte numero em formato americano
        /// <summary>
        /// Convert number to US american format
        /// </summary>
        /// <param name="number">Number to convert</param>
        /// <param name="currencySymbol">If mantain currency symbol</param>
        /// <returns>String with number in US american format</returns>
        public static string toUSANumber(string number, bool currencySymbol)
        {
            string moeda = "";
            if (number.ToLower().Contains("r$") && currencySymbol)
            { moeda = "R$"; }
            else if (number.ToLower().Contains("u$") && currencySymbol)
            { moeda = "U$"; }

            number = number.ToLower().Replace("r$", "").Replace("u$", "").Replace("$", "").Replace(" ", "");

            float numTeste = new float();

            if (!float.TryParse(number, out numTeste))
            {
                return ("err");
            }

            if (Regex.Match(number, @"(([0-9]*,[0-9]*)\.[0-9])\w+").Success)
            {
                return (moeda + number);
            }

            if (Regex.Match(number, @"(([0-9]*\.[0-9]*)\,[0-9])\w+").Success)
            {
                int virgula = number.LastIndexOf(",");
                number = number.Substring(0, virgula - 1).Replace(".", ",") + "." + number.Substring(virgula + 1);
                return (moeda + number);
            }

            if (number.Contains("."))
            {
                return (moeda + number.Replace(".", ","));
            }

            if (number.Contains(","))
            {
                return (moeda + number.Replace(",", "."));
            }

            return (moeda + number);
        }

        #endregion

        #region Converte numero em formato europeu
        /// <summary>
        /// Convert number to European format
        /// </summary>
        /// <param name="number">Number to convert</param>
        /// <param name="currencySymbol">If mantain currency symbol</param>
        /// <returns>String with number in European format</returns>
        public static string toEuropeNumber(string number, bool currencySymbol)
        {
            string moeda = "";
            if (number.ToLower().Contains("r$") && currencySymbol)
            { moeda = "R$"; }
            else if (number.ToLower().Contains("u$") && currencySymbol)
            { moeda = "U$"; }

            number = number.ToLower().Replace("r$", "").Replace("u$", "").Replace("$", "").Replace(" ", "");

            float numTeste = new float();

            if (!float.TryParse(number, out numTeste))
            {
                return ("err");
            }

            if (Regex.Match(number, @"(([0-9]*\.[0-9]*)\,[0-9])\w+").Success)
            {
                return (moeda + number);
            }

            if (Regex.Match(number, @"(([0-9]*\,[0-9]*)\.[0-9])\w+").Success)
            {
                int ponto = number.LastIndexOf(".");
                number = number.Substring(0, ponto - 1).Replace(",", ".") + "," + number.Substring(ponto + 1);
                return (moeda + number);
            }

            if (number.Contains("."))
            {
                return (moeda + number.Replace(".", ","));
            }

            if (number.Contains(","))
            {
                return (moeda + number.Replace(",", "."));
            }

            return (moeda + number);
        }

        #endregion

        #region Valida Data
        /// <summary>
        /// If value is a date
        /// </summary>
        /// <param name="data">Value to verify</param>
        /// <returns>True or False about value is date or not</returns>
        public static bool isDate(string data)
        {
            DateTime teste = new DateTime();

            return (DateTime.TryParse(data, out teste));
        }

        #endregion

        #region Valida CPF
        /// <summary>
        /// If value is one CPF(Brazilian citzen indetify. Like US social security) number
        /// </summary>
        /// <param name="cpf">Value to verify</param>
        /// <returns>true or false about value is CPF or not</returns>
        public static bool isCPF(string cpf)
        {
            cpf = cpf.Replace(",", ".");
            if (cpf.Replace("_", "").Replace("-", "").Replace(".", "").Replace("/", "") == "") { return (true); }
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11) { return false; }
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }
            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        #endregion

        #region Valida Telefone
        /// <summary>
        /// If value is a phone number
        /// </summary>
        /// <param name="vFone">Value to verify</param>
        /// <returns>true or false about value is fone number</returns>
        public static bool isFone(string vFone)
        {
            if ((vFone == "") || (vFone.Replace("_", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "") == "")) { return (true); }

            if (vFone.Length < 13) { return (false); }
            string tel = "";
            tel = vFone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            long valornumerico = 0;
            bool retorno = false;
            retorno = long.TryParse(tel, out valornumerico);
            return (retorno);
        }

        #endregion

        #region Valida E-Mail
        /// <summary>
        /// If value is a e-mail
        /// </summary>
        /// <param name="vEmail">Value to verify</param>
        /// <returns>true or false about value is e-mail</returns> 

        public static bool isEMail(string vEmail)
        {
            if (vEmail == "") { return (true); }

            if (vEmail.Length < 8) { return (false); }
            if (vEmail.Substring(vEmail.Length - 4, 1) != ".")
            {
                if (vEmail.Substring(vEmail.Length - 3, 1) != ".") { return (false); }
            }
            if (vEmail.Contains("@")) { return (true); } else { return (false); }
        }

        #endregion

        #region Valida CEP
        /// <summary>
        /// If value is a Brazilian CEP(postal code)
        /// </summary>
        /// <param name="value">Value to verify</param>
        /// <returns>true or false about value is CEP</returns>
        public static bool isCEP(string value)
        {
            if ((value == "") || (value.Replace("_", "").Replace("-", "") == "")) { return (true); }

            if (value.Substring(5, 1) != "-") { return (true); }

            if (value.Length < 8) { return (false); }
            value = value.Replace("-", "");
            int numero = 0;
            if (int.TryParse(value, out numero)) { return (true); } else { return (false); }
        }

        #endregion

        #region Valida CNPJ
        /// <summary>
        /// If value is a CNPJ(Brazilian company identify number) 
        /// </summary>
        /// <param name="vCNPJ">Value to verify</param>
        /// <returns>true or false about value is a CNPJ</returns>
        public static bool isCNPJ(string vCNPJ)
        {
            vCNPJ = vCNPJ.Replace(",", ".");
            if ((vCNPJ == "") || (vCNPJ.Replace("_", "").Replace("/", "").Replace("-", "").Replace(".", "") == "")) { return (true); }
            string CNPJ = vCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");
            if (CNPJ.Length != 14)
            { return (false); }
            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;
            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;
            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                    { soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1))); }
                    if (nrDig <= 12)
                    { soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1))); }
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                    {
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                    }
                    else
                    {
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                    }
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Valores por extenso

        public static string utilExtenso_Valor(string vValor)
        {
            decimal pdbl_Valor = decimal.Parse(vValor.Replace("R$", "").Trim());
            string strValorExtenso = ""; //Variável que irá armazenar o valor por extenso do número informado
            string strNumero = "";       //Irá armazenar o número para exibir por extenso
            string strCentena = "";
            string strDezena = "";
            //string strUnidade = "";

            decimal dblCentavos = 0;
            decimal dblValorInteiro = 0;
            int intContador = 0;
            bool bln_Bilhao = false;
            bool bln_Milhao = false;
            bool bln_Mil = false;
            //bool bln_Real = false;
            bool bln_Unidade = false;

            //Verificar se foi informado um dado indevido
            if (pdbl_Valor == 0 || pdbl_Valor <= 0)
            {
                strValorExtenso = "Verificar se há valor negativo ou nada foi informado";
            }
            if (pdbl_Valor > (decimal)9999999999.99)
            {
                strValorExtenso = "Valor não suportado pela Função";
            }
            else //Entrada padrão do método
            {
                //Gerar Extenso Centavos
                dblCentavos = pdbl_Valor - (int)pdbl_Valor;
                //Gerar Extenso parte Inteira
                dblValorInteiro = (int)pdbl_Valor;
                if (dblValorInteiro > 0)
                {
                    if (dblValorInteiro > 999)
                    {
                        bln_Mil = true;
                    }
                    if (dblValorInteiro > 999999)
                    {
                        bln_Milhao = true;
                        bln_Mil = false;
                    }
                    if (dblValorInteiro > 999999999)
                    {
                        bln_Mil = false;
                        bln_Milhao = false;
                        bln_Bilhao = true;
                    }

                    for (int i = (dblValorInteiro.ToString().Trim().Length) - 1; i >= 0; i--)
                    {
                        // strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) + 1, 1);
                        strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 1);
                        switch (i)
                        {            /*******/
                            case 9:  /*Bilhão*
                                 /*******/
                                {
                                    strValorExtenso = fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " Bilhões de" : " Bilhão de");
                                    bln_Bilhao = true;
                                    break;
                                }
                            case 8: /********/
                            case 5: //Centena*
                            case 2: /********/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strCentena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 3);

                                        if (int.Parse(strCentena) > 100 && int.Parse(strCentena) < 200)
                                        {
                                            strValorExtenso = strValorExtenso + " Cento e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " " + fcn_Numero_Centena(strNumero);
                                        }
                                        if (intContador == 8)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 5)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 7: /*****************/
                            case 4: //Dezena de Milhão*
                            case 1: /*****************/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strDezena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 2);//

                                        if (int.Parse(strDezena) > 10 && int.Parse(strDezena) < 20)
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ") + fcn_Numero_Dezena0(Right(strDezena, 1));//corrigido

                                            bln_Unidade = true;
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ") + fcn_Numero_Dezena1(Left(strDezena, 1));//corrigido

                                            bln_Unidade = false;
                                        }
                                        if (intContador == 7)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 4)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 6: /******************/
                            case 3: //Unidade de Milhão*
                            case 0: /******************/
                                {
                                    if (int.Parse(strNumero) > 0 && !bln_Unidade)
                                    {
                                        if ((Right(strValorExtenso, 5).Trim()) == "entos"
                                        || (Right(strValorExtenso, 3).Trim()) == "nte"
                                        || (Right(strValorExtenso, 3).Trim()) == "nta")
                                        {
                                            strValorExtenso = strValorExtenso + " e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " ";
                                        }
                                        strValorExtenso = strValorExtenso + fcn_Numero_Unidade(strNumero);
                                    }
                                    if (i == 6)
                                    {
                                        if (bln_Milhao || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) == 1) && !bln_Unidade ? " Milhão de" : " Milhões de");
                                            bln_Milhao = true;
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        if (bln_Mil || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + " Mil";
                                            bln_Mil = true;
                                        }
                                    }
                                    if (i == 0)
                                    {
                                        if ((bln_Bilhao && !bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0")
                                        || (!bln_Bilhao && bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0"))
                                        {
                                            strValorExtenso = strValorExtenso + " de ";
                                        }
                                        strValorExtenso = strValorExtenso + ((int.Parse(dblValorInteiro.ToString())) > 1 ? " Reais " : " Real ");
                                    }
                                    bln_Unidade = false;
                                    break;
                                }
                        }
                    }//
                }
                if (dblCentavos > 0)
                {
                    if ((dblCentavos > (decimal)0) && (dblCentavos < (decimal)0.1))
                    {
                        strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                        strValorExtenso = strValorExtenso + ((int.Parse(strNumero.ToString()) > 0) ? " e " : " ")
                        + fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " Centavos " : "Centavo ");
                    }
                    else if (dblCentavos > (decimal)0.1 && dblCentavos < (decimal)0.2)
                    {
                        strNumero = Right(((Decimal.Round(dblCentavos, 2) - (decimal)0.1).ToString().Trim()), 1);
                        strValorExtenso = strValorExtenso + ((double.Parse(strNumero.ToString()) > 0) ? " " : "e ")
                        + fcn_Numero_Dezena0(strNumero) + " Centavos ";
                    }
                    else
                    {
                        if (dblCentavos > 0) //0#
                        {
                            strNumero = Right(dblCentavos.ToString().Trim(), 2);//Mid(dblCentavos.ToString().Trim(), 0,1);//strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 0) ? " e " : "");//((Sistema.Converte.ToInt(dblCentavos.ToString()) > 0) ? " e " : " ") + fcn_Numero_Dezena1(Left(strNumero, 1));
                            if ((dblCentavos.ToString().Trim().Length) > 2)
                            {
                                string strCentavos = strNumero;

                                if ((strValorExtenso != "") && (strValorExtenso.Substring(strValorExtenso.Length - 1, 1) != "e"))
                                { strValorExtenso = strValorExtenso + " e "; }
                                if ((int.Parse(strCentavos.Replace(",", "")) > (int)9) && (int.Parse(strCentavos.Replace(",", "")) < 20))
                                { strValorExtenso = strValorExtenso + fcn_Numero_Dezena0(strCentavos.Substring(0, 1)); }
                                else if (int.Parse(strCentavos.Replace(",", "")) > (int)19)
                                { strValorExtenso = strValorExtenso + fcn_Numero_Dezena1(strCentavos.Substring(0, 1)); }
                                if (strNumero.Substring(1, 1) == "0")
                                { strValorExtenso = strValorExtenso + " Centavos "; }

                                strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                                if (int.Parse(strNumero) > 0)
                                {
                                    if (Mid(strValorExtenso.Trim(), strValorExtenso.Trim().Length - 2, 1) == "e")
                                    {
                                        strValorExtenso = strValorExtenso + " " + fcn_Numero_Unidade(strNumero);
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                    strValorExtenso = strValorExtenso + " Centavos ";
                                }
                            }

                        }
                    }
                }
                if (dblValorInteiro < 1) strValorExtenso = Mid(strValorExtenso.Trim(), 2, strValorExtenso.Trim().Length - 2);
            }
            return strValorExtenso.Trim();

        }

        public static string utilExtenso_Metragem(string vValor)
        {
            decimal pdbl_Valor = decimal.Parse(vValor.Replace("R$", "").Trim());
            string strValorExtenso = ""; //Variável que irá armazenar o valor por extenso do número informado
            string strNumero = "";       //Irá armazenar o número para exibir por extenso
            string strCentena = "";
            string strDezena = "";
            //string strUnidade = "";

            decimal dblCentavos = 0;
            decimal dblValorInteiro = 0;
            int intContador = 0;
            bool bln_Bilhao = false;
            bool bln_Milhao = false;
            bool bln_Mil = false;
            //bool bln_Real = false;
            bool bln_Unidade = false;

            //Verificar se foi informado um dado indevido
            if (pdbl_Valor == 0 || pdbl_Valor <= 0)
            {
                strValorExtenso = "Verificar se há valor negativo ou nada foi informado";
            }
            if (pdbl_Valor > (decimal)9999999999.99)
            {
                strValorExtenso = "Valor não suportado pela Função";
            }
            else //Entrada padrão do método
            {
                //Gerar Extenso Centavos
                dblCentavos = pdbl_Valor - (int)pdbl_Valor;
                //Gerar Extenso parte Inteira
                dblValorInteiro = (int)pdbl_Valor;
                if (dblValorInteiro > 0)
                {
                    if (dblValorInteiro > 999)
                    {
                        bln_Mil = true;
                    }
                    if (dblValorInteiro > 999999)
                    {
                        bln_Milhao = true;
                        bln_Mil = false;
                    }
                    if (dblValorInteiro > 999999999)
                    {
                        bln_Mil = false;
                        bln_Milhao = false;
                        bln_Bilhao = true;
                    }

                    for (int i = (dblValorInteiro.ToString().Trim().Length) - 1; i >= 0; i--)
                    {
                        // strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) + 1, 1);
                        strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 1);
                        switch (i)
                        {            /*******/
                            case 9:  /*Bilhão*
                                 /*******/
                                {
                                    strValorExtenso = fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " Bilhões de" : " Bilhão de");
                                    bln_Bilhao = true;
                                    break;
                                }
                            case 8: /********/
                            case 5: //Centena*
                            case 2: /********/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strCentena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 3);

                                        if (int.Parse(strCentena) > 100 && int.Parse(strCentena) < 200)
                                        {
                                            strValorExtenso = strValorExtenso + " Cento e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " " + fcn_Numero_Centena(strNumero);
                                        }
                                        if (intContador == 8)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 5)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 7: /*****************/
                            case 4: //Dezena de Milhão*
                            case 1: /*****************/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strDezena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 2);//

                                        if (int.Parse(strDezena) > 10 && int.Parse(strDezena) < 20)
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ") + fcn_Numero_Dezena0(Right(strDezena, 1));//corrigido

                                            bln_Unidade = true;
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ") + fcn_Numero_Dezena1(Left(strDezena, 1));//corrigido

                                            bln_Unidade = false;
                                        }
                                        if (intContador == 7)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 4)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 6: /******************/
                            case 3: //Unidade de Milhão*
                            case 0: /******************/
                                {
                                    if (int.Parse(strNumero) > 0 && !bln_Unidade)
                                    {
                                        if ((Right(strValorExtenso, 5).Trim()) == "entos"
                                        || (Right(strValorExtenso, 3).Trim()) == "nte"
                                        || (Right(strValorExtenso, 3).Trim()) == "nta")
                                        {
                                            strValorExtenso = strValorExtenso + " e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " ";
                                        }
                                        strValorExtenso = strValorExtenso + fcn_Numero_Unidade(strNumero);
                                    }
                                    if (i == 6)
                                    {
                                        if (bln_Milhao || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) == 1) && !bln_Unidade ? " Milhão de" : " Milhões de");
                                            bln_Milhao = true;
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        if (bln_Mil || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + " Mil";
                                            bln_Mil = true;
                                        }
                                    }
                                    if (i == 0)
                                    {
                                        if ((bln_Bilhao && !bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0")
                                        || (!bln_Bilhao && bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0"))
                                        {
                                            strValorExtenso = strValorExtenso + " de ";
                                        }
                                        strValorExtenso = strValorExtenso + ((int.Parse(dblValorInteiro.ToString())) > 1 ? " Metros " : " Metros ");
                                    }
                                    bln_Unidade = false;
                                    break;
                                }
                        }
                    }//
                }
                if (dblCentavos > 0)
                {
                    if (int.Parse(dblCentavos.ToString()) > 0 && dblCentavos < (decimal)0.1)
                    {
                        strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                        strValorExtenso = strValorExtenso + ((int.Parse(dblCentavos.ToString()) > 0) ? " e " : " ")
                        + fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " Centímetros " : "Centímetro ");
                    }
                    else if (dblCentavos > (decimal)0.1 && dblCentavos < (decimal)0.2)
                    {
                        strNumero = Right(((Decimal.Round(dblCentavos, 2) - (decimal)0.1).ToString().Trim()), 1);
                        strValorExtenso = strValorExtenso + ((int.Parse(dblCentavos.ToString()) > 0) ? " " : "e ")
                        + fcn_Numero_Dezena0(strNumero) + " Centímetros ";
                    }
                    else
                    {
                        if (dblCentavos > 0) //0#
                        {
                            strNumero = Right(dblCentavos.ToString().Trim(), 2);//Mid(dblCentavos.ToString().Trim(), 0,1);//
                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 0) ? " e " : "")//((Sistema.Converte.ToInt(dblCentavos.ToString()) > 0) ? " e " : " ")
                            + fcn_Numero_Dezena1(Left(strNumero, 1));
                            if ((dblCentavos.ToString().Trim().Length) > 2)
                            {
                                strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                                if (int.Parse(strNumero) > 0)
                                {
                                    if (Mid(strValorExtenso.Trim(), strValorExtenso.Trim().Length - 2, 1) == "e")
                                    {
                                        strValorExtenso = strValorExtenso + " " + fcn_Numero_Unidade(strNumero);
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                }
                            }
                            strValorExtenso = strValorExtenso + " Centímetros ";
                        }
                    }
                }
                if (dblValorInteiro < 1) strValorExtenso = Mid(strValorExtenso.Trim(), 2, strValorExtenso.Trim().Length - 2);
            }
            return strValorExtenso.Trim();

        }

        public static string utilExtenso_Numero(string vValor)
        {
            decimal pdbl_Valor = decimal.Parse(vValor.Replace("R$", "").Trim());
            string strValorExtenso = ""; //Variável que irá armazenar o valor por extenso do número informado
            string strNumero = "";       //Irá armazenar o número para exibir por extenso
            string strCentena = "";
            string strDezena = "";
            //string strUnidade = "";

            decimal dblCentavos = 0;
            decimal dblValorInteiro = 0;
            int intContador = 0;
            bool bln_Bilhao = false;
            bool bln_Milhao = false;
            bool bln_Mil = false;
            //bool bln_Real = false;
            bool bln_Unidade = false;

            //Verificar se foi informado um dado indevido
            if (pdbl_Valor == 0 || pdbl_Valor <= 0)
            {
                strValorExtenso = "Verificar se há valor negativo ou nada foi informado";
            }
            if (pdbl_Valor > (decimal)9999999999.99)
            {
                strValorExtenso = "Valor não suportado pela Função";
            }
            else //Entrada padrão do método
            {
                //Gerar Extenso Centavos
                dblCentavos = pdbl_Valor - (int)pdbl_Valor;
                //Gerar Extenso parte Inteira
                dblValorInteiro = (int)pdbl_Valor;
                if (dblValorInteiro > 0)
                {
                    if (dblValorInteiro > 999)
                    {
                        bln_Mil = true;
                    }
                    if (dblValorInteiro > 999999)
                    {
                        bln_Milhao = true;
                        bln_Mil = false;
                    }
                    if (dblValorInteiro > 999999999)
                    {
                        bln_Mil = false;
                        bln_Milhao = false;
                        bln_Bilhao = true;
                    }

                    for (int i = (dblValorInteiro.ToString().Trim().Length) - 1; i >= 0; i--)
                    {
                        // strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) + 1, 1);
                        strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 1);
                        switch (i)
                        {            /*******/
                            case 9:  /*Bilhão*
                                 /*******/
                                {
                                    strValorExtenso = fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " Bilhões de" : " Bilhão de");
                                    bln_Bilhao = true;
                                    break;
                                }
                            case 8: /********/
                            case 5: //Centena*
                            case 2: /********/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strCentena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 3);

                                        if (int.Parse(strCentena) > 100 && int.Parse(strCentena) < 200)
                                        {
                                            strValorExtenso = strValorExtenso + " Cento e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " " + fcn_Numero_Centena(strNumero);
                                        }
                                        if (intContador == 8)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 5)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 7: /*****************/
                            case 4: //Dezena de Milhão*
                            case 1: /*****************/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strDezena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 2);//

                                        if (int.Parse(strDezena) > 10 && int.Parse(strDezena) < 20)
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ") + fcn_Numero_Dezena0(Right(strDezena, 1));//corrigido

                                            bln_Unidade = true;
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ") + fcn_Numero_Dezena1(Left(strDezena, 1));//corrigido

                                            bln_Unidade = false;
                                        }
                                        if (intContador == 7)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 4)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 6: /******************/
                            case 3: //Unidade de Milhão*
                            case 0: /******************/
                                {
                                    if (int.Parse(strNumero) > 0 && !bln_Unidade)
                                    {
                                        if ((Right(strValorExtenso, 5).Trim()) == "entos"
                                        || (Right(strValorExtenso, 3).Trim()) == "nte"
                                        || (Right(strValorExtenso, 3).Trim()) == "nta")
                                        {
                                            strValorExtenso = strValorExtenso + " e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " ";
                                        }
                                        strValorExtenso = strValorExtenso + fcn_Numero_Unidade(strNumero);
                                    }
                                    if (i == 6)
                                    {
                                        if (bln_Milhao || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) == 1) && !bln_Unidade ? " Milhão de" : " Milhões de");
                                            bln_Milhao = true;
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        if (bln_Mil || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + " Mil";
                                            bln_Mil = true;
                                        }
                                    }
                                    if (i == 0)
                                    {
                                        if ((bln_Bilhao && !bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0")
                                        || (!bln_Bilhao && bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0"))
                                        {
                                            strValorExtenso = strValorExtenso + " de ";
                                        }
                                        strValorExtenso = strValorExtenso + ((int.Parse(dblValorInteiro.ToString())) > 1 ? " " : " ");
                                    }
                                    bln_Unidade = false;
                                    break;
                                }
                        }
                    }//
                }
                if (dblCentavos > 0)
                {
                    if (int.Parse(dblCentavos.ToString()) > 0 && dblCentavos < (decimal)0.1)
                    {
                        strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                        strValorExtenso = strValorExtenso + ((int.Parse(dblCentavos.ToString()) > 0) ? " e " : " ")
                        + fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " " : " ");
                    }
                    else if (dblCentavos > (decimal)0.1 && dblCentavos < (decimal)0.2)
                    {
                        strNumero = Right(((Decimal.Round(dblCentavos, 2) - (decimal)0.1).ToString().Trim()), 1);
                        strValorExtenso = strValorExtenso + ((int.Parse(dblCentavos.ToString()) > 0) ? " " : "e ")
                        + fcn_Numero_Dezena0(strNumero) + " ";
                    }
                    else
                    {
                        if (dblCentavos > 0) //0#
                        {
                            strNumero = Right(dblCentavos.ToString().Trim(), 2);//Mid(dblCentavos.ToString().Trim(), 0,1);//
                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 0) ? " e " : "")//((Sistema.Converte.ToInt(dblCentavos.ToString()) > 0) ? " e " : " ")
                            + fcn_Numero_Dezena1(Left(strNumero, 1));
                            if ((dblCentavos.ToString().Trim().Length) > 2)
                            {
                                strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                                if (int.Parse(strNumero) > 0)
                                {
                                    if (Mid(strValorExtenso.Trim(), strValorExtenso.Trim().Length - 2, 1) == "e")
                                    {
                                        strValorExtenso = strValorExtenso + " " + fcn_Numero_Unidade(strNumero);
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                }
                            }
                            strValorExtenso = strValorExtenso + " Centavos ";
                        }
                    }
                }
                if (dblValorInteiro < 1) strValorExtenso = Mid(strValorExtenso.Trim(), 2, strValorExtenso.Trim().Length - 2);
            }
            return strValorExtenso.Trim();

        }

        private static string fcn_Numero_Dezena0(string pstrDezena0)
        {
            //Vetor que irá conter o número por extenso
            List<string> array_Dezena0 = new List<string>();

            array_Dezena0.Add("Onze");
            array_Dezena0.Add("Doze");
            array_Dezena0.Add("Treze");
            array_Dezena0.Add("Quatorze");
            array_Dezena0.Add("Quinze");
            array_Dezena0.Add("Dezesseis");
            array_Dezena0.Add("Dezessete");
            array_Dezena0.Add("Dezoito");
            array_Dezena0.Add("Dezenove");

            return array_Dezena0[((int.Parse(pstrDezena0)) - 1)].ToString();
        }
        private static string fcn_Numero_Dezena1(string pstrDezena1)
        {
            //Vetor que irá conter o número por extenso
            List<string> array_Dezena1 = new List<string>();

            array_Dezena1.Add("Dez");
            array_Dezena1.Add("Vinte");
            array_Dezena1.Add("Trinta");
            array_Dezena1.Add("Quarenta");
            array_Dezena1.Add("Cinquenta");
            array_Dezena1.Add("Sessenta");
            array_Dezena1.Add("Setenta");
            array_Dezena1.Add("Oitenta");
            array_Dezena1.Add("Noventa");

            return array_Dezena1[((int.Parse(pstrDezena1)) - 1)].ToString();
        }
        private static string fcn_Numero_Centena(string pstrCentena)
        {
            //Vetor que irá conter o número por extenso
            List<string> array_Centena = new List<string>();

            array_Centena.Add("Cem");
            array_Centena.Add("Duzentos");
            array_Centena.Add("Trezentos");
            array_Centena.Add("Quatrocentos");
            array_Centena.Add("Quinhentos");
            array_Centena.Add("Seiscentos");
            array_Centena.Add("Setecentos");
            array_Centena.Add("Oitocentos");
            array_Centena.Add("Novecentos");

            return array_Centena[((int.Parse(pstrCentena)) - 1)].ToString();
        }
        private static string fcn_Numero_Unidade(string pstrUnidade)
        {
            //Vetor que irá conter o número por extenso
            List<string> array_Unidade = new List<string>();

            array_Unidade.Add("Um");
            array_Unidade.Add("Dois");
            array_Unidade.Add("Três");
            array_Unidade.Add("Quatro");
            array_Unidade.Add("Cinco");
            array_Unidade.Add("Seis");
            array_Unidade.Add("Sete");
            array_Unidade.Add("Oito");
            array_Unidade.Add("Nove");

            return array_Unidade[((int.Parse(pstrUnidade)) - 1)].ToString();
        }
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            if (param == "")
                return "";
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            if (param == "")
                return "";
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }

        #endregion

        #region Encriptar

        const string DESKey = "AQWSEDRF";
        const string DESIV = "HGFEDCBA";

        public static string strDecrypt(string stringToDecrypt)//Decrypt the content
        {

            byte[] key;
            byte[] IV;

            byte[] inputByteArray;
            try
            {

                key = Convert2ByteArray(DESKey);

                IV = Convert2ByteArray(DESIV);

                stringToDecrypt = stringToDecrypt.Replace(" ", "+");

                int len = stringToDecrypt.Length; inputByteArray = Convert.FromBase64String(stringToDecrypt);


                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8; return (encoding.GetString(ms.ToArray())).Replace("+", " ");
            }

            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public static string strEncrypt(string stringToEncrypt)// Encrypt the content
        {
            stringToEncrypt = stringToEncrypt.Replace(" ", "+");
            byte[] key;
            byte[] IV;

            byte[] inputByteArray;
            try
            {

                key = Convert2ByteArray(DESKey);

                IV = Convert2ByteArray(DESIV);

                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream(); CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }

            catch (System.Exception ex)
            {

                throw ex;
            }

        }

        static byte[] Convert2ByteArray(string strInput)
        {

            int intCounter; char[] arrChar;
            arrChar = strInput.ToCharArray();

            byte[] arrByte = new byte[arrChar.Length];

            for (intCounter = 0; intCounter <= arrByte.Length - 1; intCounter++)
                arrByte[intCounter] = Convert.ToByte(arrChar[intCounter]);

            return arrByte;
        }

        #endregion

        #region startTrim - Remove o texto no inicio da string
        /// <summary>
        /// Remove text of start string. Start spaces in string will be ignored and will be removed that too.
        /// </summary>
        /// <param name="text">String what contains text to remove</param>
        /// <param name="remove_text">Text to remove of start string</param>
        /// <returns>String without start 'text'</returns>
        public static string startTrim(string text, string remove_text)
        {
            text = text.TrimStart(' ');
            remove_text = remove_text.Trim();

            return (text.Substring(0, remove_text.Length));
        }

        #endregion

        #region endTrim - Remove o texto no final da string
        /// <summary>
        /// Remove text in end of string. 
        /// <para>Spaces in end of string and spaces in "remove_text" of start or end will be ignored and will be removed that too.</para>
        /// </summary>
        /// <param name="text">String what contains text to remove</param>
        /// <param name="remove_text">Text to remove of start string</param>
        /// <returns>String without start 'text'</returns>
        public static string endTrim(string text, string remove_text)
        {
            text = text.TrimEnd(' ');
            remove_text = remove_text.Trim();

            return (text.Substring(0, text.Length - remove_text.Length));
        }

        #endregion
    }
    #endregion

    #endregion

    //=====================================================================================

    #region smartData

    #region smartDataStr ( DataStrings )
    public static class smartDataStr
    {
        /*ErrDataStr01:DataStr vazia, ErrDataStr02:Grupo não encontrado! */

        #region get - Pega um item em um DataStr
        /// <summary>
        /// Get a value in a variable DataStr (horizontal dicionary with registers separate by *)
        /// </summary>
        /// <param name="dataStr">DataStr</param>
        /// <param name="key">Key of value</param>
        /// <returns>Value finded by identificate key(field). Ex: getItem("number=1*name=john*city=Chicago", "name") return "john" </returns>
        public static string get(string dataStr, string key)
        {
            if (dataStr == "")
            {
                return ("");
            }

            dataStr = "*" + dataStr;
            if ((dataStr).Contains("*" + key + "="))
            {
                dataStr = dataStr.Substring(dataStr.IndexOf("*" + key + "=") + 1);
                var row = dataStr.Split('*');
                var valor = row[0].Split('=');

                return (valor[1]);
            }
            else
            {
                return ("");
            }
        }

        #endregion

        #region set - Insere um item em uma DataList
        /// <summary>
        /// Set or change value in DataStr (horizontal dicionary with registers separate by *)
        /// </summary>
        /// <param name="dataSTr">DataStr</param>
        /// <param name="key">Key of register (field)</param>
        /// <param name="value">Value finded by identificate key(field). Ex: setItem("number=1*name=john*city=Chicago", "name", "bill") 
        /// replace "name=john" by "name=bill"</param>
        /// <returns>Return dataStr with change</returns>
        public static string set(string dataStr, string campo, string valor)
        {

            dataStr = "*" + dataStr;

            if ((valor != "") && (campo != "") && (dataStr != ""))
            {
                if (dataStr.Contains("*" + campo + "="))
                {
                    string item = dataStr.Substring(dataStr.IndexOf("*" + campo + "=") + 1);
                    if (item.IndexOf("*") != -1)
                    {
                        item = item.Substring(0, item.IndexOf("*"));
                    }
                    dataStr = dataStr.Replace("*" + item, "*" + campo + "=" + valor);
                }
                else
                {
                    dataStr += "*" + campo + "=" + valor;
                }
            }

            dataStr = dataStr.Replace("**", "*");

            if (dataStr.Substring(0, 1) == "*")
            {
                dataStr = dataStr.Substring(1);
            }

            return (dataStr);
        }

        #endregion

        #region concatByTable - Incluí dados de uma Tabela em um DataString
        /// <summary>
        /// Insert data of dataTable in a dataStr. Registers with same key in both, dataStr will be updated by dataTable
        /// </summary>
        /// <param name="dataStr">Data String to recive registers</param>
        /// <param name="tabelaRow">Data Row with registers</param>
        /// <param name="initColumn">The index of dataTable collumm to start data group</param>
        /// <returns>Return updated dataStr</returns>
        public static string concatByTable(string dataStr, DataRow tabelaRow, int initColumn)
        {
            string lstMontado = "*" + dataStr;

            for (int i = initColumn; i < tabelaRow.Table.Columns.Count; ++i)
            {
                string campo = tabelaRow.Table.Columns[i].ColumnName;

                if (lstMontado.Contains("*{campo}="))
                {
                    lstMontado = set(lstMontado, campo, tabelaRow[i].ToString());
                }
                else
                {
                    lstMontado += "*" + tabelaRow.Table.Columns[i].ColumnName + "=" + tabelaRow[i].ToString();
                }
            }

            if (lstMontado != "")
            {
                lstMontado = lstMontado.Replace("**", "*").Substring(1);
            }

            return (lstMontado);

        }

        #endregion

        #region concatByDictionary - Insere dados de um Dictionary em uma DataList.
        /// <summary>
        /// Insert a Dictionary data in DataStr. Registers with same key in both, dataStr will be updated by Dictionary
        /// </summary>
        /// <param name="dataStr">Data String to recive registers</param>
        /// <param name="dictionary">Dictionary with data to insert in DataStr</param>
        /// <returns>Updated DataStr.</returns>
        public static string concatByDictionary(string dataStr, Dictionary<string, string> dictionary)
        {
            if (dictionary.Count() == 0)
            {
                return (dataStr);
            }

            string lstMontado = "*" + dataStr;

            foreach (KeyValuePair<string, string> item in dictionary)
            {
                if (lstMontado.Contains($"*{item.Key}="))
                {
                    lstMontado = set(lstMontado, item.Key, item.Value);
                }
                else
                {
                    lstMontado = lstMontado + "*" + item.Key + "=" + item.Value;
                }
            }

            lstMontado = lstMontado.Substring(1);

            return (lstMontado);

        }

        #endregion

        #region groupToDictionary - Cria um Dictionary com os dados de vários DataStr 
        /// <summary>
        /// Get data group like: "[nameOfGroup]field=value*field=value|[nameOfGroup]field=value". Insert each group separed by "|" in a Dictionay key. 
        /// </summary>
        /// <param name="dataStr">A dataStr with data separated by key. Ex: field=value*field=value.</param>
        /// <param name="group">Data group name to get registers. Ex:[group1]field=value*field=value|[group2]field=value*field=value</param>
        /// <returns>A Dictionary where keys be fields and values be dataStr</returns>
        public static Dictionary<string, string> groupToDictionary(string dataStr, string group)
        {
            Dictionary<string, string> lstFinal = new Dictionary<string, string>();

            int inicioIndex = dataStr.IndexOf("[" + group + "]");

            if (!dataStr.Contains("*"))
            {
                lstFinal["err"] = "ErrDataStr01:DataStr vazia!";
                return (lstFinal);
            }

            if (inicioIndex == -1)
            {
                lstFinal["err"] = "ErrDataStr02:Grupo não encontrado!";
                return (lstFinal);
            }

            string lstGroup = dataStr.Substring(inicioIndex, dataStr.IndexOf("|") - 1);

            var lstData = lstGroup.Split('*');

            for (int i = 0; i < lstData.Count(); ++i)
            {
                var item = lstData[i].Split('=');
                lstFinal.Add(item[0].Trim(), item[1].Trim());
            }

            return (lstFinal);

        }

        #endregion

        #region convertString - Converte uma string com separação de colunas e campo/valor customizados para o padrão DataStr
        /// <summary>
        /// Change string with customized operator and separator to dataStr default. Ex: name:Luiz,age:18 => name=Luiz*age=18
        /// </summary>
        /// <param name="str">String with characters sequence</param>
        /// <param name="operador">Operator to separate field and value</param>
        /// <param name="separador">Character to separate registers</param>
        /// <returns>Return dataStr with organized registers</returns>
        public static string convertCustom(string str, string operador, string separador)
        {
            return (str.Replace(operador, "=").Replace(separador, "*"));
        }

        #endregion

        #region toDictionary - Converte em um Dictionary
        /// <summary>
        /// Convert Dictionary to dataStr
        /// </summary>
        /// <param name="dataStr">DataStr with data to be convert in Dictionary</param>
        /// <returns>Return a Dictionary with all data of DataStr</returns>
        public static Dictionary<string, object> toDicitionary(string dataStr)
        {
            Dictionary<string, object> lstFinal = new Dictionary<string, object>();

            lstFinal.Add("err", "");

            if (!dataStr.Contains("*"))
            {
                lstFinal["err"] = "Err01:DataStr vazia!";
                return (lstFinal);
            }

            if (dataStr.Substring(0, 1) == "*")
            {
                dataStr.Substring(1);
            }

            var lstData = dataStr.Split('*');

            for (int i = 0; i < lstData.Count(); ++i)
            {
                string campo = lstData[i].Substring(0, lstData[i].IndexOf('=')).Trim();
                string valor = lstData[i].Substring(lstData[i].IndexOf('=') + 1).Trim();
                if (valor == "") { valor = "-"; }
                if (lstFinal.ContainsKey(campo))
                {
                    lstFinal[campo] = valor;
                }
                else
                {
                    lstFinal.Add(campo, valor);
                }
            }

            lstFinal["err"] = "";

            return (lstFinal);

        }

        #endregion

    }

    #endregion

    #region smartDictionary ( Dictionary )
    public static class smartDictionary
    {
        #region updateByDataStr - Atualiza um Dictionary por uma DataStr
        /// <summary>
        /// Update Dictionay with DataStr content.
        /// </summary>
        /// <param name="dic">Dictionary a ser atualizada</param>
        /// <param name="dataStr">DataStr com os novos dados</param>
        /// <returns>Retorna o Dictionary atualizado pelos dados da Data String. 
        /// Os campos ja encontrados no Dictionary tem o valor atualizado e os que não tem no Dictionary serão adicionados nele.</returns>
        public static Dictionary<string, string> updateByDataStr(Dictionary<string, string> dic, string dataStr)
        {
            if (dataStr.Substring(0, 1) == "*")
            {
                dataStr.Substring(1);
            }

            var lstData = dataStr.Split('*');

            for (int i = 0; i < lstData.Count(); ++i)
            {
                var item = lstData[i].Split('=');
                string valor = item[1].Trim();
                if (valor == "") { valor = "-"; }
                if (dic.ContainsKey(item[0].Trim()))
                {
                    dic[item[0].Trim()] = item[1].Trim();
                }
                else
                {
                    dic.Add(item[0].Trim(), item[1].Trim());
                }
            }

            if (!dic.ContainsKey("err"))
            {
                dic.Add("err", "");
            }

            return (dic);
        }

        #endregion

        #region concatDictionary - Concatena dictionary destino com dictionary origem 
        /// <summary>
        /// Concat 2 dicitionarys in 1. The equal destiny keys can be update by source
        /// </summary>
        /// <param name="destino">Destiny dictionary</param>
        /// <param name="origem">Source dictionary</param>
        /// <param name="update">True or False. When the destiny key is equal a source key, update value</param>
        /// <returns>The destiny dicitonary concatened with souce</returns>
        public static Dictionary<string, string> concatDictionary(Dictionary<string, string> destino, Dictionary<string, string> origem, bool update)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();

            dataList.Add("err", "");
            dataList.Add("msg", "");

            if (origem == null)
            {
                dataList["err"] = "smart#Error01";
                dataList["msg"] = "'origem' null!";
                return (dataList);
            }

            if (destino == null)
            {
                dataList["err"] = "smart#Error01";
                dataList["msg"] = "'destino' null!";
                return (dataList);
            }

            foreach (KeyValuePair<string, string> item in origem)
            {
                if (destino.ContainsKey(item.Key))
                {
                    if (update)
                    { destino[item.Key] = origem[item.Key]; }
                }
                else { destino.Add(item.Key, item.Value); }
            }

            destino["err"] = "";
            destino["msg"] = "";

            return (destino);
        }
        #endregion

        #region toDataStr - converte o Dicitionary em DataStr.
        /// <summary>
        /// Cria uma nova DataStr com o conteúdo de um Dicitionary.
        /// </summary>
        /// <param name="dictionary">Dicitonary com os dados a serem utilizados na DataStr</param>
        /// <returns>Uma nova DataStr com o conteúdo de um Dictionary</returns>
        public static string toDataStr(Dictionary<string, object> dictionary)
        {
            string lstMontado = "";

            foreach (KeyValuePair<string, object> item in dictionary)
            {
                lstMontado = $"{lstMontado}*{item.Key}={item.Value}";
            }

            lstMontado = lstMontado.Substring(1);

            return (lstMontado);
        }

        #endregion

        #region convertByObject - Converte um obejto em dicitionary
        /// <summary>
        /// Convert a object to Dicitionary
        /// </summary>
        /// <param name="thisObject">Object with data to be convert in Dictionary</param>
        /// <returns>Return a Dictionary with all properties and data of object</returns>
        public static Dictionary<string, string> convertByObject(dynamic thisObject)
        {
            Dictionary<string, string> dicFinal = new Dictionary<string, string>();

            dicFinal.Add("error", "");

            if (thisObject.GetType() == typeof(object))
            {
                dicFinal["error"] = "Err10:Type mismatch of thisObject!";
                return dicFinal;
            }

            foreach (var prop in thisObject.GetType().GetProperties())
            {
                dicFinal.Add(prop.Name, prop.GetValue(thisObject, null));
            }

            return dicFinal;

        }

        #endregion

    }

    #endregion

    #region DataTable ( Data Table )

    public static class smartTable
    {

        #region rowToDictionay - Converte uma Row de tabela em um Dictionary<string, string>

        /// <summary>
        /// Convert a dataRow of table to Dictionary
        /// </summary>
        /// <param name="tabelaRow">DataRow with origin data</param>
        /// <param name="initColumn">Start collumm to search data in DataRow</param>
        /// <param name="filter_prefix">Only will get data with fields started with this character sequence. 
        /// Ex: filter="prod_" Valid fields: "prod_name, prod_value"</param>
        /// <param name="remove_prefix">true or false. Remove de prefix (filter_prefix) in fields that text have that before convert "key" to dicitonary. 
        /// Ex: field:"prod_name" change in dictionary to "nome"</param>
        /// <returns>A Dictionary with DataTable data</returns>
        public static Dictionary<string, string> rowToDictionary(DataRow tabelaRow, int initColumn, string filter_prefix, bool remove_prefix)
        {
            Dictionary<string, string> dataList = new Dictionary<string, string>();

            dataList.Add("err", "");
            dataList.Add("msg", "");

            if (tabelaRow == null)
            {
                dataList["err"] = "strPack01";
                dataList["msg"] = "Problemas na conexão com o sevidor! Verifique sua internet e tente novamente.";
                return (dataList);
            }

            if (tabelaRow == null)
            {
                dataList["err"] = "strPack02";
                dataList["msg"] = "Nenhum registro encontrado!";
                return (dataList);
            }

            for (int i = 0; i < tabelaRow.Table.Columns.Count; ++i)
            {
                if (filter_prefix == "")
                {
                    dataList.Add(tabelaRow.Table.Columns[i].ColumnName, tabelaRow[i].ToString());
                }
                else
                {
                    if (tabelaRow.Table.Columns[i].ColumnName.Substring(0, filter_prefix.Length) == filter_prefix)
                    {
                        if (!remove_prefix) { filter_prefix = ""; }

                        dataList.Add(tabelaRow.Table.Columns[i].ColumnName.Replace(filter_prefix, ""), tabelaRow[i].ToString());
                    }
                }
            }

            dataList["err"] = "";
            dataList["msg"] = "";

            return (dataList);

        }
        #endregion

        #region toDictionay - Converte uma Row de tabela em um List<Dictionary<string, object>>

        /// <summary>
        /// Convert a dataTable of in List Dictionary like List[Dictionary[string, object]]
        /// </summary>
        /// <param name="table">DataTable with origin data</param>
        /// <param name="initColumn">Start collumm to search data in DataRow</param>
        /// <param name="filter_prefix">Only will get data with fields started with this character sequence. 
        /// Ex: filter="prod_" Valid fields: "prod_name, prod_value"</param>
        /// <param name="remove_prefix">true or false. Remove de prefix (filter_prefix) in fields that text have that before convert "key" to dicitonary. 
        /// Ex: field:"prod_name" change in dictionary to "nome"</param>
        /// <returns>One List to rows, with a Dictionary with fileds(keys) and values of the DataTable</returns>
        public static List<Dictionary<string, object>> toDictionary(DataTable table, int initColumn, string filter_prefix, bool remove_prefix)
        {
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

            if (table == null)
            {
                throw new ArgumentException("Data Table is null!");
            }

            for (int row = 0; row < table.Rows.Count; ++row)
            {
                for (int i = 0; i < table.Columns.Count; ++i)
                {
                    if (filter_prefix == "")
                    {
                        dataList.Add(new Dictionary<string, object> { { table.Columns[i].ColumnName, table.Rows[row][i].ToString() } });
                    }
                    else
                    {
                        if (table.Columns[i].ColumnName.Substring(0, filter_prefix.Length) == filter_prefix)
                        {
                            if (!remove_prefix) { filter_prefix = ""; }

                            dataList.Add(new Dictionary<string, object>{
                                { table.Columns[i].ColumnName.Replace(filter_prefix, ""), table.Rows[row][i].ToString()}
                            });
                        }
                    }
                }
            }

            return (dataList);

        }
        #endregion

        #region toDataStr - Converte em uma nova DataString
        /// <summary>
        /// Convert DataRow in existing dataStr. Registers with same key in both, dataStr will be updated by DataRow
        /// </summary>
        /// <param name="tabelaRow">Data Row with data</param>
        /// <param name="initColumn">Start collumm to search data in DataRow</param>
        /// <param name="filter_prefix">Only will get data with fields started with this character sequence.
        /// Ex: filter="prod_" Valid fields: "prod_name, prod_value</param>
        /// <param name="remove_prefix">true or false. Remove de prefix (filter_prefix) in fields that text have that before convert "key" to dataStr. 
        /// Ex: field:"prod_name" change in dataStr to "nome"</param>
        /// <returns>Return a new dataStr with data</returns>
        public static string toDataStr(DataRow tabelaRow, int initColumn, string filtro_prefixo, bool remove_prefixo)
        {
            string lstMontado = "";

            for (int i = initColumn; i < tabelaRow.Table.Columns.Count; ++i)
            {
                string campo = tabelaRow.Table.Columns[i].ColumnName;
                string valor = tabelaRow[i].ToString();

                if (campo.Substring(0, filtro_prefixo.Count()).ToLower() == filtro_prefixo.ToLower())
                {
                    if (remove_prefixo)
                    {
                        campo = campo.Replace(filtro_prefixo, "");
                    }

                    lstMontado += "*" + campo + "=" + valor;
                }
            }

            if (lstMontado != "")
            {
                lstMontado = lstMontado.Replace("**", "*").Substring(1);
            }

            return (lstMontado);

        }

        #endregion

        #region newDataTable - Cria as colunas em uma DataTable

        /// <summary>
        /// Create empty dataTable with collumns to fields 
        /// </summary>
        /// <param name="fields">Fields names to create in dataTable. Inform that each field separeted by comma. 
        /// Ex: name, age, city ... </param>
        /// <returns>Return a DataTable with collumns without rows</returns>
        public static DataTable newDataTable(string fields)
        {
            DataTable tabela = new DataTable();

            var colunas = fields.Split(',');

            for (int i = 0; i < colunas.Count(); ++i)
            {
                DataColumn newColuna = new DataColumn(colunas[i].Replace(" ", ""));
                newColuna.DataType = Type.GetType("System.String");
                tabela.Columns.Add(newColuna);
            }

            tabela.Rows.Add("");

            return (tabela);
        }

        #endregion

    }

    #endregion

    #endregion

    //=====================================================================================

    #region smartObject

    public static class smartObject
    {
        #region toDictionary - Converte um obejto em Dictionary
        /// <summary>
        /// Convert a object to Dictionary
        /// </summary>
        /// <param name="thisObject">Object with data to be convert</param>
        /// <returns>Return a Dictionary with all properties and data of object</returns>
        public static Dictionary<string, object> toDictionary(object thisObject)
        {
            Dictionary<string, object> dicFinal = new Dictionary<string, object> { { "error", string.Empty } };

            if (thisObject.GetType() == typeof(object))
            {
                dicFinal["error"] = "Err10:Type mismatch of thisObject!";
                return dicFinal;
            }

            foreach (var prop in thisObject.GetType().GetProperties())
            {
                string valor = Convert.ToString(prop.GetValue(thisObject, null));
                dicFinal.Add(prop.Name, valor);
            }

            return dicFinal;
        }

        #endregion

        #region toDataStr - Converte um obejto em dataStr
        /// <summary>
        /// Convert a object to dataStr
        /// </summary>
        /// <param name="thisObject">Object with data to be convert in DataStr</param>
        /// <returns>Return a DataStr with all properties and data of object</returns>
        public static string toDataStr(object thisObject)
        {
            string strFinal = "";

            if (thisObject.GetType() == typeof(object))
            {
                return "Err10:Type mismatch of thisObject!";
            }

            foreach (var prop in thisObject.GetType().GetProperties())
            {
                string valor = Convert.ToString(prop.GetValue(thisObject, null));
                strFinal = $"*{prop.Name}={valor}";
            }

            return strFinal.Substring(1);
        }

        #endregion
    }

    #endregion

    //=====================================================================================

    #region smartDataBase
    public static class smartDB
    {
        #region Propriedades

        //private const string cadbanco = "DRIVER={PostGreSQL Ansi};SERVER=200.147.61.76;UID=gobroker;PWD=borisbr@00;DATABASE=gobroker;OPTION=3";
        public  const string cadbanco = "DRIVER={PostGreSQL Ansi};SERVER=pgsql02-farm36.kinghost.net;UID=gobroker1;PWD=borisbr00;DATABASE=gobroker1;OPTION=3";

        public static string cadBanco => cadbanco;

        private const string cadbanco_goByte = "DRIVER={PostGreSQL Ansi};SERVER=pgsql02-farm36.kinghost.net; UID=gobyte;PWD=borisbr; DATABASE=gobyte; OPTION=3";
        public static string cadBanco_goByte => cadbanco_goByte;

        public static string ExceptionsTable { get => exceptionsTable; set => exceptionsTable = value; }
        private static string exceptionsTable = "";

        #endregion

        #region Open By Query With Tables Names
        /// <summary>
        /// Open in DataSet a SQLQuery query.
        /// </summary>
        /// <param name="SQLQuery">Query in SQL</param>
        /// <param name="tables_names">(optional)Names to tables. Ex:client,users,products</param>
        /// <returns>DataSet with a returned query</returns>
        /// 
        public static DataSet openQuery(string SQLQuery, string tables_names, string string_connection)
        {
            SQLQuery = smartHTML.decodeSymbolsInHtml(SQLQuery);
            int tentativa = 0;
            DataSet ds = new DataSet();
            DataTable exeption = new DataTable();

        AcessarBanco:
            try
            {
                OdbcConnection dbBanco = new OdbcConnection(string_connection);

                if (dbBanco.State != ConnectionState.Open)
                { dbBanco.Open(); }

                OdbcDataAdapter da = new OdbcDataAdapter();
                OdbcCommandBuilder cb = new OdbcCommandBuilder(da);
                da.SelectCommand = new OdbcCommand(SQLQuery, dbBanco);

                if (tables_names != "")
                {
                    var tablesName = tables_names.Replace(" ", "").Split(',');

                    for (int i = 0; i < tablesName.Count(); ++i)
                    {
                        if (i == 0)
                        {
                            da.TableMappings.Add("Table", tablesName[0]);
                        }
                        else
                        {
                            da.TableMappings.Add("Table" + i.ToString(), tablesName[i]);
                        }
                    }
                }

                da.Fill(ds);
                dbBanco.Dispose();
            }
            catch (Exception err)
            {
                exeption = verificaErro(err, SQLQuery, tentativa);

                if (exeption ==null)
                {
                    tentativa += 1;
                    goto AcessarBanco;
                }
                else
                {
                    ds.Tables.Add(exeption);
                }

            }
            return (ds);
        }

        #endregion

        #region Execute Query In DataBase
        /// <summary>
        /// Execute query in database. 
        /// </summary>
        /// <param name="SQLQuery">SQL query</param>
        /// <returns>Number of successful occurrences</returns>
        public static string executeQuery(string SQLQuery, string string_connection)
        {
            int tentativa = 0;

            SQLQuery = smartHTML.decodeSymbolsInHtml(SQLQuery);

        AcessarBanco:
            try
            {
                OdbcConnection dbBanco = new OdbcConnection(string_connection);
                if (dbBanco.State != ConnectionState.Open)
                { dbBanco.Open(); }

                OdbcCommand dbFuncao = new OdbcCommand(SQLQuery, dbBanco);
                int affectedRows = dbFuncao.ExecuteNonQuery();
                dbBanco.Dispose();

                if(affectedRows ==0)
                {
                    return "!!Não foi possível enviar suas informações para nossa base de dados!";
                }

                return (affectedRows.ToString());
            }
            catch (Exception err)
            {
                DataTable exeption = verificaErro(err, SQLQuery, tentativa);

                if (exeption == null)
                {
                    tentativa += 1;
                    goto AcessarBanco;
                }
                
                return (exeption.Rows[0]["mensagem"].ToString());
            }
        }

        #endregion

        #region Insert in DataBase
        /// <summary>
        /// Insere um registro no banco baseado nos campos(key) e dados(values) armazenados no dictionary "Dados" 
        /// </summary>
        /// <param name="table">(INSERT INTO) Tabela no banco de dados</param>
        /// <param name="dados">(VALUES) Dicitionary com o registro a ser inserido, sendo no dicitionary as keys(campos) com o mesmo nome dos campos no banco de dados.</param>
        /// <param name="chaves_primarias">(SELECT) Campos que não podem ser repetidos e não podem ser vazios. Todos os campos devem estar entre "[]". Se não forem valores numéricos devem estar entre ['']. O campo ID será ignorado se informado aqui. Os campos WHERE são separados e condicionados por cláusula OR, para separar e condicionar por AND, adicione + no campo ou seja ['campo']+</param>
        /// <param name="campo_id">(ID) Campo de chave primária que precisa gerar o ID automaticamente. Por ser gerado automático na execução do insert, o campo_id não participa da query 'WHERE'.</param>
        /// <param name="css_msg_retorno">(MSG CSS) Se for informado, a mensagem retorno será um span em HTML com esse CSS.</param>
        /// <returns>DataTable retornando um SELECT confirmando os valores salvos nos campos "chaves_primarias" ou com algum código de erro no campo "error" e mensagem de erro ou sucesso "msg" as mensagens de erro iniciam sempre com !! para ser fácil identificar.</returns>
        public static DataTable insert(string table, Dictionary<string, object> dados, string chaves_primarias, string campo_id, string css_msg_retorno, string connection_string)
        {

            #region inicializa tabela retorno

            DataTable retorno = new DataTable();
            DataRow erroRow = retorno.NewRow();
            retorno.Rows.Add(erroRow);

            DataColumn error = new DataColumn();
            error.ColumnName = "error";
            retorno.Columns.Add(error);

            DataColumn msg = new DataColumn();
            msg.ColumnName = "msg";
            retorno.Columns.Add(msg);

            #endregion

            #region Se o dictionary dados está vazio

            if (dados.Count == 0)
            {
                retorno.Rows[0][error] = "dbPack-01";
                if (css_msg_retorno != "")
                {
                    retorno.Rows[0][msg] = $"<span class=\"{css_msg_retorno}\">!!Não foi informado dados para salvar o cadastro!</span>";
                }
                else
                {
                    retorno.Rows[0][msg] = "!!Não foi informado dados para salvar o cadastro!";
                }
                return (retorno);
            }
            #endregion

            //string campos_where = chaves_primarias.Replace("[" + campo_id + "]", "").Replace("['" + campo_id + "']", "");

            Dictionary<string, string> select = geraFiltroJaExisteDados(chaves_primarias, campo_id, dados);

            #region Carrega tabela para saber quais campos tem e para verificar se já existe o cadastro

            DataSet dbInsert = smartDB.openQuery($"SELECT * FROM {table} {select["where"]} LIMIT 1", "", connection_string);

            #region Se não carregou nenhuma tabela

            if (dbInsert.Tables.Count == 0)
            {
                retorno.Rows[0]["error"] = "dbPack-03";
                if (css_msg_retorno != "")
                {
                    retorno.Rows[0]["msg"] = $"<span class=\"{css_msg_retorno}\">!!Ocorreu um problema de conexão com o servidor. Verifique sua internet e tente novamente.</span>";
                }
                else
                {
                    retorno.Rows[0]["msg"] = "!!Ocorreu um problema de conexão com o servidor. Verifique sua internet e tente novamente.";
                }
                return (retorno);
            }

            #endregion

            #region Se tem chaves_primarias e retornou algum registro com os mesmos dados

            if ((dbInsert.Tables[0].Rows.Count != 0) && (chaves_primarias != ""))
            {
                retorno.Rows[0]["error"] = "dbPack-06";
                string msgError = "!!Não foi possível salvar, pois já existem um cadastro com essas informações:";

                if (css_msg_retorno != "")
                {
                    msgError = $"<span class=\"{css_msg_retorno}\">!!Não foi possível salvar, pois já existem cadastros com as informações:<br />";
                }

                for (int i = 0; i < dbInsert.Tables[0].Columns.Count; ++i)
                {
                    string campo = dbInsert.Tables[0].Columns[i].ColumnName;

                    if ((!dados.ContainsKey(campo)) && (campo != campo_id))
                    {
                        throw new System.ArgumentException($"A key '{campo}' não foi encontrada em 'dados'!");
                    }

                    if (campo != campo_id)
                    {
                        if ((dados[campo].ToString() == dbInsert.Tables[0].Rows[0][campo].ToString()) && (chaves_primarias.ToString().Replace("'", "").Contains("[" + campo + "]")))
                        {
                            if (css_msg_retorno != "")
                            {
                                msgError += $"•<b> {smartString.toTitleCase(campo)}:</b> {dados[campo]}.<br />";
                            }
                            else
                            {
                                msgError += $"• {smartString.toTitleCase(campo)}: {dados[campo]}.";
                            }
                        }
                    }
                }

                if (css_msg_retorno != "")
                {
                    msgError = msgError + "</span>";
                }

                retorno.Rows[0]["msg"] = msgError;
                return (retorno);
            }

            #endregion

            #endregion

            string insert = montaInsert(dados, dbInsert.Tables[0], campo_id);

            #region Campo ID primário

            string sql_campo_id = "";

            if (campo_id != "")
            {
                if (select["select"] != "")
                {
                    select["select"] = campo_id + ", " + select["select"];
                }
                else
                {
                    select["select"] = campo_id;
                }

                sql_campo_id = $"/*{campo_id}*/ (SELECT CASE WHEN COUNT(*)=0 THEN 1 ELSE (SELECT {campo_id} FROM {table} ORDER BY {campo_id} desc LIMIT 1) + 1 END FROM {table} LIMIT 1), ";
            }

            #endregion

            string campos_chaves_primarias = chaves_primarias;

            if (chaves_primarias != "")
            {
                campos_chaves_primarias = ", " + chaves_primarias.Replace("][", "],[").Replace("'", "").Replace("[!", "").Replace("[#", "").Replace("[", "").Replace("+", ", ").Replace("]", "");
            }

            dbInsert = smartDB.openQuery($"INSERT INTO {table} VALUES({sql_campo_id} {insert}); SELECT {campo_id}{campos_chaves_primarias}, {select["select"]} FROM {table} {select["where"]}", "", connection_string);

            #region Se deu algum erro

            if (dbInsert.Tables.Count == 0)
            {
                retorno.Rows[0]["error"] = "dbPack-03";
                if (css_msg_retorno != "")
                {
                    retorno.Rows[0]["msg"] = $"<span class=\"{css_msg_retorno}\">!!Ocorreu algum problema na conexão com o servidor! Por favor verifique sua internet e tente novamente!";
                }
                else
                {
                    retorno.Rows[0]["msg"] = "!!Ocorreu algum problema na conexão com o servidor! Por favor verifique sua internet e tente novamente!";
                }

                return (retorno);
            }

            if (dbInsert.Tables[0].Rows.Count == 0)
            {
                retorno.Rows[0]["error"] = "dbPack-05";
                if (css_msg_retorno != "")
                {
                    retorno.Rows[0]["msg"] = $"<span class=\"{css_msg_retorno}\">!!Não foi possível salvar este cadastro! Por favor verifique as informações e tente novamente!";
                }
                else
                {
                    retorno.Rows[0]["msg"] = "!!Não foi possível salvar este cadastro! Por favor verifique as informações e tente novamente!";
                }
                return (retorno);
            }

            #endregion

            #region Valores dos campos de chave primárias para completar a mensagem de retorno ao usuário

            var mensagem = "";

            if (chaves_primarias != "")
            {
                chaves_primarias = chaves_primarias.Replace("'", "").Replace("[!", "").Replace("[#", "").Replace("[", "").Replace("+", "");
                var chaves = chaves_primarias.Substring(0, chaves_primarias.Length - 1).Split(']');

                for (int i = 0; i < chaves.Count(); ++i)
                {
                    if (dbInsert.Tables[0].Columns.Contains(chaves[i].ToLower()))
                    {
                        if (css_msg_retorno != "")
                        {
                            mensagem += $"<br />{chaves[i].ToUpper()}:<b>{dbInsert.Tables[0].Rows[0][chaves[i].ToLower()]}</b>";
                        }
                        else
                        {
                            mensagem += $"{chaves[i].ToUpper()}:{dbInsert.Tables[0].Rows[0][chaves[i].ToLower()]}";
                        }
                    }
                }
            }

            #endregion

            retorno = dbInsert.Tables[0];

            DataColumn errorFinal = new DataColumn();
            if (!retorno.Columns.Contains("error"))
            {
                errorFinal.ColumnName = "error";
                retorno.Columns.Add(errorFinal);
            }

            if (!retorno.Columns.Contains("msg"))
            {
                DataColumn msgFinal = new DataColumn();
                msgFinal.ColumnName = "msg";
                retorno.Columns.Add(msgFinal);
            }

            return (retorno);
        }

        #region Monta query de chaves primárias
        private static Dictionary<string, string> geraFiltroJaExisteDados(string chaves_primarias, string campo_id, Dictionary<string, object> dados)
        {
            string filtro = "WHERE ";
            string select = "";
            Dictionary<string, string> lstFiltro = new Dictionary<string, string>();

            if (chaves_primarias == "")
            {
                lstFiltro.Add("select", "*");
                lstFiltro.Add("where", "");

                return (lstFiltro);
            }

            if ((!chaves_primarias.Contains("[")) || (!chaves_primarias.Contains("]")))
            {
                throw new System.ArgumentException($"dbPack-101 - Cada campo de chave primária precisa estar informado entre [ ]!");
            }

            Dictionary<string, object> campos = dados;

            foreach (string key in campos.Keys)
            {
                if (key != campo_id)
                {
                    string dados_item = dados[key].ToString();
                    DateTime datTeste = new DateTime();

                    if ((chaves_primarias == "*") || (chaves_primarias.Contains("[" + key + "]")) || (chaves_primarias.Contains("['" + key + "']")) || (chaves_primarias.Contains("[#'" + key + "']")))
                    {
                        if (dados_item.Replace("-", "") != "")
                        {
                            if ((chaves_primarias.Contains("['" + key + "']")) || (chaves_primarias.Contains("[#'" + key + "']")))
                            {
                                if (chaves_primarias.Replace("'", "").Contains("[#" + key + "]"))
                                {

                                    if (!DateTime.TryParse(dados_item, out datTeste))
                                    {
                                        throw new System.ArgumentException($"dbPack-102 - Formato de data inválido no campo [#{key}] das chaves primárias. Verifique se o campo indicado com # retorna mesmo uma data.");
                                    }
                                    else
                                    {
                                        dados_item = DateTime.Parse(dados_item).ToString("yyyy-MM-dd hh:mm:ss");
                                    }
                                }

                                double numTeste = new double();

                                if ((chaves_primarias.Contains("['" + key + "']+")) || (chaves_primarias.Contains("[#'" + key + "']+")))
                                {

                                    if ((DateTime.TryParse(dados_item, out datTeste)) || (double.TryParse(dados_item, out numTeste)))
                                    {
                                        filtro += $"{key}='{dados_item}' AND ";
                                    }
                                    else
                                    {
                                        filtro += $"LOWER(REPLACE({key}, ' ', ''))='{dados_item.ToLower().Replace(" ", "")}' AND ";
                                    }
                                }
                                else
                                {
                                    if ((DateTime.TryParse(dados_item, out datTeste)) || (double.TryParse(dados_item, out numTeste)))
                                    {
                                        filtro += $"{key}='{dados_item}' OR ";
                                    }
                                    else
                                    {
                                        filtro += $"LOWER(REPLACE({key}, ' ', ''))='{dados_item.ToLower().Replace(" ", "")}' OR ";
                                    }
                                }
                            }
                            else
                            {
                                if (Regex.IsMatch(dados_item, @"^\d+$"))
                                {
                                    if (chaves_primarias.Contains("[" + key + "]+"))
                                    {
                                        filtro += $"{key}={dados_item} AND ";
                                    }
                                    else
                                    {
                                        filtro += $"{key}={dados_item} OR ";
                                    }
                                }
                                else
                                {
                                    throw new System.ArgumentException($"dbPack-103 - Formato de valor inválido. Ou valor de [{key}] deve ser numérico ou se o campo for uma string precisa estar entre ' ' nas chaves_primarias.");
                                }
                            }

                        }

                        select += $"{key}, ";
                    }
                }
            }

            if (filtro.Trim().ToLower() == "where")
            {
                filtro = "";
            }

            if (filtro != "")
            {
                filtro = filtro.Substring(0, filtro.Length - 4);
            }

            if (select != "")
            {
                select = select.Substring(0, select.Length - 2);
            }

            lstFiltro.Add("where", filtro);
            lstFiltro.Add("select", select);

            return (lstFiltro);
        }

        #endregion

        #region Monta INSERT

        private static string montaInsert(Dictionary<string, object> dados, DataTable dbInsert, string campo_id)
        {
            string insert = "";

            if ((campo_id.Contains(",")) || (campo_id.Contains(";")))
            {
                throw new System.ArgumentException($"dbPack-201 - Só pode ser informado 1 campo para o campo_id!");
            }

            if (campo_id.Contains(" "))
            {
                throw new System.ArgumentException($"dbPack-202 - O campo_id não pode conter espaços!");
            }

            for (int col = 0; col < dbInsert.Columns.Count; ++col)
            {
                if ((!dados.ContainsKey(dbInsert.Columns[col].ColumnName)) && (dbInsert.Columns[col].ColumnName != campo_id))
                {
                    throw new System.ArgumentException($"dbPack-203 - Key não especificada no dictionary: '{dbInsert.Columns[col].ColumnName}'!");
                }

                if (dbInsert.Columns[col].ColumnName != campo_id)
                {
                    if ((dbInsert.Columns[col].DataType.Name.ToLower() == "string") || (dbInsert.Columns[col].DataType.Name.ToLower() == "datetime"))
                    {
                        string valor = dados[dbInsert.Columns[col].ColumnName].ToString().Replace("'", "");
                        if ((dbInsert.Columns[col].DataType.Name.ToLower() == "datetime") && (valor.Contains("/")))
                        {
                            valor = DateTime.Parse(valor).ToString("yyyy-MM-dd hh:mm:ss");
                        }
                        insert += $"/*{dbInsert.Columns[col].ColumnName}*/ '{valor}',";
                    }
                    else
                    {
                        string numero = dados[dbInsert.Columns[col].ColumnName].ToString().Replace("'", "");

                        int char_pos = numero.IndexOf(",");
                        
                        if(char_pos !=-1)
                        {
                            if(((char_pos + 2) + 1 == numero.Length) || ((char_pos + 1) + 1 == numero.Length))
                            {
                                numero = numero.Replace(".", "").Replace(",", ".");
                            }
                            else
                            {
                                numero = numero.Replace(",", "");
                            }
                        }
                        
                        insert += $"/*{dbInsert.Columns[col].ColumnName}*/ {numero},";
                    }
                }
            }

            return (insert.Substring(0, insert.Length - 1));
        }

        #endregion

        #endregion

        #region Update in DataBase
        /// <summary>
        /// Atualiza registros em uma tabela do banco de dados com os campos (keys) e dados (values) do Dictionary "dados" aplicando as regras em "filtro" 
        /// </summary>
        /// <param name="table">(UPDATE) Tabela destino a ser atualizada</param>
        /// <param name="dados">(SET) Campos(keys) e dados(values) a serem atualizados</param>
        /// <param name="filtro">(WHERE) Filtro de regras para determinar que registros devem ser alterados.</param>
        /// <param name="ignoraKeysSemCampos">Ignora e não dá mensagem de erro nas keys do dictionary que não tem nenhum campo com o mesmo nome na tabela do banco.</param>
        /// <returns>Mensagem sobre os erros ou sobre o sucesso da atualização.</returns>
        public static DataTable update(string table, Dictionary<string, object> dados, string filtro, bool ignoraKeysSemCampos, string connection_string)
        {
            #region Tabela de Erros

            //dbPack-301(dados para salvar cadastro não informados)
            //dbPack-302(não infomou a tabela do banco de dados)
            //dbPack-303(problema de conexão com o banco de dados)
            //dbPack-304(Key não encontrada no dictionary)
            //dbPack-305(Select não retorna nenhuma row)
            //dbPack-306(Insert para um registro já existente)
            //dbPack-307(Não foi encontrado um campo na tabela para a Key do dictonary)
            //dbPack-308(Não foi possível atualizar os dados da tabela)
            //dbPack-310(Resgistro não encontrado!)

            #endregion

            #region filtro

            filtro = smartHTML.decodeSymbolsInHtml(filtro);
            if ((filtro != "") && (filtro.ToLower().Trim().Replace("order by", "") !=""))
            {
                if (filtro.Trim().Substring(0, 1) != ";")
                {
                    filtro = "WHERE " + filtro;
                }
            }

            #endregion

            #region Verifica se o banco retorna algum registro baseado no filtro

            string filtroSELECT = filtro;

            #region Se no filtro existe mais alguma instrução SQL para outras ações

            if (filtro.Contains(";"))
            {
                filtro = filtro.Replace(";  ", "; ").Replace(";   ", "; ").Replace(";UPDATE", "; UPDATE").Replace(";DELETE", "; DELETE").Replace(";SELECT", "; SELECT").Replace(";INSERT", "; INSERT");
                filtro = filtro.Replace("; update", "; UPDATE").Replace("; delete", "; DELETE").Replace("; select", "; SELECT").Replace("; update", "; UPDATE").Replace("; insert", "; INSERT");
                filtro = filtro.Replace("; Update", "; UPDATE").Replace("; Delete", "; DELETE").Replace("; Select", "; SELECT").Replace("; Update", "; UPDATE").Replace("; Insert", "; INSERT");
            }

            #endregion

            if ((!filtro.Contains("; UPDATE")) && (!filtro.Contains("; DELETE")) && (!filtro.Contains("; SELECT")) && (!filtro.Contains("; UPDATE")) && (!filtro.Contains("; INSERT")))
            {
                filtroSELECT = filtro + " LIMIT 1";
            }
            else
            {
                filtroSELECT = filtroSELECT.Replace("; UPDATE", " LIMIT 1; UPDATE").Replace("; DELETE", " LIMIT 1; DELETE").Replace("; SELECT", " LIMIT 1; SELECT").Replace("; INSERT", " LIMIT 1; INSERT");
            }

            DataSet dbUpdate = smartDB.openQuery($"SELECT * FROM {table} {filtroSELECT}", "", connection_string);

            if (dbUpdate.Tables.Count == 0)
            {
                return (null);
            }

            if(dbUpdate.Tables[0].Columns.Contains("error"))
            {
                DataColumn msgSelect = new DataColumn();
                msgSelect.ColumnName = "msg";
                dbUpdate.Tables[0].Columns.Add(msgSelect);
                dbUpdate.Tables[0].Rows[0]["msg"] = dbUpdate.Tables[0].Rows[0]["error"].ToString();
                return (dbUpdate.Tables[0]);
            }

            if (dbUpdate.Tables[0].Rows.Count == 0)
            {
                DataColumn errorSelect = new DataColumn();
                errorSelect.ColumnName = "error";
                dbUpdate.Tables[0].Columns.Add(errorSelect);

                DataColumn msgSelect = new DataColumn();
                msgSelect.ColumnName = "msg";
                dbUpdate.Tables[0].Columns.Add(msgSelect);

                dbUpdate.Tables[0].Rows[0]["error"] = "dbPack-310";
                dbUpdate.Tables[0].Rows[0]["msg"] = "!!Este cadastro não foi encontrado.";

                return (dbUpdate.Tables[0]);
            }

            #endregion

            string update = montaCamposPeloDictionary(dbUpdate.Tables[0], dados, ignoraKeysSemCampos);

            #region Atualiza no banco

            dbUpdate = smartDB.openQuery($"UPDATE {table} SET {update} {filtro}; SELECT * FROM {table} {filtro}", "", connection_string);

            if(dbUpdate.Tables.Count ==0)
            {
                return (null);
            }

            DataColumn errorFinal = new DataColumn();
            DataColumn msgFinal = new DataColumn();

            if (!dbUpdate.Tables[0].Columns.Contains("error"))
            {
                errorFinal.ColumnName = "error";
                dbUpdate.Tables[0].Columns.Add(errorFinal);
            }
            else
            {
                if (!dbUpdate.Tables[0].Columns.Contains("msg"))
                {
                    msgFinal.ColumnName = "msg";
                    dbUpdate.Tables[0].Columns.Add(msgFinal);
                }
                dbUpdate.Tables[0].Rows[0]["msg"] = dbUpdate.Tables[0].Rows[0]["eror"].ToString();
            }

            if (!dbUpdate.Tables[0].Columns.Contains("msg"))
            {
                msgFinal.ColumnName = "msg";
                dbUpdate.Tables[0].Columns.Add(msgFinal);
            }

            #endregion

            #region Confere se todos foram atualizado

            for (int col =0; col < dados.Count; ++col )
            {
                string key = dados.Keys.ElementAt(col);
                if (dbUpdate.Tables[0].Columns.Contains(key))
                {
                    string dados_value = dados[key].ToString();
                    string table_value = dbUpdate.Tables[0].Rows[0][key].ToString();

                    if (dbUpdate.Tables[0].Columns[key].DataType.Name.ToLower() =="datetime")
                    {
                        dados_value = DateTime.Parse(dados_value).ToString("yyyy-MM-dd");
                        table_value = DateTime.Parse(table_value).ToString("yyyy-MM-dd");
                    }

                    if (dbUpdate.Tables[0].Columns[key].DataType.Name.ToLower() == "boolean")
                    {
                        if(dados_value == "1" || dados_value =="t") 
                        { dados_value = "true"; }
                        else if (dados_value == "0" || dados_value == "f") { dados_value = "false"; }

                        if (table_value == "1" || table_value == "t")
                        { table_value = "true"; }
                        else if (table_value == "0" || table_value == "f") { table_value = "false"; }
                    }

                    if (dados_value != table_value)
                    {
                        dbUpdate.Tables[0].Rows[0]["error"] = "dbPack-308";
                        dbUpdate.Tables[0].Rows[0]["msg"] = "!!Não foi possível atualizar os dados da tabela";
                    }
                }
            }

            #endregion

            return (dbUpdate.Tables[0]);
        }

        #region Monta query de campos a serem atualizados

        private static string montaCamposPeloDictionary(DataTable table, Dictionary<string, object> dados, bool ignoraKeysSemCampos)
        {
            string update = "";

            foreach (string key in dados.Keys)
            {
                if (table.Columns.Contains(key))
                {
                    int col = table.Columns.IndexOf(key);

                    if (col == -1)
                    {
                        if ((key != "err") && (key != "msg") && (!ignoraKeysSemCampos))
                        {
                            throw new System.ArgumentException("dbPack-307: Não foi encontrado na tabela do banco de dados um campo para key '" + key + "' do dictionary!");
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (table.Columns[col].DataType.Name.ToLower() == "string")
                    {
                        update += $"{key}='{dados[key]}', ";
                    }
                    else if (table.Columns[col].DataType.Name.ToLower() == "datetime")
                    {
                        DateTime dateTeste = new DateTime();
                        if (!DateTime.TryParse(dados[key].ToString(), out dateTeste))
                        {
                            throw new System.ArgumentException("dbPack-308: Formato de data inválido!  '" + key + "' do dictionary!");
                        }
                        else
                        {
                            update += $"{key}='{DateTime.Parse(dados[key].ToString()):yyyy-MM-dd 00:00:00}', ";
                        }
                    }
                    else
                    {
                        string numero = dados[table.Columns[col].ColumnName].ToString().Replace("'", "");

                        int char_pos = numero.IndexOf(",");

                        if (char_pos != -1)
                        {
                            if (((char_pos + 2) + 1 == numero.Length) || ((char_pos + 1) + 1 == numero.Length))
                            {
                                numero = numero.Replace(".", "").Replace(",", ".");
                            }
                            else
                            {
                                numero = numero.Replace(",", "");
                            }
                        }

                        update += $"{key}={numero}, ";
                    }
                }
            }
            return (update.Substring(0, update.Length - 2));

        }

        #endregion

        #endregion

        #region Select in DataBase
        /// <summary>
        /// Retorna um DataSet com os registros resultantes das regras(WHERE) estabelecidas em "filtro"
        /// </summary>
        /// <param name="table">(FROM) Nome da tabela a ser pesquisada</param>
        /// <param name="campos">(SELECT) Quais campos devem estar nessa pesquisa</param>
        /// <param name="filtro">(WHERE) Regras da query para pesquisa</param>
        /// <returns>DataSet com os registros resultantes da pesquisa.</returns>
        public static DataSet select(string table, string campos, string filtro, string innerJoin, string connection_string)
        {
            if ((filtro != "") && (filtro.Trim().Substring(0, 5) != "ORDER") && (filtro.Trim().Substring(0, 1) != ";"))
            {
                filtro = smartHTML.decodeSymbolsInHtml(filtro);
                filtro = "WHERE " + filtro;
            }

            DataColumn colError = new DataColumn();
            colError.ColumnName = "error";
            DataColumn colMsg = new DataColumn();
            colMsg.ColumnName = "msg";


            DataSet dbSelect = smartDB.openQuery($"SELECT {campos} FROM {table} {innerJoin} {filtro}", "", connection_string);

            if (dbSelect.Tables.Count == 0)
            {
                DataTable tabSelect = new DataTable();
                DataRow row = tabSelect.NewRow();
                tabSelect.Columns.Add(colError);
                tabSelect.Columns.Add(colMsg);
                tabSelect.Rows.Add(row);

                tabSelect.Rows[0]["error"] = "dbPack-03";
                tabSelect.Rows[0]["msg"] = "!!Ocorreu algum problema na cone~xão com o servidor! Por favor verifique sua internet e tente novamente.";
                dbSelect.Tables.Add(tabSelect);

                return (null);
            }

            dbSelect.Tables[0].Columns.Add(colError);
            dbSelect.Tables[0].Columns.Add(colMsg);

            if (dbSelect.Tables[0].Rows.Count == 0)
            {
                DataRow rowError = dbSelect.Tables[0].NewRow();
                dbSelect.Tables[0].Rows.Add(rowError);
                dbSelect.Tables[0].Rows[0][colError] = "dbPack-05";
                dbSelect.Tables[0].Rows[0][colMsg] = "!!Nenhum registro foi encontrado!";

            }
            else
            {
                dbSelect.Tables[0].Rows[0][colError] = "";
                dbSelect.Tables[0].Rows[0][colMsg] = $"{dbSelect.Tables[0].Rows.Count} registros encontrados!";
            }

            return (dbSelect);
        }

        #endregion

        #region Delete in DataBase
        /// <summary>
        /// Exclui os registros definidos pelas regras definidas em "filtro"
        /// </summary>
        /// <param name="tabela">(FROM) Nome da tabela onde encontrar o registro</param>
        /// <param name="filtro">(WHERE) Regras de exclusão do registro</param>
        /// <returns>String com a mensagenm de erro ou sucesso da exclusão</returns>
        public static string delete(string tabela, string filtro, string connection_string)
        {
            if (filtro == "")
            {
                throw new System.ArgumentException("dbPack-07: Exclusão sem filtro! Exclusão cancelada para proteção dos dados.");
            }

            filtro = smartHTML.decodeSymbolsInHtml(filtro);

            string affectedRows = smartDB.executeQuery($"DELETE FROM {tabela} WHERE {filtro}", connection_string);

            if (affectedRows.Contains("!!"))
            {
                return ("!!Não foi possível excluir o cadastro! Por favor verifique se as informações e sua conexão de internet estão ok e tente novamente!");
            }


            return ("");
        }

        #endregion

        #region Convert DataStr In Insert

        public static string convertDataStrInInsert(string dataStr)
        {
            string insert = "";
            string values = "";

            string[] registro = dataStr.Split('*');

            for(int i=0; i < registro.Count(); ++i)
            {
                string[] campo = registro[i].Split('=');
                insert += $",{campo[i]}";
                values += $",{campo[i]}";
            }

            return $"({insert.Substring(1)}) VALUES ({values.Substring(1)})";
        }

        #endregion

        #region Convert Dictionary In Insert

        public static string convertDictionaryInInsert(Dictionary<string, object> dados)
        {
            string insert = "";
            string values = "";

            foreach (string key in dados.Keys)
            {
                insert += $",{key}";
                values += $",{dados[key]}";
            }

            return $"({insert.Substring(1)}) VALUES ({values.Substring(1)})";
        }

        #endregion

        #region Envia relatório de erro

        public static void enviarErro(string mensagem, string descricao, string connection_string)
        {
            string ususario = "visitante";
            string empreendimento = "-";

            if (HttpContext.Current.Session["id_empreendimento"] != null)
            {
                empreendimento = HttpContext.Current.Session["id_empreendimento"].ToString();
            }

            if (HttpContext.Current.Session["usr"] != null)
            {
                Dictionary<string, string> usr = (Dictionary<string, string>)HttpContext.Current.Session["usr"];

                if (usr.ContainsKey("usuario_apelido"))
                {
                    ususario = usr["usuario_apelido"].ToString();
                }
            }

            if (HttpContext.Current.Session["emp"] != null)
            {
                Dictionary<string, string> emp = (Dictionary<string, string>)HttpContext.Current.Session["emp"];
                empreendimento = emp["nome"];
            }

            if (exceptionsTable != "")
            {
                smartDB.executeQuery($@"INSERT INTO ocorrencias VALUES(
                (SELECT cod FROM ocorrencias ORDER BY cod DESC LIMIT 1) + 1,
                now(), '{mensagem.Replace("'", "").Replace(@"\", "")}', '{descricao.Replace("'", "").Replace(@"\", "")}', '{empreendimento}', '{ususario}')",connection_string );
            }
            else
            {
                throw new Exception(mensagem.Replace("'", "").Replace(@"\", "") + Environment.NewLine + descricao.Replace("'", "").Replace(@"\", ""));
            }

        }

        public static DataTable verificaErro(Exception err, string SQLQuery, int attempt)
        {
            string mensagem = "";

            if ((err.Message.Substring(0, 13) == "ERROR [08001]") ||
                (err.Message.Substring(0, 13) == "ERROR [28000]") ||
                (err.Message.Substring(0, 13) == "ERROR [57P03]") ||
                (err.Message.Substring(0, 13) == "ERROR [23505]"))
            {

                if (attempt < 5)
                {
                    return (null);
                }
                else
                {
                    mensagem = "O Servidor está ocupado ou está demorando muito para responder!";
 
                    //throw new Exception($"{err.Source} ==> {err.TargetSite} ==> {err.InnerException}<br />Dev Obs:{mensagem}<br /><br />{err.Message}<br /><br />{SQLQuery}");
                }
            }
            else
            {
                mensagem = $"!!{err.Source} ==> {err.TargetSite} ==> {err.InnerException}<br /><br /><br />{err.Message}<br /><br />{SQLQuery}";
            }

            DataTable retorno = new DataTable();
            DataRow erroRow = retorno.NewRow();
            retorno.Rows.Add(erroRow);

            DataColumn error = new DataColumn();
            error.ColumnName = "error";
            retorno.Columns.Add(error);

            retorno.Rows[0]["error"] = mensagem;

            return retorno;

            //HttpContext.Current.Session["error_messagem_interna"] = $"{err.Source} ==> {err.TargetSite} ==> {err.InnerException}<br />Dev Obs:{HttpContext.Current.Session["error_mensagem"]}<br />{err.Message}<br /><br />{SQLQuery}";

            //throw new Exception($"{err.Source} ==> {err.TargetSite}<br />{err.Message}<br /><br />{SQLQuery}");
        }

        #endregion

        #region generatePass - Gerar senha temporária
        /// <summary>
        /// Generate temporary password
        /// </summary>
        /// <returns>Temporary password</returns>
        public static string generatePass()
        {
            string senha = HttpContext.Current.Session.SessionID;

            if(senha.Length >5)
            {
                senha = senha.Substring(0, 5);
            }

            senha = senha + DateTime.Now.ToString("ddmm");

            return (senha);
        }

        #endregion

        //============================= Acessorios ===================================

    }

    #endregion

    //=====================================================================================

    #region smartHTML

    public static class smartHTML
    {
        #region Gera uma tabela HTML para mostrar dados de uma dbTable
        /// <summary>
        /// Resgata os dados de um DataTable e distribui em uma HTML Table formatada.
        /// </summary>
        /// <param name="dbTable">DataTable com os dados a serem carregados</param>
        /// <param name="nameTable">Nome a ser dado no id html do controle "Table"</param>
        /// <param name="clickCol">Coluna com algum controle para ser clicado (os códigos HTML do controle a ser inserido nas célular da coluna é informada nesta string. Neste código, as etiquetas com o mesmo nome do campo da tabela que estiverem entre "[]" serão substituídas pelos valores deste campo. Já a etiqueta "[index]" será substituída pelo index da Row referente.). Você pode utilizar formula de filtro para coluna click ex: (campo!=valor){html a ser inserido}. Mas só utilize condições = ou !=</param>
        /// <param name="cssCase">Crie uma instrução tipo Switch/Case para alterar a linha css de aparência da celula. Ex: (valor_parcela > 100){rowVermelha}. Obs: separe todos os itens com espaço para verifivação de sintaxe.
        /// No caso de ser utilizado o operador "#" é traduzido como index de row. Ex: (row # 2) significa condição se for a linha 2 da tabela. Pode ser utilizado o operando "impar" e "par", ou seja, se for linha impar ou linha par.</param>
        /// <param name="colunas_invisiveis">O index da colunas que farão parte da tabela, mas não serão mostradas na página. O primeiro index é 1 e não 0.</param>
        /// <param name="index_row">Qual valor de qual campo idenfica a row na Grid? Pode ser um campo numérico ou string. Se for informado "" ele utiliza o index do loop de for ou seja do index de posição da row</param>
        /// <returns>Retorna em formato String o HTML da tabela pronta para ser enviada ao Literal.text. Se no nome do campo pode ter codigo de formatação para valores: _cur_(formato moeda), _dat_(formato data), _dec_(formato decimal), _html_(formato HTML, ou seja, será filtrada na função decodeHTMLSybols. Estes código são retirados do nome do campo antes de serem incorporados no HTML final. O char "_" também é retirado. Já o "__"(2 underlines) são substituídos pelo código de espaço (&nbsp;) HTML</returns>
        public static string createTableHTML(DataTable dbTable, string nameTable, string clickCol, string cssCase, string colunas_invisiveis, string index_row)
        {
            string htmlTabela = "<table id=\"tab" + nameTable + "\">" + Environment.NewLine;

            for (int row = -1; row < dbTable.Rows.Count; ++row)
            {
                #region Monta linha

                string rowDefinitiva = row.ToString();

                if ((index_row != "") && (row > -1))
                {
                    rowDefinitiva = dbTable.Rows[row][index_row].ToString();
                }

                if (row == -1)
                {
                    #region Se é a 1a linha da tabela

                    htmlTabela = htmlTabela + "    <tr id=\"row" + nameTable + "Titulo\" class=\"css" + nameTable + "_title\">" + Environment.NewLine;

                    #endregion
                }
                else
                {
                    #region Se não é a 1a linha


                    htmlTabela = htmlTabela + "    <tr id=\"row" + nameTable + "[" + rowDefinitiva + "]\" class=\"[cssLinha]\">" + Environment.NewLine;

                    #endregion
                }

                #endregion

                #region Monta coluna

                if ((clickCol != "") && (row > -1))
                {
                    var controles = clickCol.Split('|');

                    string controles_filtrados = "";

                    for (int indexControles = 0; indexControles < controles.Length; ++indexControles)
                    {
                        controles_filtrados += subFltroClickCol(controles[indexControles], dbTable.Rows[row]).Replace("[index]", row.ToString());
                    }

                    htmlTabela = htmlTabela + "        <td id=\"control[" + rowDefinitiva + "]\" class=\"css" + nameTable + "_control\">" + controles_filtrados + "</td>";
                }
                else if ((row == -1) && (clickCol != ""))
                {
                    htmlTabela = htmlTabela + "        <td id=\"control[" + rowDefinitiva + "]\" class=\"css" + nameTable + "_control\"></td>";
                }

                for (int col = 0; col < dbTable.Columns.Count; ++col)
                {
                    string valor = "";
                    string cssCol = " class=\"css" + nameTable + "_col\"";
                    string colName = dbTable.Columns[col].ColumnName.Replace("_cur_", "").Replace("_dat_", "").Replace("_dec_", "").Replace("_html_", "").Replace("_hid_", "").Replace("_agg_", "");

                    if (row == -1)
                    {
                        #region Se a é a 1a linha, o valor da célula será o nome do campo

                        valor = "<span id=\"col" + smartString.toTitleCase(colName) + "_title\">&nbsp;" + smartString.toTitleCase(colName).Replace("__", "&nbsp;").Replace("_", " ") + "&nbsp;</span>";

                        cssCol = " class=\"css" + nameTable + "_col_title\"";

                        #endregion
                    }
                    else
                    {
                        #region se não for a 1a linha, o valor será o registro vindo do banco

                        double testeNumber = 0;
                        DateTime testeDate = DateTime.Today;

                        valor = dbTable.Rows[row][col].ToString();

                        if ((dbTable.Columns[col].ColumnName.Contains("_cur_")) && (!string.IsNullOrEmpty(valor)) && (double.TryParse(valor, out testeNumber)))
                        {
                            valor = double.Parse(valor).ToString("c");
                            //valor = double.Parse(valor).ToString("c").Replace("," , ".");
                            //valor = valor.Substring(0, valor.Length - 3) + "," + valor.Substring(valor.Length - 2);
                        }
                        else if ((dbTable.Columns[col].ColumnName.Contains("_dec_")) && double.TryParse(valor, out testeNumber))
                        {
                            valor = float.Parse(valor).ToString("#.##");
                        }
                        else if ((dbTable.Columns[col].ColumnName.Contains("_dat_")) && DateTime.TryParse(valor, out testeDate))
                        {
                            valor = DateTime.Parse(valor).ToString("dd/MM/yyyy");
                        }
                        else if (dbTable.Columns[col].ColumnName.Contains("_html_"))
                        {
                            valor = decodeSymbolsInHtml(valor);
                        }
                        else if (dbTable.Columns[col].ColumnName.Contains("_agg_"))
                        {
                            valor = valor.Replace("{", "").Replace("}", "");
                        }

                        #endregion
                    }

                    string indexCssRow = "";

                    if (row > -1)
                    {
                        indexCssRow = "[" + rowDefinitiva + "]";

                        htmlTabela = htmlTabela.Replace("[" + colName + "]", dbTable.Rows[row][col].ToString());
                    }

                    if ((colunas_invisiveis.Contains("[" + col.ToString() + "]")) || (colunas_invisiveis.Contains("[" + colName + "]")) || (dbTable.Columns[col].ColumnName.Contains("_hid_")))
                    {
                        htmlTabela = htmlTabela + "        <td id=\"" + colName + indexCssRow + "\"" + cssCol + " style=\"display:none\">" + valor + "</td>";
                    }
                    else
                    {
                        htmlTabela = htmlTabela + "        <td id=\"" + colName + indexCssRow + "\"" + cssCol + ">" + valor + "</td>";
                    }

                    #region Filtro de condição. Se tem alguma condição com o campo da coluna

                    string cssComCondicao = "";

                    if ((clickCol == "") && (col == 0))
                    {
                        cssComCondicao = "";
                    }
                    else
                    {
                        if (row > -1)
                        {
                            if (cssCase != "")
                            {
                                cssComCondicao = subLeFiltroCase(cssCase, colName, valor, row);
                            }
                        }
                    }

                    if (cssComCondicao != "")
                    {
                        htmlTabela = htmlTabela.Replace("[cssLinha]", cssComCondicao);
                    }

                    #endregion
                }

                #endregion

                htmlTabela = htmlTabela + "</tr>";

                htmlTabela = htmlTabela.Replace("[cssLinha]", "css" + nameTable + "_row");
            }

            htmlTabela = htmlTabela + "</table>";

            return (htmlTabela);
        }

        private static string subFltroClickCol(string clickCol, DataRow row)
        {
            MatchCollection clickColFiltros = Regex.Matches(clickCol, @"(\(\((.*?)\)\)(\s*){(.*?)\>\})");

            for(int i =0; i < clickColFiltros.Count; ++i)
            {
                string condicao = clickColFiltros[i].ToString().Substring(0, clickColFiltros[i].ToString().IndexOf(")")).Replace("(", "");
                string resultado = clickColFiltros[i].ToString().Substring(clickColFiltros[i].ToString().IndexOf("{") + 1).Replace("}", "");
                resultado = resultado.Replace("}", "");

                var itensCondicao = new string[2];

                itensCondicao = condicao.Split('=');

                //o parâmetro @none é para "se o valor está vazio"
                itensCondicao[0] = itensCondicao[0].ToString().Replace("!", ""); //Campo
                itensCondicao[1] = itensCondicao[1].ToString().Replace("@none", ""); //Valor

                if (!row.Table.Columns.Contains(itensCondicao[0].Trim()))
                {
                    resultado = "";
                }
                else
                {
                    if (condicao.Contains("!="))
                    {
                        string valor_no_banco = row[itensCondicao[0].Trim()].ToString();

                        if (valor_no_banco == itensCondicao[1].Trim().ToLower())
                        { resultado = ""; }
                    }
                    else if (condicao.Contains("="))
                    {
                        if (row[itensCondicao[0].Trim()].ToString() != itensCondicao[1].Trim().ToLower())
                        { resultado = ""; }
                    }
                }

                clickCol = clickCol.Replace(clickColFiltros[i].ToString(), resultado);
            }

            return (clickCol);
        }

        private static string subLeFiltroCase(string cssCase, string campo, string valor, float row)
        {
            if (valor == "especial")
            {
                string teste = valor;
            }

            if (cssCase == "")
            { return (""); }

            campo = campo.ToLower().Replace(" ", "");
            valor = valor.ToLower().Replace(" ", "");

            var filtros = cssCase.Split(';');

            for (int i = 0; i < filtros.Count(); ++i)
            {
                string campoCase = "";
                string valorCase = "";
                string cssReturn = "";
                string filtro = filtros[i];

                if(!filtro.Contains("((") || !filtro.Contains("{") || !filtro.Contains("))") || !filtro.Contains("}"))
                {
                    throw new System.ArgumentException("!!SmartSharp Exeption! SmartHTML.createTableHTMl.FiltroCase: Invalid format of the case filter! Please verify if you writed condition between '((condition))' and css result between '{cssresult}'.");
                }

                cssReturn = filtro.Substring(filtro.IndexOf("{"));
                cssReturn = cssReturn.Replace("}", "").Replace("{", "").Replace(" ", "");

                if (filtros[i].Contains("!="))
                {
                    campoCase = Regex.Replace(Regex.Match(filtros[i], @"(\(\((.*?)\!\=)").ToString(), @"[\(\!\=]", "").ToLower();
                    valorCase = Regex.Replace(Regex.Match(filtros[i], @"(\!\=(.*?)\)\))").ToString(), @"[\)\!\=]", "").ToLower();
                }
                else if (filtros[i].Contains("="))
                {
                    campoCase = Regex.Replace(Regex.Match(filtros[i], @"(\(\((.*?)\=)").ToString(), @"[\(\=]", "").ToLower();
                    valorCase = Regex.Replace(Regex.Match(filtros[i], @"(\=(.*?)\)\))").ToString(), @"[\)\=]", "").ToLower();
                }
                else if (filtros[i].Contains(">"))
                {
                    campoCase = Regex.Replace(Regex.Match(filtros[i], @"(\(\((.*?)\>)").ToString(), @"[\(\>]", "").ToLower();
                    valorCase = Regex.Replace(Regex.Match(filtros[i], @"(\>(.*?)\)\))").ToString(), @"[\)\>]", "").ToLower();
                }
                else if (filtros[i].Contains("<"))
                {
                    campoCase = Regex.Replace(Regex.Match(filtros[i], @"(\(\((.*?)\<)").ToString(), @"[\(\<]", "").ToLower();
                    valorCase = Regex.Replace(Regex.Match(filtros[i], @"(\<(.*?)\)\))").ToString(), @"[\)\<]", "").ToLower();
                }
                else if (filtros[i].Contains("#"))
                {
                    campoCase = filtros[i].Substring(0, filtros[i].IndexOf("#"));
                    campoCase = campoCase.Replace("(", "");
                    campoCase = campoCase.Replace(" ", "").ToLower();

                    if (campoCase == "row")
                    {
                        valorCase = filtros[i].Substring(filtros[i].IndexOf("#"));
                        valorCase = valorCase.Substring(0, valorCase.IndexOf(")"));
                        valorCase = valorCase.Replace("#", "").Replace(")", "");
                        valorCase = valorCase.Replace(" ", "").ToLower();

                        if (valorCase == "impar")
                        {
                            double numeroImpar = row / 2.0;
                            numeroImpar = numeroImpar - Math.Round(numeroImpar);
                            if (numeroImpar != 0)
                            { return (cssReturn); }
                        }
                        else if (valorCase == "par")
                        {
                            double numeroPar = row / 2.0;
                            numeroPar = numeroPar - Math.Round(numeroPar);

                            if (numeroPar == 0)
                            { return (cssReturn); }
                        }
                        else if (valorCase == row.ToString())
                        { return (cssReturn); }

                    }
                }

                if (campo == campoCase)
                {
                    if (valorCase == valor)
                    {
                        return (cssReturn);
                    }
                }
            }

            return ("");
        }

        #endregion

        #region Converte fileds no código HTML referente

        public static string encodeSymbolsInHtml(string html)
        {
            html = html.Replace("=", "[equal]").
                        Replace("\"", "[quote]").
                        Replace("*", "[star]").
                        Replace("'", "[squote]").
                        Replace("<", "[otag]").
                        Replace(">", "[ctag]").
                        Replace("{", "[obrace]").
                        Replace("}", "[cbrace").
                        Replace("%", "[percent]");

            return (html);
        }

        #endregion

        #region Extrai Section em pagina HTML e substitui fields de uma dataStr

        /// <summary>
        /// Get section HTML block in a page to create a cutomizate control to use in Literal .net object   
        /// </summary>
        /// <param name="page">Page where is section block. This page can have various diferents sections blocks</param>
        /// <param name="name_block">Identify block name in 'id='. This structure is the same of html default entries.
        /// <param name="dataStr">dataStr with the fields and values to be replaced. Ex: [name] replaced by value in "name=john". In this case is john </param>
        /// <returns>Criated HTML codes to be a HTML obejct and insert it in a Literal .Net object </returns>
        public static string getSectionDataStr(string page, string name_block, string datalist)
        {
            string docFile = HttpRuntime.AppDomainAppPath + page;

            StreamReader documento = new StreamReader(docFile.Replace("?", ""), System.Text.UTF8Encoding.UTF8);
            string docText = documento.ReadToEnd();

            docText = docText.Replace("\r", "").Replace("\n", "");
            docText = docText.Replace("< section", "<section").Replace("<  section", "<section");
            docText = docText.Replace("<section  ", "<section ").Replace("<section   ", "<section ");
            docText = docText.Replace("id =", "id=").Replace("id  =", "id=");
            docText = docText.Replace("id= ", "id=").Replace("id=  ", "id=");
            docText = docText.Replace("< /", "</").Replace("<  /", "</");
            docText = docText.Replace("</ section", "</section").Replace("</  section", "</section");
            docText = docText.Replace("section >", "section>").Replace("section  >", "section>");

            if (!docText.Contains("<section id=\"" + name_block + "\">"))
            {
                return ("Abertura de bloco não encontrado!");
            }

            docText = docText.Substring(docText.IndexOf("<section id=\"" + name_block + "\">"));

            docText = docText.Replace("<section id=\"" + name_block + "\">", "");

            if (!docText.Contains("</section>"))
            {
                return ("Fechamento de bloco não encontrado!");
            }

            docText = docText.Substring(0, docText.IndexOf("</section>"));

            #region Substitui as labels

            if (datalist != "")
            {
                var lstData = datalist.Split('*');

                for (int i = 0; i < lstData.Count(); ++i)
                {
                    var item = lstData[i].Split('=');

                    if (item[0].Trim().Contains("_cur_"))
                    {
                        item[0].Trim().Replace("_cur_", "");
                        item[1] = double.Parse(item[1]).ToString("c");
                    }
                    else if (item[0].Trim().Contains("_dat_"))
                    {
                        item[0].Trim().Replace("_dat_", "");
                        item[1] = DateTime.Parse(item[1]).ToString("dd/MM/yyyy");
                    }
                    else if (item[0].Trim().Contains("_num_"))
                    {
                        item[0].Trim().Replace("_num_", "");
                        item[1] = double.Parse(item[1]).ToString("0.00");
                    }

                    docText = docText.Replace("[" + item[0].Trim() + "]", item[1].Trim());
                }
            }

            #endregion

            return (docText);
        }

        #endregion

        #region Extrai Section em pagina HTML e substituí fields por valores de uma DataRow

        /// <summary>
        /// Get section HTML block in a page to create a cutomizate control to use in Literal .net object   
        /// </summary>
        /// <param name="page">Page where is section block. This page can have various diferents sections blocks</param>
        /// <param name="name_block">Identify block name in 'id='. This structure is the same of html default entries.</param>
        /// <param name="dataRow">DataRow with the fields and values to be replaced. Ex: [name] replaced by value in Row["name"].</param>
        /// <returns>Criated HTML codes to be a HTML obejct and insert it in a Literal .Net object.
        /// Warning: Upon finding the prefix 'url' or 'path_ ", the class will check if the files exist. If this not exist, the value is replaced by the caption of the field  and the file extension, but without a prefix.
        /// Ex: Label: '/test/[file]', Field:url_image, Value: 'dir/myfile.png' 
        /// If exist file = '/test/dir/myfile.png'
        /// If not exist file = 'test/image.png'</returns>
        public static string getSectionDataRow(string pagina, string nome_bloco, DataRow rowDados, bool substituir_labels_vazias)
        {
            string docFile = HttpRuntime.AppDomainAppPath + pagina;

            StreamReader documento = new StreamReader(docFile.Replace("?", ""), System.Text.UTF8Encoding.UTF8);
            string docText = documento.ReadToEnd();

            string path_imagens = docText.Substring(docText.IndexOf("meta name=\"path_imagens\" content=\""));
            path_imagens = path_imagens.Substring(0, path_imagens.IndexOf("/>") + 2);
            path_imagens = path_imagens.Replace("meta name=\"path_imagens\" content=\"", "").Replace(" ", "").Replace("metaname=\"path_imagens\"content=\"", "").Replace("\"/>", "");


            docText = docText.Replace("< section", "<section").Replace("<  section", "<section");
            docText = docText.Replace("<section  ", "<section ").Replace("<section   ", "<section ");
            docText = docText.Replace("id =", "id=").Replace("id  =", "id=");
            docText = docText.Replace("id= ", "id=").Replace("id=  ", "id=");
            docText = docText.Replace("< /", "</").Replace("<  /", "</");
            docText = docText.Replace("</ section", "</section").Replace("</  section", "</section");
            docText = docText.Replace("section >", "section>").Replace("section  >", "section>");

            if (!docText.Contains("<section id=\"" + nome_bloco + "\">"))
            {
                return ("Abertura de bloco não encontrado!");
            }

            docText = docText.Substring(docText.IndexOf("<section id=\"" + nome_bloco + "\">"));

            docText = docText.Replace("<section id=\"" + nome_bloco + "\">", "");

            if (!docText.Contains("</section>"))
            {
                return ("Fechamento de bloco não encontrado!");
            }

            docText = docText.Substring(0, docText.IndexOf("</section>"));

            for (int collumn = 0; collumn < rowDados.Table.Columns.Count; ++collumn)
            {
                if (rowDados[collumn].ToString() == "")
                {
                    if (substituir_labels_vazias == true)
                    {
                        docText = docText.Replace("[" + rowDados.Table.Columns[collumn].ColumnName + "]", rowDados[collumn].ToString());
                    }
                }
                else
                {
                    if ((rowDados.Table.Columns[collumn].ColumnName.Contains("path_")) || (rowDados.Table.Columns[collumn].ColumnName.Contains("url_")))
                    {
                        string arquivo = rowDados[collumn].ToString();
                        if ((rowDados.Table.Columns[collumn].ColumnName.Contains("url_")) && (!rowDados[collumn].ToString().Contains("http://")))
                        {
                            arquivo = HttpRuntime.AppDomainAppPath + arquivo.Replace("/", "\\");
                        }

                        if ((File.Exists(arquivo)) || (rowDados[collumn].ToString().Contains("http://")))
                        {
                            docText = docText.Replace("[" + rowDados.Table.Columns[collumn].ColumnName + "]", rowDados[collumn].ToString());
                        }
                        else
                        {
                            //Se não existe o arquivo, utiliza o nome do campo sem os prefixos url ou path para o nome de um arquivo padrão "Sem Foto". 
                            docText = docText.Replace("[" + rowDados.Table.Columns[collumn].ColumnName + "]", rowDados.Table.Columns[collumn].ColumnName.Replace("path_", "").Replace("url_", "") + Path.GetExtension(rowDados[collumn].ToString()));
                        }
                    }
                    else
                    {
                        docText = docText.Replace("[" + rowDados.Table.Columns[collumn].ColumnName + "]", rowDados[collumn].ToString());
                    }
                }

            }

            return (docText.Replace("\r\n", ""));
        }

        #endregion

        #region Mensagem em formato DIV
        /// <summary>
        /// Message in DIV format to use it in Literal .Net object
        /// </summary>
        /// <param name="msg">Text to message</param>
        /// <returns>HTML code with message text</returns>
        public static string msgDiv(string msg)
        {
            return ("<div style=\"margin-left:auto; margin-right:auto; font-size:16px; color:#e8e8e8;\">" + msg + "</div>");
        }

        #endregion

        #region Converte código HTML em referenciadas fileds para se tornar um simples texto 
        public static string decodeSymbolsInHtml(string html)
        {
            html = html.Replace("[equal]", "=").
                        Replace("[quote]", "\"").
                        Replace("[star]", "*").
                        Replace("[squote]", "'").
                        Replace("[otag]", "<").
                        Replace("[ctag]", ">").
                        Replace("[obrace]", "{").
                        Replace("[cbrace]", "}").
                        Replace("[percent]", "%");

            return (html);
        }

        #endregion

        #region Decodifica characetres latin em HTML

        public static string decodeHTMLCodeToLatinChars(string html)
        {
            html = html.Replace("%C3%83", "Ã").Replace("%c3%83", "Ã").Replace("%C3%A3", "ã").Replace("%c3%a3", "ã").
                        Replace("%C3%81", "Á").Replace("%c3%81", "Á").Replace("%c3%a1", "á").Replace("%C3%A1", "á").
                        Replace("%C3%82", "Â").Replace("%c3%82", "Â").Replace("%C3%A2", "â").Replace("%c3%a2", "â").
                        Replace("%C3%89", "É").Replace("%c3%89", "É").Replace("%C3%A9", "é").Replace("%c3%a9", "é").
                        Replace("%c3%aa", "ê").Replace("%C3%AA", "ê").Replace("%C3%8A", "Ê").Replace("%c3%8a", "Ê").
                        Replace("%C3%8D", "Í").Replace("%c3%8d", "Í").Replace("%C3%AD", "í").Replace("%c3%ad", "í").
                        Replace("%C3%95", "Õ").Replace("%c3%95", "Õ").Replace("%C3%B5", "õ").Replace("%c3%b5", "õ").
                        Replace("%C3%93", "Ó").Replace("%c3%93", "Ó").Replace("%C3%B3", "ó").Replace("%c3%b3", "ó").
                        Replace("%C3%94", "Ô").Replace("%c3%94", "Ô").Replace("%C3%B4", "ô").Replace("%c3%b4", "ô").
                        Replace("%C3%9A", "Ú").Replace("%c3%9a", "Ú").Replace("%C3%BA", "ú").Replace("%c3%ba", "ú").
                        Replace("%C3%87", "Ç").Replace("%c3%87", "Ç").Replace("%C3%A7", "ç").Replace("%c3%a7", "ç");
            ;

            return (html);
        }

        #endregion
    }

    #endregion

    //=====================================================================================

    #region smartJS
    public static class smartJS
    {

        #region Abre Alert
        /// <summary>
        /// Shows a client-side JavaScript alert in the browser.
        /// </summary>
        /// <param name="message">The message to appear in the alert.</param>
        public static void alert(string message)
        {
            // Cleans the message to allow single quotation marks
            string cleanMessage = message.Replace("'", "\'");
            string script = "<script type='text/javascript'>alert('" + cleanMessage + "');</script>";

            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(smartJS), "alert", script);
            }
        }

        #endregion

        #region Abre Alert e da um refresh na página quando apertar ok
        /// <summary>
        /// Shows a client-side JavaScript alert in the browser and reload page when user click ok button.
        /// </summary>
        /// <param name="message">The message to appear in the alert.</param>
        public static void ShowAndReload(string message)
        {
            // Cleans the message to allow single quotation marks
            string cleanMessage = message.Replace("'", "\'");
            string script = "<script type='text/javascript'>alert('" + cleanMessage + "'); window.parent.location.reload();</script>";

            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(smartJS), "alert", script);
            }
        }

        #endregion

        #region Abre msg box do JQuery Sweet

        /// <summary>
        /// Shows a client-side "Sweet" JQuery alert in the browser.
        /// </summary>
        /// <param name="icon">Icon to show in Popup. success, error, warning, info or question</param>
        /// <param name="title">Title in header of popup</param>
        /// <param name="message">Message of center popup</param>
        /// <param name="footer">Text of bottom popup</param>
        public static void sweet(string icon, string title, string message, string footer)
        {
            // Cleans the message to allow single quotation marks
            string cleanMessage = message.Replace("'", "\'");
            string script = $"<script type='text/javascript'>openAlert('{icon}','{title}','{message}','{footer}');</script>";

            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(smartJS), "alert", script);
            }
        }

        #endregion
    }

    #endregion
}
