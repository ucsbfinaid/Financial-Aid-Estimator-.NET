using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ucsb.Sa.FinAid.AidEstimation.Test
{
    [TestClass]
    public class AwardRangeTests
    {
        [TestMethod]
        public void Constructor_ValidValues_Constructed()
        {
            AwardRange range = new AwardRange(500, 1000);
            Assert.AreEqual(500, range.Minimum);
            Assert.AreEqual(1000, range.Maximum);
        }

        [TestMethod]
        public void Constructor_NegativeNumber_EqualsZero()
        {
            AwardRange range = new AwardRange(-1000, 100);
            Assert.AreEqual(0, range.Minimum);
            Assert.AreEqual(100, range.Maximum);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_MinimumGreaterThanMaximum_ThrowsException()
        {
            AwardRange range = new AwardRange(500, 0);
        }

        [TestMethod]
        public void ToString_DifferentValues_OutputsRangeString()
        {
            AwardRange range = new AwardRange(500, 600);
            Assert.AreEqual("$500.00 - $600.00", range.ToString());
        }

        [TestMethod]
        public void ToString_EqualValues_OutputsSingleValueString()
        {
            AwardRange range = new AwardRange(500, 500);
            Assert.AreEqual("$500.00", range.ToString());
        }

        [TestMethod]
        public void GetRangeFromValue_SingleValidBuffer_CreatesRange()
        {
            AwardRange range = AwardRange.GetRangeFromValue(500, 100);
            Assert.AreEqual(400, range.Minimum);
            Assert.AreEqual(600, range.Maximum);
        }

        [TestMethod]
        public void GetRangeFromValue_MultipleValidBuffers_CreatesRange()
        {
            AwardRange range = AwardRange.GetRangeFromValue(500, 50, 200);
            Assert.AreEqual(450, range.Minimum);
            Assert.AreEqual(700, range.Maximum);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRangeFromValue_FirstNegativeBufferValue_ThrowsException()
        {
            AwardRange.GetRangeFromValue(400, -100, 500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRangeFromValue_FirstSecondBufferValue_ThrowsException()
        {
            AwardRange.GetRangeFromValue(400, 100, -500);
        }
    }
}
