using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Commands
{
    public class SendInvitationCommand : IRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public string UserDisplayName { get; set; }
        [JsonIgnore]
        public string OrgId { get; set; }
        [JsonIgnore]
        public string OrgName { get; set; }

        public string InvitationEmail { get; set; }
        public string Name { get; set; }
        public string TeamId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
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
