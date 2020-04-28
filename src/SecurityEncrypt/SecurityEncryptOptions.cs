using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityEncrypt
{
    public class SecurityEncryptOptions
    {
        /// <summary>
        /// cadena que indica que path se ve a desencriptar parametros
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// cadena que indica que path se ve a desencriptar parametros
        /// </summary>
        public string RedirectPath { get; set; }

        /// <summary>
        /// Parametros a desencriptar
        /// </summary>
        public List<string> Parameters { get; set; }
    }
}
