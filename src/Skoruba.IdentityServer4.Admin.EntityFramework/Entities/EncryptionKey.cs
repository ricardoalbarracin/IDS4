using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.Entities
{    
    public class EncryptionKey
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string EncryptionSecret { get; set; }

        public bool Enable { get; set; }

    }
}
