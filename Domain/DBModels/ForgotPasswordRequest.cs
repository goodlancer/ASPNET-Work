using System;

namespace Domains.DBModels
{
    public class ForgotPasswordRequest : BaseEntity
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public DateTime RequestedDateTime { get; set; }
    }
}
