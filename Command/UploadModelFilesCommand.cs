using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Commands
{
    public class UploadModelFilesCommand : IRequest<string>
    {
        public string ProjectId { get; set; }
        public string S3FileName { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
    }
}
