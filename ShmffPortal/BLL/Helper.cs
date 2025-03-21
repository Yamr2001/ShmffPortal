using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ShmffPortal.Helpers
{
    public class Helper
    {
         
        public static string GetSubString(string str, int charLength = 200)
        {
            if (!String.IsNullOrWhiteSpace(str) && str.Length > 200)
                return str.Substring(0, charLength) + "...";
            return str;
        }

        public static string GetFormatedDate(DateTime? date)
        {
            if (!date.HasValue)
                return "";
            return date.Value.ToString("dd-MM-yyyy hh:mm ") + ((date.Value.Hour >= 12) ? "م" : "ص");
        }

        public static string GetFormatedShortDate(DateTime? date)
        {
            if (!date.HasValue)
                return "";
            return date.Value.ToString("dd-MM-yyyy");
        }

        #region --> Generate SALT Key

        //private static byte[] Get_SALT()
        //{
        //    return Get_SALT(saltLengthLimit);
        //}

        public static byte[] Get_SALT(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];

            //Require NameSpace: using System.Security.Cryptography;
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        #endregion

        #region --> Generate HASH Using SHA512
        public static string Get_HASH_SHA512(string password, byte[] salt)
        {
            try
            {
                //required NameSpace: using System.Text;
                //Plain Text in Byte
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);

                //Plain Text + SALT Key in Byte
                byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
                }

                HashAlgorithm hash = new SHA512Managed();
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
                byte[] hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashWithSaltBytes[i] = hashBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    hashWithSaltBytes[hashBytes.Length + i] = salt[i];
                }

                return Convert.ToBase64String(hashWithSaltBytes);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region --> Comapare HASH Value
        public static bool CompareHashValue(string passwordHashed, string password, byte[] SALT)
        {
            try
            {
                string expectedHashString = Get_HASH_SHA512(password, SALT);

                return (passwordHashed == expectedHashString);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        public static string GenerateToken()
        {
            string token = "";
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        public static string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }

}