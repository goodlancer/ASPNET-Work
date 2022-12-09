using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class ExchangeTokenCommand : IRequest<string>
    {
        public string OrgId { get; set; }
        [JsonIgnore]
        public string UserName { get; set; }
    }
}
