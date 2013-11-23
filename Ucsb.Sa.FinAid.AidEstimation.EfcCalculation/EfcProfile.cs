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

        internal EfcProfile(double expectedFamilyContribution,
                                    double parentContribution,
                                    double studentContribution)
        {
            ExpectedFamilyContribution = expectedFamilyContribution;
            ParentContribution = parentContribution;
            StudentContribution = studentContribution;
        }
    }
}