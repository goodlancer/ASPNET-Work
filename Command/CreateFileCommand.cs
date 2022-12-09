using MediatR;

using System;
using System.Text.Json.Serialization;

namespace Commands
{
    public class CreateFileCommand : IRequest<string>
    {
        public string FileKey { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string OrgId { get; set; }
        [JsonIgnore]
        public string UploadedBy { get; set; }
        public DateTime UploadDate { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public double FileSize { get; set; }
        public string Url { get; set; }
    }
}
