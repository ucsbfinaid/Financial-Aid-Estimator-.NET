using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Test
{
    [TestClass]
    public class ExactAaiContributionCalculatorTests
    {
        private AaiContributionCalculator _aaiContributionCalculator;

        [TestInitialize]
        public void Init()
        {
            AaiContributionCalculatorConstants constants = new AaiContributionCalculatorConstants();

            constants.AaiContributionBases = new[] { 0, 3366, 4341, 5472, 6798, 8358 };
            constants.AaiContributionPercents = new double[] { 22, 25, 29, 34, 40, 47 };
            constants.AaiContributionRanges = new[] { -3409, 15300, 19200, 23100, 27000, 30900 };

            _aaiContributionCalculator = new AaiContributionCalculator(constants);
        }

        [TestMethod]
        public void CalculateContributionFromAai_Student_Equals()
        {
            double result
                = _aaiContributionCalculator.CalculateContributionFromAai(EfcCalculationRole.DependentStudent, 18300);
            Assert.AreEqual(18300, result);
        }

        [TestMethod]
        public void CalculateContributionFromAai_IndependentStudentWithoutDep_Equals()
        {
            double result
                = _aaiContributionCalculator.CalculateContributionFromAai(EfcCalculationRole.IndependentStudentWithoutDependents, 18300);
            Assert.AreEqual(18300, result);
        }

        [TestMethod]
        public void CalculateContributionFromAai_Value_Calculated()
        {
            double result = _aaiContributionCalculator.CalculateContributionFromAai(EfcCalculationRole.Parent, 18300);
            Assert.AreEqual(4116, result);
        }

        [TestMethod]
        public void CalculateContributionFromAai_HighValue_Calculated()
        {
            double result = _aaiContributionCalculator.CalculateContributionFromAai(EfcCalculationRole.Parent, 202202);
            Assert.AreEqual(88870, result);
        }

        [TestMethod]
        public void CalculateContributionFromAai_LowValue_Calculated()
        {
            double result = _aaiContributionCalculator.CalculateContributionFromAai(EfcCalculationRole.Parent, -5000);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CalculateContributionFromAai_DecimalValue_Rounded()
        {
            double result = _aaiContributionCalculator.CalculateContributionFromAai(EfcCalculationRole.Parent, 14500.55);
            Assert.AreEqual(3190, result);
        }
    }
}