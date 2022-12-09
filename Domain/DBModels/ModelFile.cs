using System;

namespace Domains.DBModels
{
    public class ModelFile : BaseEntity
    {
        public string ProjectId { get; set; }
        public string S3FileName { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public DateTime UploadDateTime { get; set; }
    }
}
