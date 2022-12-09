using Domains.DBModels;

using MediatR;
using System.Collections.Generic;

namespace Commands.Query
{
    public class GetPropertyStatusQuery : IRequest<List<PropertyStatus>>
    {
    }
}
