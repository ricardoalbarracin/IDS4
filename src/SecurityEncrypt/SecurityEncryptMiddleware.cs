using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace SecurityEncrypt
{
    public class SecurityEncryptMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<SecurityEncryptOptions> _options;
        public SecurityEncryptMiddleware(RequestDelegate next, List<SecurityEncryptOptions> options)
        {
            _next = next;
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context, IEncryptionKeyService encryptionKeyService, IClientService clientService)
        {
            
            if (context.Request.HasFormContentType)
            {
                NameValueCollection fcnvc = context.Request.Form.AsNameValueCollection();
                Dictionary<string, StringValues> dictValues = new Dictionary<string, StringValues>();
                var option = _options.Where(m => m.Path == context.Request.Path).FirstOrDefault();
                if (option != null)
                {
                    foreach (var key in fcnvc.AllKeys)
                    {
                        var parameter = option.Parameters.Where(m => m == key).FirstOrDefault();
                        if (parameter != null)
                        {
                            if (option.RedirectPath?.Length > 0)
                            {
                                context.Request.Path = new PathString(option.RedirectPath);
                            }
                            var client = clientService.GetClientsAsync(fcnvc["client_id"]).Result.Clients.Where(m=> m.ClientId== fcnvc["client_id"]).FirstOrDefault();
                            var encryptionKey = encryptionKeyService.GetEncryptionKeyByClientIdAsync(client?.Id ?? 0).Result;
                            if (encryptionKey != null)
                            {
                                var encryptionSecret = Encoding.ASCII.GetBytes(encryptionKey.EncryptionSecret);
                                var decryptText = DecryptStringFromBytes_Aes(fcnvc[key], encryptionSecret);

                                var decodeStrings = decryptText.Split("]-[");
                                DateTime dateClientSecret = DateTime.ParseExact(decodeStrings[1].Replace("]", ""), "yyyy-MM-ddTHH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture);
                                var minutes = (DateTime.Now - dateClientSecret).TotalMinutes;
                                if (minutes < 1 && minutes > -5 || true)
                                {
                                    var valueParameter = decodeStrings[0].Replace("[", "");
                                    fcnvc.Set(parameter, valueParameter);
                                }
                            }
                        }
                    }
                    foreach (var key in fcnvc.AllKeys)
                    {
                        dictValues.Add(key, fcnvc.Get(key));
                    }
                    var fc = new FormCollection(dictValues);
                    context.Request.Form = fc;
                }
                
            }

            await _next(context);
        }

        string DecryptStringFromBytes_Aes(string cipherstring, byte[] Key)
        {
            var cipherTextCombined = Convert.FromBase64String(cipherstring);
            string plaintext = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                byte[] IV = new byte[aesAlg.BlockSize / 8];
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new MemoryStream(cipherTextCombined))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
    
}

