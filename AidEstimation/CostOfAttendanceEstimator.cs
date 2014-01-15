using System;
using System.Collections.Generic;

namespace Ucsb.Sa.FinAid.AidEstimation
{
    /// <summary>
    /// A calculator for estimating the Cost of Attendance
    /// </summary>
    public class CostOfAttendanceEstimator
    {
        private readonly Dictionary<CostOfAttendanceKey, CostOfAttendance> _constants;

        public CostOfAttendanceEstimator(Dictionary<CostOfAttendanceKey, CostOfAttendance> constants)
        {
            if (constants == null)
            {
                throw new ArgumentException("No Cost of Attendance constants were provided");
            }

            _constants = constants;
        }

        public CostOfAttendance GetCostOfAttendance(EducationLevel educationLevel, HousingOption housingOption)
        {
            CostOfAttendanceKey key = new CostOfAttendanceKey(educationLevel, housingOption);
            return _constants.ContainsKey(key) ? _constants[key] : null;
        }
    }
}