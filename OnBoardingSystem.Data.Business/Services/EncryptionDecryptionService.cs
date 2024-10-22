//-----------------------------------------------------------------------
// <copyright file="EncryptionDecryptionService.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Services
{
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Mail;
    using System.Security.Cryptography;
    using System.Text;
    using DocumentFormat.OpenXml.InkML;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json.Linq;
    using OnBoardingSystem.Data.Abstractions.Models;
    /// <inheritdoc /> 
    public sealed class EncryptionDecryptionService
    {
        private const string ConnectionString = "ConnectionStrings:OnBoardingSystem";
        public readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EncryptionDecryptionService(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Decryption(string cipherTextString)
        {

            //string EncKey = _config.GetSection("EncryptionDecryptionKey:EncryptionKey").Value.ToString();
            //string EncIv = _config.GetSection("EncryptionDecryptionKey:EncryptionIv").Value.ToString();
            using (SqlConnection conn = new SqlConnection())
            {
                var ConnectionStrings = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                var webConfigConnectionString = ConnectionStrings.GetValue<string>(ConnectionString);
                conn.ConnectionString = webConfigConnectionString;
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "EXEC " + "usp_ConfigurationAPISecureKey";

                //foreach (var parameterDefinition in param)
                //{
                //    command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                //}
                string EncryptionKey = "";
                string EncryptionIv = "";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        EncryptionKey = reader.GetString(reader.GetOrdinal("SecretKey"));
                        EncryptionIv = reader.GetString(reader.GetOrdinal("Salt"));
                    }
                }
                conn.Close();
                byte[] Key = Encoding.ASCII.GetBytes(EncryptionKey);
                byte[] IV = Encoding.ASCII.GetBytes(EncryptionIv);

                cipherTextString = cipherTextString.Replace(" ", "+");
                cipherTextString = cipherTextString.Replace("s1L2a3S4h", "/");
                byte[] cipherText = Convert.FromBase64String(cipherTextString);

                if (cipherText == null || cipherText.Length <= 0)
                {
                    throw new ArgumentNullException("cipherText");
                }
                if (Key == null || Key.Length <= 0)
                {
                    throw new ArgumentNullException("Key");
                }
                if (IV == null || IV.Length <= 0)
                {
                    throw new ArgumentNullException("IV");
                }

                string plaintext = null;

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                    {
                        using (CryptoStream csDecrypt =
                                new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }

                return plaintext;
            }
        }
        public string Encrypt(string simpletext, byte[] key, byte[] iv)
        {
            byte[] cipheredtext;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(simpletext);
                        }

                        cipheredtext = memoryStream.ToArray();
                    }
                }
            }

            string text = Encoding.UTF8.GetString(cipheredtext, 0, cipheredtext.Length);
            return text;
        }

        //public string Decrypted(string cipherText)
        //{
        //    string EncryptionKey = "37ZA3D89D64C115122DF9178C8R99c1x";
        //    cipherText = cipherText.Replace(" ", "+");
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
        //    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        //});
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return cipherText;
        //}
        public string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

    }
}
