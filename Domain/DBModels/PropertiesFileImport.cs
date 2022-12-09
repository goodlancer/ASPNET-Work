using System;
using System.Collections.Generic;

namespace Domains.DBModels
{
    public class PropertiesFileImport : BaseEntity
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string Extension { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OrgId { get; set; }
        public string ListProvider { get; set; }
        public string PropertyType { get; set; }
        public DateTime UploadDate { get; set; }
        public Dictionary<string, string> ColumnMapping { get; set; }
        public string Status { get; set; }

        public string Message { get; set; }
    }
}
