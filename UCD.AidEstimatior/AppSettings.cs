namespace UCD.AidEstimator
{
    /// <summary>
    /// References to values stored in the appsettings.json
    /// </summary>
    public class AppSettings
    {
        public string EfcCalculationConstants { get; set; }

        public string AidEstimationConstants { get; set; }

        public string PercentageGrantDependent { get; set; }

        public string PercentageGrantIndependent { get; set; }

        public double SelfHelpConstant { get; set; }

        public double MaxLoanAmount { get; set; }

        public string AidYear { get; set; }
    }
}
