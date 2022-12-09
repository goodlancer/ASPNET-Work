using System;
using System.Collections.Generic;

namespace Domains.DBModels
{
    public class Document : BaseEntity
    {
        public string ProjectId { get; set; }
        public string DocumentTypeId { get; set; }
        public string S3Key { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDateTimeUtc { get; set; }
        public List<string> TagsIds { get; set; }
        public Document()
        {
            TagsIds = new List<string>();
        }
    }
}
