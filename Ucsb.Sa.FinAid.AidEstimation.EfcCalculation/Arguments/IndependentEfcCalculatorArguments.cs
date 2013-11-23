namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments
{
    /// <summary>
    /// Parameters used in the calculation of an independent student's Expected Family Contribution (EFC)
    /// </summary>
    public class IndependentEfcCalculatorArguments
    {
        /// <summary>
        /// Student
        /// </summary>
        public HouseholdMember Student
        {
            get;
            set;
        }

        /// <summary>
        /// Spouse
        /// </summary>
        public HouseholdMember Spouse
        {
            get;
            set;
        }

        /// <summary>
        /// Student and Spouse's Adjusted Gross Income (AGI)
        /// </summary>
        public double AdjustedGrossIncome
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the student or spouse is required to file taxes
        /// </summary>
        public bool AreTaxFilers
        {
            get;
            set;
        }

        /// <summary>
        /// Student and spouse's U.S. income tax paid
        /// </summary>
        public double IncomeTaxPaid
        {
            get;
            set;
        }

        /// <summary>
        /// Student and spouse's total untaxed income and benefits
        /// </summary>
        public double UntaxedIncomeAndBenefits
        {
            get;
            set;
        }

        /// <summary>
        /// Student and spouse's total additional financial information
        /// </summary>
        public double AdditionalFinancialInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Student and spouse's total cash, savings, and checkings
        /// </summary>
        public double CashSavingsCheckings
        {
            get;
            set;
        }

        /// <summary>
        /// Student and spouse's net worth of investments
        /// </summary>
        public double InvestmentNetWorth
        {
            get;
            set;
        }

        /// <summary>
        /// Student and spouse's net worth of business and/or investment farm
        /// </summary>
        public double BusinessFarmNetWorth
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the student has dependents
        /// </summary>
        public bool HasDependents
        {
            get;
            set;
        }

        /// <summary>
        /// Student's marital status
        /// </summary>
        public MaritalStatus MaritalStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Student's state of residency
        /// </summary>
        public UnitedStatesStateOrTerritory StateOfResidency
        {
            get;
            set;
        }

        /// <summary>
        /// Number in the household
        /// </summary>
        public int NumberInHousehold
        {
            get;
            set;
        }

        /// <summary>
        /// Number of people in the household that are in college
        /// </summary>
        public int NumberInCollege
        {
            get;
            set;
        }

        /// <summary>
        /// Age of the student
        /// </summary>
        public int Age
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the student qualifies for the simplified EFC calculation
        /// </summary>
        public bool IsQualifiedForSimplified
        {
            get;
            set;
        }

        /// <summary>
        /// Months that student will be enrolled in college
        /// </summary>
        public int MonthsOfEnrollment
        {
            get;
            set;
        }
    }
}