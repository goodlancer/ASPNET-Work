using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class CreateNewOrgCommand : IRequest
    {
        public string OrgName { get; set; }
        public string OrgTitle { get; set; }
        public string ImageId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string TimeZone { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }

    }
}
