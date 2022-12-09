using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Commands;
using MediatR;
using Microsoft.Extensions.Options;
using Services.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteFilesFromS3CommandHandler : IRequestHandler<DeleteFilesFromS3Command, List<DeleteObjectResponse>>
    {
        private IOptions<AwsSettings> _awsSettings { get; set; }
        public DeleteFilesFromS3CommandHandler(IOptions<AwsSettings> awsSettings)
        {
            _awsSettings = awsSettings;
        }

        public async  Task<List<DeleteObjectResponse>> Handle(DeleteFilesFromS3Command request, CancellationToken cancellationToken)
        {
            var result = new List<DeleteObjectResponse>();
            foreach (var key in request.KeysToDelete)
            {
                IAmazonS3 client = new AmazonS3Client(_awsSettings.Value.AwsAccessKey, _awsSettings.Value.AWSSecretKey, RegionEndpoint.USEast2);
                DeleteObjectRequest deleteObjectRequest = new()
                {
                    BucketName = _awsSettings.Value.BucketName,
                    Key = key,
                };
                result.Add(await client.DeleteObjectAsync(deleteObjectRequest, cancellationToken));
            }
            return result;
        }
    }
}
