using MediatR;

using System;

using System.Text.Json.Serialization;

namespace Commands
{
    public class CreateMailhouseCommand : IRequest
    {
        public string Name { get; set; }
       
        public string APIKey { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string OrganizationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ContactName { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public string MailhouseId { get; set; }

    }
}
