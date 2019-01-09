namespace Web
{
    /// <summary>
    /// References to values stored in the appsettings.json
    /// </summary>
    public class AppSettings
    {
        public string EfcCalculationConstants { get; set; }

        public string AidEstimationConstants { get; set; }

        public string PercentageGrantDependant { get; set; }

        public string PercentageGrantIndependant { get; set; }

        public double SelfHelpConstant { get; set; }

        public double MaxLoanAmount { get; set; }

        public string AidYear { get; set; }
    }
}
