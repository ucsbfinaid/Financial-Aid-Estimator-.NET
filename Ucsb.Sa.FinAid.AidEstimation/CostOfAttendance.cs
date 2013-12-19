using System.Collections.Generic;
using System.Linq;

namespace Ucsb.Sa.FinAid.AidEstimation
{
    public class CostOfAttendance
    {
        public List<CostOfAttendanceItem> Items
        {
            get;
            private set;
        }
        public double Total
        {
            get
            {
                return Items.Sum(item => item.Value);
            }
        }
        public CostOfAttendance(params CostOfAttendanceItem[] items) : this()
        {
            Items.AddRange(items);
        }

        public CostOfAttendance()
        {
            Items = new List<CostOfAttendanceItem>();
        }
    }
}