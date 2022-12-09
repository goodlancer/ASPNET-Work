using Domains.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Commands
{
    public class AddToS3Command : IRequest<S3File>
    {
        public IFormFile FileData { get; set; }
        public bool GetFullUrl { get; set; } = true;
    }
}
