using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Data;
using goByte.smartSharp;

namespace goByte.smartCookiesPack
{
    /// <summary>
    /// Cookies toolbox
    /// </summary>
    public class smartCookies
    {

        #region "Read" Cookie da sessão
        /// <summary>
        /// Read cookie by name. If cookie been writed by smartCookie configured "encrypt=true", the method wil decrypt that. 
        /// </summary>
        /// <param name="key">Name of cookie</param>
        /// <returns></returns>
        public static string read(string key)
        {
            HttpCookie myCookie = new HttpCookie(key);

            myCookie = System.Web.HttpContext.Current.Request.Cookies[key];

            try
            {
                if (myCookie == null)
                {
                    return ("");
                }

                string valor = myCookie.Value.ToString();
                if (valor.Substring(0, 7) == "[header]")
                {
                    valor = strDecrypt(valor.Replace("[header]", ""));
                }
                return (valor);
            }
            catch
            {
                return ("");
            }
        }

        #endregion

        #region "ReadItem" Ler item em uma DataStr em um cookie
        /// <summary>
        /// Get value in cookie DatStr type by key. See about DataStr in smartData library
        /// </summary>
        /// <param name="cookie">name of cookie</param>
        /// <param name="key">Field of value in DataStr cookie. If cookie not a dataStr, return all data in cookie</param>
        /// <returns>String with value finded</returns>
        public static string readItem(string cookie, string key)
        {
            string data = read(cookie);
            return (smartDataStr.get(data, key));
        }

        #endregion

        #region "Write" Salvar cookie
        /// <summary>
        /// Create and save a cookie
        /// </summary>
        /// <param name="cookie">Name of cookie</param>
        /// <param name="value">Characters sequence to save in cookie</param>
        /// <param name="encrypt">If value must be encrypted</param>
        /// <param name="expireInSeconds">How much seconds to expire. If is 0, so cookie expire when browser is closed</param>
        public static void write(string cookie, string value, bool encrypt, int expireInSeconds)
        {
            if (encrypt) { value = "[header]" + strEncrypt(value); }

            var encValor = UTF8Encoding.UTF8.GetBytes(value);
            HttpCookie myCookie = new HttpCookie(cookie);

            DateTime now = DateTime.Now;

            myCookie.Value = UTF8Encoding.UTF8.GetString(encValor);

            // Set the cookie expiration date.
            if (expireInSeconds > 0)
            { myCookie.Expires = now.AddSeconds(expireInSeconds); }

            // Add the cookie.
            System.Web.HttpContext.Current.Response.AppendCookie(myCookie);
        }

        #endregion

        #region Encriptar
        const string DESKey = "HUNGARTSDER";
        const string DESIV = "KANGOUS";


        private static string strDecrypt(string stringToDecrypt)//Decrypt the content
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
    }
}
