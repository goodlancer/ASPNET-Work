using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class PlaceholdersQuery : IRequest<List<Placeholders>>
    {
    }
}
