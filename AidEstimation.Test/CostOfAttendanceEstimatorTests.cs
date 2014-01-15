using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ucsb.Sa.FinAid.AidEstimation.Test
{
    [TestClass]
    public class CostOfAttendanceEstimatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullConstants_ThrowsException()
        {
            CostOfAttendanceEstimator estimator = new CostOfAttendanceEstimator(null);
        }

        [TestMethod]
        public void GetCostOfAttendance_NoItems_ReturnsNull()
        {
            CostOfAttendanceEstimator estimator = new CostOfAttendanceEstimator(new Dictionary<CostOfAttendanceKey, CostOfAttendance>());
            CostOfAttendance coa = estimator.GetCostOfAttendance(EducationLevel.Graduate, HousingOption.OffCampus);
            Assert.IsNull(coa);
        }

        [TestMethod]
        public void GetCostOfAttendance_HasItems_ReturnsItem()
        {
            CostOfAttendanceEstimator estimator = new CostOfAttendanceEstimator(new Dictionary<CostOfAttendanceKey, CostOfAttendance>
            {
                {
                    new CostOfAttendanceKey(EducationLevel.Graduate, HousingOption.OffCampus),
                    new CostOfAttendance
                    (
                        new CostOfAttendanceItem { Value = 100 }
                    )
                }
            });

            CostOfAttendance coa = estimator.GetCostOfAttendance(EducationLevel.Graduate, HousingOption.OffCampus);
            Assert.AreEqual(100, coa.Items[0].Value);
        }
    }
}
