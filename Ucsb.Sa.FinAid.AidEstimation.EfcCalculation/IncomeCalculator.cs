using System;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants;

namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation
{
    /// <summary>
    /// Income calculator
    /// </summary>
    public class IncomeCalculator
    {
        private readonly IncomeCalculatorConstants _constants;

        /// <summary>
        /// Constructs a new income calculator
        /// </summary>
        /// <param name="constants">Constants used in income calculations</param>
        public IncomeCalculator(IncomeCalculatorConstants constants)
        {
            _constants = constants;
        }

        /// <summary>
        /// Calculates Total Income
        /// </summary>
        /// <param name="agi">Adjusted Gross Income (AGI)</param>
        /// <param name="workIncome">Total income earned from work</param>
        /// <param name="areTaxFilers">Whether or not the person(s) is required to file taxes</param>
        /// <param name="untaxedIncomeAndBenefits">Total untaxed income and benefits</param>
        /// <param name="additionalFinancialInfo">Total additional financial infomation</param>
        /// <returns>"Total Income"</returns>
        public double CalculateTotalIncome(
            double agi,
            double workIncome,
            bool areTaxFilers,
            double untaxedIncomeAndBenefits,
            double additionalFinancialInfo)
        {
            double totalIncome = areTaxFilers
                ? CalculateAdjustedGrossIncome(agi)
                : CalculateIncomeEarnedFromWork(workIncome);

            totalIncome += CalculateTotalUntaxedIncomeAndBenefits(untaxedIncomeAndBenefits);
            totalIncome -= CalculateAdditionalFinancialInformation(additionalFinancialInfo);

            return totalIncome;
        }

        /// <summary>
        /// Calculates contributions from Adjusted Gross Income
        /// </summary>
        /// <param name="agi">Adjusted gross income</param>
        /// <returns>Contributions from Adjusted Gross Income</returns>
        public double CalculateAdjustedGrossIncome(double agi)
        {
            return Math.Round(agi < 0 ? 0 : agi, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates contributions from Income Earned From Work
        /// </summary>
        /// <param name="workIncome">Total income earned from work</param>
        /// <returns>Contributions from Income Earned From Work</returns>
        public double CalculateIncomeEarnedFromWork(double workIncome)
        {
            return Math.Round(workIncome < 0 ? 0 : workIncome, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates contributions from Untaxed Income and Benefits
        /// </summary>
        /// <param name="untaxedIncomeAndBenefits">Total untaxed income and benefits</param>
        /// <returns>Contributions from Untaxed Income and Benefits</returns>
        public double CalculateTotalUntaxedIncomeAndBenefits(double untaxedIncomeAndBenefits)
        {
            return Math.Round(untaxedIncomeAndBenefits < 0 ? 0 : untaxedIncomeAndBenefits, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Contributions from Additional Financial Information
        /// </summary>
        /// <param name="additionalFinancialInfo">Total additional financial infomation</param>
        /// <returns>Contributions from Additional Financial Information</returns>
        public double CalculateAdditionalFinancialInformation(double additionalFinancialInfo)
        {
            return Math.Round(additionalFinancialInfo < 0 ? 0 : additionalFinancialInfo, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates Available Income
        /// </summary>
        /// <param name="role">Role of the subject within the calculation</param>
        /// <param name="totalIncome">Total income</param>
        /// <param name="totalAllowances">Total allowances</param>
        /// <returns></returns>
        public double CalculateAvailableIncome(EfcCalculationRole role, double totalIncome, double totalAllowances)
        {
            double availableIncome = Math.Round(totalIncome - totalAllowances, MidpointRounding.AwayFromZero);

            if (role == EfcCalculationRole.DependentStudent || role == EfcCalculationRole.IndependentStudentWithoutDependents)
            {
                return availableIncome < 0
                    ? 0
                    : Math.Round(availableIncome * _constants.AiAssessmentPercent, MidpointRounding.AwayFromZero);
            }

            return availableIncome;
        }
    }
}