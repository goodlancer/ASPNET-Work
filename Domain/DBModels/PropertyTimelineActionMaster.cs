using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.DBModels
{
    public class PropertyTimelineActionMaster : BaseEntity
    {
        public string Text { get; set; }
        public string Action { get; set; }
        public string FillColumns { get; set; }
    }
}
