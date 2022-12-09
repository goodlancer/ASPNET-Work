using Domains.DBModels;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class TagsForUi 
    {
        public long Count { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
