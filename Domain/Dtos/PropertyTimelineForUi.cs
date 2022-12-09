using System;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class PropertyTimelineForUi
    {
        public List<SingleDayEventList> SingleDayEventLists { get; set; }
        public PropertyTimelineForUi()
        {
            SingleDayEventLists = new List<SingleDayEventList>();
        }
    }
    public class SingleDayEventList
    {
        public List<Tuple<DateTime, string>> Tuples { get; set; }
        public SingleDayEventList()
        {
            Tuples = new List<Tuple<DateTime, string>>();
        }
    }
}
