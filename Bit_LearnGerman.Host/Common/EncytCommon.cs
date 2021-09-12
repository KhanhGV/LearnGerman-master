using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Oauth_2._0_v2.Common
{
    public static class EncytCommon
    {
        public static string Base64UrlEncode(this string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-') // replace URL unsafe characters with safe ones
              .Replace('/', '_') // replace URL unsafe characters with safe ones
              .Replace("=", ""); // no padding
        }

        public static string Encrypt(string original)
        {
            return Encrypt(original, "!@#$%^&*()~_+|");
        }

        private static string Encrypt(string original, string key)
        {
            try
            {
                var objHashMd5Provider = new MD5CryptoServiceProvider();
                var keyhash = objHashMd5Provider.ComputeHash(Encoding.Unicode.GetBytes(key));

                var objDesProvider = new TripleDESCryptoServiceProvider
                {
                    Key = keyhash,
                    Mode = CipherMode.ECB
                };

                var buffer = Encoding.Unicode.GetBytes(original);
                return
                    Convert.ToBase64String(objDesProvider.CreateEncryptor()
                        .TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Decrypt(string encrypted)
        {
            return Decrypt(encrypted, "!@#$%^&*()~_+|");
        }

        private static string Decrypt(string encrypted, string key)
        {
            try
            {
                var objHashMd5Provider = new MD5CryptoServiceProvider();
                var keyhash = objHashMd5Provider.ComputeHash(Encoding.Unicode.GetBytes(key));

                var objDesProvider = new TripleDESCryptoServiceProvider
                {
                    Key = keyhash,
                    Mode = CipherMode.ECB
                };

                var buffer = Convert.FromBase64String(encrypted);
                return
                    Encoding.Unicode.GetString(objDesProvider.CreateDecryptor()
                        .TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}