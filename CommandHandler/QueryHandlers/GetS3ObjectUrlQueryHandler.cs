using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Commands.Query;
using MediatR;
using Microsoft.Extensions.Options;
using Services.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    class GetS3ObjectUrlQueryHandler : IRequestHandler<GetS3ObjectUrlQuery, string>
    {
        private readonly IOptions<AwsSettings> _awsSettings;
        public GetS3ObjectUrlQueryHandler(IOptions<AwsSettings> awsSettings)
        {
            _awsSettings = awsSettings;
        }
        public async Task<string> Handle(GetS3ObjectUrlQuery request, CancellationToken cancellationToken)
        {
            IAmazonS3 client = new AmazonS3Client(_awsSettings.Value.AwsAccessKey, _awsSettings.Value.AWSSecretKey, RegionEndpoint.USEast2);
            GetPreSignedUrlRequest urlRequest = new()
            {
                BucketName = _awsSettings.Value.BucketName,
                Key = request.Key,
                Expires = DateTime.Now.AddMinutes(10)
            };
            return client.GetPreSignedURL(urlRequest);
        }
    }
}
