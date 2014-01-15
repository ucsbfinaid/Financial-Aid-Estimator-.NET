namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Constants
{
    /// <summary>
    /// Constants used in the calculation of the Contribution from Adjusted Available Income (AAI)
    /// </summary>
    public class AaiContributionCalculatorConstants
    {
        /// <summary>
        /// Adjusted Available Income (AAI) ranges
        /// </summary>
        public int[] AaiContributionRanges
        {
            get;
            set;
        }

        /// <summary>
        /// Adjusted Available Income (AAI) base values
        /// </summary>
        public int[] AaiContributionBases
        {
            get;
            set;
        }

        /// <summary>
        /// Adjusted Available Income (AAI) percentages
        /// </summary>
        public double[] AaiContributionPercents
        {
            get;
            set;
        }

        /// <summary>
        /// Constructs a new <see cref="AaiContributionCalculatorConstants"/> with default values
        /// </summary>
        public AaiContributionCalculatorConstants()
        {
        }
    }
}