using Amazon.S3.Model;
using MediatR;
using System.Collections.Generic;

namespace Commands
{
    public class DeleteFilesFromS3Command : IRequest<List<DeleteObjectResponse>>
    {
        public List<string> KeysToDelete { get; set; }
    }
}
