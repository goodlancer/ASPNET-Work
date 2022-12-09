using System;

namespace Domains.DBModels
{
    public class Invitation : BaseEntity
    {
        public string SenderId { get; set; }
        public string SenderEmail { get; set; }
        public string OrgId { get; set; }
        public string InvitedUserEmail { get; set; }
        public string TeamId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Website { get; set; }
        public bool IsInvited { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAdress { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public string InvitedBy { get; set; }

    }
}
