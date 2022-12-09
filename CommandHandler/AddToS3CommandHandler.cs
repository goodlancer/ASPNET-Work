using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Commands;
using Commands.Query;
using Domains.Dtos;
using MediatR;
using Microsoft.Extensions.Options;
using Services.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace CommandHandlers
{
    public class AddToS3CommandHandler : IRequestHandler<AddToS3Command, S3File>
    {
        private readonly IOptions<AwsSettings> _awsSettings;
        private IMediator _mediator;
        public AddToS3CommandHandler(IOptions<AwsSettings> awsSettings, IMediator mediator)
        {
            _awsSettings = awsSettings;
            _mediator = mediator;
        }

        public async Task<S3File> Handle(AddToS3Command request, CancellationToken cancellationToken)
        {
            var extension = request.FileData.FileName.Split('.').Last();
            IAmazonS3 client = new AmazonS3Client(_awsSettings.Value.AwsAccessKey, _awsSettings.Value.AWSSecretKey, RegionEndpoint.USEast2);
            
            PutObjectRequest putObjectRequest = new()
            {
                BucketName = _awsSettings.Value.BucketName,
                Key = Guid.NewGuid().ToString() + "." + extension,
                InputStream = request.FileData.OpenReadStream()
            };
            await client.PutObjectAsync(putObjectRequest, cancellationToken);
            var result = new S3File()
            {
                S3Key = putObjectRequest.Key
            };
            if (request.GetFullUrl)
                result.SafeUrl = await _mediator.Send(new GetS3ObjectUrlQuery() { Key = putObjectRequest.Key });
            return result;
        }
    }
}
