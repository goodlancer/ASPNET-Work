using MediatR;

using System;
using System.Text.Json.Serialization;

namespace Commands
{
    public class ImportFileCommand : IRequest<string>
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string Extension { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public string UserName { get; set; }
        [JsonIgnore]
        public string OrgId { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
