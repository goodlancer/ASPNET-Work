using Domains.DBModels;
using System;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class DocumentForUi : BaseEntity
    {
        public string ProjectId { get; set; }
        public string DocumentTypeId { get; set; }
        public string S3Key { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDateTimeUtc { get; set; }
        public List<Tag> Tags { get; set; }
        public DocumentForUi()
        {
            Tags = new List<Tag>();
        }
    }
}
