using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ucsb.Sa.FinAid.AidEstimation.Test
{
    [TestClass]
    public class CostOfAttendanceTests
    {
        [TestMethod]
        public void Total_NoItems_EqualsZero()
        {
            CostOfAttendance coa = new CostOfAttendance();
            Assert.AreEqual(0, coa.Total);
        }

        [TestMethod]
        public void Total_Items_Calculated()
        {
            CostOfAttendance coa = new CostOfAttendance();

            coa.Items.Add(new CostOfAttendanceItem { Value = 100 });
            coa.Items.Add(new CostOfAttendanceItem { Value = 300 });

            Assert.AreEqual(400, coa.Total);
        }
    }
}
