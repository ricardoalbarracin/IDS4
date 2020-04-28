using System;
using System.Collections.Generic;
using System.Text;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Dtos.EncryptionKey
{
    public class EncryptionKeyDto
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string EncryptionSecret { get; set; }

        public bool Enable { get; set; }
    }
}
