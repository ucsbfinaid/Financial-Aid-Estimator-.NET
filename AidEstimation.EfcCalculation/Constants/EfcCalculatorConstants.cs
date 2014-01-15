namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants
{
    /// <summary>
    /// Constants used in the calculation of the Expected Family Contribution (EFC)
    /// </summary>
    public class EfcCalculatorConstants
    {
        /// <summary>
        /// Maximum income to qualify for the Simplified EFC formula
        /// </summary>
        public int SimplifiedEfcMax
        {
            get;
            set;
        }

        /// <summary>
        /// Maximum income to qualify for the Auto Zero EFC formula
        /// </summary>
        public int AutoZeroEfcMax
        {
            get;
            set;
        }

        /// <summary>
        /// The difference between the income protection allowance for a family of four and a family of five,
        /// with one in college (used in the calculation of EFC for months of enrollment greater than the standard
        /// months of enrollment)
        /// </summary>
        public int AltEnrollmentIncomeProtectionAllowance
        {
            get;
            set;
        }
    }
}