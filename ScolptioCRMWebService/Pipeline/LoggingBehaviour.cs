using MediatR;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System.Threading;
using System.Threading.Tasks;

namespace ScolptioCRMWebService.Pipeline
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Request
            _logger.LogInformation($"Handling {typeof(TRequest).Name} : {JsonConvert.SerializeObject(request)}");
            /*Type myType = request.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(request, null);
                stringBuilder.Append("Property : ").Append(prop.Name).Append(" Value : ").Append(propValue).AppendLine().Append(JsonConvert.SerializeObject(request));
                // stringBuilder.Append("Property : Value", prop.Name, propValue).Append(" ");
            }*/

            // _logger.LogInformation(stringBuilder.ToString());
            var response = await next();
            //Response
            _logger.LogInformation($"Handled {typeof(TResponse).Name}");
            return response;
        }
    }
}
