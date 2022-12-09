using Domains.DBModels;
using MediatR;
using System.Collections.Generic;

namespace Commands
{
    public class UpdateDocumentTagsCommand : IRequest<bool>
    {
        public string S3Key { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
