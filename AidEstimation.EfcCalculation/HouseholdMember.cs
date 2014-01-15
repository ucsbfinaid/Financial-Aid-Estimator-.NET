namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation
{
    /// <summary>
    /// Represents a person within a household
    /// </summary>
    public class HouseholdMember
    {
        /// <summary>
        /// Income earned from work
        /// </summary>
        public double WorkIncome
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the person is currently working
        /// </summary>
        public bool IsWorking
        {
            get;
            set;
        }
    }
}