using System;
using Skoruba.AuditLogging.Events;

namespace Skoruba.IdentityServer4.Admin.BusinessLogic.Events.EncryptionKey
{
    public class EncryptionKeyDeletedEvent : AuditEvent
    {
        public DateTime DeleteOlderThan { get; set; }

        public EncryptionKeyDeletedEvent(DateTime deleteOlderThan)
        {
            DeleteOlderThan = deleteOlderThan;
        }
    }
}