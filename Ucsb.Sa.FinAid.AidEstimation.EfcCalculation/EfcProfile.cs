namespace Ucsb.Sa.FinAid.AidEstimation.EfcCalculation
{
    public class EfcProfile
    {
        public double ExpectedFamilyContribution
        {
            get;
            private set;
        }

        public double ParentContribution
        {
            get;
            private set;
        }

        public double StudentContribution
        {
            get;
            private set;
        }

        public double ParentTotalIncome
        {
            get;
            private set;
        }

        public EfcProfile(double expectedFamilyContribution,
                            double parentContribution,
                            double studentContribution,
                            double parentTotalIncome)
        {
            ExpectedFamilyContribution = expectedFamilyContribution;
            ParentContribution = parentContribution;
            StudentContribution = studentContribution;
            ParentTotalIncome = ParentTotalIncome;
        }
    }
}