namespace Skoruba.IdentityServer4.STS.Identity.Configuration
{
    public class AuditLoggingConfiguration
    {
        public string Source { get; set; }

        public string SubjectIdentifierClaim { get; set; }

        public string SubjectNameClaim { get; set; }

        public bool IncludeFormVariables { get; set; }
    }
}